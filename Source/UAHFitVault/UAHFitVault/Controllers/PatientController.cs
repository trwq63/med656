using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.DataAccess;
using UAHFitVault.DataAccess.BasisPeakServices;
using UAHFitVault.DataAccess.MicrosoftBandServices;
using UAHFitVault.DataAccess.ZephyrServices;
using UAHFitVault.Database.Entities;
using UAHFitVault.Helpers;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.LogicLayer.LogicFiles;
using UAHFitVault.Models;
using UAHFitVault.Helpers;

namespace UAHFitVault.Controllers
{
    [Authorization("ROLES_PATIENT")]
    public class PatientController : Controller
    {
        #region Private Members

        /// <summary>
        /// UserManager object used to get the logged in user's information
        /// </summary>
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Service object for accessing patient data database functions.
        /// </summary>
        private readonly IPatientDataService _patientDataService;

        /// <summary>
        /// Service object for accessing Zephyr Accelerometer database functions.
        /// </summary>
        private readonly IZephyrAccelService _zephyrAccelService;

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
        /// Service object for accessing basis peak summary database functions.
        /// </summary>
        private readonly IBasisPeakSummaryService _basisPeakService;

        /// <summary>
        /// Service for accessing medical devices.
        /// </summary>
        private readonly IMedicalDeviceService _medicalDeviceService;

        /// <summary>
        /// Service object for accessing Microsoft Band Accelerometer database functions.
        /// </summary>
        private readonly IMSBandAccelService _msBandAccelService;

        /// <summary>
        /// Service object for accessing Microsoft Band Calories database functions.
        /// </summary>
        private readonly IMSBandCaloriesService _msBandCaloriesService;

        /// <summary>
        /// Service object for accessing Microsoft Band Distance database functions.
        /// </summary>
        private readonly IMSBandDistanceService _msBandDistanceService;

        /// <summary>
        /// Service object for accessing Microsoft Band Gryoscope database functions.
        /// </summary>
        private readonly IMSBandGyroscopeService _msBandGyroscopeService;

        /// <summary>
        /// Service object for accessing Microsoft Band Heart Rate database functions.
        /// </summary>
        private readonly IMSBandHeartRateService _msBandHeartRateService;

        /// <summary>
        /// Service object for accessing Microsoft Band Pedometer database functions.
        /// </summary>
        private readonly IMSBandPedometerService _msBandPedometerService;

        /// <summary>
        /// Service object for accessing Microsoft Band Temperature database functions.
        /// </summary>
        private readonly IMSBandTemperatureService _msBandTemperatureService;

        /// <summary>
        /// Service object for accessing Microsoft Band UV database functions.
        /// </summary>
        private readonly IMSBandUVService _msBandUVService;

        /// <summary>
        /// Service object for access activities database functions.
        /// </summary>
        private readonly IActivityService _activityService;

        /// <summary>
        /// Service object for access to physician database functions.
        /// </summary>
        private readonly IPhysicianService _physicianService;
        
        #endregion

        #region Private Properties

