using UAHFitVault.LogicLayer.Enums;

namespace UAHFitVault.Models
{
    /// <summary>
    /// This class is used to collect various user and role data to be used on the system admin view screens
    /// </summary>
    public class UserInfoModel
    {
        #region Public Properties

        /// <summary>
        /// Id of for the user from the AspNetUser table.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Role user is requesting to have.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// The user's current account status.
        /// </summary>
        public Account_Status Status { get; set; }

        /// <summary>
        /// Explanation for account request.
        /// </summary>
        public string ReasonForRequest { get; set; }

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserInfoModel() {

        }

        /// <summary>
        /// Constructor to initialize object properties of at construction.
        /// </summary>
        /// <param name="id">Id of for the user from the AspNetUser table.</param>
        /// <param name="name">The full name of the user.</param>
        /// <param name="role">Role user is requesting to have.</param>
        /// <param name="status">The user's current account status.</param>
        /// <param name="requestReason">Explanation for account request.</param>
        public UserInfoModel(string id, string name, string role, Account_Status status, string requestReason) {
            bool valid = IsValid(id, name, role, requestReason);

            if (valid) {
                UserId = id;
                FullName = name;
                Role = role;
                //Status defaults to pending so their and is not nullable so no reason to check for validity.
                Status = status;    
                ReasonForRequest = requestReason;
            }
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Check to make sure the object is valid user the object's properties.
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return IsValid(UserId, FullName, Role, ReasonForRequest);
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// Checks the paramaters passed into the function to make sure they are valid.
        /// </summary>
        /// <param name="id">Id of for the user from the AspNetUser table.</param>
        /// <param name="name">The full name of the user.</param>
        /// <param name="role">Role user is requesting to have.</param>
        /// <param name="requestReason">Explanation for account request.</param>
        /// <returns></returns>
        protected bool IsValid(string id, string name, string role, string requestReason) {
            bool valid = false;
            if(!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(requestReason)) {
                valid = true;
            }

            return valid;
        }

        #endregion
    }
}