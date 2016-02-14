using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace UAHFitVault.Models
{
    /// <summary>
    /// Class used compile objects needed for the system admin views.
    /// </summary>
    public class AdminViewModel
    {
        #region Public Properties

        /// <summary>
        /// List of users that will be displayed on an admin view page.
        /// </summary>
        public List<UserInfoModel> Users { get; set; }

        /// <summary>
        /// List of the system roles that will be used in a dropdown box
        /// </summary>
        public SelectList RoleList { get; set; }

        /// <summary>
        /// Selected role to default the drop down list to.
        /// </summary>
        public string SelectedRole { get; set; }

        #endregion

        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public AdminViewModel() {

        }

        /// <summary>
        /// Constructor used to initialize object properties at construction.
        /// </summary>
        /// <param name="userModels">List of users that will be displayed on an admin view page.</param>
        /// <param name="roleList">List of the system roles that will be used in a dropdown box</param>
        /// <param name="selectedRole">Selected role to default the drop down list to.</param>
        public AdminViewModel(List<UserInfoModel> userModels, SelectList roleList, string selectedRole) {
            if(userModels != null && userModels.Count > 0 && roleList != null && roleList.Count() > 0 && !string.IsNullOrEmpty(selectedRole)) {
                Users = userModels;
                RoleList = roleList;
                SelectedRole = selectedRole;
            }
        }

        #endregion
    }
}