        /// <summary>
        /// User manager object needed throughout the controller to access applicationuser objects.
        /// </summary>
        /// <returns></returns>
        private ApplicationUserManager UserManager {
            get {
                if (_userManager == null) {
                    _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }

                return _userManager;
            }
        }

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
        /// <param name="patientDataService">Service object for accessing patient data database functions.</param>
        /// <param name="zephyrAccelService">Service object for accessing Zephyr Accelerometer database functions.</param>
        /// <param name="breathingService">Service object for accessing Zephyr Breathing Waveform database functions.</param>
        /// <param name="ecgService">Service object for accessing Zephyr ECG Waveform database functions.</param>
        /// <param name="eventDataService">Service object for accessing Zephyr Event Data database functions.</param>
        /// <param name="summaryService">Service object for accessing Zephyr Summary database functions.</param>
        /// <param name="patientService">Service object for accessing patient database functions.</param>
        /// <param name="basisPeakService">Service object for accessing basis peak summary database functions.</param>
        /// <param name="medicalDeviceService">Service for accessing medical devices.</param>
        /// <param name="msBandAccelService">Service object for accessing Microsoft Band Accelerometer database functions.</param>
        /// <param name="msBandCaloriesService">Service object for accessing Microsoft Band Calories database functions.</param>
        /// <param name="msBandDistanceService">Service object for accessing Microsoft Band Distance database functions.</param>
        /// <param name="msBandGyroscopeService">Service object for accessing Microsoft Band Gyroscope database functions.</param>
        /// <param name="msBandHeartRateService">Service object for accessing Microsoft Band Heart Rate database functions.</param>
        /// <param name="msBandPedometerService">Service object for accessing Microsoft Band Pedometer database functions.</param>
        /// <param name="msBandTemperatureService">Service object for accessing Microsoft Band Temperature database functions.</param>
        /// <param name="msBandUVService">Service object for accessing Microsoft Band UV database functions.</param>
        /// <param name="activityService">Service object for accessing activity database functions.</param>
        /// <param name="physicianService">Service object for accessing physician database functions.</param>
        public PatientController(IPatientDataService patientDataService, IZephyrAccelService zephyrAccelService,
                                    IZephyrBreathingService breathingService, IZephyrECGService ecgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IPatientService patientService, IBasisPeakSummaryService basisPeakService,
                                    IMedicalDeviceService medicalDeviceService, IMSBandAccelService msBandAccelService,
                                    IMSBandCaloriesService msBandCaloriesService, IMSBandDistanceService msBandDistanceService, 
                                    IMSBandGyroscopeService msBandGyroscopeService, IMSBandHeartRateService msBandHeartRateService, 
                                    IMSBandPedometerService msBandPedometerService, IMSBandTemperatureService msBandTemperatureService,
                                    IMSBandUVService msBandUVService, IActivityService activityService, IPhysicianService physicianService) {

            _patientDataService = patientDataService;
            _zephyrAccelService = zephyrAccelService;
            _breathingService = breathingService;
            _ecgService = ecgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
            _patientService = patientService;
            _basisPeakService = basisPeakService;
            _medicalDeviceService = medicalDeviceService;
            _msBandAccelService = msBandAccelService;
            _msBandCaloriesService = msBandCaloriesService;
            _msBandDistanceService = msBandDistanceService;
            _msBandGyroscopeService = msBandGyroscopeService;
            _msBandHeartRateService = msBandHeartRateService;
            _msBandPedometerService = msBandPedometerService;
            _msBandTemperatureService = msBandTemperatureService;
            _msBandUVService = msBandUVService;
            _activityService = activityService;
            _physicianService = physicianService;

        }
        #endregion

        /// <summary>
        /// Loads the initial view for when a patient first logs in.  This view is used for selecting data to view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Find all the patient or patients that have data records available to export to display on the view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Export() {
            List<ExportViewModel> viewModel = new List<ExportViewModel>();

            if (User.IsInRole(UserRole.Patient.ToString())) {
                Patient patient = _patientService.GetPatient(UserManager.FindById(User.Identity.GetUserId()).PatientId);
                if(patient.PatientData.Count > 0) {
                    ExportViewModel model = new ExportViewModel() {
                        PatientId = patient.Id,
                        Username = User.Identity.Name,
                        PatientData = patient.PatientData
                    };
                    viewModel.Add(model);
                }
            }
            else if (User.IsInRole(UserRole.Physician.ToString())) {
                Physician physician = _physicianService.GetPhysician(UserManager.FindById(User.Identity.GetUserId()).PhysicianId);
                foreach (Patient patient in physician.Patients) {
                    if(patient.PatientData.Count > 0) {
                        ExportViewModel model = new ExportViewModel() {
                            PatientId = patient.Id,
                            Username = User.Identity.Name,
                            PatientData = patient.PatientData
                        };
                        viewModel.Add(model);
                    }
                }
            }

            return View(viewModel);
        }

