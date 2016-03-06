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


namespace UAHFitVault.Controllers
{
    [Authorization("ROLES_EXPERIMENTADMIN")]
    public class ExperimentController : Controller
    {
        #region Private Members
        private readonly IExperimentAdminService _experimentAdminService;
        private readonly IExperimentService _experimentService;
        #endregion

        #region Public Constructor
        public ExperimentController (IExperimentAdminService expAdminService, IExperimentService expService)
        {
            _experimentAdminService = expAdminService;
            _experimentService = expService;
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

            List<Experiment> AllExperiments = new List<Experiment>();
            AllExperiments.AddRange(_experimentService.GetExperiments(manager.FindByName(User.Identity.Name).ExperimentAdministratorId));



            ViewExperimentsViewModel model = new ViewExperimentsViewModel();
            model.Experiments = AllExperiments;
            // Get all of the experiments parsed data
            for (int i = 0; i < AllExperiments.Count; i++)
            {
                model.VisualExperimentCriteria.Add(ParseQueryString(AllExperiments.ElementAt(i).QueryString));
            }



            return View(AllExperiments);
        }
        
        /// <summary>
        /// Displays the view for an experiment administrator to create an experiment
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateExperiment ()
        {
            CreateExperimentViewModel model = new CreateExperimentViewModel();
            return View(model);
        }

        /// <summary>
        /// This handles the processing after the user hits the submit button on the create experiment view
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateExperiment (CreateExperimentViewModel model,
            string[] selectedGenders, string[] selectedRaces, string[] selectedEthnicities,
            string[] selectedLocations, string[] selectedActivities)
        {
            string genderString = "genders:";
            string raceString = "races:";
            string ethnicityString = "ethnicities:";
            string locationString = "locations:";
            string activityString = "activities:";

            // Generate Gender string
            if (selectedGenders != null)
            {
                // No genders were selected, so get all genders
                model.selectedGenders = selectedGenders;
                genderString += GenerateStringFromStringArray(selectedGenders);
            }
            else
            {
                string[] allGenders = Enum.GetNames(typeof(PatientGender));
                genderString += GenerateStringFromStringArray(allGenders);
            }

            // Generate Races string
            if (selectedRaces != null)
            {
                model.selectedRaces = selectedRaces;
                raceString += GenerateStringFromStringArray(selectedRaces);
            }
            else
            {
                string[] allRaces = Enum.GetNames(typeof(PatientRace));
                raceString += GenerateStringFromStringArray(allRaces);
            }

            // Generate ethnicity string
            if (selectedEthnicities != null)
            {
                model.selectedEthnicities = selectedEthnicities;
                ethnicityString += GenerateStringFromStringArray(selectedEthnicities);
            }
            else
            {
                string[] allEthnicities = Enum.GetNames(typeof(PatientEthnicity));
                ethnicityString += GenerateStringFromStringArray(allEthnicities);
            }

            // Generate locations string
            if (selectedLocations != null)
            {
                model.selectedLocations = selectedLocations;
                locationString += GenerateStringFromStringArray(selectedLocations);
            }
            else
            {
                string[] allLocations = Enum.GetNames(typeof(Location));
                locationString += GenerateStringFromStringArray(allLocations);
            }

            // Generate activities string
            if (selectedActivities != null)
            {
                model.selectedActivities = selectedActivities;
                activityString += GenerateStringFromStringArray(selectedActivities);
            }
            else
            {
                string[] allActivities = Enum.GetNames(typeof(ActivityType));
                activityString += GenerateStringFromStringArray(allActivities);
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

            string queryString = genderString + raceString + ethnicityString + locationString + activityString;
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
        /// Confirmation of successful experiment creation
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateExperimentConfirmation()
        {
            return View();
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Reads in a query string input and parses it in to a two-dimensional array.
        /// </summary>
        /// <param name="queryString">Input query string</param>
        /// <returns></returns>
        private List<List<string>> ParseQueryString (string queryString)
        {
            // For first index, 0=genders, 1=races, 2=ethnicity, 3=location, 4=activites
            List<List<string>> returnList = new List<List<string>>();

            List<string> races = new List<string>();
            List<string> genders = new List<string>();
            List<string> ethnicities = new List<string>();
            List<string> locations = new List<string>();
            List<string> activities = new List<string>();

            queryString = queryString.Replace("races:", "").Replace("genders:", "")
                .Replace("locations:", "").Replace("activities:", "").Replace("ethnicities:", "");

            string[] splitQueryString = queryString.Split(';');
            
            for (int i = 0; i < 5; i++)
            {
                string[] splitInput = splitQueryString[i].Split(','); // Split up all of the commas
                switch (i)
                {
                    case 0:
                        // Genders
                        for (int j = 0; j < splitInput.Length; j++)
                        {
                            genders.Add(splitInput[j]);
                        }
                        break;
                    case 1:
                        // Races
                        for (int j = 0; j < splitInput.Length; j++)
                        {
                            races.Add(splitInput[j]);
                        }
                        break;
                    case 2:
                        // Ethnicities
                        for (int j = 0; j < splitInput.Length; j++)
                        {
                            ethnicities.Add(splitInput[j]);
                        }
                        break;
                    case 3:
                        // Locations
                        for (int j = 0; j < splitInput.Length; j++)
                        {
                            locations.Add(splitInput[j]);
                        }
                        break;
                    case 4:
                        // Activities
                        for (int j = 0; j < splitInput.Length; j++)
                        {
                            activities.Add(splitInput[j]);
                        }
                        break;
                }
            }

            returnList.Add(races);
            returnList.Add(genders);
            returnList.Add(ethnicities);
            returnList.Add(locations);
            returnList.Add(activities);
            
            return returnList;
        }

        /// <summary>
        /// This function produces a single string from an input array of strings
        /// </summary>
        /// <param name="input">Array of strings to be processed.</param>
        /// <returns></returns>
        private string GenerateStringFromStringArray (string[] input)
        {
            string returnString = "";
            
            for (int i = 0; i < input.Length; i++)
            {
                returnString += input[i];
                if (i < (input.Length-1))
                {
                    returnString += ",";
                }
            }
            returnString += ";";
            return returnString;
        }

        /// <summary>
        /// Checks to see if the experiment name is already used
        /// </summary>
        /// <param name="experimentName">Experiment name</param>
        /// <returns>True if the experiment name exists, false otherwise</returns>
        private bool ExperimentNameIsUsed (string experimentName)
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