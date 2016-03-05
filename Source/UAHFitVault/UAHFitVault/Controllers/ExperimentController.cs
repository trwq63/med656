using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.Helpers;
using UAHFitVault.Models;
using UAHFitVault.LogicLayer.Enums;

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
            return View();
        }

        /// <summary>
        /// This handles the processing after the user hits the submit button on the create experiment view
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateExperiment (CreateExperimentViewModel model)
        {
            /*  Code doesn't work
            int maxGenders = Enum.GetNames(typeof(PatientGender)).Length;

            for (int i = 0; i < maxGenders; i++)
            {
                if (model.genders[i] == true)
                {

                }
                else {
                    ModelState.AddModelError("", "ERROR: No race was checked");
                }
            }
            */
            if (!ModelState.IsValid)
            {
                return View();
            }
            return View();
        }
    }
}