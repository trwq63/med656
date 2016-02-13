using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.Helpers;
using UAHFitVault.Models;
using UAHFitVault.LogicLayer.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UAHFitVault.DataAccess;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Controllers
{
    [Authorization("ROLES_PHYSICIAN")]
    public class PhysicianController : Controller
    {
        private ApplicationUserManager _userManager;

        private readonly IPatientService _patientService;
        private readonly IPhysicianService _physicianService;

        public PhysicianController(IPatientService patientService, IPhysicianService physicianService)
        {
            _patientService = patientService;
            _physicianService = physicianService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        // GET: Physician
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This function processes the form from the Physician and creates a Patient account in the database.
        /// </summary>
        /// <param name="model">Model carrying input data</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult CreatePatient (CreatePatientViewModel model)
        {
            Conversions convert = new Conversions();
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            Physician physician = _physicianService.GetPhysician(user.PhysicianId);

            string patientUsername = user.UserName + (physician.PatientCount+1).ToString();
            model.Username = patientUsername;

            if (patientUsername == null)
            {
                ModelState.Clear();
                ModelState.AddModelError("UsernameNullError", "Username was not provided.");
                return View(model);
            }

            // Conversions from the model
            int race = convert.GenderRaceStringToInt(model.Race);
            int gender = convert.PatientGenderStringToInt(model.Gender);
            int ethnicity = convert.PatientEthnicityStringToInt(model.Ethnicity);


            // Check if the user is already in the database.
            if (UserIsInDatabase(patientUsername))
            {
                // User is already in the database
                // Display an error and request the physician to enter in a different username.
                ModelState.Clear();
                ModelState.AddModelError("UsernameExistsError", "Username already exists.");
            }
            else
            {
                // User is not in the database.
                // Proceed to add the user to the database.
                Patient patient = new Patient();

                patient.Age = 0; //model.Birthdate; // Need to fix this, temporarily putting in 0.
                patient.Height = model.Height;
                patient.Weight = model.Weight;
                patient.Location = 12;//model.Location; // Need to fix this. temporarily putting in 12.
                patient.Ethnicity = ethnicity;
                patient.Gender = gender;
                patient.Race = race;
                
                var newUser = new ApplicationUser { UserName = patientUsername, Email = patientUsername+ "@null.com" };
                if ((newUser != null) && (model.Password != null))
                {
                    var result = UserManager.Create(newUser, model.Password);
                    
                    if (result.Succeeded)
                    {
                        // User added to database successfully.
                        _patientService.CreatePatient(patient);
                        _patientService.SaveChanges();
                        newUser.PatientId = patient.Id;
                        patient.Physician = physician;
                        physician.PatientCount++;
                        _physicianService.SaveChanges();

                        result = UserManager.Update(newUser);

                        //Role must match what is found in the database AspNetRoles table.
                        result = UserManager.AddToRole(newUser.Id, "Patient");
                    }
                    else
                    {
                        // User failed to add.
                        ModelState.Clear();
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("ResultError", error);
                        }
                        return View(model);
                    }
                }
                else
                {
                    // Username or password was null if we got here.
                }
                
            }
            return View(model);
        }

        /// <summary>
        /// Checks if the user is already in the database.
        /// </summary>
        /// <param name="Username">Username for the user being added to the database</param>
        /// <returns>True if the user is in the database, False if the user is not in the database.</returns>
        private bool UserIsInDatabase (string Username)
        {
            ApplicationUser user = UserManager.FindByName(Username);
            if (user == null)
            {
                // Username is not in database.
                return false;
            }
            return true;
        }
    }
}