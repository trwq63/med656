using System.Web;
using System.Web.Mvc;
using UAHFitVault.DataAccess.ZephyrServices;
using UAHFitVault.DataAccess;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.LogicLayer.LogicFiles;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using UAHFitVault.Database.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Controllers
{
    public class SelectDataController : Controller
    {
        #region Private Members

        /// <summary>
        /// Service object for accessing patient data database functions.
        /// </summary>
        private readonly IPatientDataService _patientDataService;

        /// <summary>
        /// Service object for accessing Zephyr Accelerometer database functions.
        /// </summary>
        private readonly IZephyrAccelService _accelService;

        /// <summary>
        /// Service object for accessing Zephyr Breathing Waveform database functions.
        /// </summary>
        private readonly IZephyrBreathingService _breathingService;

        /// <summary>
        /// Service object for accessing Zephyr ECG Waveform database functions.
        /// </summary>
        private readonly IZephyrECGService _ecgService;

        /// <summary>
        /// Service object for accessing Zephyr Event Data database functions.
        /// </summary>
        private readonly IZephyrEventDataService _eventDataService;

        /// <summary>
        /// Service object for accessing Zephyr Summary database functions.
        /// </summary>
        private readonly IZephyrSummaryService _summaryService;

        /// <summary>
        /// Service object for accessing patient database functions.
        /// </summary>
        private readonly IPatientService _patientService;

        /// <summary>
        /// Service object for accessing physician database functions.
        /// </summary>
        private readonly IPhysicianService _physicianService;

        /// <summary>
        /// Service for accessing medical devices.
        /// </summary>
        private readonly IMedicalDeviceService _medicalDeviceService;       

        #endregion

        #region Public Properties

        /// <summary>
        /// Session property used to store the medical devices available in the system.
        /// </summary>
        public List<MedicalDevice> MedicalDevices {
            get {
                if (Session["sMedicalDevices"] == null) {
                    Session["sMedicalDevices"] = new List<MedicalDevice>();
                }
                return (List<MedicalDevice>)Session["sMedicalDevices"];
            }
            private set {
                Session["sMedicalDevices"] = value;
            }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SelectDataController(IPatientDataService patientDataService, IZephyrAccelService accelService,
                                    IZephyrBreathingService breathingService, IZephyrECGService ecgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IPatientService patientService, IPhysicianService physicianService,
                                    IMedicalDeviceService medicalDeviceService) {

            _patientDataService = patientDataService;
            _accelService = accelService;
            _breathingService = breathingService;
            _ecgService = ecgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
            _patientService = patientService;
            _physicianService = physicianService;
            _medicalDeviceService = medicalDeviceService;

        }
        #endregion

        /// <summary>
        /// Load the initial select data view when first navigating to the page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<MedicalDevice> medicalDevices = (List<MedicalDevice>)_medicalDeviceService.GetMedicalDevices();
            if(medicalDevices != null && medicalDevices.Count > 0) {
                //Save session property
                MedicalDevices = medicalDevices;
            }
            return View();
        }

        /// <summary>
        /// Process the data submitted from the form on the view.
        /// </summary>
        /// <param name="files">Comma delimited list of strings of file names.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessData(HttpPostedFileBase[] files, string medicalDeviceType) {

            MedicalDevice medicalDevice = SelectDataLogic.DetermineDeviceType(MedicalDevices, medicalDeviceType);

            foreach (HttpPostedFileBase file in files) { 
                
                File_Type fileType = SelectDataLogic.DetermineFileType(file.FileName, medicalDevice);

                //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();

                Patient patient = _patientService.GetPatient(1);               

                PatientData patientData = new PatientData() {
                    DataType = (int)fileType,
                    Name = file.FileName,
                    UploadDate = DateTime.Now,
                    Date = DateTime.Now,                    
                    MedicalDevice = medicalDevice,
                    Patient = patient
                };

                if(medicalDevice.Name == Device_Type.BasisPeak.ToString()) {
                    ProcessBasisPeakData(file);
                }
                else if(medicalDevice.Name == Device_Type.Zephyr.ToString()) { 
                    switch (fileType) {
                        case File_Type.Accel:
                            ProcessZephyrAccelData(file, patientData);
                            break;
                        default:
                            break;
                    }
                }
                else if(medicalDevice.Name.Trim() == Device_Type.MicrosoftBand.ToString()) {
                    //TODO: Create msband process methods.
                }
            }

            

            return RedirectToAction("Index", "UserDashboard");
        }

        #region Protected Methods

        //protected string ReadFile() {

        //}

        protected void ProcessBasisPeakData(HttpPostedFileBase file) {


        }

        protected void ProcessZephyrAccelData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrAccelerometer> zephyrAccelData = new List<ZephyrAccelerometer>();

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {
                int fieldCount = csvReader.FieldCount;
                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Time | Vertical | Lateral | Sagittal
                        string dateFormat = "dd/MM/yyyy HH:mm:ss.fff";
                        string date = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            ZephyrAccelerometer zephyrAccel = new ZephyrAccelerometer() {
                                Time = dateTime,
                                Vertical = Convert.ToInt32(csvReader[1]),
                                Lateral = Convert.ToInt32(csvReader[2]),
                                Sagittal = Convert.ToInt32(csvReader[3]),
                                PatentDataId = patientData.Id
                            };
                            zephyrAccelData.Add(zephyrAccel);
                        }
                    }
                }

            }
            
            if(zephyrAccelData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();
                foreach(ZephyrAccelerometer accel in zephyrAccelData) {
                    _accelService.CreateZephyrAccel(accel);
                    _accelService.SaveChanges();
                }
            }
        }

        #endregion
    }
}