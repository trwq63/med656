using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.DataAccess;
using UAHFitVault.Database.Entities;
using UAHFitVault.Helpers;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.Models;
using UAHFitVault.Resources;

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
        /// <param name="accountRequestService">Service interface for accessing the account request database functions.</param>
        public AdminController(IPhysicianService physicianService, IExperimentAdminService expAdminService,
                                IAccountRequestService accountRequestService) {
            _physicianService = physicianService;
            _experimentAdminService = expAdminService;
            _accountRequestService = accountRequestService;
        }

        #endregion

        /// <summary>
        /// Function used to setup and load the initial view for system administrators.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //Get all users in the system.
            List<ApplicationUser> allusers = new ApplicationUserService().GetUserAccounts(manager);

            List<UserInfoModel> userModels = new List<UserInfoModel>();

            //Create a dictionary connecting the user object to the corresponding object that is the correct user type.
            foreach(ApplicationUser user in allusers) {
                string fullName = string.Empty;
                string role = string.Empty;

                //Check if user is a physician
                if(user.PhysicianId > 0) {
                    Physician physician = _physicianService.GetPhysician(user.PhysicianId);
                    fullName = physician.FirstName + " " + physician.LastName;
                    role = "Physician";
                }
                //Check if user is an experiment administrator
                else if (user.ExperimentAdministratorId > 0) {
                    ExperimentAdministrator expAdmin = _experimentAdminService.GetExperimentAdministrator(user.ExperimentAdministratorId);
                    fullName = expAdmin.FirstName + " " + expAdmin.LastName;
                    role = "Experiment Administrator";
                }
                //Check if user is a patient
                else if (user.PatientId > 0) {
                    //System administrators can't manage patients per requirement 3.1.1.1.4.2
                    continue;
                }                
                else {
                    fullName = user.UserName;
                    //Determine if the user is a system admin or has no role.
                    if (user.Roles.Select(r => r.RoleId).Contains(Roles.ADMIN_ROLE_DB_TABLE_ID)) {                        
                        role = "System Administrator";
                    }
                    else {
                        role = "None";
                    }
                }
               
                string accountRequest = string.Empty;
                //Get the user's account request if they are a pending user.
                if(user.Status == (int)Account_Status.Pending) {
                    accountRequest = _accountRequestService.GetAccountRequest(user.AccountRequestId).ReasonForAccount;
                }

                UserInfoModel model = new UserInfoModel() {
                    UserId = user.Id,
                    FullName = fullName,
                    Role = role,
                    Status = (Account_Status)user.Status,
                    ReasonForRequest = accountRequest
                };

                userModels.Add(model);
            }

            //Create view model
            AdminViewModel viewModel = new AdminViewModel();
            if (userModels.Count > 0) {
                viewModel.Users = userModels;
            }

            viewModel.SelectedRole = "All Roles";

            //Get the list of roles from the database.
            using (ApplicationDbContext context = new ApplicationDbContext()) {
                List<string> roles = context.Roles.Select(r => r.Name).ToList();
                roles.Sort();
                roles.Insert(0, "All Roles");
                viewModel.RoleList = new SelectList(roles, viewModel.SelectedRole);
            }

            return View(viewModel);
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

        /// <summary>
        /// Enable a user that is inactive so they can begin using the system again or 
        /// approve a pending user requesting access to the system.
        /// </summary>
        /// <param name="userId">Id of the user being rejected.</param>
        /// <returns></returns>
        public string EnableUser(string userId) {
            ActivateUser(userId);
            return null;
        }

        /// <summary>
        /// Delete a user from the system.
        /// </summary>
        /// <param name="userId">Id of the user being deleted.</param>
        /// <returns></returns>
        public string DeleteUser(string userId) {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(userId);
            if(user.PhysicianId > 0) {
                _physicianService.DeletePhysician(user.PhysicianId);
            }
            else if (user.ExperimentAdministratorId > 0) {
                _experimentAdminService.DeleteExperimentAdministrator(user.ExperimentAdministratorId);
            }

            //Delete an account activation request if there is still one referenced by the user entry in the database.
            RemoveActivationRequest(user);

            manager.Delete(user);

            return null;
        }
        #region Protected Methods

        /// <summary>
        /// Activate the user to be able to access the system.
        /// </summary>
        /// <param name="userId">Id of the user being activated.</param>
        protected void ActivateUser(string userId) {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(userId);
            user.Status = (int)Account_Status.Active;

            //Delete an account activation request if there is still one referenced by the user entry in the database.
            RemoveActivationRequest(user);
            
            user.AccountRequestId = 0;
            manager.Update(user);            
        }

        /// <summary>
        /// Delete the user's activation request from the database after they are approved for access.
        /// </summary>
        /// <param name="user">User object for the user being updated.</param>
        protected void RemoveActivationRequest(ApplicationUser user) {
            //Very the user object exists and that is still has a reference to an account request.
            if (user != null && user.AccountRequestId > 0) {
                //Remove the user's account request from database since they have been approved.
                bool result = _accountRequestService.DeleteAccountRequest(user.AccountRequestId);
                if (result) {
                    _accountRequestService.SaveChanges();
                }
            }
        }

        #endregion
    }
}