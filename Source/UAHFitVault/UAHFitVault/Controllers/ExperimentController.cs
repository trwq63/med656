using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.Helpers;
using UAHFitVault.Models;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.DataAccess;
using UAHFitVault.Database.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using UAHFitVault.Resources;
using UAHFitVault.LogicLayer.Models;
using UAHFitVault.DataAccess.ZephyrServices;
using UAHFitVault.DataAccess.BasisPeakServices;
using UAHFitVault.DataAccess.MicrosoftBandServices;
using UAHFitVault.LogicLayer.LogicFiles;

namespace UAHFitVault.Controllers
{
    [Authorization("ROLES_EXPERIMENTADMIN")]
    public class ExperimentController : Controller
    {
        #region Private Members
        /// <summary>
        /// Service interface for accessing patient data database functions.
        /// </summary>
        private readonly IExperimentAdminService _experimentAdminService;

        /// <summary>
        /// Service interface for accessing patient data database functions.
        /// </summary>
        private readonly IExperimentService _experimentService;

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
        #endregion

        #region Public Constructor
        public ExperimentController(IExperimentAdminService expAdminService, IExperimentService expService, IPatientService patientService,
                                    IZephyrAccelService zephyrAccelService, IZephyrBreathingService zephyrBreathingService, IZephyrECGService zephyrEcgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IZephyrBrRrService brRrService, IBasisPeakSummaryService basisPeakService,
                                    IMSBandHeartRateService msBandHeartRateService, IPatientDataService patientDataService)
        {
            _experimentAdminService = expAdminService;
            _experimentService = expService;
            _patientService = patientService;
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
        }
        #endregion

        #region Public Methods
        // GET: Experiment
        /// <summary>
        /// Display the view for the experiment administrator to view the experiments
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ViewAllExperimentsViewModel model = new ViewAllExperimentsViewModel();
            model.Experiments = new List<Experiment>();
            model.Experiments.AddRange(_experimentService.GetExperiments(manager.FindByName(User.Identity.Name).ExperimentAdministratorId));
            model.ExperimentCriteria = new List<ExperimentViewModel>();

            // Get all of the experiments parsed data
            for (int i = 0; i < model.Experiments.Count; i++)
            {
                ExperimentViewModel temp = JsonConvert.DeserializeObject<ExperimentViewModel>(model.Experiments.ElementAt(i).QueryString);
                model.ExperimentCriteria.Add(temp);
            }

            return View(model);
        }

        /// <summary>
        /// Displays the view for an experiment administrator to create an experiment
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateExperiment()
        {
            ExperimentViewModel model = new ExperimentViewModel();
            return View(model);
        }

