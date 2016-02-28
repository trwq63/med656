using System.ComponentModel.DataAnnotations;
using System;

namespace UAHFitVault.Models
{
    /// <summary>
    /// View model used to capture the data displayed on physician's patient management view.
    /// </summary>
    public class PatientManagementViewModel
    {
        #region Pubilc Properties

        /// <summary>
        /// Id of the patient's user object.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The patient's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Id for the patient.
        /// </summary>
        public int PatientId { get; set; }

        #endregion

        #region Public Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public PatientManagementViewModel() {

        }

        /// <summary>
        /// Constructor used to initialize object properties at construction
        /// </summary>
        /// <param name="userId">Id of the patient's user object.</param>
        /// <param name="username">The patient's username</param>
        /// <param name="patientId">Id for the patient.</param>
        public PatientManagementViewModel(string userId, string username, int patientId) {
            //Verify parameters before setting properties
            if(IsValid(userId, username, patientId)) {
                UserId = userId;
                PatientId = patientId;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Verify the object's property values.
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return IsValid(UserId, Username, PatientId);
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// Verfiy that the parameters for the object are valid.
        /// </summary>
        /// <param name="userId">Id of the patient's user object.</param>
        /// <param name="username">The patient's username</param>
        /// <param name="patientId">Id for the patient.</param>
        /// <returns></returns>
        protected bool IsValid(string userId, string username, int patientId) {
            bool valid = false;

            if(!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(username) && patientId > 0) {
                valid = true;
            }

            return valid;
        }

        #endregion
    }

    /// <summary>
    /// View model for a physician deleting a patient
    /// </summary>
    public class DeletePatientViewModel
    {
        public string Username { get; set; }
    }

    /// <summary>
    /// View model for editing a patient
    /// </summary>
    public class EditPatientViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required]
        [Display(Name = "Weight (lbs)")]
        public float Weight { get; set; }

        [Required]
        [Display(Name = "Height (inches)")]
        public int Height { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Race")]
        public string Race { get; set; }

        [Required]
        [Display(Name = "Ethnicity")]
        public string Ethnicity { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

    }

    /// <summary>
    /// View model for viewing data for a patient
    /// </summary>
    public class ViewPatientDataViewModel
    {
        [Required]
        public string Username { get; set; }
        
    }

    /// <summary>
    /// View model for resetting a patient's password
    /// </summary>
    public class PhysicianResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}