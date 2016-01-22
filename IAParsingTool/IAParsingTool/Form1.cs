using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using Marshal = System.Runtime.InteropServices.Marshal;
using System.Diagnostics;

namespace IAParsingTool
{
    public partial class ParsingTool : Form
    {
        /// <summary>
        /// Constructor
        /// Initializes components and objects
        /// </summary>
        public ParsingTool()
        {
            InitializeComponent();
            Action<object, EventArgs> navClick = (object sender, EventArgs e) =>
                {
                    insertText();
                };
            bindingNavigatorMoveFirstItem.Click += new EventHandler(navClick);
            bindingNavigatorMoveNextItem.Click += new EventHandler(navClick);
            bindingNavigatorMovePreviousItem.Click += new EventHandler(navClick);
            bindingNavigatorMoveLastItem.Click += new EventHandler(navClick);
            btnFolderBrowse.Click += delegate(object sender, EventArgs e)
                { txtOutputPath.Text = browserPath("CHOOSE AN OUTPUT DIRECTORY", Environment.SpecialFolder.Desktop); };
            btnInput.Click += delegate(object sender, EventArgs e)
                { txtInput.Text = browserPath("CHOOSE AN INPUT DIRECTORY", Environment.SpecialFolder.Desktop); };
            btnClear.Click += delegate(object sender, EventArgs e) { ClearValues(clearState.Form); };
            btnReset.Click += delegate(object sender, EventArgs e) { ClearValues(clearState.All); };
            fileList = new List<string>();
            templateFile = "ResultsTemplate.xlsx";
            txtOutputPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            cbxFileExt.SelectedIndex = 0;
            SUPPORTED_EXTENSIONS = new List<string>(Properties.Settings.Default.SupportedExt.Cast<string>().ToList());
            FOLDER_LEVEL = Properties.Settings.Default.FolderLevels;
            parsingDelegates = new List<parsingStruct>(new parsingStruct[] 
                                                    {
                                                        new parsingStruct { pd = ParseRetina, tp = typeof(scanJob) },
                                                        new parsingStruct { pd = ParseStigViewer, tp = typeof(CHECKLIST) },
                                                        new parsingStruct { pd = ParseNessus, tp = typeof(NessusClientData_v2) },
                                                        new parsingStruct { pd = ParseMbsaFile, tp = typeof(SecScan) },
                                                        new parsingStruct { pd = ParseMbsaFile, tp = typeof(XMLOut)  },
                                                        new parsingStruct { pd = ParseNmap, tp = typeof(nmaprun)  }
                                                    });
            txtBoxes = Controls.OfType<System.Windows.Forms.TextBox>().Cast<System.Windows.Forms.TextBox>().ToList();
            addToList();
            VIDs = new versionIDs();
            VIDs.STIGs = new Dictionary<string, string>();
            zeroResults();
        }

        /// <summary>
        /// Member fields
        /// </summary>
        #region CLASS VARIABLES
        private static string templateFile;
        private List<string> fileList;
        private List<Results> resultsList = new List<Results>();
        private List<Nmap> nmapList = new List<Nmap>();
        private static Workbook workBook = null;
        private Sheets workSheets = null;
        private static Microsoft.Office.Interop.Excel.Application excelApp = null;
        private versionIDs VIDs;
        private static List<System.Windows.Forms.TextBox> txtBoxes;
        private static List<string> SUPPORTED_EXTENSIONS;
        private static int FOLDER_LEVEL;
        private static int XLS_PROCESS;
        #endregion CLASS VARIABLES

        /// <summary>
        /// Struct to hold machine name and files
        /// </summary>
        private struct parsingObjects
        {
            public string machineName;
            public string inputpath;
            public List<string> files;
        }

        /// <summary>
        /// Struct for the scan versions
        /// </summary>
        private struct versionIDs
        {
            [Description("Retina Audit Version")]
            public string Retina;
            [Description("Nessus Plugin Version")]
            public string Nessus;
            [Description("MBSAb wsuscab file date")]
            public string MBSA;
            [Description("STIG Version")]
            public Dictionary<string, string> STIGs;
        }

        private parseResults PARSE_RESULTS;

        private struct parseResults
        {
            public int mbsa;
            public int retina;
            public int stig;
            public int nessus;
            public int nmap;
            public int open;
            public int notfind;
            public int notapp;
            public int notrev;
            public int cat1;
            public int cat2;
            public int cat3;
            public int low;
            public int inform;
            public int unknown;
        }


        /// <summary>
        /// Enum to hold how much to clear
        /// </summary>
        private enum clearState
        {
            TextOnly,
            Form,
            AllForm,
            All
        };

