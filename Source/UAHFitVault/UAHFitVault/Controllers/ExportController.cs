using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.DataAccess;
using UAHFitVault.DataAccess.BasisPeakServices;
using UAHFitVault.DataAccess.MicrosoftBandServices;
using UAHFitVault.DataAccess.ZephyrServices;
using UAHFitVault.Database;
using UAHFitVault.Database.Entities;
using UAHFitVault.Helpers;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.LogicLayer.LogicFiles;
using UAHFitVault.LogicLayer.Resources;
using UAHFitVault.Models;
using System.Linq;

namespace UAHFitVault.Controllers
{
    /// <summary>
    /// Controller needed for physican and patient data exporting.
    /// </summary>
    [Authorization("ROLES_PATIENTDATA")]
    public class ExportController : Controller
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
        /// Service object for accessing Zephyr BR RR database functions.
        /// </summary>
        private readonly IZephyrBrRrService _brRrService;

        /// <summary>
        /// Service object for accessing patient database functions.
        /// </summary>
        private readonly IPatientService _patientService;

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
        public ExportController(IPatientDataService patientDataService, IZephyrAccelService zephyrAccelService,
                                    IZephyrBreathingService breathingService, IZephyrECGService ecgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IZephyrBrRrService brRrService, IPatientService patientService, 
                                    IBasisPeakSummaryService basisPeakService, IMSBandAccelService msBandAccelService,
                                    IMSBandCaloriesService msBandCaloriesService, IMSBandDistanceService msBandDistanceService,
                                    IMSBandGyroscopeService msBandGyroscopeService, IMSBandHeartRateService msBandHeartRateService,
                                    IMSBandPedometerService msBandPedometerService, IMSBandTemperatureService msBandTemperatureService,
                                    IMSBandUVService msBandUVService, IPhysicianService physicianService) {

            _patientDataService = patientDataService;
            _zephyrAccelService = zephyrAccelService;
            _breathingService = breathingService;
            _ecgService = ecgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
            _brRrService = brRrService;
            _patientService = patientService;
            _basisPeakService = basisPeakService;
            _msBandAccelService = msBandAccelService;
            _msBandCaloriesService = msBandCaloriesService;
            _msBandDistanceService = msBandDistanceService;
            _msBandGyroscopeService = msBandGyroscopeService;
            _msBandHeartRateService = msBandHeartRateService;
            _msBandPedometerService = msBandPedometerService;
            _msBandTemperatureService = msBandTemperatureService;
            _msBandUVService = msBandUVService;
            _physicianService = physicianService;

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Find all the patient or patients that have data records available to export to display on the view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            List<ExportViewModel> viewModel = new List<ExportViewModel>();

            if (User.IsInRole(UserRole.Patient.ToString())) {
                Patient patient = _patientService.GetPatient(UserManager.FindById(User.Identity.GetUserId()).PatientId);

                if (patient.PatientData.Count > 0) {
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
                    if (patient.PatientData.Count > 0) {
                        ApplicationUser user = UserManager.Users.FirstOrDefault(u => u.PatientId == patient.Id);
                        ExportViewModel model = new ExportViewModel() {
                            PatientId = patient.Id,
                            Username = user.UserName,
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
            int count = 0;
            int index = 1;
            string fileIndex = string.Empty;
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
                                        export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                        zephyrBreathingData = _breathingService.GetZephyrBreathingWaveformData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                        count = 0;
                        foreach (ZephyrBreathingWaveform data in zephyrBreathingData) {
                            export.AddRow();
                            export["Time"] = data.Time;
                            export["BreathingWaveform"] = data.Data;
                            count++;
                            if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                        ecgData = _ecgService.GetZephyrECGWaveFormData(patientData, ((index - 1) * count), SystemConstants.MAX_ITEMS_RETURNED);
                        count = 0;
                        foreach (ZephyrECGWaveform data in ecgData) {
                            export.AddRow();
                            export["Time Stamp"] = data.Time;
                            export["Motion Type"] = data.Data;
                            count++;
                            if (count == SystemConstants.MAX_ITEMS_RETURNED) {
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                        export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                        export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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
                                export.ExportToFile(@path + "\\" + index + "_" + patientData.Name);
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

            export.ExportToFile(@path + "\\" + fileIndex + patientData.Name);

            return null;
        }

        #endregion
    }
}