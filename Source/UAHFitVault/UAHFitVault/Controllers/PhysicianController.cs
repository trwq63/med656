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
using Microsoft.AspNet.Identity.EntityFramework;

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
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            Physician physician = _physicianService.GetPhysician(user.PhysicianId);

            string patientUsername = user.UserName + (physician.Patients.Count()+1).ToString();
            model.Username = patientUsername;

            if (patientUsername == null)
            {
                ModelState.Clear();
                ModelState.AddModelError("UsernameNullError", "Username was not provided.");
                return View(model);
            }

            // Conversions from the model
            int race = (int)Enum.Parse(typeof(PatientRace), model.Race);//convert.GenderRaceStringToInt(model.Race);
            int gender = (int)Enum.Parse(typeof(PatientGender), model.Gender);//convert.PatientGenderStringToInt(model.Gender);
            int ethnicity = (int)Enum.Parse(typeof(PatientEthnicity), model.Ethnicity);// convert.PatientEthnicityStringToInt(model.Ethnicity);


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

                patient.Birthdate = DateTime.Now; //model.Birthdate; // Need to fix this, temporarily putting in 0.
                patient.Height = model.Height;
                patient.Weight = model.Weight;
                patient.Location = 12;//model.Location; // Need to fix this. temporarily putting in 12.
                patient.Ethnicity = ethnicity;
                patient.Gender = gender;
                patient.Race = race;
                patient.Physician = physician;
                
                var newUser = new ApplicationUser { UserName = patientUsername };
                if ((newUser != null) && (model.Password != null))
                {                
                    //Create a new context to change the user creation validation rules for the patient only.
                    using (ApplicationDbContext context = new ApplicationDbContext()) {
                        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                        // This user validator is being created only to remove email as a required field for creating a user.
                        // The email field in the AspNetUsers table is nullable and our requirments state that a patient does not
                        // have an email address so in order to satisfy that requiremnt we need to remove the required email
                        // parameter on user creation validation.
                        manager.UserValidator = new UserValidator<ApplicationUser>(manager) {
                            AllowOnlyAlphanumericUserNames = false,
                            RequireUniqueEmail = false
                        };
                        var result = manager.Create(newUser, model.Password);

                        if (result.Succeeded) {
                            // User added to database successfully.
                            _patientService.CreatePatient(patient);
                            _patientService.SaveChanges();
                            newUser.PatientId = patient.Id;

                            result = UserManager.Update(newUser);

                            //Role must match what is found in the database AspNetRoles table.
                            result = UserManager.AddToRole(newUser.Id, "Patient");
                        }
                        else {
                            // User failed to add.
                            ModelState.Clear();
                            foreach (string error in result.Errors) {
                                ModelState.AddModelError("ResultError", error);
                            }
                            return View(model);
                        }
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