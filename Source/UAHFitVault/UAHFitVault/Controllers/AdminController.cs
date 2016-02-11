using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.Helpers;
using UAHFitVault.Models;
using UAHFitVault.Database.Entities;
using UAHFitVault.DataAccess;
using UAHFitVault.LogicLayer.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace UAHFitVault.Controllers
{
    /// <summary>
    /// Controller used for controlling the views displayed to system administrators.
    /// </summary>
    [Authorization("ROLES_ADMIN")]
    public class AdminController : Controller
    {
        #region Private Members

        /// <summary>
        /// Service object for accessing physician database functions.
        /// </summary>
        private readonly IPhysicianService _physicianService;

        /// <summary>
        /// Service object for accessing experiement administrator database functions.
        /// </summary>
        private readonly IExperimentAdminService _experimentAdminService;

        /// <summary>
        /// Service object for accessing patient database functions.
        /// </summary>
        private readonly IPatientService _patientService;

        /// <summary>
        /// Service object for accessing the account request database functions.
        /// </summary>
        private readonly IAccountRequestService _accountRequestService;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="physicianService">Service interface for accessing physician database functions.</param>
        /// <param name="expAdminService">Service interface for accessing physician database functions.</param>
        /// <param name="patientService">Service interface for accessing patient database functions.</param>
        /// <param name="accountRequestService">Service interface for accessing the account request database functions.</param>
        public AdminController(IPhysicianService physicianService, IExperimentAdminService expAdminService,
                                IPatientService patientService, IAccountRequestService accountRequestService) {
            _physicianService = physicianService;
            _experimentAdminService = expAdminService;
            _patientService = patientService;
            _accountRequestService = accountRequestService;
        }

        #endregion

        /// <summary>
        /// Function used to setup and load the initial view for system administrators.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Get all users in the system.
            List<ApplicationUser> allusers = new ApplicationUserService().GetUserAccounts();

            List<AdminViewModel> viewModel = new List<AdminViewModel>();

            //Create a dictionary connecting the user object to the corresponding object that is the correct user type.
            foreach(ApplicationUser user in allusers) {
                string fullName = string.Empty;
                UserRole role = UserRole.None;

                //Check if user is a physician
                if(user.PhysicianId > 0) {
                    Physician physician = _physicianService.GetPhysician(user.PhysicianId);
                    fullName = physician.FirstName + " " + physician.LastName;
                    role = UserRole.Physician;
                }
                //Check if user is an experiment administrator
                else if (user.ExperimentAdministratorId > 0) {
                    ExperimentAdministrator expAdmin = _experimentAdminService.GetExperimentAdministrator(user.ExperimentAdministratorId);
                    fullName = expAdmin.FirstName + " " + expAdmin.LastName;
                    role = UserRole.ExperimentAdmin;
                }
                //Check if user is a patient
                else if (user.PatientId > 0) {
                    Patient patient = _patientService.GetPatient(user.PatientId);
                    fullName = user.UserName;
                    role = UserRole.Patient;
                }
                //If the user does not have a role that matches one of the roles above but does have a role id
                //then that user is a system admin.
                else {
                    fullName = user.UserName;
                    role = UserRole.SystemAdmin;
                }
               
                string accountRequest = string.Empty;
                //Get the user's account request if they are a pending user.
                if(user.Status == (int)Account_Status.Pending) {
                    accountRequest = _accountRequestService.GetAccountRequest(user.AccountRequestId).ReasonForAccount;
                }

                AdminViewModel model = new AdminViewModel() {
                    UserId = user.Id,
                    FullName = fullName,
                    Role = role,
                    Status = (Account_Status)user.Status,
                    ReasonForRequest = accountRequest
                };

                viewModel.Add(model);
            }

            return View(viewModel);
        }

        /// <summary>
        /// Approve the user enabling access to the system.
        /// </summary>
        /// <param name="userId">Id of the user being approved.</param>
        /// <returns></returns>
        public string ApproveUser(string userId) {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(userId);
            user.Status = (int)Account_Status.Active;
            manager.Update(user);

            return null;
        }

        /// <summary>
        /// Reject the user from accessing the system.
        /// </summary>
        /// <param name="userId">Id of the user being rejected.</param>
        /// <returns></returns>
        public string RejectUser(string userId) {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(userId);
            user.Status = (int)Account_Status.Inactive;
            manager.Update(user);
            return null;
        }
    }
}