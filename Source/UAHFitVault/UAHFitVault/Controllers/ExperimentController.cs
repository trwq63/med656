using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.Helpers;
using UAHFitVault.Models;
using UAHFitVault.LogicLayer.Enums;
using System.Web.UI.WebControls;

namespace UAHFitVault.Controllers
{
    [Authorization("ROLES_EXPERIMENTADMIN")]
    public class ExperimentController : Controller
    {
        // GET: Experiment
        public ActionResult Index()
        {
            return View();
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
            if (selectedGenders != null)
            {
                model.selectedGenders = selectedGenders;
            }
            if (selectedRaces != null)
            {
                model.selectedRaces = selectedRaces;
            }
            if (selectedEthnicities != null)
            {
                model.selectedEthnicities = selectedEthnicities;
            }
            if (selectedLocations != null)
            {
                model.selectedLocations = selectedLocations;
            }
            if (selectedActivities != null)
            {
                model.selectedActivities = selectedActivities;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Need to generate DB Query to store in db and save the experiment criteria to the db
            
            return View(model);
        }
    }
}