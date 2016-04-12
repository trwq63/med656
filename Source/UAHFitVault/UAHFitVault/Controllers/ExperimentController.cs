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


namespace UAHFitVault.Controllers
{
    [Authorization("ROLES_EXPERIMENTADMIN")]
    public class ExperimentController : Controller
    {
        #region Private Members
        private readonly IExperimentAdminService _experimentAdminService;
        private readonly IExperimentService _experimentService;
        private readonly IPatientService _patientService;
        #endregion

        #region Public Constructor
        public ExperimentController(IExperimentAdminService expAdminService, IExperimentService expService, IPatientService patientService)
        {
            _experimentAdminService = expAdminService;
            _experimentService = expService;
            _patientService = patientService;
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