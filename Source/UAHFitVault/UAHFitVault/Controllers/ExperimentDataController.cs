using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Mvc;
using UAHFitVault.DataAccess;
using UAHFitVault.DataAccess.BasisPeakServices;
using UAHFitVault.DataAccess.MicrosoftBandServices;
using UAHFitVault.DataAccess.ZephyrServices;
using UAHFitVault.Database.Entities;
using UAHFitVault.Helpers;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.LogicLayer.LogicFiles;
using UAHFitVault.LogicLayer.Models;
using UAHFitVault.LogicLayer.Resources;
using UAHFitVault.Models;
using UAHFitVault.Resources;

namespace UAHFitVault.Controllers
{
    /// <summary>
    /// Controller used for viewing and exporting experiment data.
    /// </summary>
    [Authorization("ROLES_EXPERIMENTDATA")]
    public class ExperimentDataController : Controller
    {
        #region Private Members

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
        /// Service object for accessing Microsoft Band Acclerometer database functions.
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

        #endregion

        #region Public Properties

        /// <summary>
        /// The path to the export directory.
        /// </summary>
        public string ExportPath {
            get {
                return Session["eExportPath"].ToString();
            }
            set {
                Session["eExportPath"] = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="patientDataService">Service object for accessing patient data database functions.</param>
        /// <param name="zephyrAccelService">Service object for accessing Zephyr Accelerometer database functions.</param>
        /// <param name="breathingService">Service object for accessing Zephyr Breathing Waveform database functions.</param>
        /// <param name="ecgService">Service object for accessing Zephyr ECG Waveform database functions.</param>
        /// <param name="eventDataService">Service object for accessing Zephyr Event Data database functions.</param>
        /// <param name="summaryService">Service object for accessing Zephyr Summary database functions.</param>
        /// <param name="brRrService">Service object for accessing Zephyr BR RR database functions.</param>
        /// <param name="patientService">Service object for accessing patient database functions.</param>
        /// <param name="basisPeakService">Service object for accessing basis peak summary database functions.</param>
        /// <param name="msBandAccelService">Service object for accessing Microsoft Band Accelerometer database functions.</param>
        /// <param name="msBandCaloriesService">Service object for accessing Microsoft Band Calories database functions.</param>
        /// <param name="msBandDistanceService">Service object for accessing Microsoft Band Distance database functions.</param>
        /// <param name="msBandGyroscopeService">Service object for accessing Microsoft Band Gyroscope database functions.</param>
        /// <param name="msBandHeartRateService">Service object for accessing Microsoft Band Heart Rate database functions.</param>
        /// <param name="msBandPedometerService">Service object for accessing Microsoft Band Pedometer database functions.</param>
        /// <param name="msBandTemperatureService">Service object for accessing Microsoft Band Temperature database functions.</param>
        /// <param name="msBandUVService">Service object for accessing Microsoft Band UV database functions.</param>
        /// <param name="physicianService">Service object for accessing physician database functions.</param>
        public ExperimentDataController(IPatientDataService patientDataService, IZephyrAccelService zephyrAccelService,
                                    IZephyrBreathingService breathingService, IZephyrECGService ecgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IZephyrBrRrService brRrService, IMSBandUVService msBandUVService,
                                    IBasisPeakSummaryService basisPeakService, IMSBandAccelService msBandAccelService,
                                    IMSBandCaloriesService msBandCaloriesService, IMSBandDistanceService msBandDistanceService,
                                    IMSBandGyroscopeService msBandGyroscopeService, IMSBandHeartRateService msBandHeartRateService,
                                    IMSBandPedometerService msBandPedometerService, IMSBandTemperatureService msBandTemperatureService) {

            _patientDataService = patientDataService;
            _zephyrAccelService = zephyrAccelService;
            _zephyrBreathingService = breathingService;
            _zephyrEcgService = ecgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
            _brRrService = brRrService;
            _basisPeakService = basisPeakService;
            _msBandAccelService = msBandAccelService;
            _msBandCaloriesService = msBandCaloriesService;
            _msBandDistanceService = msBandDistanceService;
            _msBandGyroscopeService = msBandGyroscopeService;
            _msBandHeartRateService = msBandHeartRateService;
            _msBandPedometerService = msBandPedometerService;
            _msBandTemperatureService = msBandTemperatureService;
            _msBandUVService = msBandUVService;

        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Create the graph to be displayed to the user.
        /// </summary>
        /// <param name="activityType">Type of activity associated with the data being graphed.</param>
        /// <param name="patientData">List of patient data records to be displayed.</param>
        /// <returns></returns>
        public string GraphExperiment(string activityType, string patientData) {

            PatientDataViewModel graphViewModel = new PatientDataViewModel();

            if (!string.IsNullOrEmpty(activityType) && !string.IsNullOrEmpty(patientData)) {

                PatientData dataRecord = _patientDataService.GetPatientData(patientData);
                List<Activity> activityRecords = new List<Activity>();
                if (activityType != "All") {
                    activityRecords = dataRecord.Activities.Where(a => a.DataActivity == (int)Enum.Parse(typeof(ActivityType), activityType)).ToList();
                }
                else {
                    activityRecords = dataRecord.Activities.ToList();
                }
                if (activityRecords != null && activityRecords.Count > 0) {

                    foreach (Activity record in activityRecords) {
                        DateTime start = record.StartTime;
                        DateTime end = record.EndTime;

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

                }
            }
            string jsonResult;

            if (graphViewModel.LineGraphModels.Count > 0) {
                jsonResult = JsonConvert.SerializeObject(graphViewModel);
            }
            else {
                Dictionary<string, string> errorMsg = new Dictionary<string, string>();
                errorMsg.Add("error", "No Data Found");
                jsonResult = JsonConvert.SerializeObject(errorMsg);
            }

            return jsonResult;
        }

        /// <summary>
        /// Export experiment data.
        /// </summary>
        /// <param name="patientDataId">Id of the data record that contains the experiment data.</param>
        /// <param name="activityType">type of activity associated with experiment.</param>
        /// <returns></returns>
        public string ExportExperiment(string patientDataId, string activityType) {

            string path = @"c:\exports\" + Guid.NewGuid().ToString();
            ExportPath = path;
            //Create download directory
            Directory.CreateDirectory(path);

            PatientData patientData = _patientDataService.GetPatientData(patientDataId);

            List<Activity> activityRecords = new List<Activity>();
            if (activityType != "All") {
                activityRecords = patientData.Activities.Where(a => a.DataActivity == (int)Enum.Parse(typeof(ActivityType), activityType)).ToList();
            }
            else {
                if (patientData.Activities != null && patientData.Activities.Count > 0) {
                    CsvExport activityExport = new CsvExport();
                    foreach (Activity activity in patientData.Activities) {
                        activityExport.AddRow();
                        activityExport["Start Time"] = activity.StartTime;
                        activityExport["End Time"] = activity.EndTime;
                        activityExport["Activity Type"] = (ActivityType)activity.DataActivity;
                    }
                    activityExport.ExportToFile(@path + "\\Activities_" + patientData.Name);
                }
                activityRecords = patientData.Activities.ToList();

                //Create a fake activity if no activities exist for the All selection. This will allow the user to download the
                //entire data record file.
                if (activityRecords.Count == 0) {
                    Activity activity = new Activity() {
                        StartTime = new DateTime(),
                        EndTime = DateTime.MaxValue.AddDays(-1)
                    };
                    activityRecords.Add(activity);
                }
            }

            List<string> columnNames = new List<string>();
            CsvExport export = new CsvExport();
            int count = 0;
            int index = 1;
            string fileIndex = string.Empty;
            foreach (Activity activity in activityRecords) {
                DateTime start = activity.StartTime;
                DateTime end = activity.EndTime;
                string filename = "Experiment_" + Session["eExperimentName"].ToString() + "_" + DateTime.Now.ToString("mm-dd-yyyy") +".csv";
                //string filename = "Experiment_Results_Run_" + DateTime.Now.ToString("mm-dd-yyyy") + Guid.NewGuid().ToString() + ".csv";
                try {
                    switch (patientData.DataType) {
                        case (int)File_Type.Accelerometer:
                            Device_Type deviceType = PatientLogic.DetermineDeviceType(patientData.Name);
                            switch (deviceType) {
                                case Device_Type.Zephyr:
                                    IEnumerable<ZephyrAccelerometer> zephyrAccelData = null;
                                    index = 1;
                                    do {
                                        zephyrAccelData = _zephyrAccelService.GetZephyrAccelerometerData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                        count = 0;
                                        foreach (ZephyrAccelerometer data in zephyrAccelData) {
                                            export.AddRow();
                                            export["Time"] = data.Time;
                                            export["Vertical"] = data.Vertical;
                                            export["Lateral"] = data.Lateral;
                                            export["Sagittal"] = data.Sagittal;
                                            count++;
                                            if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
                                                index++;
                                                fileIndex = index.ToString() + "_";
                                                export = new CsvExport();
                                            }
                                        }
                                    } while (zephyrAccelData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                                    break;
                                case Device_Type.Microsoft_Band:
                                    IEnumerable<MSBandAccelerometer> msAccelData = null;
                                    index = 1;
                                    do {
                                        msAccelData = _msBandAccelService.GetMSBandAccelerometerData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                        count = 0;
                                        foreach (MSBandAccelerometer data in msAccelData) {
                                            export.AddRow();
                                            export["Time Stamp"] = data.Date;
                                            export["X(m/s²)"] = data.Lateral;
                                            export["Y(m/s²)"] = data.Vertical;
                                            export["Z(m/s²)"] = data.Sagittal;
                                            count++;
                                            if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                                export.ExportToFile(@path + "\\" + index + "_" + filename);
                                                index++;
                                                fileIndex = index.ToString() + "_";
                                                export = new CsvExport();
                                            }
                                        }
                                    } while (msAccelData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case (int)File_Type.Breathing:
                            IEnumerable<ZephyrBreathingWaveform> zephyrBreathingData = null;
                            index = 1;
                            do {
                                zephyrBreathingData = _zephyrBreathingService.GetZephyrBreathingWaveformData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (ZephyrBreathingWaveform data in zephyrBreathingData) {
                                    export.AddRow();
                                    export["Time"] = data.Time;
                                    export["BreathingWaveform"] = data.Data;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (zephyrBreathingData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.Calorie:
                            IEnumerable<MSBandCalories> calorieData = null;
                            index = 1;
                            do {
                                calorieData = _msBandCaloriesService.GetMSBandCaloriesData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (MSBandCalories data in calorieData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.Date;
                                    export["Total Calories(kCal)"] = data.Total;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (calorieData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.Distance:
                            IEnumerable<MSBandDistance> distanceData = null;
                            index = 1;
                            do {
                                _msBandDistanceService.GetMSBandDistanceData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (MSBandDistance data in distanceData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.Date;
                                    export["Motion Type"] = data.MotionType;
                                    export["Pace(min/km)"] = data.Pace;
                                    export["Speed(km/hr)"] = data.Speed;
                                    export["Total(km)"] = data.Total;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (distanceData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.ECG:
                            IEnumerable<ZephyrECGWaveform> ecgData = null;
                            index = 1;
                            do {
                                ecgData = _zephyrEcgService.GetZephyrECGWaveFormData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (ZephyrECGWaveform data in ecgData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.Time;
                                    export["Motion Type"] = data.Data;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (ecgData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.EventData:
                            IEnumerable<ZephyrEventData> eventData = null;
                            index = 1;
                            do {
                                eventData = _eventDataService.GetZephyrEventData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (ZephyrEventData data in eventData) {
                                    export.AddRow();
                                    export["SeqNo"] = "0";
                                    export["Time Stamp"] = data.Date;
                                    export["EventCode"] = data.EventCode;
                                    export["Type"] = data.Type;
                                    export["Source"] = data.Source;
                                    export["EventID"] = data.EventId;
                                    export["EventSpecificData"] = data.EventSpecificData;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (eventData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.Gyroscope:
                            IEnumerable<MSBandGyroscope> gyroscopeData = null;
                            index = 1;
                            do {
                                gyroscopeData = _msBandGyroscopeService.GetMSBandGyroscopeData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (MSBandGyroscope data in gyroscopeData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.Date;
                                    export["X-Axis(Â°/s)"] = data.X;
                                    export["Y-Axis(Â°/s)"] = data.Y;
                                    export["Z-Axis(Â°/s)"] = data.Z;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (gyroscopeData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.HeartRate:
                            IEnumerable<MSBandHeartRate> heartRateData = null;
                            index = 1;
                            do {
                                heartRateData = _msBandHeartRateService.GetMSBandHeartRateData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (MSBandHeartRate data in heartRateData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.Date;
                                    export["Read Status"] = data.ReadStatus;
                                    export["Heart Rate(bpm)"] = data.HeartRate;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (heartRateData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.Pedometer:
                            IEnumerable<MSBandPedometer> pedometerData = null;
                            index = 1;
                            do {
                                pedometerData = _msBandPedometerService.GetMSBandPedometerData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (MSBandPedometer data in pedometerData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.Date;
                                    export["Steps"] = data.Steps;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (pedometerData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.Summary:
                            Device_Type summaryDevice = PatientLogic.DetermineDeviceType(patientData.Name);
                            switch (summaryDevice) {
                                case Device_Type.Zephyr:
                                    IEnumerable<ZephyrSummaryData> zephyrSummaryData = null;
                                    index = 1;
                                    do {
                                        zephyrSummaryData = _summaryService.GetZephyrSummaryData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                        count = 0;
                                        foreach (ZephyrSummaryData data in zephyrSummaryData) {
                                            export.AddRow();
                                            export["Time"] = data.Date;
                                            export["HR"] = data.HeartRate;
                                            export["BR"] = data.BreathingRate;
                                            export["SkinTemp"] = data.SkinTemp;
                                            export["Posture"] = data.Posture;
                                            export["Activity"] = data.Activity;
                                            export["PeakAccel"] = data.PeakAccel;
                                            export["BatteryVolts"] = data.BatteryVolts;
                                            export["BatteryLevel"] = data.BatteryLevel;
                                            export["BRAmplitude"] = data.BRAmplitude;
                                            export["BRNoise"] = data.BRNoise;
                                            export["BRConfidence"] = data.BRConfidence;
                                            export["ECGAmplitude"] = data.ECGAmplitude;
                                            export["ECGNoise"] = data.ECGNoise;
                                            export["HRConfidence"] = data.HRConfidence;
                                            export["HRV"] = data.HRV;
                                            export["SystemConfidence"] = data.SystemConfidence;
                                            export["GSR"] = data.GSR;
                                            export["ROGState"] = data.ROGState;
                                            export["ROGTime"] = data.ROGTime;
                                            export["VerticalMin"] = data.VerticalMin;
                                            export["VerticalPeak"] = data.VerticalPeak;
                                            export["LateralMin"] = data.LateralMin;
                                            export["LateralPeak"] = data.LateralPeak;
                                            export["SagittalMin"] = data.SagittalMin;
                                            export["SagittalPeak"] = data.SagittalPeak;
                                            export["DeviceTemp"] = data.DeviceTemp;
                                            export["StatusInfo"] = data.StatusInfo;
                                            export["LinkQuality"] = data.LinkQuality;
                                            export["RSSI"] = data.RSSI;
                                            export["TxPower"] = data.TxPower;
                                            export["CoreTemp"] = data.CoreTemp;
                                            export["AuxADC1"] = data.AuxADC1;
                                            export["AuxADC2"] = data.AuxADC2;
                                            export["AuxADC3"] = data.AuxADC3;
                                            count++;
                                            if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                                export.ExportToFile(@path + "\\" + index + "_" + filename);
                                                index++;
                                                fileIndex = index.ToString() + "_";
                                                export = new CsvExport();
                                            }
                                        }
                                    } while (zephyrSummaryData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                                    break;
                                case Device_Type.BasisPeak:
                                    IEnumerable<BasisPeakSummaryData> basisData = null;
                                    index = 1;
                                    do {
                                        basisData = _basisPeakService.GetBasisPeakSummaryData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                        count = 0;
                                        foreach (BasisPeakSummaryData data in basisData) {
                                            export.AddRow();
                                            export["date"] = data.Date;
                                            export["calories"] = data.Calories;
                                            export["gsr"] = data.GSR;
                                            export["heart-rate"] = data.HeartRate;
                                            export["skin-temp"] = data.SkinTemp;
                                            export["steps"] = data.Steps;
                                            count++;
                                            if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                                export.ExportToFile(@path + "\\" + index + "_" + filename);
                                                index++;
                                                fileIndex = index.ToString() + "_";
                                                export = new CsvExport();
                                            }
                                        }
                                    } while (basisData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case (int)File_Type.General:
                            IEnumerable<ZephyrSummaryData> zephyrGeneralData = null;
                            index = 1;
                            do {
                                zephyrGeneralData = _summaryService.GetZephyrSummaryData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (ZephyrSummaryData data in zephyrGeneralData) {
                                    export.AddRow();
                                    export["Timestamp"] = data.Date;
                                    export["HR"] = data.HeartRate;
                                    export["BR"] = data.BreathingRate;
                                    export["Temp"] = data.SkinTemp;
                                    export["Posture"] = data.Posture;
                                    export["Activity"] = data.Activity;
                                    export["Acceleration"] = data.PeakAccel;
                                    export["Battery"] = data.BatteryVolts;
                                    export["BRAmplitude"] = data.BRAmplitude;
                                    export["ECGAmplitude"] = data.ECGAmplitude;
                                    export["ECGNoise"] = data.ECGNoise;
                                    export["XMin"] = data.LateralMin;
                                    export["XPeak"] = data.LateralPeak;
                                    export["YMin"] = data.VerticalMin;
                                    export["YPeak"] = data.VerticalPeak;
                                    export["ZMin"] = data.SagittalMin;
                                    export["ZPeak"] = data.SagittalPeak;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (zephyrGeneralData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.Temperature:
                            IEnumerable<MSBandTemperature> temperatureData = null;
                            index = 1;
                            do {
                                temperatureData = _msBandTemperatureService.GetMSBandTemperatureData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (MSBandTemperature data in temperatureData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.Date;
                                    export["Temperature(Â°C)"] = data.Temperature;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (temperatureData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.UV:
                            IEnumerable<MSBandUV> uvData = null;
                            index = 1;
                            do {
                                uvData = _msBandUVService.GetMSBandUVData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (MSBandUV data in uvData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.Date;
                                    export["UV Index (0-4)"] = data.UVIndex;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (uvData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        case (int)File_Type.BR_RR:
                            IEnumerable<ZephyrBRRR> brRrData = null;
                            index = 1;
                            do {
                                brRrData = _brRrService.GetZephyrBRRRData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                                count = 0;
                                foreach (ZephyrBRRR data in brRrData) {
                                    export.AddRow();
                                    export["Time Stamp"] = data.TimeStamp;
                                    export["BR"] = data.BR;
                                    export["RtoR"] = data.RR;
                                    count++;
                                    if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                        export.ExportToFile(@path + "\\" + index + "_" + filename);
                                        index++;
                                        fileIndex = index.ToString() + "_";
                                        export = new CsvExport();
                                    }
                                }
                            } while (brRrData != null && count == SystemConstants.MAX_ITEMS_RETURNED);
                            break;
                        default:
                            break;
                    }
                }
                catch (OutOfMemoryException ex) {
                    //Don't need to do anything here just catch the exception and continue.  Meant to prevent
                    //error screen but still allow some data to export.  This would need to be troubleshooted
                    //by a future team.
                }

                export.ExportToFile(@path + "\\" + fileIndex + filename);
            }

            //Zip files if there are any
            if(Directory.GetFiles(path).Length > 0) {
                string zipFileName = "ExperimentResults-" + Guid.NewGuid().ToString() + ".zip";

                ZipArchive zip = ZipFile.Open(path + "\\" + zipFileName, ZipArchiveMode.Create);
                
                foreach (var file in Directory.EnumerateFiles(path)) {
                    if (!file.Contains(".zip")) {
                        zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                    }
                }
                zip.Dispose();

                return zipFileName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Prompt the user to download the file.
        /// </summary>
        /// <param name="filename">Name of the file to download.</param>
        /// <returns></returns>
        public ActionResult DownloadFile(string filename) {
            string fullpath = ExportPath + "\\" + filename;
            return File(fullpath, "application/zip", filename);
        }
        #endregion
    }
}