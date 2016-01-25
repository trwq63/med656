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
using Microsoft.AspNet.Identity;

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

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SelectDataController(IPatientDataService patientDataService, IZephyrAccelService accelService,
                                    IZephyrBreathingService breathingService, IZephyrECGService ecgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IPatientService patientService, IPhysicianService physicianService) {

            _patientDataService = patientDataService;
            _accelService = accelService;
            _breathingService = breathingService;
            _ecgService = ecgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
            _patientService = patientService;
            _physicianService = physicianService;
        }
        #endregion

        /// <summary>
        /// Load the initial select data view when first navigating to the page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Process the data submitted from the form on the view.
        /// </summary>
        /// <param name="files">Comma delimited list of strings of file names.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessData(HttpPostedFileBase[] files) {           

            foreach (HttpPostedFileBase file in files) {

                Device_Type deviceType = SelectDataLogic.DetermineDeviceType(file.FileName);
                File_Type fileType = SelectDataLogic.DetermineFileType(file.FileName, deviceType);

                //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                Guid guid = new Guid("b2b47c0d-1add-41c3-a783-391aca1c97ab");

                Patient patient = _patientService.GetPatient(guid);

                Physician physician = _physicianService.GetPhysician(1);

                PatientData patientData = new PatientData() {
                    Id = new Guid(),
                    DataType = 0,
                    Name = file.FileName,
                    UploadDate = DateTime.Now,
                    Date = DateTime.Now,
                    Patient = patient
                };

                switch (deviceType) {
                    case Device_Type.BasisPeak:
                        ProcessBasisPeakData(file);
                        break;
                    case Device_Type.Zephyr:
                        switch (fileType) {
                            case File_Type.Accel:
                                ProcessZephyrAccelData(file, patientData);
                                break;
                        }
                        break;
                    case Device_Type.MicrosoftBand:
                        break;
                    default:
                        break;
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