        /// <summary>
        /// Adds files to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            string files = txtFiles.Text + txtInput.Text;
            if (invalid(new string[] { txtMN.Text, files }))
            {
                MessageBox.Show("Machine name, and files cannot be blank.", "Empty values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            addToList();
            ClearValues(clearState.Form);
            bindingNavigatorDeleteItem.Enabled = true;
        }

        /// <summary>
        /// Parsing delegate, either ParseTextFile or ParseCsvFile
        /// </summary>
        /// <param name="file">File to parse</param>
        /// <param name="machine">Machine name</param>
        private delegate void ParseDelegate(string file, string machine);

        /// <summary>
        /// Finished adding files and ready to parse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            bool error = false;
            try
            {
                resultsList.Clear();

                if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
                {
                    MessageBox.Show("Output path cannot be blank.");
                    return;
                }
                addToList(false);
                progressBar1.Visible = true;
                lblProgress.Visible = true;
                foreach (var item in bindingSource1)
                {
                    parsingObjects p = (parsingObjects)item;
                    if (!string.IsNullOrWhiteSpace(p.machineName))
                    {
                        foreach (string file in p.files)
                        {
                            var pd = (Path.GetExtension(file).ToLower() == ".txt") ? new ParseDelegate(ParseMbsaFile) : new ParseDelegate(ParseXMLFile);
                            pd(file, p.machineName);
                        }
                    }
                }
                OpenExcel();
                resetProgress(resultsList.Count(), "Writing Excel");
                Action<string, string, bool> PS = (string tabName, string status, bool nmapfile) =>
                {
                    var sheet = (Worksheet)workSheets.get_Item(tabName);
                    if (nmapfile)
                    {
                        WriteExcel(sheet, nmapList);
                    }
                    else
                    {
                        IEnumerable<Results> findings = resultsList.Where(a => a.status == status);
                        switch (tabName)
                        {
                            case "Open":
                                PARSE_RESULTS.open += findings.Count();
                                break;
                            case "Not A Finding":
                                PARSE_RESULTS.notfind += findings.Count();
                                break;
                            case "Not Applicable":
                                PARSE_RESULTS.notapp += findings.Count();
                                break;
                            case "Not Reviewed":
                                PARSE_RESULTS.notrev += findings.Count();
                                break;
                            default:
                                break;
                        }
                        WriteExcel(sheet, findings);
                    }
                };

                PS("Open", "Open", false);
                PS("Open", "FAILED", false);
                PS("Not A Finding", "PASSED", false);
                PS("Not A Finding", "NotAFinding", false);
                PS("Not Applicable", "Not_Applicable", false);
                PS("Not Reviewed", "WARNING", false);
                PS("Not Reviewed", "Not_Reviewed", false);
                PS("NMAP", "", true);

                insertResults();
                //Write NMAP results
                //if (nmapList != null)
                //{
                    //PS("NMAP", "", true);
                    //var nmapSheet = (Worksheet)workSheets.get_Item("NMAP");
                    //WriteExcel(nmapSheet, nmapList);
                //}

                SaveExcelAs();
                ClearValues(clearState.All, new string[] { "txtOutputPath" });
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show("An error occured while writing the excel file: " + ex.Message);
            }
            finally
            {
                releaseExcel();
                if (!error) MessageBox.Show("Parsing Complete", "Parsing Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Populates the results tab
        /// </summary>
        private void insertResults()
        {
            var sheet = (Worksheet)workSheets.get_Item("Results");
            sheet.Cells[4, 2] = PARSE_RESULTS.retina;
            sheet.Cells[5, 2] = PARSE_RESULTS.nessus;
            sheet.Cells[6, 2] = PARSE_RESULTS.stig;
            sheet.Cells[7, 2] = PARSE_RESULTS.mbsa;
            sheet.Cells[8, 2] = PARSE_RESULTS.nmap;

            sheet.Cells[4, 5] = PARSE_RESULTS.open;
            sheet.Cells[5, 5] = PARSE_RESULTS.notfind;
            sheet.Cells[6, 5] = PARSE_RESULTS.notapp;
            sheet.Cells[7, 5] = PARSE_RESULTS.notrev;
            
            sheet.Cells[4, 8] = PARSE_RESULTS.cat1;
            sheet.Cells[5, 8] = PARSE_RESULTS.cat2;
            sheet.Cells[6, 8] = PARSE_RESULTS.cat3;
            sheet.Cells[7, 8] = PARSE_RESULTS.low;
            sheet.Cells[8, 8] = PARSE_RESULTS.inform;
            sheet.Cells[9, 8] = PARSE_RESULTS.unknown;
        }

        /// <summary>
        /// Saves the excel file
        /// </summary>
        private void SaveExcelAs()
        {
            try
            {
                if (!invalid(new string[] { txtOutputPath.Text, txtFileName.Text, cbxFileExt.SelectedItem.ToString() }))
                {
                    string path = Path.Combine(txtOutputPath.Text, txtFileName.Text + cbxFileExt.SelectedItem.ToString());
                    excelApp.ActiveWorkbook.SaveAs(path);
                }
                else
                {
                    MessageBox.Show("Output path and file name must contain values in order for the excel file to save.", "File Save Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save file.  File may be in use");
            }
        }

        /// <summary>
        /// Checks for black values
        /// </summary>
        /// <param name="items">array of textbox values to check</param>
        /// <returns></returns>
        private bool invalid(string[] items)
        {
            try
            {
                foreach (string item in items)
                {
                    if (string.IsNullOrWhiteSpace(item)) return true;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds items to the parsing list
        /// </summary>
        /// <returns>Boolean: true if successful, false if error</returns>
        private bool addToList(bool clearVals = true, string inputPath = "")
        {
            try
            {
                parsingObjects temp = new parsingObjects();
                temp.machineName = txtMN.Text;
                temp.inputpath = inputPath == "" ? txtInput.Text : inputPath;
                temp.files = new List<string>();
                if (!string.IsNullOrWhiteSpace(temp.inputpath))
                {
                    if (FOLDER_LEVEL > 0)
                    {
                        IterateDirectory(FOLDER_LEVEL, new DirectoryInfo(temp.inputpath));
                    }
                    else
                    {
                        //IterateDirectory(new DirectoryInfo(temp.inputpath));
                        fileList.AddRange(new List<string>(Directory.GetFiles(temp.inputpath, "*.*", System.IO.SearchOption.AllDirectories)));
                    }
                    fileList.RemoveAll(items => !SUPPORTED_EXTENSIONS.Contains(Path.GetExtension(items).ToLower()));
                }

                foreach (string filePath in fileList)
                {
                    temp.files.Add(filePath);
                }

                int count, position;
                count = bindingSource1.Count;
                position = bindingSource1.Position;
                if (count == 0)
                {
                    bindingSource1.Add(temp);
                }
                else if (position < count - 1)
                {
                    bindingSource1.Insert(position, temp);
                    bindingSource1.RemoveAt(position + 1);
                }
                else
                {
                    bindingSource1.Insert(position, temp);
                }

                bindingSource1.MoveLast();
                if (clearVals) ClearValues(clearState.Form);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /*private void IterateDirectory(DirectoryInfo dir)
        {
            try
            {
                if (dir.GetFiles() != null)
                {
                    FolderStructure test = new FolderStructure();
                    test.machineName = dir.Name;
                    test.files = dir.GetFiles().ToList();
                    test.files.RemoveAll(items => !SUPPORTED_EXTENSIONS.Contains(Path.GetExtension(items.ToString())));
                    // filesList.Add(test);
                }


                var directories = new List<string>(Directory.GetDirectories(dir.FullName));
                foreach (var directory in directories)
                {
                    //fileList.AddRange(new List<string>(Directory.GetFiles(directory)));
                    IterateDirectory(new DirectoryInfo(directory));
                }

            }
            catch (Exception ex)
            {

            }
        }*/

        /// <summary>
        /// Iterates through a directory based on depth
        /// </summary>
        /// <param name="depth">Depth to iterate through</param>
        /// <param name="dir">Directory to iterate</param>
        private void IterateDirectory(int depth, DirectoryInfo dir)
        {
            try
            {
                if (depth > 0)
                {

                    fileList.AddRange(new List<string>(Directory.GetFiles(dir.FullName)));
                    var directories = new List<string>(Directory.GetDirectories(dir.FullName));
                    foreach (var directory in directories)
                    {
                        fileList.AddRange(new List<string>(Directory.GetFiles(directory)));
                        IterateDirectory(depth - 1, new DirectoryInfo(directory));
                    }
                    depth--;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Clears the values based on clear state
        /// </summary>
        /// <param name="_cState">The level at which you wish to clear</param>
        /// <param name="additional">Any additional fields to exclude</param>
        private void ClearValues(clearState _cState, string[] additional = null)
        {
            var exclude = (additional == null) ? new List<string>() : new List<string>(additional);
            switch (_cState)
            {
                case clearState.TextOnly:
                    {
                        exclude.AddRange(new string[] { "txtOutputPath", "txtFileName" });
                        clearText(exclude);
                        break;
                    }
                case clearState.Form:
                    {
                        exclude.AddRange(new string[] { "txtOutputPath", "txtFileName" });
                        clearText(exclude);
                        fileList.Clear();
                        break;
                    }
                case clearState.AllForm:
                    {
                        clearText(exclude);
                        fileList.Clear();
                        break;
                    }
                default: // Clears All
                    {
                        clearText(exclude);
                        fileList.Clear();
                        bindingSource1.Clear();
                        bindingNavigatorDeleteItem.Enabled = false;
                        addToList();
                        break;
                    }
            }
            zeroResults();
            txtFiles.Enabled = true;
        }

        /// <summary>
        /// Zero out the results struct
        /// </summary>
        private void zeroResults()
        {
            PARSE_RESULTS.mbsa = 0;
            PARSE_RESULTS.nessus = 0;
            PARSE_RESULTS.nmap = 0;
            PARSE_RESULTS.retina = 0;
            PARSE_RESULTS.stig = 0;
            PARSE_RESULTS.cat1 = 0;
            PARSE_RESULTS.cat2 = 0;
            PARSE_RESULTS.cat3 = 0;
            PARSE_RESULTS.inform = 0;
            PARSE_RESULTS.low = 0;
            PARSE_RESULTS.notapp = 0;
            PARSE_RESULTS.notfind = 0;
            PARSE_RESULTS.notrev = 0;
            PARSE_RESULTS.open = 0;
            PARSE_RESULTS.unknown = 0;
        }

        /// <summary>
        /// Clears the text box values
        /// </summary>
        /// <param name="txtBoxNames">List of excluded textboxes</param>
        /// <returns>Boolean value, true if successful</returns>
        private bool clearText(List<string> txtBoxNames = null)
        {
            try
            {
                lblProgress.Text = string.Empty;
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                List<System.Windows.Forms.TextBox> temp = new List<System.Windows.Forms.TextBox>(txtBoxes);
                if (txtBoxNames != null)
                {
                    foreach (string txtboxname in txtBoxNames)
                    {
                        temp.RemoveAll(txtbx => txtbx.Name == txtboxname);
                    }
                }
                foreach (var txt in temp)
                {
                    txt.Text = string.Empty;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Releases Interop excel com objects
        /// </summary>
        private void releaseExcel()
        {
            if (workBook != null) workBook.Close();
            //if (currentSheet != null) Marshal.ReleaseComObject(currentSheet);
            if (workBook != null) Marshal.ReleaseComObject(workBook);
            if (excelApp != null)
            {
                excelApp.Application.Quit();
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
            }
            using (var process = Process.GetProcessById(XLS_PROCESS))
            {
                if (process != null && process.ProcessName.ToUpper() == "EXCEL")
                    process.Kill();
            }
            XLS_PROCESS = 0;
        }

        /// <summary>
        /// Drag input files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFiles_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        /// <summary>
        /// Accepts and validates files being dropped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFiles_DragDrop(object sender, DragEventArgs e)
        {

            string[] droppedItems = (string[])e.Data.GetData(DataFormats.FileDrop);
            // loop through the string array, validating each filename
            bool allow = true;
            foreach (string item in droppedItems)
            {
                //Check if list item is a folder
                FileAttributes attr = File.GetAttributes(item);
                bool isFolder = (attr & FileAttributes.Directory) == FileAttributes.Directory;

                if (isFolder)
                {
                    addToList(false, item.ToString());
                }

                if (!SUPPORTED_EXTENSIONS.Contains(Path.GetExtension(item).ToLower()))
                {
                    allow = false;
                    MessageBox.Show(Path.GetFileName(item) + ": File type is not supported");
                    fileList.Clear();
                    break;
                }
                else
                {

                    if (!FileExists(item))
                        fileList.Add(item);
                    else
                        MessageBox.Show(Path.GetFileName(item) + " is already in the list");
                }
            }

            e.Effect = (allow ? DragDropEffects.Copy : DragDropEffects.None);
            txtFiles.Text = string.Join(Environment.NewLine, fileList);
        }

        /*private void AddFilesFromDirectory(string filePath)
        {
            DirectoryInfo source = new DirectoryInfo(filePath);
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder 
            try
            {
                subDirs = source.GetDirectories();
                bool isEmpty = !subDirs.Any();
                if (isEmpty)
                {
                    files = source.GetFiles();

                    FolderStructure test = new FolderStructure();
                    test.machineName = source.Name;
                    test.files = files.ToList();

                    test.files.RemoveAll(item => SUPPORTED_EXTENSIONS.Contains(Path.GetExtension(item.FullName)));
                    // filesList.Add(test);
                }
                else
                {
                    foreach (DirectoryInfo subDir in subDirs)
                    {
                        AddFilesFromDirectory(subDir.FullName);
                    }

                }
            }
            catch (UnauthorizedAccessException ex)
            {

            }
        }*/

        /// <summary>
        /// Checks if File Exists
        /// </summary>
        /// <param name="file">Path to file</param>
        /// <returns>True is file exists, false if it does not</returns>
        private bool FileExists(string file)
        {
            return (fileList.Contains(file));
        }

        /// <summary>
        /// Opens Excel Template File
        /// </summary>
        private void OpenExcel()
        {
            string appID = DateTime.Now.Ticks.ToString();
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Application.Caption = appID;
            excelApp.Application.Visible = true;
            using (var process = Process.GetProcesses().Where(p => p.MainWindowTitle.Equals(appID)).First())
            {
                XLS_PROCESS = (process == null) ? 0 : process.Id;
            }
            //excelPID = getProcessID(appID);
            excelApp.Application.Visible = false;
            //excelApp.Visible = true;
            excelApp.DisplayAlerts = false;

            var path = System.IO.Directory.GetCurrentDirectory();
            //Uncomment below for testing
            // path = System.IO.Directory.GetParent(path).ToString();
            // path = System.IO.Directory.GetParent(path).ToString();
            //
            path = path + @"\" + templateFile;


            // if (!File.Exists(path))
            if (!File.Exists(templateFile))
            {
                // workBook = excelApp.Workbooks.Add();
            }
            else
            {
                var workbooks = excelApp.Workbooks;
                workBook = workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true,
                    false, 0, true, false, false);

                workSheets = workBook.Worksheets;

                // currentSheet = workBook.ActiveSheet;
                // currentSheet.Name = "CurrentSheet";
                Marshal.ReleaseComObject(workbooks);
            }
        }

        /// <summary>
        /// Writes all Results objects from resultsList to the open Excel Template
        /// </summary>
        private void WriteExcel(Worksheet newSheet, IEnumerable<Results> resultsList)
        {
            int row = 1;
            int col = 1;

            if (excelApp != null && newSheet != null)
            {
                try
                {
                    //Add new row for each results object
                    foreach (Results result in resultsList)
                    {
                        col = 1;
                        row++;
                        //loop through and write each results property
                        foreach (PropertyInfo property in result.GetType().GetProperties())
                        {

                            string propName = property.Name.ToString();
                            string propValue = "";

                            if (property.GetValue(result) != null)
                            {
                                propValue = property.GetValue(result).ToString();
                            }

                            //Set background color for Category Level
                            Microsoft.Office.Interop.Excel.Range backColorRange;


                            try
                            {
                                newSheet.Cells[row, col] = propValue.ToString();
                            }
                            catch (Exception cantWrite)
                            {
                                continue;
                            }

                            backColorRange = newSheet.get_Range(GetExcelRange(col, row));

                            if (propName == "category")
                            {
                                backColorRange.Cells.Interior.Color = GetCellBackgroundColor(propValue);
                            }

                            col++;
                        }
                        progressBar1.Increment(1);
                    }

                    CreateProgressDropDown(row, newSheet);

                    //Format Excel Doc
                    Microsoft.Office.Interop.Excel.Range autoFitRange;
                    autoFitRange = newSheet.get_Range("A1", ("Z" + row));

                    //Format numbers so Excel doesn't convert them to scientific notation
                    autoFitRange.NumberFormat = 0;

                    //Turning off wordwrap as requested by Shakira
                    autoFitRange.Columns.WrapText = false;

                    autoFitRange.RowHeight = 15;
                }
                catch (Exception ex) { }
            }
        }


        private void WriteExcel(Worksheet newSheet, List<Nmap> nmapList)
        {
            int col = 1;
            int row = 10;
            if (excelApp != null && newSheet != null)
            {
                try
                {
                    //Add new row for each results object
                    foreach (Nmap result in nmapList)
                    {
                        col = 1;
                        row++;
                        //loop through and write each results property
                        foreach (PropertyInfo property in result.GetType().GetProperties())
                        {

                            string propName = property.Name.ToString();
                            string propValue = "";

                            if (property.GetValue(result) != null)
                            {
                                propValue = property.GetValue(result).ToString();
                            }

                            try
                            {
                                newSheet.Cells[row, col] = propValue.ToString();

                                if (propName == "portNum")
                                {
                                    col++;
                                    newSheet.Cells[row, col] = propValue.ToString();
                                }
                            }
                            catch (Exception cantWrite)
                            {
                                continue;
                            }
                            col++;
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        /// Sets cell background color
        /// </summary>
        /// <param name="propValue">Category</param>
        /// <returns>color</returns>
        private Color GetCellBackgroundColor(string propValue)
        {
            Color catColor;
            switch (propValue)
            {
                case "I":
                case "Critical":
                case "FAILED":
                case "4":
                    catColor = Color.Red;
                    PARSE_RESULTS.cat1++;
                    break;
                case "II":
                case "High":
                case "WARNING":
                case "Important":
                case "3":
                    catColor = Color.Orange;
                    PARSE_RESULTS.cat2++;
                    break;
                case "III":
                case "Medium":
                case "Moderate":
                case "2":
                    catColor = Color.Yellow;
                    PARSE_RESULTS.cat3++;
                    break;
                case "Low":
                case "1":
                    catColor = Color.Green;

                    break;
                case "IV":
                case "PASSED":
                case "Info":
                case "Information":
                case "0":
                case "None":
                    catColor = Color.Blue;
                    break;
                default:
                    catColor = Color.White;
                    break;
            }
            return catColor;
        }

        static string GetExcelRange(int colIndex, int rowIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter + rowIndex;
        }

        /// <summary>
        /// Creates, populates, and selects drop down list in Progress Column
        /// </summary>
        private void CreateProgressDropDown(int row, Worksheet newSheet)
        {
            //Check if ProgressDropDownSheet exists
            bool found = false;
            // Loop through all worksheets in the workbook
            foreach (Worksheet sheet in workBook.Sheets)
            {
                // Check the name of the current sheet
                if (sheet.Name == "ProgressDropDown")
                {
                    found = true;
                    break; // Exit the loop now
                }
            }

            //IF the ProgressDropDown sheet doesn't exist.  Create it
            var sheets = workBook.Sheets;
            if (!found)
            {
                //Create new sheet to hold dropdown values
                Worksheet dropDownSheet = sheets.Add();
                dropDownSheet.Name = "ProgressDropDown";
                dropDownSheet.Visible = XlSheetVisibility.xlSheetVeryHidden;

                dropDownSheet.Range["A1"].Value = "None";
                dropDownSheet.Range["A2"].Value = "Analyzing";
                dropDownSheet.Range["A3"].Value = "Remediation";
                dropDownSheet.Range["A4"].Value = "Finished";
            }

            //Create lookup value string
            var lookupValues = "=ProgressDropDown!$" + "A" + "$1:$" + "A" + "$14";

            //Set range and fill with dropdown lists
            string progressCol = GetProgressColumnName(newSheet);

            Range validatingCellsRange = newSheet.Range[progressCol + "2", progressCol + row.ToString()];
            validatingCellsRange.Validation.Delete();
            validatingCellsRange.Validation.Add(XlDVType.xlValidateList,
                   XlDVAlertStyle.xlValidAlertInformation,
                   XlFormatConditionOperator.xlBetween, lookupValues, Type.Missing);
            validatingCellsRange.Validation.IgnoreBlank = true;
            validatingCellsRange.Validation.InCellDropdown = true;


            //Loop through Progress column and set dropdrop value = None
            foreach (Range rangeRow in validatingCellsRange.Rows)
            {
                Range cell = (Range)rangeRow.Cells[1, 1];
                if (cell.Value2 == null)
                    cell.set_Value(XlRangeValueDataType.xlRangeValueDefault, "None");
            }
            Marshal.ReleaseComObject(sheets);
        }

        /// <summary>
        /// Creates new Retina Results object
        /// </summary>
        /// <param name="machineName"></param>
        /// <returns></returns>
        private Results NewRetinaResultsObject(string machineName)
        {
            Results textResults = new Results();
            textResults = new Results();
            textResults.machine = machineName;
            textResults.status = "Open";
            textResults.tool = "Retina";
            return textResults;

        }

        /// <summary>
        /// Inserts text into the form fields
        /// when using the form navigator
        /// </summary>
        /// <returns>boolean value</returns>
        private bool insertText()
        {
            try
            {
                ClearValues(clearState.Form);
                var item = (parsingObjects)bindingSource1.Current;
                txtMN.Text = item.machineName;
                if (!string.IsNullOrWhiteSpace(item.inputpath))
                {
                    txtInput.Text = item.inputpath;
                    txtFiles.Enabled = false;
                }
                else
                {
                    txtFiles.Enabled = true;
                    foreach (string file in item.files)
                        fileList.Add(file);
                }
                StringBuilder sb = new StringBuilder();
                foreach (string file in item.files)
                    sb.AppendLine(file);
                txtFiles.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Opens the folder browse dialog and returns the path
        /// </summary>
        /// <param name="description">Description to display</param>
        /// <param name="defaultFolder">Default browse folder</param>
        /// <returns></returns>
        private static string browserPath(string description, Environment.SpecialFolder defaultFolder)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = description;
            fbd.RootFolder = defaultFolder;
            return (fbd.ShowDialog() == DialogResult.OK) ? fbd.SelectedPath : "";
        }

        /// <summary>
        /// If a user select an input directory
        /// instead of dragging files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            txtFiles.Text = string.Empty;
            txtFiles.Enabled = false;
            fileList.Clear();
        }

        /// <summary>
        /// Form to prompt the user for input
        /// </summary>
        /// <param name="text">Text to be displayed on the form header</param>
        /// <param name="description">Description about the version id</param>
        /// <param name="label2">Any additional info (Mainly STIG type)</param>
        /// <param name="error">If the user didn't enter a value</param>
        /// <returns></returns>
        private static string promptForm(string text, string description, string label2 = "", bool error = false)
        {
            bool validate = true;
            int top = 0;
            Form prompt = new Form();
            prompt.AutoSize = true;
            prompt.Text = text;
            var textLabel = new System.Windows.Forms.Label() { Left = 15, Top = incrementTop(ref top, 10), AutoSize = true, Text = description + ":" };
            prompt.Controls.Add(textLabel);
            var textLabel2 = new System.Windows.Forms.Label() { Left = 15, AutoSize = true, Text = label2 };
            if (!string.IsNullOrWhiteSpace(label2))
            {
                textLabel2.Top = incrementTop(ref top, 15);
                prompt.Controls.Add(textLabel2);
            }
            var textError = new System.Windows.Forms.Label() { Left = 15, AutoSize = true, Text = "You must enter 'N/A' or a value.", ForeColor = Color.Red };
            if (error)
            {
                textError.Top = incrementTop(ref top, 15);
                prompt.Controls.Add(textError);
            }
            var textBox = new System.Windows.Forms.TextBox() { Left = 15, Top = incrementTop(ref top, 20), Width = 135 };
            textBox.Focus();
            prompt.Controls.Add(textBox);
            var confirmation = new System.Windows.Forms.Button() { Text = "OK", Left = 15, Top = incrementTop(ref top, 25), Width = 65 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            var cancel = new System.Windows.Forms.Button() { Text = "Cancel", Left = 85, Top = top, Width = 65 };
            cancel.Click += (sender, e) => { validate = false; prompt.Close(); };
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.ShowDialog();
            return (string.IsNullOrWhiteSpace(textBox.Text) && validate ? promptForm(text, description, label2, true) : textBox.Text);
        }

        /// <summary>
        /// Increments the variable by the specified amount
        /// </summary>
        /// <param name="val">Reference to the variable to increment</param>
        /// <param name="increment">Increment value</param>
        /// <returns></returns>
        private static int incrementTop(ref int val, int increment)
        {
            val += increment;
            return val;
        }

        /// <summary>
        /// Deletes an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                bindingSource1.RemoveCurrent();
                bindingSource1.EndEdit();
                ClearValues(clearState.AllForm);
                bindingSource1.MoveLast();
                if (bindingSource1.Count <= 1) bindingNavigatorDeleteItem.Enabled = false;
            }
        }


        #region New Parsing Stuff

        private string GetProgressColumnName(Worksheet newSheet)
        {
            for (int i = 1; i < newSheet.Columns.Count; i++)
            {
                var cellValue = (string)(newSheet.Cells[1, i] as Range).Value;

                if (cellValue == "Progress")
                {
                    return (GetExcelColumnName(i));
                }
            }
            return "";
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private string GetSeverityCode(string severityCode)
        {
            string correctCode = "";

            severityCode = string.IsNullOrWhiteSpace(severityCode) ? "" : severityCode.ToLower().Trim();

            switch (severityCode)
            {
                case "category i":
                    correctCode = "I";
                    break;
                case "category ii":
                    correctCode = "II";
                    break;
                case "category iii":
                    correctCode = "III";
                    break;
                case "category iv":
                    correctCode = "IV";
                    break;
                case "category v":
                    correctCode = "V";
                    break;
                case "high":
                    correctCode = "I";
                    break;
                case "medium":
                    correctCode = "II";
                    break;
                case "low":
                    correctCode = "III";
                    break;
                case "none":
                    correctCode = "IV";
                    break;
                case "critical":
                    correctCode = "I";
                    break;
                case "important":
                    correctCode = "II";
                    break;
                case "nothing":
                    correctCode = "III";
                    break;
                default:
                    correctCode = "Unkown";
                    break;
            }
            return correctCode;
        }

        /// <summary>
        /// Gets the severity code
        /// </summary>
        /// <param name="severityCode">Integer code</param>
        /// <returns></returns>
        private string GetSeverityCode(int severityCode)
        {
            string correctCode = "";

            switch (severityCode)
            {
                case 0:
                    correctCode = "0";
                    break;
                case 1:
                    correctCode = "Low";
                    break;
                case 2:
                    correctCode = "Moderate";
                    break;
                case 3:
                    correctCode = "Important";
                    break;
                case 4:
                    correctCode = "Critical";
                    break;
                default:
                    correctCode = "Unkown";
                    break;
            }
            return correctCode;
        }


        private void ParseRetina(object fileObject, string machineName, string fileName = "")
        {
            scanJob retinaScan = (scanJob)fileObject;
            if (string.IsNullOrWhiteSpace(VIDs.Retina))
                VIDs.Retina = promptForm("Retina", "Retina Version", fileName);
            foreach (scanJobHostsHostAudit result in retinaScan.hosts.host.audit)
            {
                Results retina = new Results();
                retina.machine = machineName;
                retina.tool = "Retina";
                retina.version = VIDs.Retina;
                retina.status = "Open";
                retina.vId = result.rthID.ToString();
                retina.name = result.name;
                retina.category = (string.IsNullOrWhiteSpace(result.sevCode)) ? result.risk : GetSeverityCode(result.sevCode);
                retina.description = result.description;
                retina.fix = result.fixInformation;
                retina.cve = result.cve;
                retina.iavm = result.iav;
                resultsList.Add(retina);
            }
            PARSE_RESULTS.retina++;
        }

        private void ParseStigViewer(object fileObject, string machineName, string fileName = "")
        {

            CHECKLIST stigScan = (CHECKLIST)fileObject;

            string title = stigScan.STIG_INFO.STIG_TITLE.ToString();
            string version = promptForm("STIG", "STIG Version", title);
            string stigId = machineName + title;

            if (!VIDs.STIGs.ContainsKey(stigId))
            {
                VIDs.STIGs.Add(stigId, version);
            }

            foreach (CHECKLISTVULN result in stigScan.VULN)
            {
                Results stigViewer = new Results();
                stigViewer.machine = machineName;
                stigViewer.tool = title;
                stigViewer.version = version;
                stigViewer.status = result.STATUS;
                stigViewer.findingDetails = result.FINDING_DETAILS.ToString();
                stigViewer.comments = result.COMMENTS.ToString();

                foreach (CHECKLISTVULNSTIG_DATA stigData in result.STIG_DATA)
                {
                    switch (stigData.VULN_ATTRIBUTE)
                    {
                        case "Vuln_Num":
                            stigViewer.vId = stigData.ATTRIBUTE_DATA.ToString();
                            break;
                        case "Severity":
                            stigViewer.category = GetSeverityCode(stigData.ATTRIBUTE_DATA.ToString());
                            break;
                        case "Rule_Title":
                            stigViewer.name = stigData.ATTRIBUTE_DATA.ToString();
                            break;
                        case "Vuln_Discuss":
                            stigViewer.description = stigData.ATTRIBUTE_DATA.ToString().Trim();
                            break;
                        case "IA_Controls":
                            stigViewer.iaControls = stigData.ATTRIBUTE_DATA.ToString();
                            break;
                        case "Fix_Text":
                            stigViewer.fix = stigData.ATTRIBUTE_DATA.ToString().Trim();
                            break;
                        case "Check_Content":
                            stigViewer.checkContent = stigData.ATTRIBUTE_DATA.ToString().Trim();
                            break;
                        //case "STIGRef":
                        //    stigViewer.version = version;
                        //   break;
                        case "Rule_Ver":
                            //stigViewer.iavm = stigData.ATTRIBUTE_DATA.ToString();
                            break;
                        default:
                            break;
                    }
                }
                resultsList.Add(stigViewer);
            }
            PARSE_RESULTS.stig++;
        }

        /// <summary>
        /// Parses the nessus file
        /// </summary>
        /// <param name="fileObject">file object</param>
        /// <param name="machineName">machine name</param>
        private void ParseNessus(object fileObject, string machineName, string fileName = "")
        {
            NessusClientData_v2 NessusScan = (NessusClientData_v2)fileObject;
            if (string.IsNullOrWhiteSpace(VIDs.Nessus))
                VIDs.Nessus = promptForm("Nessus", "Nessus Version", fileName);
            //string compliance = null;
            string userEnteredMachineName = "";

            //foreach (NessusClientData_v2ReportReportHost host in NessusScan.Report.ReportHost)
            //{
                userEnteredMachineName = promptForm("Machine Name Prompt", "Enter machine name for IP " + NessusScan.Report.ReportHost.name);
                foreach (NessusClientData_v2ReportReportHostReportItem item in NessusScan.Report.ReportHost.ReportItem)
                {
                    Results nessus = new Results();
                    nessus.machine = userEnteredMachineName;
                    nessus.tool = "Nessus";
                    nessus.version = VIDs.Nessus;
                    nessus.iaControls = "N/A";
                    nessus.vId = item.pluginID.ToString();
                    /*compliance = getValByElementName(item, ItemsChoiceType.compliance).Trim().ToLower();

                    if (compliance != null && compliance.Equals("true"))
                    {
                        nessus.name = getValByElementName(item, ItemsChoiceType.compliancecheckname);
                        nessus.category = getValByElementName(item, ItemsChoiceType.complianceresult);
                        nessus.iaControls = getValByElementName(item, ItemsChoiceType.compliancereference);
                        nessus.description = getValByElementName(item, ItemsChoiceType.description);
                        nessus.findingDetails = getValByElementName(item, ItemsChoiceType.complianceactualvalue);

                        ComplianceReference compRef = ComplianceReference.MakeComplianceReference(nessus.iaControls);
                        if (compRef != null)
                        {
                            nessus.category = compRef.cat;
                            nessus.iaControls = string.Join(", ", compRef.iaControls);
                        }

                        //Set Status - nessus.status
                        switch (getValByElementName(item, ItemsChoiceType.complianceresult))
                        {
                            case "PASSED":
                                nessus.status = "NotAFinding";
                                break;
                            case "FAILED":
                                nessus.status = "Open";
                                break;
                            case "WARNING":
                                nessus.status = "Not_Reviewed";
                                break;
                            default:
                                nessus.status = "Not_Reviewed";
                                break;
                        }
                    }
                    else
                    {*/
                        nessus.status = "Open";
                        nessus.name = item.pluginName;
                        nessus.category = string.IsNullOrWhiteSpace(item.stig_severity) ? item.risk_factor : item.stig_severity;//nessusSeverity(item);
                        nessus.description = item.synopsis;//getValByElementName(item, ItemsChoiceType.synopsis);
                        nessus.fix = item.solution;//getValByElementName(item, ItemsChoiceType.solution);
                        /*StringBuilder sb = new StringBuilder("");
                        if (!object.ReferenceEquals(item.cve, null))
                        {
                            foreach (string value in item.cve)
                            {
                                sb.Append(value);
                                sb.Append(", ");
                            }
                        }*/
                        //sb.Remove(sb.Length - 3, 2);
                        nessus.cve = object.ReferenceEquals(item.cve, null) ? "" : string.Join(", ", item.cve);
                        //nessus.cve = sb.ToString();//getValByElementName(item, ItemsChoiceType.cve);
                        nessus.iavm = item.iava;//getValByElementName(item, ItemsChoiceType.iava);
                        nessus.findingDetails = item.plugin_output;//getValByElementName(item, ItemsChoiceType.plugin_output);
                    //}
                    resultsList.Add(nessus);
                }
            //}
                PARSE_RESULTS.nessus++;
        }

        /// <summary>
        /// Gets the Nessus severity if NOT a compliance check
        /// </summary>
        /// <param name="item">Reporting item</param>
        /// <returns></returns>
        /*private string nessusSeverity(NessusClientData_v2ReportReportHostReportItem item)
        {
            string returnVal = string.Empty;
            try
            {
                returnVal = getValByElementName(item, item.stig_severity);
                if (string.IsNullOrWhiteSpace(returnVal))
                {
                    returnVal = getValByElementName(item, ItemsChoiceType.risk_factor);
                    if (returnVal.Trim().ToLower().Equals("none"))
                        returnVal = "Info";
                }
            }
            catch (Exception ex)
            {
                returnVal = "Unkown";
            }
            return returnVal;
        }*/

        /// <summary>
        /// Parses MBSA text file and adds entries to a list of Results objects
        /// </summary>
        /// <param name="file"></param>
        /// <param name="machineName"></param>
        private void ParseMbsaFile(string file, string machineName)
        {
            //  return;
            string line;
            string[] lineSplit;
            string[] kbSplit;
            string catalogSyncDate = "";

            /*if (string.IsNullOrWhiteSpace(VIDs.MBSA))
                VIDs.MBSA = promptForm("MBSA", "MBSA Version");*/

            try
            {
                System.IO.StreamReader parser = new System.IO.StreamReader(file);
                while ((line = parser.ReadLine()) != null)
                {
                    if (line.Contains("Catalog synchronization date:"))
                    {
                        // lineSplit = line.Split(':');
                        //textResults.version = lineSplit[1];

                        line = line.Replace("Catalog synchronization date:", "");
                        catalogSyncDate = line;
                    }


                    if (line.Contains("Missing"))
                    {

                        Results textResults = new Results();
                        //Split Line by '|'
                        lineSplit = line.Split('|');
                        //Split line to grab KB number e.g.(KB1234)
                        kbSplit = lineSplit[3].Split('(');
                        textResults.vId = kbSplit[1].Substring(0, kbSplit[1].Length - 2);
                        textResults.name = kbSplit[0].ToString().Trim();
                        textResults.machine = machineName;
                        textResults.tool = "MBSA";
                        textResults.category = lineSplit[4].ToString().Trim();
                        textResults.status = "Open";
                        textResults.version = catalogSyncDate;

                        resultsList.Add(textResults);
                    }
                    continue;
                }
                PARSE_RESULTS.mbsa++;
            }
            catch (Exception ex)
            {

            }
        }

        private void ParseNmap(object fileObject, string machineName, string fileName = "")
        {
            nmaprun nmapScan = (nmaprun)fileObject;
            string nmapMachineName = "";
            foreach (var port in nmapScan.host.ports)
            {
                Nmap nmapResults = new Nmap();
                if (string.IsNullOrEmpty(nmapMachineName))
                    try
                    {
                        nmapMachineName = nmapScan.host.hostnames.hostname.name;
                    }
                    catch
                    {
                        nmapMachineName = promptForm("Machine Name Prompt", "Enter machine name", fileName);
                    }
                nmapResults.machineName = nmapMachineName;
                nmapResults.protocol = port.protocol;
                nmapResults.portNum = port.portid.ToString();
                nmapResults.name = port.service.name;
                nmapList.Add(nmapResults);
            }
            PARSE_RESULTS.nmap++;
        }

        /// <summary>
        /// Parses MBSA xml file
        /// </summary>
        /// <param name="fileObject">MBSA Datamodel</param>
        /// <param name="machineName">Machine name</param>
        private void ParseMbsaFile(object fileObject, string machineName, string fileName = "")
        {
            dynamic mbsaScan = null;
            string mbsaVersion;
            if (fileObject.GetType().Equals(typeof(SecScan)))
            {
                mbsaScan = (SecScan)fileObject;
                mbsaVersion = mbsaScan.HotfixDataVersion;
            }
            else
            {
                mbsaScan = (XMLOut)fileObject;
                mbsaVersion = mbsaScan.CatalogInfo.CreationDate;
            }

            /*if (string.IsNullOrWhiteSpace(VIDs.MBSA))
                VIDs.MBSA = promptForm("MBSA", "MBSA Version");*/
            foreach (var check in mbsaScan.Check)
            {
                // the XMLOut Schema may have check elements with no detail elements
                if (Object.ReferenceEquals(check.Detail, null)) continue;
                foreach (var data in check.Detail)
                {
                    if (data.IsInstalled == false)
                    {
                        Results mbsa = new Results();
                        mbsa.machine = machineName;
                        mbsa.tool = "MBSA";
                        mbsa.version = mbsaVersion;
                        mbsa.status = "Open";
                        mbsa.vId = "KB" + data.KBID.ToString();
                        mbsa.name = data.Title;
                        mbsa.category = data.Severity.ToString();
                        resultsList.Add(mbsa);
                    }
                }
            }
            PARSE_RESULTS.mbsa++;
        }

        /// <summary>
        /// Gets the value based on element name
        /// </summary>
        /// <param name="item">reporting item</param>
        /// <param name="type">item type</param>
        /// <returns></returns>
        /*private string getValByElementName(NessusClientData_v2ReportReportHostReportItem item, string type)
        {
            int index = -1;
            try
            {
                index = Array.IndexOf(item.ItemsElementName, type);
                return item.Items[index].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }*/

        /// <summary>
        /// Parsing method delegate
        /// </summary>
        /// <param name="fileObject">File to be parsed</param>
        /// <param name="machineName">Machine name</param>
        /// <param name="fileName">File name</param>
        private delegate void ParseXMLDelegate(object fileObject, string machineName, string fileName = "");
        /// <summary>
        /// Struct to hold the the read delegate, the parsing delegate, and the xml type
        /// </summary>
        private struct parsingStruct
        {
            public ParseXMLDelegate pd; // The parsing delegate
            public Type tp;  // The xml type
        }

        /// <summary>
        /// List variable to hold the parsing delegate structs
        /// Initialized in the constructor
        /// </summary>
        private static List<parsingStruct> parsingDelegates = null;

        /// <summary>
        /// Determines which type of xml file needs to be parsed
        /// </summary>
        /// <param name="file">File path</param>
        /// <param name="machineName">Machine name</param>
        private void ParseXMLFile(string file, string machineName)
        {
            object fileObject = null;

            string fileName = "File: " + new FileInfo(file).Name;

            Func<string, Type, object> readXML = (string fileLoc, Type T) =>
                {
                    try
                    {
                        System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(T);
                        System.IO.StreamReader newFile = new System.IO.StreamReader(fileLoc);
                        return Convert.ChangeType(reader.Deserialize(newFile), T);
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                };

            foreach (var item in parsingDelegates)
            {
                fileObject = readXML(file, item.tp);
                if (fileObject != null && fileObject.GetType() == item.tp)
                {
                    item.pd(fileObject, machineName, fileName);
                    break;
                }
            }
        }

        #endregion New Parsing Stuff

        /// <summary>
        /// Resets the progress bar
        /// </summary>
        /// <param name="max">Max progress bar size</param>
        /// <param name="text">Text for the progress bar label</param>
        private void resetProgress(int max, string text, int value = 0)
        {
            progressBar1.Maximum = max;
            progressBar1.Value = value;
            lblProgress.Text = text;
        }

        /// <summary>
        /// When help button is clicked
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHelpButtonClicked(CancelEventArgs e)
        {
            base.OnHelpButtonClicked(e);

            Help.ShowHelp(this, helpProvider1.HelpNamespace);
        }

        /// <summary>
        /// Confirm the user wants to close
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                switch (MessageBox.Show(this, "Are you sure you want to quit?", "Close Parser", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
