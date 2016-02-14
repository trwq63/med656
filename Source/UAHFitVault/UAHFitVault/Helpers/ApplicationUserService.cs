using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
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
        /// <returns></returns>
        public List<ApplicationUser> GetUserAccounts(ApplicationUserManager manager) {
            List<ApplicationUser> allUsers = null;

            if(manager != null) {
                allUsers = new List<ApplicationUser>();
                allUsers = manager.Users.ToList();
            }

            return allUsers;
        }

        #endregion

    }
}