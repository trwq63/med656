﻿using System;
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
        #region Private Members
        /// <summary>
        /// UserManager object used to get the logged in user's information
        /// </summary>
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Interface for the patient service to access patient database queries
        /// </summary>
        private readonly IPatientService _patientService;

        /// <summary>
        /// Interface for the physician service to access physician database queries
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

        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="patientService">Interface for the patient service to access patient database queries</param>
        /// <param name="physicianService">Interface for the physician service to access physician database queries</param>
        public PhysicianController(IPatientService patientService, IPhysicianService physicianService)
        {
            _patientService = patientService;
            _physicianService = physicianService;            
        }

        #endregion

        #region Public Functions
        /// <summary>
        /// Load initial patient management view for the physician
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Get logged in user's physician object.
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            Physician physician = _physicianService.GetPhysician(user.PhysicianId);

            List<Patient> patients = _patientService.GetPatients(physician).ToList();

            List<PatientManagementViewModel> viewModel = new List<PatientManagementViewModel>();

            //The view will need both the patient id and the patient's user id.
            foreach(Patient patient in patients) {
                ApplicationUser patientUser = UserManager.Users.FirstOrDefault(u => u.PatientId == patient.Id);
                if(patientUser != null) {
                    PatientManagementViewModel model = new PatientManagementViewModel() {
                        UserId = patientUser.Id,
                        PatientId = patient.Id,
                        Username = patientUser.UserName
                    };

                    viewModel.Add(model);
                }
            }

            return View(viewModel);
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
            
            // Conversions from the model
            int race = (int)Enum.Parse(typeof(PatientRace), model.Race);//convert.GenderRaceStringToInt(model.Race);
            int gender = (int)Enum.Parse(typeof(PatientGender), model.Gender);//convert.PatientGenderStringToInt(model.Gender);
            int ethnicity = (int)Enum.Parse(typeof(PatientEthnicity), model.Ethnicity);// convert.PatientEthnicityStringToInt(model.Ethnicity);


            // Check if the user is already in the database.
            if (UserIsInDatabase(model.Username))
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

                patient.Birthdate = DateTime.Parse(model.Birthdate); //model.Birthdate; // Need to fix this, temporarily putting in 0.
                patient.Height = (int)model.Height;
                patient.Weight = (int)model.Weight;
                patient.Location = (int)Enum.Parse(typeof(Location), model.Location);//model.Location; // Need to fix this. temporarily putting in 12.
                patient.Ethnicity = ethnicity;
                patient.Gender = gender;
                patient.Race = race;
                patient.Physician = physician;
                
                var newUser = new ApplicationUser { UserName = model.Username, Status = (int)Account_Status.Active };
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

                            result = manager.Update(newUser);

                            //Role must match what is found in the database AspNetRoles table.
                            result = manager.AddToRole(newUser.Id, "Patient");
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
        /// This function allows the physician to edit a patient's information.
        /// * This function will also allow a physician to change a patient's password.
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPatient (string username)
        {
            EditPatientViewModel model = new EditPatientViewModel();
            if (username == null)
            {
                // Username was not provided.
                model.Username = "NULL";
                ModelState.AddModelError("", "ERROR: No username provided.");
                return View(model);
            }

            model.Username = username;

            ApplicationUser physicianUser = UserManager.FindByName(User.Identity.Name);
            ApplicationUser patientUser = UserManager.FindByName(username);
            if (patientUser == null)
            {
                // Patient was not in database
                ModelState.AddModelError("", "ERROR: Patient not found in database.");
                return View(model);
            }

            Patient patient = _patientService.GetPatient(patientUser.PatientId);
            Physician physician = _physicianService.GetPhysician(physicianUser.PhysicianId);

            if (!PatientBelongsToPhysician(patient, physician))
            {
                // Patient does not belong to the current physician
                ModelState.AddModelError("", "ERROR: This patient does not belong to you.");
                return View(model);
            }

            // Fill in model with patient information
            model.Birthdate = patient.Birthdate;
            model.Ethnicity = patient.Ethnicity.ToString();
            model.Gender = patient.Gender.ToString();
            model.Height = patient.Height;
            model.Location = patient.Location.ToString();
            model.Race = patient.Race.ToString();
            model.Weight = patient.Weight;
                

            return View(model);
        }

        /// <summary>
        /// This function writes the model from the Edit Patient form to the database.
        /// </summary>
        /// <param name="model">Model containing patient information</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPatient (EditPatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Patient patient = _patientService.GetPatient(UserManager.FindByName(model.Username).PatientId);
            Physician physician = _physicianService.GetPhysician(UserManager.FindByName(User.Identity.Name).PhysicianId);

            if (!PatientBelongsToPhysician(patient, physician))
            {
                ModelState.AddModelError("", "ERROR: You do not have permission to update this patient.");
                return View(model);
            }
            
            patient.Birthdate = model.Birthdate;
            patient.Ethnicity = (int)Enum.Parse(typeof(PatientEthnicity), model.Ethnicity);
            patient.Gender = (int)Enum.Parse(typeof(PatientGender), model.Gender);
            patient.Height = model.Height;
            patient.Location = (int)Enum.Parse(typeof(Location), model.Location);
            patient.Race = (int)Enum.Parse(typeof(PatientRace), model.Race);
            patient.Weight = model.Weight;
            _patientService.SaveChanges(); // Write changes to DB

            return Redirect("/Account/LoginRedirect");
        }

        /// <summary>
        /// This function confirms that a physician wishes to delete a patient from the database
        /// </summary>
        /// <param name="username">Username of the patient</param>
        /// <returns></returns>
        public ActionResult DeletePatient (string username)
        {
            DeletePatientViewModel model = new DeletePatientViewModel();

            // Check if username is null, if so - return early.
            if (username == null)
            {
                model.Username = "NULL"; // So model does not crash
                ModelState.AddModelError("", "Error: username is null");
                return View(model);
            }

            ApplicationUser patientUser = UserManager.FindByName(username);
            Patient patient = _patientService.GetPatient(patientUser.PatientId);
            ApplicationUser physicianUser = UserManager.FindByName(User.Identity.Name);
            Physician physician = _physicianService.GetPhysician(physicianUser.PhysicianId);

            // Check to verify that the patient is a patient for the current physician.
            if (!PatientBelongsToPhysician(patient, physician))
            {
                ModelState.AddModelError("", "Error: You cannot delete somebody that is not your patient.");
                return View(model);
            }

            model.Username = username;
            return View(model);
        }

        /// <summary>
        /// The function that deletes the patient from the system.
        /// </summary>
        /// <param name="username">Username of the patient to delete</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeletePatientConfirm (string username)
        {
            DeletePatientViewModel model = new DeletePatientViewModel();
            ApplicationUser user = UserManager.FindByName(username);
            Patient patient = _patientService.GetPatient(user.PatientId);
            ApplicationUser physicianUser = UserManager.FindByName(User.Identity.Name);
            Physician physician = _physicianService.GetPhysician(physicianUser.PhysicianId);

            // Check to verify that the patient is a patient for the current physician.
            if (!PatientBelongsToPhysician(patient, physician))
            {
                ModelState.AddModelError("", "Error: You cannot delete somebody that is not your patient.");
                return View(model);
            }
            _patientService.DeletePatient(patient);
            _patientService.SaveChanges();
            _userManager.Delete(user);

            /*Glen: need to go back and add deleting data here if we decide to delete the patient's data*/

            return Redirect("/Account/LoginRedirect");
        }

        #endregion

        #region Private Functions

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

        /// <summary>
        /// Checks if the patient belongs to the physician
        /// </summary>
        /// <param name="patient">Patient input</param>
        /// <param name="physician">Physician input</param>
        /// <returns>True if the patient's physician is the physician user</returns>
        private bool PatientBelongsToPhysician(Patient patient, Physician physician)
        {
            if (patient.Physician != physician)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}