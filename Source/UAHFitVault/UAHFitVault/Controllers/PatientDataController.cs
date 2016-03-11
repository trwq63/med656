using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.Helpers;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.DataAccess;
using UAHFitVault.DataAccess.ZephyrServices;
using UAHFitVault.DataAccess.BasisPeakServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using UAHFitVault.Database.Entities;
using UAHFitVault.LogicLayer.Models;
using UAHFitVault.LogicLayer.LogicFiles;
using UAHFitVault.Models;
using UAHFitVault.Resources;
using Newtonsoft.Json;

namespace UAHFitVault.Controllers
{
    /// <summary>
    /// Controller used to manage views for viewing patient data by a patient or physician
    /// </summary>
    [Authorization("ROLES_PATIENTDATA")]
    public class PatientDataController : Controller
    {
        #region Private Members

        /// <summary>
        /// UserManager object used to get the logged in user's information
        /// </summary>
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Service interface for accessing patient data database functions
        /// </summary>
        private readonly IPatientService _patientService;

        /// <summary>
        /// Service interface for accessing patient data records database functions
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
        /// Service object for accessing basis peak summary database functions.
        /// </summary>
        private readonly IBasisPeakSummaryService _basisPeakService;

        /// <summary>
        /// Service for accessing medical devices.
        /// </summary>
        private readonly IMedicalDeviceService _medicalDeviceService;

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
        /// Session property to save the patient's user id
        /// </summary>
        public string PatientUserId {
            get {
                return Session["patientUserId"].ToString();
            }
            set {
                Session["patientUserId"] = value;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="patientDataService">Service object for accessing patient data database functions.</param>
        /// <param name="accelService">Service object for accessing Zephyr Accelerometer database functions.</param>
        /// <param name="breathingService">Service object for accessing Zephyr Breathing Waveform database functions.</param>
        /// <param name="ecgService">Service object for accessing Zephyr ECG Waveform database functions.</param>
        /// <param name="eventDataService">Service object for accessing Zephyr Event Data database functions.</param>
        /// <param name="summaryService">Service object for accessing Zephyr Summary database functions.</param>
        /// <param name="patientService">Service object for accessing patient database functions.</param>
        /// <param name="basisPeakService">Service object for accessing basis peak summary database functions.</param>
        /// <param name="medicalDeviceService">Service for accessing medical devices.</param>
        public PatientDataController(IPatientDataService patientDataService, IZephyrAccelService zephyrAccelService,
                                    IZephyrBreathingService breathingService, IZephyrECGService ecgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IPatientService patientService, IBasisPeakSummaryService basisPeakService,
                                    IMedicalDeviceService medicalDeviceService) {

            _patientDataService = patientDataService;
            _zephyrAccelService = zephyrAccelService;
            _breathingService = breathingService;
            _ecgService = ecgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
            _patientService = patientService;
            _basisPeakService = basisPeakService;
            _medicalDeviceService = medicalDeviceService;

        }

        #endregion

        /// <summary>
        /// Load initial view where user can select patient data to view.
        /// </summary>
        /// <param name="patientUserId">Patient's user id</param>
        /// <returns></returns>
        public ActionResult Index(string patientUserId)
        {
            List<PatientDataByDevice> dataByDevice = new List<PatientDataByDevice>();

            //If no patientUserId check to see if the user is a patient
            if (string.IsNullOrEmpty(patientUserId)) {
                //If the user is a patient get the current logged in user id.
                if (User.IsInRole(UserRole.Patient.ToString())) {
                    patientUserId = User.Identity.GetUserId();
                }
            }

            //If there is still no user id return nothing to view.
            if (!string.IsNullOrEmpty(patientUserId)) {

                PatientUserId = patientUserId;

                Patient patient = _patientService.GetPatient(UserManager.FindById(patientUserId).PatientId);                

                if (patient.PatientData != null && patient.PatientData.Count > 0) {
                    List<PatientData> patientData = patient.PatientData;

                    //Get a list of all the medical devices a patient may have uploaded data for.
                    List<string> medicalDevices = _medicalDeviceService.GetMedicalDevices().Where(d => d.Name != "Unknown").Select(d => d.Name).ToList();

                    //Sort patient data records by device.
                    foreach (string device in medicalDevices) {
                        PatientDataByDevice deviceData = PatientDataLogic.SortPatientData(patientData, device);
                        if (deviceData != null) {
                            dataByDevice.Add(deviceData);
                        }
                    }
                }
                //Sort patient data records by type.
            }
            return View(dataByDevice);
        }

        /// <summary>
        /// Create the graph to be displayed to the user.
        /// </summary>
        /// <param name="patientData">List of patient data records to be displayed.</param>
        /// <returns></returns>
        public string GraphData(DateTime date, DateTime startTime, DateTime endTime, List<string> patientData) {

            PatientDataViewModel graphViewModel = new PatientDataViewModel();

            DateTime start = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, startTime.Second);
            DateTime end = new DateTime(date.Year, date.Month, date.Day, endTime.Hour, endTime.Minute, endTime.Second);

            foreach (string record in patientData) {
                PatientData dataRecord = _patientDataService.GetPatientData(record);

                switch (dataRecord.MedicalDevice.Name) {
                    case "Zephyr":
                        switch ((File_Type)dataRecord.DataType) {
                            case File_Type.Accelerometer:
                                List<ZephyrAccelerometer> zephyrAccelData = new List<ZephyrAccelerometer>();
                                zephyrAccelData = _zephyrAccelService.GetZephyrAccelerometerData(dataRecord, start, end).ToList();
                                if(zephyrAccelData.Count > 0) {
                                    List<double> accelData = ZephyrLogic.ConvertAccelWaveformToGs(zephyrAccelData.Select(z => z.Vertical).ToList());
                                    LineGraphModel lineModel = new LineGraphModel() {
                                        XAxisName = AxisNames.ACCEL_X_AXIS,
                                        YAxisName = AxisNames.ACCEL_Y_AXIS,
                                        XAxisData = zephyrAccelData.Select(z => z.Time).ToList(),
                                        YAxisData = accelData
                                    };
                                    graphViewModel.LineGraphModels.Add(lineModel);
                                }
                                
                                break;
                            case File_Type.Breathing:
                                break;
                            case File_Type.ECG:
                                break;
                            case File_Type.EventData:
                                break;
                            case File_Type.Summary:
                                break;
                            default:
                                break;
                        }
                        break;
                    case "BasisPeak":
                        break;
                    case "Microsoft Band":
                        break;
                    default:
                        break;
                }
            }

            string jsonResult;

            if(graphViewModel.LineGraphModels.Count > 0) {
                jsonResult = JsonConvert.SerializeObject(graphViewModel);
            }
            else {
                Dictionary<string, string> errorMsg = new Dictionary<string, string>();
                errorMsg.Add("error", "No Data Found");
                jsonResult = JsonConvert.SerializeObject(errorMsg);
            }

            return jsonResult;
        }
    }
}