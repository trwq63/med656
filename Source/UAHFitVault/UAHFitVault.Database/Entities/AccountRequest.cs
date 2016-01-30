using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class AccountRequest
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountRequest() {
            ReasonForRequest = "No Reason Specified.";
            PhysicianID = 0;
            ExperimentAdministratorID = 0;
        }
        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public string ReasonForRequest { get; set; }
        public int PhysicianID { get; set; }
        public int ExperimentAdministratorID { get; set; }

        #endregion

    }
}
