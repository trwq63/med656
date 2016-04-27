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

        }
        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public string ReasonForAccount { get; set; }

        #endregion

    }
}
