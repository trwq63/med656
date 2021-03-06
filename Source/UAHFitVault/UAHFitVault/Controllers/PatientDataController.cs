﻿using System;
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
using UAHFitVault.DataAccess.MicrosoftBandServices;

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
        private readonly IZephyrBreathingService _zephyrBreathingService;

        /// <summary>
        /// Service object for accessing Zephyr ECG Waveform database functions.
        /// </summary>
        private readonly IZephyrECGService _zephyrEcgService;

        /// <summary>
        /// Service object for accessing Zephyr Event Data database functions.
        /// </summary>
        private readonly IZephyrEventDataService _eventDataService;

        /// <summary>
        /// Service object for accessing Zephyr Summary database functions.
        /// </summary>
        private readonly IZephyrSummaryService _summaryService;

        /// <summary>
        /// Service object for accessing Zephyr BR RR database functions.
        /// </summary>
        private readonly IZephyrBrRrService _brRrService;

        /// <summary>
        /// Service object for accessing basis peak summary database functions.
        /// </summary>
        private readonly IBasisPeakSummaryService _basisPeakService;

        /// <summary>
        /// Service object for accessing Microsoft Band Heart Rate database functions.
        /// </summary>
        private readonly IMSBandHeartRateService _msBandHeartRateService;

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
        /// <param name="zephyrAccelService">Service object for accessing Zephyr Accelerometer database functions.</param>
        /// <param name="zephyrBreathingService">Service object for accessing Zephyr Breathing Waveform database functions.</param>
        /// <param name="zephyrEcgService">Service object for accessing Zephyr ECG Waveform database functions.</param>
        /// <param name="eventDataService">Service object for accessing Zephyr Event Data database functions.</param>
        /// <param name="summaryService">Service object for accessing Zephyr Summary database functions.</param>
        /// <param name="brRrService">Service object for accessing Zephyr BR RR database functions.</param>
        /// <param name="patientService">Service object for accessing patient database functions.</param>
        /// <param name="basisPeakService">Service object for accessing basis peak summary database functions.</param>
        /// <param name="msBandHeartRateService">Service object for accessing Microsoft Band Heart Rate database functions.</param>
        /// <param name="medicalDeviceService">Service for accessing medical devices.</param>
        public PatientDataController(IPatientDataService patientDataService, IZephyrAccelService zephyrAccelService,
                                    IZephyrBreathingService zephyrBreathingService, IZephyrECGService zephyrEcgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IZephyrBrRrService brRrService, IPatientService patientService, IBasisPeakSummaryService basisPeakService,
                                    IMSBandHeartRateService msBandHeartRateService, IMedicalDeviceService medicalDeviceService) {

            _patientDataService = patientDataService;
            _zephyrAccelService = zephyrAccelService;
            _zephyrBreathingService = zephyrBreathingService;
            _zephyrEcgService = zephyrEcgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
            _brRrService = brRrService;
            _patientService = patientService;
            _basisPeakService = basisPeakService;
            _msBandHeartRateService = msBandHeartRateService;
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
        /// <param name="date">Date of the data to graph</param>
        /// <param name="startTime">Start time of the data you wish to view.</param>
        /// <param name="endTime">End time of the data you wish to view.</param>
        /// <param name="patientData">List of patient data records to be displayed.</param>
        /// <param name="option">Indicates the type of data to display to the user.</param>
        /// <returns></returns>
        public string GraphData(string date, string startTime, string endTime, List<string> patientData, DataViewOptions option) {

            PatientDataViewModel graphViewModel = new PatientDataViewModel();

            if (patientData != null && patientData.Count > 0) {

                DateTime day;
                DateTime start = DateTime.MinValue;
                DateTime end = DateTime.MaxValue;
                if (!string.IsNullOrEmpty(date)) {
                    day = DateTime.Parse(date);
                    if (!string.IsNullOrEmpty(startTime)) {
                        start = DateTime.Parse(startTime);
                        start = new DateTime(day.Year, day.Month, day.Day, start.Hour, start.Minute, start.Second);
                    }
                    else {
                        start = day;
                    }
                    if (!string.IsNullOrEmpty(endTime)) {
                        end = DateTime.Parse(endTime);
                        end = new DateTime(day.Year, day.Month, day.Day, end.Hour, end.Minute, end.Second);
                    }
                    else {
                        end = day.AddDays(1).AddSeconds(-1);
                    }
                }

                switch (option) {
                    case DataViewOptions.Heart_Rate:
                        foreach (string record in patientData) {
                            PatientData dataRecord = _patientDataService.GetPatientData(record);
                            switch (dataRecord.MedicalDevice.Name) {
                                case "Zephyr":
                                    List<ZephyrSummaryData> zephyrSummaryData = _summaryService.GetZephyrSummaryData(dataRecord, start, end).ToList();
                                    if (zephyrSummaryData != null && zephyrSummaryData.Count > 0) {
                                        LineGraphModel lineModel = new LineGraphModel() {
                                            GraphType = "Zephyr Heart Rate",
                                            XAxisName = AxisNames.GENERIC_X_AXIS,
                                            YAxisName = AxisNames.BEATS_PER_MINUTE,
                                            XAxisData = zephyrSummaryData.Select(b => b.Date).ToList(),
                                            YAxisData = zephyrSummaryData.Select(b => b.HeartRate).Select(d => (double)d).ToList()
                                        };
                                        graphViewModel.LineGraphModels.Add(lineModel);
                                    }
                                    break;
                                case "BasisPeak":
                                    List<BasisPeakSummaryData> basisSummaryData = _basisPeakService.GetBasisPeakSummaryData(dataRecord, start, end).ToList();
                                    if (basisSummaryData != null && basisSummaryData.Count > 0) {
                                        LineGraphModel lineModel = new LineGraphModel() {
                                            GraphType = "BasisPeak Heart Rate",
                                            XAxisName = AxisNames.GENERIC_X_AXIS,
                                            YAxisName = AxisNames.BEATS_PER_MINUTE,
                                            XAxisData = basisSummaryData.Select(b => b.Date).ToList(),
                                            YAxisData = basisSummaryData.Where(b => b.HeartRate != null).Select(b => b.HeartRate).Select(d => (double)d).ToList()
                                        };
                                        graphViewModel.LineGraphModels.Add(lineModel);
                                    }
                                    break;
                                case "Microsoft Band":
                                    List<MSBandHeartRate> msBandHeartRate = _msBandHeartRateService.GetMSBandHeartRateData(dataRecord, start, end).ToList();
                                    if (msBandHeartRate != null && msBandHeartRate.Count > 0) {
                                        LineGraphModel lineModel = new LineGraphModel() {
                                            GraphType = "MS Band Heart Rate",
                                            XAxisName = AxisNames.GENERIC_X_AXIS,
                                            YAxisName = AxisNames.BEATS_PER_MINUTE,
                                            XAxisData = msBandHeartRate.Select(b => b.Date).ToList(),
                                            YAxisData = msBandHeartRate.Select(b => b.HeartRate).Select(d => (double)d).ToList()
                                        };
                                        graphViewModel.LineGraphModels.Add(lineModel);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case DataViewOptions.Waveforms:
                        foreach (string record in patientData) {
                            PatientData dataRecord = _patientDataService.GetPatientData(record);

                            switch (dataRecord.MedicalDevice.Name) {
                                case "Zephyr":
                                    switch ((File_Type)dataRecord.DataType) {
                                        case File_Type.Accelerometer:
                                            List<ZephyrAccelerometer> zephyrAccelData = _zephyrAccelService.GetZephyrAccelerometerData(dataRecord, start, end).ToList();
                                            if (zephyrAccelData != null && zephyrAccelData.Count > 0) {
                                                List<double> accelData = ZephyrLogic.ConvertAccelWaveformToGs(zephyrAccelData.Select(z => z.Vertical).ToList());
                                                LineGraphModel lineModel = new LineGraphModel() {
                                                    GraphType = "Zephyr Accel",
                                                    XAxisName = AxisNames.GENERIC_X_AXIS,
                                                    YAxisName = AxisNames.ZEPHYR_ACCEL_Y_AXIS,
                                                    XAxisData = zephyrAccelData.Select(z => z.Time).ToList(),
                                                    YAxisData = accelData
                                                };
                                                graphViewModel.LineGraphModels.Add(lineModel);
                                            }
                                            break;
                                        case File_Type.Breathing:
                                            List<ZephyrBreathingWaveform> zephyrBreathingData =
                                                _zephyrBreathingService.GetZephyrBreathingWaveformData(dataRecord, start, end).ToList();
                                            if (zephyrBreathingData != null && zephyrBreathingData.Count > 0) {
                                                LineGraphModel lineModel = new LineGraphModel() {
                                                    GraphType = "Zephyr Breathing",
                                                    XAxisName = AxisNames.GENERIC_X_AXIS,
                                                    YAxisName = AxisNames.GENERIC_Y_AXIS,
                                                    XAxisData = zephyrBreathingData.Select(z => z.Time).ToList(),
                                                    YAxisData = zephyrBreathingData.Select(z => z.Data).Select(d => (double)d).ToList()
                                                };
                                                graphViewModel.LineGraphModels.Add(lineModel);
                                            }
                                            break;
                                        case File_Type.ECG:
                                            List<ZephyrECGWaveform> zephyrEcgWaveform =
                                                _zephyrEcgService.GetZephyrECGWaveFormData(dataRecord, start, end).ToList();
                                            if (zephyrEcgWaveform != null && zephyrEcgWaveform.Count > 0) {
                                                LineGraphModel lineModel = new LineGraphModel() {
                                                    GraphType = "Zephyr ECG",
                                                    XAxisName = AxisNames.GENERIC_X_AXIS,
                                                    YAxisName = AxisNames.GENERIC_Y_AXIS,
                                                    XAxisData = zephyrEcgWaveform.Select(z => z.Time).ToList(),
                                                    YAxisData = zephyrEcgWaveform.Select(z => z.Data).Select(d => (double)d).ToList()
                                                };
                                                graphViewModel.LineGraphModels.Add(lineModel);
                                            }
                                            break;
                                        case File_Type.EventData:
                                            break;
                                        case File_Type.Summary:
                                            List<ZephyrSummaryData> zephyrSummaryData = _summaryService.GetZephyrSummaryData(dataRecord, start, end).ToList();
                                            if (zephyrSummaryData != null && zephyrSummaryData.Count > 0) {
                                                LineGraphModel lineModel = new LineGraphModel() {
                                                    GraphType = "Zephyr Heart Rate",
                                                    XAxisName = AxisNames.GENERIC_X_AXIS,
                                                    YAxisName = AxisNames.BEATS_PER_MINUTE,
                                                    XAxisData = zephyrSummaryData.Select(b => b.Date).ToList(),
                                                    YAxisData = zephyrSummaryData.Select(b => b.HeartRate).Select(d => (double)d).ToList()
                                                };
                                                graphViewModel.LineGraphModels.Add(lineModel);
                                            }
                                            break;
                                        case File_Type.General:
                                            List<ZephyrSummaryData> zephyrGeneralData = _summaryService.GetZephyrSummaryData(dataRecord, start, end).ToList();
                                            if (zephyrGeneralData != null && zephyrGeneralData.Count > 0) {
                                                LineGraphModel lineModel = new LineGraphModel() {
                                                    GraphType = "Zephyr Heart Rate",
                                                    XAxisName = AxisNames.GENERIC_X_AXIS,
                                                    YAxisName = AxisNames.BEATS_PER_MINUTE,
                                                    XAxisData = zephyrGeneralData.Select(b => b.Date).ToList(),
                                                    YAxisData = zephyrGeneralData.Select(b => b.HeartRate).Select(d => (double)d).ToList()
                                                };
                                                graphViewModel.LineGraphModels.Add(lineModel);
                                            }
                                            break;
                                        case File_Type.BR_RR:
                                            List<ZephyrBRRR> zephyrBrRrData = _brRrService.GetZephyrBRRRData(dataRecord, start, end).ToList();
                                            if (zephyrBrRrData != null && zephyrBrRrData.Count > 0) {
                                                LineGraphModel lineModel = new LineGraphModel() {
                                                    GraphType = "Zephyr BR",
                                                    XAxisName = AxisNames.GENERIC_X_AXIS,
                                                    YAxisName = AxisNames.GENERIC_Y_AXIS,
                                                    XAxisData = zephyrBrRrData.Select(b => b.TimeStamp).ToList(),
                                                    YAxisData = zephyrBrRrData.Select(b => b.BR).Select(d => (double)d).ToList()
                                                };
                                                graphViewModel.LineGraphModels.Add(lineModel);
                                                lineModel = new LineGraphModel() {
                                                    GraphType = "Zephyr RR",
                                                    XAxisName = AxisNames.GENERIC_X_AXIS,
                                                    YAxisName = AxisNames.GENERIC_Y_AXIS,
                                                    XAxisData = zephyrBrRrData.Select(b => b.TimeStamp).ToList(),
                                                    YAxisData = zephyrBrRrData.Select(b => b.RR).Select(d => (double)d).ToList()
                                                };
                                                graphViewModel.LineGraphModels.Add(lineModel);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case "BasisPeak":
                                    List<BasisPeakSummaryData> basisSummaryData = _basisPeakService.GetBasisPeakSummaryData(dataRecord, start, end).ToList();
                                    if (basisSummaryData != null && basisSummaryData.Count > 0) {
                                        LineGraphModel lineModel = new LineGraphModel() {
                                            GraphType = "BasisPeak Heart Rate",
                                            XAxisName = AxisNames.GENERIC_X_AXIS,
                                            YAxisName = AxisNames.BEATS_PER_MINUTE,
                                            XAxisData = basisSummaryData.Select(b => b.Date).ToList(),
                                            YAxisData = basisSummaryData.Where(b => b.HeartRate != null).Select(b => b.HeartRate).Select(d => (double)d).ToList()
                                        };
                                        graphViewModel.LineGraphModels.Add(lineModel);
                                    }
                                    break;
                                case "Microsoft Band":
                                    switch ((File_Type)dataRecord.DataType) {
                                        case File_Type.HeartRate:
                                            List<MSBandHeartRate> msBandHeartRate = _msBandHeartRateService.GetMSBandHeartRateData(dataRecord, start, end).ToList();
                                            if (msBandHeartRate != null && msBandHeartRate.Count > 0) {
                                                LineGraphModel lineModel = new LineGraphModel() {
                                                    GraphType = "MS Band Heart Rate",
                                                    XAxisName = AxisNames.GENERIC_X_AXIS,
                                                    YAxisName = AxisNames.BEATS_PER_MINUTE,
                                                    XAxisData = msBandHeartRate.Select(b => b.Date).ToList(),
                                                    YAxisData = msBandHeartRate.Select(b => b.HeartRate).Select(d => (double)d).ToList()
                                                };
                                                graphViewModel.LineGraphModels.Add(lineModel);
                                            }
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
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