        /// <summary>
        /// Export the contents of the patient data record to the user's harddrive.
        /// </summary>
        /// <param name="patientDataId"></param>
        /// <returns></returns>
        public string ExportData(string path, string patientDataId) {

            PatientData patientData = _patientDataService.GetPatientData(patientDataId);

            List<string> columnNames = new List<string>();
            CsvExport export = new CsvExport();
            

            switch (patientData.DataType) {
                case (int)File_Type.Accelerometer :
                    Device_Type deviceType = PatientLogic.DetermineDeviceType(patientData.Name);
                    switch (deviceType) {
                        case Device_Type.Zephyr:
                            break;
                        case Device_Type.Microsoft_Band:
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)File_Type.Breathing:
                    break;
                case (int)File_Type.Calorie:
                    IEnumerable<MSBandCalories> calorieData = _msBandCaloriesService.GetMSBandCaloriesData(patientData);
                    foreach (MSBandCalories data in calorieData) {
                        export.AddRow();
                        export["Time Stamp"] = data.Date;
                        export["Total Calories(kCal)"] = data.Total;
                    }
                    break;
                case (int)File_Type.Distance:
                    IEnumerable<MSBandDistance> distanceData = _msBandDistanceService.GetMSBandDistanceData(patientData);
                    foreach (MSBandDistance data in distanceData) {
                        export.AddRow();
                        export["Time Stamp"] = data.Date;
                        export["Motion Type"] = data.MotionType;
                        export["Pace(min/km)"] = data.Pace;
                        export["Speed(km/hr)"] = data.Speed;
                        export["Total(km)"] = data.Total;
                    }
                    break;
                case (int)File_Type.ECG:
                    IEnumerable<ZephyrECGWaveform> ecgData = _ecgService.GetZephyrECGWaveFormData(patientData);
                    var test = typeof(ZephyrECGWaveform).GetProperties();
                    break;
                case (int)File_Type.EventData:
                    IEnumerable<ZephyrEventData> eventData = _eventDataService.GetZephyrEventData(patientData);
                    foreach (ZephyrEventData data in eventData) {
                        export.AddRow();
                        export["SeqNo"] = "0";
                        export["Time Stamp"] = data.Date;
                        export["EventCode"] = data.EventCode;
                        export["Type"] = data.Type;
                        export["Source"] = data.Source;
                        export["EventID"] = data.EventId;
                        export["EventSpecificData"] = data.EventSpecificData;
                    }
                    break;
                case (int)File_Type.Gyroscope:
                    IEnumerable<MSBandGyroscope> gyroscopeData = _msBandGyroscopeService.GetMSBandGyroscopeData(patientData);
                    foreach (MSBandGyroscope data in gyroscopeData) {
                        export.AddRow();
                        export["Time Stamp"] = data.Date;
                        export["X-Axis(Â°/s)"] = data.X;
                        export["Y-Axis(Â°/s)"] = data.Y;
                        export["Z-Axis(Â°/s)"] = data.Z;
                    }
                    break;
                case (int)File_Type.HeartRate:
                    IEnumerable<MSBandHeartRate> heartRateData = _msBandHeartRateService.GetMSBandHeartRateData(patientData);
                    foreach (MSBandHeartRate data in heartRateData) {
                        export.AddRow();
                        export["Time Stamp"] = data.Date;
                        export["Read Status"] = data.ReadStatus;
                        export["Heart Rate(bpm)"] = data.HeartRate;
                    }
                    break;
                case (int)File_Type.Pedometer:
                    IEnumerable<MSBandPedometer> pedometerData = _msBandPedometerService.GetMSBandPedometerData(patientData);
                    foreach (MSBandPedometer data in pedometerData) {
                        export.AddRow();
                        export["Time Stamp"] = data.Date;
                        export["Steps"] = data.Steps;
                    }
                    break;
                case (int)File_Type.Summary:
                    Device_Type deviceType = PatientLogic.DetermineDeviceType(patientData.Name);
                    switch (deviceType) {
                        case Device_Type.Zephyr:
                            break;
                        case Device_Type.BasisPeak:
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)File_Type.Temperature:
                    IEnumerable<MSBandTemperature> temperatureData = _msBandTemperatureService.GetMSBandTemperatureData(patientData);
                    foreach (MSBandTemperature data in temperatureData) {
                        export.AddRow();
                        export["Time Stamp"] = data.Date;
                        export["Temperature(Â°C)"] = data.Temperature;
                    }
                    break;
                case (int)File_Type.UV:                    
                    IEnumerable<MSBandUV> uvData = _msBandUVService.GetMSBandUVData(patientData);
                    foreach (MSBandUV data in uvData) {
                        export.AddRow();
                        export["Time Stamp"] = data.Date;
                        export["UV Index (0-4)"] = data.UVIndex;
                    }
                    break;
                default:
                    break;
            }

            export.ExportToFile(@path + "\\" + patientData.Name);

            return null;
        }