        /// <summary>
        /// This handles the processing after the user hits the submit button on the create experiment view
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateExperiment(ExperimentViewModel model,
            string[] selectedGenders, string[] selectedRaces, string[] selectedEthnicities,
            string[] selectedLocations)
        {
            ExperimentViewModel serializedModel = new ExperimentViewModel();

            serializedModel.ageRangeStart = model.ageRangeStart;
            serializedModel.ageRangeEnd = model.ageRangeEnd;
            serializedModel.weightRangeBegin = model.weightRangeBegin;
            serializedModel.weightRangeEnd = model.weightRangeEnd;
            serializedModel.heightRangeBegin = model.heightRangeBegin;
            serializedModel.heightRangeEnd = model.heightRangeEnd;


            // Generate Gender string
            if (selectedGenders != null)
            {
                model.selectedGenders = selectedGenders;
                serializedModel.selectedGenders = selectedGenders;
            }
            else
            {
                // No genders were selected, so get all genders
                string[] allGenders = Enum.GetNames(typeof(PatientGender));
                serializedModel.selectedGenders = allGenders;
            }

            // Generate Races string
            if (selectedRaces != null)
            {
                model.selectedRaces = selectedRaces;
                serializedModel.selectedRaces = selectedRaces;
            }
            else
            {
                string[] allRaces = Enum.GetNames(typeof(PatientRace));
                serializedModel.selectedRaces = allRaces;
            }

            // Generate Ethnicity string
            if (selectedEthnicities != null)
            {
                model.selectedEthnicities = selectedEthnicities;
                serializedModel.selectedEthnicities = selectedEthnicities;
            }
            else
            {
                string[] allEthnicities = Enum.GetNames(typeof(PatientEthnicity));
                serializedModel.selectedEthnicities = allEthnicities;
            }

            // Generate Locations string
            if (selectedLocations != null)
            {
                model.selectedLocations = selectedLocations;
                serializedModel.selectedLocations = selectedLocations;
            }
            else
            {
                string[] allLocations = Enum.GetNames(typeof(Location));
                serializedModel.selectedLocations = allLocations;
            }

            // These need to be down here to ensure the model is repopulated if the user enters bad criteria.
            // Check to make sure ranges are correct.
            if (model.ageRangeStart > model.ageRangeEnd)
            {
                ModelState.AddModelError("", "ERROR: The starting age in the age range is less than the ending age.");
            }
            if (model.weightRangeBegin > model.weightRangeEnd)
            {
                ModelState.AddModelError("", "ERROR: The beginning of weight range is less than the end.");
            }
            if (model.heightRangeBegin > model.heightRangeEnd)
            {
                ModelState.AddModelError("", "ERROR: The beginning of height range is less than the end.");
            }
            // Check if experiment name already exists.
            if (ExperimentNameIsUsed(model.ExperimentName))
            {
                ModelState.AddModelError("", "ERROR: That experiment name is already in use.");
            }
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string queryString = JsonConvert.SerializeObject(serializedModel);

            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            Experiment experiment = new Experiment();
            experiment.QueryString = queryString;
            experiment.Name = model.ExperimentName;
            experiment.ExperimentAdministrator = _experimentAdminService.GetExperimentAdministrator(manager.FindByName(User.Identity.Name).ExperimentAdministratorId); // current user exp admin id
            experiment.LastModified = DateTime.Now;

            _experimentService.CreateExperiment(experiment);
            _experimentService.SaveChanges();

            return Redirect("/Experiment/CreateExperimentConfirmation");
        }


        /// <summary>
        /// Deletes an experiment from the database
        /// </summary>
        /// <param name="experimentName">Name of the experiment to delete</param>
        /// <returns></returns>
        public ActionResult DeleteExperiment(string experimentName)
        {
            DeleteExperimentViewModel model = new DeleteExperimentViewModel();
            model.ExperimentName = experimentName;

            return View(model);
        }

