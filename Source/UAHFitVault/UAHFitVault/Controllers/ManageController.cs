using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using UAHFitVault.Models;
using UAHFitVault.DataAccess;
using UAHFitVault.Database.Entities;
using UAHFitVault.LogicLayer.Enums;

namespace UAHFitVault.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private readonly IPatientService _patientService;
        private readonly IPhysicianService _physicianService;
        private readonly IExperimentAdminService _experimentAdminService;

        public ManageController(IPatientService patientService, IPhysicianService physicianService,
            IExperimentAdminService experimentAdminService)
        {
            _patientService = patientService;
            _physicianService = physicianService;
            _experimentAdminService = experimentAdminService;
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
           
            string accountRole = "";
            //user = UserManager.FindById(userId);
            UserRole userRole = UserRole.Patient;
            
            if (User.IsInRole(UserRole.Patient.ToString())) {
                userRole = UserRole.Patient;
                accountRole = UserRole.Patient.ToString();
            }
            else if (User.IsInRole(UserRole.Physician.ToString())) {
                userRole = UserRole.Physician;
                accountRole = UserRole.Physician.ToString();
            }
            else if (User.IsInRole(UserRole.Experiment_Administrator.ToString().Replace("_", " "))) {
                userRole = UserRole.Experiment_Administrator;
                accountRole = UserRole.Experiment_Administrator.ToString().Replace("_", " ");
            } else
            {
                userRole = UserRole.System_Administrator;
                accountRole = UserRole.System_Administrator.ToString().Replace("_", " ");                
            }

            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            IndexViewModel model = new IndexViewModel {
                HasPassword = (user.PasswordHash != null) ? true : false,
                PhoneNumber = user.PhoneNumber,
                TwoFactor = user.TwoFactorEnabled,
                //Logins = user.Logins,
                //BrowserRemembered = user.brows
                        AccountRole = accountRole
                    };

            switch (userRole)
            {
                case UserRole.Patient:
                    // Patient
                    Patient patient = new Patient();
                    patient = _patientService.GetPatient(user.PatientId);

                    model.Username = user.UserName;
                    model.Weight = patient.Weight;
                    model.Height = patient.Height;
                    model.Race = patient.Race.ToString();
                    model.Ethnicity = patient.Ethnicity.ToString();
                    model.Location = patient.Location.ToString();
                    model.Birthdate = patient.Birthdate;     
                    model.Gender = patient.Gender.ToString();

                    break;


                case UserRole.Physician:
                    // Physician
                    Physician physician = new Physician();
                    physician = _physicianService.GetPhysician(user.PhysicianId);

                    model.Email = physician.Email;
                    model.Username = user.UserName;
                    model.Address = physician.Address;
                    model.PhoneNumber = physician.PhoneNumber;
                    model.FirstName = physician.FirstName;
                    model.LastName = physician.LastName;

                    break;


                case UserRole.Experiment_Administrator:
                    // Experiment Administrator
                    ExperimentAdministrator experimentAdministrator = new ExperimentAdministrator();
                    experimentAdministrator = _experimentAdminService.GetExperimentAdministrator(user.ExperimentAdministratorId);

                    model.Email = experimentAdministrator.Email;
                    model.Username = user.UserName;
                    model.Address = experimentAdministrator.Address;
                    model.PhoneNumber = experimentAdministrator.PhoneNumber;
                    model.FirstName = experimentAdministrator.FirstName;
                    model.LastName = experimentAdministrator.LastName;

                    break;


                case UserRole.System_Administrator:
                    // System Admin
                    model.Username = user.UserName;
                    model.Email = user.Email;
                    break;


                default:
                    // Display error
                    break;
            }
            return View(model);
        }

        //
        // POST: /Manage/Index
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UpdateUser(IndexViewModel inmodel)
        {
            UpdateUserViewModel model = new UpdateUserViewModel();

            model.AccountRole = inmodel.AccountRole;
            model.Address = inmodel.Address;
            model.Birthdate = inmodel.Birthdate;
            model.Email = inmodel.Email;
            model.Ethnicity = inmodel.Ethnicity;
            model.FirstName = inmodel.FirstName;
            model.Gender = inmodel.Gender;
            model.Height = inmodel.Height;
            model.LastName = inmodel.LastName;
            model.Location = inmodel.Location;
            model.PhoneNumber = inmodel.PhoneNumber;
            model.Race = inmodel.Race;
            model.Username = inmodel.Username;
            model.Weight = inmodel.Weight;
            
            return View(model);
        }

        /// <summary>
        /// ConfirmUpdateUser() - This is the HttpPost function to handle updating the user in the database from the 
        ///   submit button being clicked on the UpdateUser view.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmUpdateUser (UpdateUserViewModel model)
        {
            var user = new ApplicationUser();
            user = UserManager.FindById(User.Identity.GetUserId());
            
            if (User.IsInRole("Patient"))
            {
                Patient patient = new Patient();
                patient = _patientService.GetPatient(user.PatientId);
                patient.Birthdate = model.Birthdate;
                patient.Height = model.Height;
                patient.Weight = model.Weight;
                patient.Ethnicity = (int)Enum.Parse(typeof(PatientEthnicity), model.Ethnicity);
                patient.Gender = (int)Enum.Parse(typeof(PatientGender), model.Gender);
                patient.Location = (int)Enum.Parse(typeof(Location), model.Location);
                patient.Race = (int)Enum.Parse(typeof(PatientRace), model.Race);
                _patientService.SaveChanges();
            }
            else if (User.IsInRole("Physician"))
            {
                Physician physician = new Physician();
                physician = _physicianService.GetPhysician(user.PhysicianId);
                physician.Email = model.Email;
                user.Email = model.Email;
                physician.Address = model.Address;
                physician.FirstName = model.FirstName;
                physician.LastName = model.LastName;
                physician.PhoneNumber = model.PhoneNumber;
                _physicianService.SaveChanges();
            }
            else if (User.IsInRole("Experiment Administrator"))
            {
                ExperimentAdministrator experimentAdministrator = new ExperimentAdministrator();
                experimentAdministrator = _experimentAdminService.GetExperimentAdministrator(user.ExperimentAdministratorId);
                experimentAdministrator.Email = model.Email;
                user.Email = model.Email;
                experimentAdministrator.Address = model.Address;
                experimentAdministrator.FirstName = model.FirstName;
                experimentAdministrator.LastName = model.LastName;
                experimentAdministrator.PhoneNumber = model.PhoneNumber;
                _experimentAdminService.SaveChanges();
            }
            else if (User.IsInRole("System Administrator"))
            {
                // Not yet implemented.
                user.Email = model.Email;
            }
            else
            {
                // Error path.
                ModelState.AddModelError("", "ERROR: User role not specified.");
                return View();
            }

            return (Redirect ("/Account/LoginRedirect"));
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}