        /// <summary>
        /// Load the patient view for the uploading data.
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadData() {

            List<MedicalDevice> medicalDevices = (List<MedicalDevice>)_medicalDeviceService.GetMedicalDevices();
            if (medicalDevices != null && medicalDevices.Count > 0) {
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
        public ActionResult ProcessData(UploadDataViewModel model) {

            MedicalDevice medicalDevice = PatientLogic.DetermineDeviceType(MedicalDevices, model.MedicalDeviceType);

            //If one file is sucessfully inserted then activities need to be inserted if any were entered.
            bool insertActivities = false;

            foreach (HttpPostedFileBase file in model.Files) { 
                
                File_Type fileType = PatientLogic.DetermineFileType(file.FileName, medicalDevice);
                
                Patient patient = _patientService.GetPatient(UserManager.FindById(User.Identity.GetUserId()).PatientId);

                PatientData patientData = new PatientData() {
                    Id = Guid.NewGuid().ToString(),
                    DataType = (int)fileType,
                    Name = file.FileName,
                    UploadDate = DateTime.Now,
                    FromDate = model.FromDate,
                    ToDate = model.ToDate,           
                    MedicalDeviceId = medicalDevice.Id,
                    Patient = patient
                };
                                
                bool result = false;

                if(medicalDevice.Name == Device_Type.BasisPeak.ToString()) {
                    result = ProcessBasisPeakData(file, patientData);
                    if (result) {
                        insertActivities = true;
                    }
                }
                else if(medicalDevice.Name == Device_Type.Zephyr.ToString()) { 
                    switch (fileType) {
                        case File_Type.Accelerometer:
                            result = ProcessZephyrAccelData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.Breathing:
                            result = ProcessZephyrBreathingData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.ECG:
                            result = ProcessZephyrECGData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.EventData:
                            result = ProcessZephyrEventData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.Summary:
                            result = ProcessZephyrSummaryData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if(medicalDevice.Name.Trim() == Device_Type.Microsoft_Band.ToString().Replace("_", " ")) {
                    switch (fileType) {
                        case File_Type.Accelerometer:
                            result = ProcessMSBandAccelData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.Calorie:
                            result = ProcessMSBandCalorieData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.Distance:
                            result = ProcessMSBandDistanceData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.Gyroscope:
                            result = ProcessMSBandGyroscopeData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.HeartRate:
                            result = ProcessMSBandHeartRateData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.Pedometer:
                            result = ProcessMSBandPedometerData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.Temperature:
                            result = ProcessMSBandTemperatureData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        case File_Type.UV:
                            result = ProcessMSBandUVData(file, patientData);
                            if (result) {
                                insertActivities = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            if (insertActivities && model.Activities.Count > 0) {
                InsertActivities(model.Activities);
            }

            return RedirectToAction("Index", "PatientData");
        }

        #region Protected Methods

        /// <summary>
        /// Insert Basis Peakk Summary data records from file to database.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="patientData"></param>
        protected bool ProcessBasisPeakData(HttpPostedFileBase file, PatientData patientData) {
            List<BasisPeakSummaryData> basisPeakSummaryData = null;

            Stream stream = file.InputStream;

            //Note: Excel Reader is disposable per wiki on github
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {
                basisPeakSummaryData = PatientLogic.BuildBasisPeakSummaryDataList(csvReader, patientData);              
            }

            if(basisPeakSummaryData != null && basisPeakSummaryData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _basisPeakService.BulkInsert(basisPeakSummaryData);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert Zephyr accel records from file into database
        /// </summary>
        /// <param name="file">Zephyr accel file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected bool ProcessZephyrAccelData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrAccelerometer> zephyrAccelData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrAccelData = PatientLogic.BuildZephyrAccelDataList(csvReader, patientData);
            }
            
            if(zephyrAccelData != null && zephyrAccelData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _zephyrAccelService.BulkInsert(zephyrAccelData);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert Zephyr ECG records from file into database.
        /// </summary>
        /// <param name="file">Zephyr ecg file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected bool ProcessZephyrECGData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrECGWaveform> zephyrEcgData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrEcgData = PatientLogic.BuildZephyrEcgDataList(csvReader, patientData);
            }

            if (zephyrEcgData != null && zephyrEcgData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _ecgService.BulkInsert(zephyrEcgData);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert Zephyr Breathing records from file into database.
        /// </summary>
        /// <param name="file">Zephyr breathing file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected bool ProcessZephyrBreathingData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrBreathingWaveform> zephyrBreathingData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrBreathingData = PatientLogic.BuildZephyrBreathingDataList(csvReader, patientData);
            }

            if (zephyrBreathingData != null && zephyrBreathingData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _breathingService.BulkInsert(zephyrBreathingData);

                return true;
            }

            return false;
        }


        /// <summary>
        /// Insert Zephyr Event Data records from file into database.
        /// </summary>
        /// <param name="file">Zephyr breathing file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected bool ProcessZephyrEventData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrEventData> zephyrEventData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrEventData = PatientLogic.BuildZephyrEventDataList(csvReader, patientData);
            }

            if (zephyrEventData != null && zephyrEventData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _eventDataService.BulkInsert(zephyrEventData);

                return true;
            }

            return false;
        }


        /// <summary>
        /// Insert Zephyr Summary Data records from file into database.
        /// </summary>
        /// <param name="file">Zephyr breathing file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected bool ProcessZephyrSummaryData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrSummaryData> zephyrSummaryData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrSummaryData = PatientLogic.BuildZephyrSummaryDataList(csvReader, patientData);
            }

            if (zephyrSummaryData != null && zephyrSummaryData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _summaryService.BulkInsert(zephyrSummaryData);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert Microsoft Band accel records from file into database
        /// </summary>
        /// <param name="file">Microsoft Band accel file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected bool ProcessMSBandAccelData(HttpPostedFileBase file, PatientData patientData) {
            List<MSBandAccelerometer> msBandAccelData = null;

            Stream stream = file.InputStream;
            DateTime date = DateTime.MinValue;

            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                date = PatientLogic.FindMSBandDate(csvReader);
                stream.Seek(0, SeekOrigin.Begin);
                if (date != DateTime.MinValue) {
                    //Assume that the first line which contains the date is now a comment.  Set the comment
                    //indicator to the character M.
                    using (CsvReader reader = new CsvReader(new StreamReader(stream), true, ',', '"', '"', 'M', ValueTrimmingOptions.UnquotedOnly)) {

                        msBandAccelData = PatientLogic.BuildMSBandAccelerometerDataList(reader, patientData, date);
                    }
                }
            }

            if (msBandAccelData != null && msBandAccelData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert
                _msBandAccelService.BulkInsert(msBandAccelData);

                return true;
            }

            return false;         
        }

        /// <summary>
        /// Insert Microsoft Band calorie records from file into database
        /// </summary>
        /// <param name="file">Microsoft Band calorie file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this calorie data.</param>
        protected bool ProcessMSBandCalorieData(HttpPostedFileBase file, PatientData patientData) {
            List<MSBandCalories> msBandCalorieData = null;

            Stream stream = file.InputStream;
            DateTime date = DateTime.MinValue;

            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                date = PatientLogic.FindMSBandDate(csvReader);
                stream.Seek(0, SeekOrigin.Begin);
                if (date != DateTime.MinValue) {
                    //Assume that the first line which contains the date is now a comment.  Set the comment
                    //indicator to the character M.
                    using (CsvReader reader = new CsvReader(new StreamReader(stream), true, ',', '"', '"', 'M', ValueTrimmingOptions.UnquotedOnly)) {

                        msBandCalorieData = PatientLogic.BuildMSBandCalorieDataList(reader, patientData, date);
                    }
                }
            }

            if (msBandCalorieData != null && msBandCalorieData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert 
                _msBandCaloriesService.BulkInsert(msBandCalorieData);

                return true;
            }
            return false;
        }


        /// <summary>
        /// Insert Microsoft Band distance records from file into database
        /// </summary>
        /// <param name="file">Microsoft Band distance file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this distance data.</param>
        protected bool ProcessMSBandDistanceData(HttpPostedFileBase file, PatientData patientData) {
            List<MSBandDistance> msBandDistanceData = null;

            Stream stream = file.InputStream;
            DateTime date = DateTime.MinValue;

            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                date = PatientLogic.FindMSBandDate(csvReader);
                stream.Seek(0, SeekOrigin.Begin);
                if (date != DateTime.MinValue) {
                    //Assume that the first line which contains the date is now a comment.  Set the comment
                    //indicator to the character M.
                    using (CsvReader reader = new CsvReader(new StreamReader(stream), true, ',', '"', '"', 'M', ValueTrimmingOptions.UnquotedOnly)) {

                        msBandDistanceData = PatientLogic.BuildMSBandDistanceDataList(reader, patientData, date);
                    }
                }
            }

            if (msBandDistanceData != null && msBandDistanceData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert 
                _msBandDistanceService.BulkInsert(msBandDistanceData);

                return true;
            }

            return false;
        }


        /// <summary>
        /// Insert Microsoft Band gyroscope records from file into database
        /// </summary>
        /// <param name="file">Microsoft Band gyroscope file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this gyroscope data.</param>
        protected bool ProcessMSBandGyroscopeData(HttpPostedFileBase file, PatientData patientData) {
            List<MSBandGyroscope> msBandGyroscopeData = null;

            Stream stream = file.InputStream;
            DateTime date = DateTime.MinValue;

            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                date = PatientLogic.FindMSBandDate(csvReader);
                stream.Seek(0, SeekOrigin.Begin);
                if (date != DateTime.MinValue) {
                    //Assume that the first line which contains the date is now a comment.  Set the comment
                    //indicator to the character M.
                    using (CsvReader reader = new CsvReader(new StreamReader(stream), true, ',', '"', '"', 'M', ValueTrimmingOptions.UnquotedOnly)) {

                        msBandGyroscopeData = PatientLogic.BuildMSBandGyroscopeDataList(reader, patientData, date);
                    }
                }
            }

            if (msBandGyroscopeData != null && msBandGyroscopeData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert 
                _msBandGyroscopeService.BulkInsert(msBandGyroscopeData);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert Microsoft Band heart rate records from file into database
        /// </summary>
        /// <param name="file">Microsoft Band heart rate file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this heart rate data.</param>
        protected bool ProcessMSBandHeartRateData(HttpPostedFileBase file, PatientData patientData) {
            List<MSBandHeartRate> msBandHeartRateData = null;

            Stream stream = file.InputStream;
            DateTime date = DateTime.MinValue;

            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                date = PatientLogic.FindMSBandDate(csvReader);
                stream.Seek(0, SeekOrigin.Begin);
                if (date != DateTime.MinValue) {
                    //Assume that the first line which contains the date is now a comment.  Set the comment
                    //indicator to the character M.
                    using (CsvReader reader = new CsvReader(new StreamReader(stream), true, ',', '"', '"', 'M', ValueTrimmingOptions.UnquotedOnly)) {

                        msBandHeartRateData = PatientLogic.BuildMSBandHeartRateDataList(reader, patientData, date);
                    }
                }
            }

            if (msBandHeartRateData != null && msBandHeartRateData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert 
                _msBandHeartRateService.BulkInsert(msBandHeartRateData);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert Microsoft Band pedometer records from file into database
        /// </summary>
        /// <param name="file">Microsoft Band pedometer file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this pedometer data.</param>
        protected bool ProcessMSBandPedometerData(HttpPostedFileBase file, PatientData patientData) {
            List<MSBandPedometer> msBandPedometerData = null;

            Stream stream = file.InputStream;
            DateTime date = DateTime.MinValue;

            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                date = PatientLogic.FindMSBandDate(csvReader);
                stream.Seek(0, SeekOrigin.Begin);
                if (date != DateTime.MinValue) {
                    //Assume that the first line which contains the date is now a comment.  Set the comment
                    //indicator to the character M.
                    using (CsvReader reader = new CsvReader(new StreamReader(stream), true, ',', '"', '"', 'M', ValueTrimmingOptions.UnquotedOnly)) {

                        msBandPedometerData = PatientLogic.BuildMSBandPedometerDataList(reader, patientData, date);
                    }
                }
            }

            if (msBandPedometerData != null && msBandPedometerData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert 
                _msBandPedometerService.BulkInsert(msBandPedometerData);

                return true;
            }

            return false;
        }


        /// <summary>
        /// Insert Microsoft Band temperature records from file into database
        /// </summary>
        /// <param name="file">Microsoft Band temperature file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this temperature data.</param>
        protected bool ProcessMSBandTemperatureData(HttpPostedFileBase file, PatientData patientData) {
            List<MSBandTemperature> msBandTemperatureData = null;

            Stream stream = file.InputStream;
            DateTime date = DateTime.MinValue;

            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                date = PatientLogic.FindMSBandDate(csvReader);
                stream.Seek(0, SeekOrigin.Begin);
                if (date != DateTime.MinValue) {
                    //Assume that the first line which contains the date is now a comment.  Set the comment
                    //indicator to the character M.
                    using (CsvReader reader = new CsvReader(new StreamReader(stream), true, ',', '"', '"', 'M', ValueTrimmingOptions.UnquotedOnly)) {

                        msBandTemperatureData = PatientLogic.BuildMSBandTemperatureDataList(reader, patientData, date);
                    }
                }
            }

            if (msBandTemperatureData != null && msBandTemperatureData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert 
                _msBandTemperatureService.BulkInsert(msBandTemperatureData);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert Microsoft Band uv records from file into database
        /// </summary>
        /// <param name="file">Microsoft Band uv file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this uv data.</param>
        protected bool ProcessMSBandUVData(HttpPostedFileBase file, PatientData patientData) {
            List<MSBandUV> msBandUVData = null;

            Stream stream = file.InputStream;
            DateTime date = DateTime.MinValue;

            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                date = PatientLogic.FindMSBandDate(csvReader);
                stream.Seek(0, SeekOrigin.Begin);
                if (date != DateTime.MinValue) {
                    //Assume that the first line which contains the date is now a comment.  Set the comment
                    //indicator to the character M.
                    using (CsvReader reader = new CsvReader(new StreamReader(stream), true, ',', '"', '"', 'M', ValueTrimmingOptions.UnquotedOnly)) {

                        msBandUVData = PatientLogic.BuildMSBandUVDataList(reader, patientData, date);
                    }
                }
            }

            if (msBandUVData != null && msBandUVData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert 
                _msBandUVService.BulkInsert(msBandUVData);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Insert activities the user wants associated with the data into the database.
        /// </summary>
        /// <param name="activityModels">List of activities created by the user.</param>
        protected void InsertActivities(List<ActivityModel> activityModels) {            
            foreach(ActivityModel model in activityModels) {
                if (model.ActivityType != null) {
                    Activity activity = new Activity() {
                        DataActivity = (int)Enum.Parse(typeof(ActivityType), model.ActivityType),
                        StartTime = model.StartTime,
                        EndTime = model.EndTime
                    };
                    if (PatientLogic.IsActivityValid(activity)) {
                        _activityService.CreateActivity(activity);
                    }
                }
            }
        }

        #endregion
    }
}