        /// <summary>
        /// Deletes an experiment from the database.
        /// </summary>
        /// <param name="experimentName">Name of the experiment to delete</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteExperiment(DeleteExperimentViewModel model)
        {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ExperimentAdministrator user = _experimentAdminService.GetExperimentAdministrator(
                manager.FindByName(User.Identity.Name).ExperimentAdministratorId);
            int userId = manager.FindByName(User.Identity.Name).ExperimentAdministratorId;
            Experiment experiment = _experimentService.GetExperimentByName(model.ExperimentName, userId);
            if (experiment.ExperimentAdministrator == user) // Confirm the user owns the experiment
            {
                // Get the experiment ID
                int experimentId = experiment.Id;
                _experimentService.DeleteExperiment(experimentId, userId);
                _experimentService.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "ERROR: You do not own that experiment.");
                return View(model);
            }
            return Redirect("/Experiment/DeleteExperimentConfirmation");
        }

        /// <summary>
        /// Displays a confirmation page for deleting the experiment.
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteExperimentConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Displays information about the experiment
        /// </summary>
        /// <param name="experimentName">Name of the experiment</param>
        /// <returns></returns>
        public ActionResult ViewExperiment(string experimentName)
        {
            ViewExperimentViewModel model = new ViewExperimentViewModel();
            model.criteriaModel = new ViewExperimentCriteriaViewModel();

            if (experimentName != null)
            {
                model.experimentName = experimentName;
            }

            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            int userId = manager.FindByName(User.Identity.Name).ExperimentAdministratorId;
            Experiment experiment = _experimentService.GetExperimentByName(experimentName, userId);
            model.patientList = GetPatientsForExperiment(experiment);   // Get the patients from the database

            ExperimentViewModel temp = new ExperimentViewModel();
            temp = JsonConvert.DeserializeObject<ExperimentViewModel>(experiment.QueryString);

            model.criteriaModel.experiment = temp;

            return View(model);
        }

        /// <summary>
        /// Displays the information about the patient
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <returns></returns>
        public ActionResult ViewPatient (int patientId)
        {
            ViewPatientViewModel model = new ViewPatientViewModel();
            model.patient = _patientService.GetPatient(patientId);
            model.patientData = model.patient.PatientData;
            model.ActivityTagFilter = "All";
            return View(model);
        }

        /// <summary>
        /// Displays the information about the patient.  This is used when the activity tag is selected.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewPatient (ViewPatientViewModel model)
        {
            model.patient = _patientService.GetPatient(model.patient.Id);
            model.patientData = model.patient.PatientData;
            string activityTag = model.ActivityTagFilter.Replace(" ", "_");
            // Get all data sessions for patient matching activityTag
            return View(model);
        }

        /// <summary>
        /// Confirmation of successful experiment creation
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateExperimentConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Edit an experiment
        /// </summary>
        /// <param name="experimentName">Name of the experiment to edit</param>
        /// <returns></returns>
        public ActionResult EditExperiment (string experimentName)
        {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            int userId = manager.FindByName(User.Identity.Name).ExperimentAdministratorId;
            Experiment experiment = _experimentService.GetExperimentByName(experimentName, userId);

            ExperimentViewModel temp = new ExperimentViewModel();
            temp = JsonConvert.DeserializeObject<ExperimentViewModel>(experiment.QueryString);
            temp.ExperimentName = experimentName;

            return View(temp);
        }

        /// <summary>
        /// Page for editing an experiment
        /// </summary>
        /// <param name="model">Model of the experiment</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditExperiment (ExperimentViewModel model, 
            string[] selectedGenders, string[] selectedRaces, string[] selectedEthnicities,
            string[] selectedLocations)
        {
            ExperimentViewModel serializedModel = new ExperimentViewModel();

            serializedModel.ageRangeStart = model.ageRangeStart;
            serializedModel.ageRangeEnd = model.ageRangeEnd;
            serializedModel.weightRangeBegin = model.weightRangeBegin;
            serializedModel.weightRangeEnd = model.weightRangeEnd;
            serializedModel.heightRangeBegin = model.heightRangeBegin;
            serializedModel.heightRangeEnd = model.heightRangeEnd;


            // Generate Gender string
            if (selectedGenders != null)
            {
                model.selectedGenders = selectedGenders;
                serializedModel.selectedGenders = selectedGenders;
            }
            else
            {
                // No genders were selected, so get all genders
                string[] allGenders = Enum.GetNames(typeof(PatientGender));
                serializedModel.selectedGenders = allGenders;
            }

            // Generate Races string
            if (selectedRaces != null)
            {
                model.selectedRaces = selectedRaces;
                serializedModel.selectedRaces = selectedRaces;
            }
            else
            {
                string[] allRaces = Enum.GetNames(typeof(PatientRace));
                serializedModel.selectedRaces = allRaces;
            }

            // Generate Ethnicity string
            if (selectedEthnicities != null)
            {
                model.selectedEthnicities = selectedEthnicities;
                serializedModel.selectedEthnicities = selectedEthnicities;
            }
            else
            {
                string[] allEthnicities = Enum.GetNames(typeof(PatientEthnicity));
                serializedModel.selectedEthnicities = allEthnicities;
            }

            // Generate Locations string
            if (selectedLocations != null)
            {
                model.selectedLocations = selectedLocations;
                serializedModel.selectedLocations = selectedLocations;
            }
            else
            {
                string[] allLocations = Enum.GetNames(typeof(Location));
                serializedModel.selectedLocations = allLocations;
            }

            // These need to be down here to ensure the model is repopulated if the user enters bad criteria.
            // Check to make sure ranges are correct.
            if (model.ageRangeStart > model.ageRangeEnd)
            {
                ModelState.AddModelError("", "ERROR: The starting age in the age range is less than the ending age.");
            }
            if (model.weightRangeBegin > model.weightRangeEnd)
            {
                ModelState.AddModelError("", "ERROR: The beginning of weight range is less than the end.");
            }
            if (model.heightRangeBegin > model.heightRangeEnd)
            {
                ModelState.AddModelError("", "ERROR: The beginning of height range is less than the end.");
            }
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string queryString = JsonConvert.SerializeObject(serializedModel);

            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            Experiment experiment = _experimentService.GetExperimentByName(model.ExperimentName, manager.FindByName(User.Identity.Name).ExperimentAdministratorId);
            experiment.QueryString = queryString;   // Update experiment
            experiment.Name = model.ExperimentName;
            experiment.ExperimentAdministrator = _experimentAdminService.GetExperimentAdministrator(manager.FindByName(User.Identity.Name).ExperimentAdministratorId); // current user exp admin id
            experiment.LastModified = DateTime.Now; // Update date modified

            _experimentService.SaveChanges();       // Save changes

            return Redirect("/Experiment/EditExperimentConfirmation");
        }

        /// <summary>
        /// Display confirmation view that an experiment was edited
        /// </summary>
        /// <returns></returns>
        public ActionResult EditExperimentConfirmation ()
        {
            return View();
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
        public string GraphData(string activityType, string patientData) {

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

        public string ExportExperiment() {
            return null;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the patients matched with an experiment
        /// </summary>
        /// <param name="experiment">Experiment to match the patients with</param>
        /// <returns></returns>
        private List<Patient> GetPatientsForExperiment(Experiment experiment)
        {
            List<Patient> patientList = new List<Patient>();

            ExperimentViewModel model = JsonConvert.DeserializeObject<ExperimentViewModel>(experiment.QueryString);
            ExperimentCriteria criteria = CopyModelToCriteria(model);

            IEnumerable<Patient> patients = _experimentService.GetPatientsForExperiment(criteria);
            if (patients != null)
            {
                patientList.AddRange(patients);
            }

            return patientList;
        }

        /// <summary>
        /// Copy the members of the model to an ExperimentCriteria object
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns></returns>
        private ExperimentCriteria CopyModelToCriteria(ExperimentViewModel model)
        {
            ExperimentCriteria criteria = new ExperimentCriteria();

            criteria.ageRangeEnd = model.ageRangeEnd;
            criteria.ageRangeStart = model.ageRangeStart;
            criteria.heightRangeEnd = model.heightRangeEnd;
            criteria.heightRangeBegin = model.heightRangeBegin;
            criteria.weightRangeBegin = model.weightRangeBegin;
            criteria.weightRangeEnd = model.weightRangeEnd;
            criteria.selectedGenders = model.selectedGenders;
            criteria.selectedRaces = model.selectedRaces;
            criteria.selectedEthnicities = model.selectedEthnicities;
            criteria.selectedLocations = model.selectedLocations;

            return criteria;
        }

        /// <summary>
        /// Checks to see if the experiment name is already used
        /// </summary>
        /// <param name="experimentName">Experiment name</param>
        /// <returns>True if the experiment name exists, false otherwise</returns>
        private bool ExperimentNameIsUsed(string experimentName)
        {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            Experiment temp = _experimentService.GetExperimentByName(experimentName,
                        manager.FindByName(User.Identity.Name).ExperimentAdministratorId);
            if (temp != null)
            {
                return true; // Experiment name is used
            }
            return false;
        }
        
        #endregion
    }
}