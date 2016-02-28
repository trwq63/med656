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
        /// Get all of the pending user accounts and send the user objects to the view.
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountRequests() {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //Get pending users in the system.
            List<ApplicationUser> pendingUsers = new ApplicationUserService().GetPendingUsers(manager);

            List<UserInfoModel> userModels = new List<UserInfoModel>();

            foreach (ApplicationUser user in pendingUsers) {
                string fullName = string.Empty;
                string role = string.Empty;
                //Check if user is a physician
                if (user.PhysicianId > 0) {
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

                //Get the account request information provided by the user during account request.
                string accountRequest = accountRequest = _accountRequestService.GetAccountRequest(user.AccountRequestId).ReasonForAccount;
                
                UserInfoModel model = new UserInfoModel() {
                    UserId = user.Id,
                    FullName = fullName,
                    Role = role,
                    Status = (Account_Status)user.Status,
                    ReasonForRequest = accountRequest
                };

                userModels.Add(model);
            }
            return View(userModels);
        }

        /// <summary>
        /// Function used to setup and load the initial view for system administrators.
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageUsers() {
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //Get all users in the system.
            List<ApplicationUser> allusers = new ApplicationUserService().GetUserAccounts(manager);

            List<UserInfoModel> userModels = new List<UserInfoModel>();

            //Create a dictionary connecting the user object to the corresponding object that is the correct user type.
            foreach (ApplicationUser user in allusers) {
                string fullName = string.Empty;
                string role = string.Empty;

                //Check if user is a physician
                if (user.PhysicianId > 0) {
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
                        //If the system admin is the currently logged in system admin do not include them 
                        //in the list of users to be managed.
                        if(user.Id == User.Identity.GetUserId()) {
                            continue;
                        }
                        role = "System Administrator";
                    }
                    else {
                        role = "None";
                    }
                }

                UserInfoModel model = new UserInfoModel() {
                    UserId = user.Id,
                    FullName = fullName,
                    Role = role,
                    Status = (Account_Status)user.Status
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
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(userId);
            user.Status = (int)Account_Status.Active;

            //Delete an account activation request if there is still one referenced by the user entry in the database.
            RemoveActivationRequest(user);

            user.AccountRequestId = 0;
            manager.Update(user);
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
            if (user.PhysicianId > 0) {
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

        /// <summary>
        /// The controller used for creating a system admin
        /// </summary>
        /// <returns>View for creating system admin</returns>
        public ActionResult CreateAdmin() {
            return View();
        }

        /// <summary>
        /// Controller used after the user presses the submit button on the Create Admin view.
        /// </summary>
        /// <param name="model">Content passed in from the model</param>
        /// <returns>View for creating the </returns>
        [HttpPost]
        public ActionResult CreateAdmin(CreateAdminModel model) {
            if (ModelState.IsValid) {
                ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser newUser = new ApplicationUser { UserName = model.Username, Email = model.Email, Status = (int)Account_Status.Active };

                var result = manager.Create(newUser, model.Password);

                if (result.Succeeded) {
                    // User was added successfully.
                    CreateAdminModel newModel = new CreateAdminModel();
                    newModel.Username = model.Username;
                    newModel.Email = model.Email;
                    manager.AddToRole(newUser.Id, "System Administrator"); // Add new user as a system administrator
                    return RedirectToAction("CreateAdminConfirm", newModel);
                    //new System.Web.Routing.RouteValueDictionary (new { model = newModel }));
                }
                else {
                    // Add all errors to the model state and return.
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                    return View(model);
                }

            }
            return View(model);
        }

        /// <summary>
        /// The controller for displaying the confirmation view for creating a new system admin
        /// </summary>
        /// <param name="model">Parameters for new system admin</param>
        /// <returns></returns>
        public ActionResult CreateAdminConfirm(CreateAdminModel model) {
            return View(model);
        }

        /// <summary>
        /// The controller for resetting the user password
        /// </summary>
        /// <param name="userId">User Id for the user to change the password</param>
        /// <returns></returns>
        public ActionResult ResetPassword (string userId)
        {
            SystemAdminResetPasswordViewModel model = new SystemAdminResetPasswordViewModel();
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var result = manager.FindById(userId);
            if (result != null) // User was found. Should always be found.
            {
                model.Username = result.UserName;
            }
            else
            {   // UserId was not found in the database.
                string errorString = "ERROR: UserId " + userId.ToString() + " could not be found in the database.";
                ModelState.AddModelError("", errorString);
            }
            return View(model);
        }

        /// <summary>
        /// The controller for when the system admin presses the submit button when changing a user's password.
        /// </summary>
        /// <param name="inModel">Model for input</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassword (SystemAdminResetPasswordViewModel inModel)
        {
            if (!ModelState.IsValid)
            {
                // Model has a problem.
                return View(inModel);
            }            
            ApplicationUserManager manager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindByName(inModel.Username);

            if (user != null)
            {
                // User exists
                string resetPasswordToken = manager.GeneratePasswordResetToken(user.Id); // Generate new pw reset token
                if (resetPasswordToken == null)
                {
                    ModelState.AddModelError("", "Error generating password reset token.");
                    return View(inModel);
                }

                var result = manager.ResetPassword(user.Id, resetPasswordToken, inModel.Password);
                if (result.Succeeded)
                {   // Password update succeeded
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                else
                {   // Password update failed
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", "Error updating password:  " + error);
                    }
                }
            }

            return View(inModel);
        }

        /// <summary>
        /// The controller for reset password confirmation
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetPasswordConfirmation ()
        {
            return View();
        }

        #region Protected Methods

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