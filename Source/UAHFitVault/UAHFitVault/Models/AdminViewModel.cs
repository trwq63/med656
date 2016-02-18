using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

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
    
    /// <summary>
    /// Model used for the create admin view
    /// </summary>
    public class CreateAdminModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Desired Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}