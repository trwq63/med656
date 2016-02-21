using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.Models;

namespace UAHFitVault.Helpers
{
    /// <summary>
    /// Helper class used as a service for custom queries for accessing Asp .Net Identity Tables
    /// </summary>
    public class ApplicationUserService
    { 

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationUserService() {
            
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get a list of all the users that are currently in the system.
        /// </summary>
        /// /// <param name="manager">ApplicationUserManager object created in controller used to access user data.</param>
        /// <returns></returns>
        public List<ApplicationUser> GetUserAccounts(ApplicationUserManager manager) {
            List<ApplicationUser> allUsers = null;

            if(manager != null) {
                allUsers = new List<ApplicationUser>();
                allUsers = manager.Users.ToList();
            }

            return allUsers;
        }

        /// <summary>
        /// Get the users in the system that are pending approval to acceess the system.
        /// </summary>
        /// <param name="manager">ApplicationUserManager object created in controller used to access user data.</param>
        /// <returns></returns>
        public List<ApplicationUser> GetPendingUsers(ApplicationUserManager manager) {
            List<ApplicationUser> pendingUsers = null;

            if (manager != null) {
                pendingUsers = new List<ApplicationUser>();
                pendingUsers = manager.Users.Where(u => u.Status == (int)Account_Status.Pending && u.PhysicianId > 0 && u.ExperimentAdministratorId > 0).ToList();
            }

            return pendingUsers;
        }

        #endregion

    }
}