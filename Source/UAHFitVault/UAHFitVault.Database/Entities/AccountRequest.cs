using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class AccountRequest
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountRequest(string accountId, string reasonForAccount) {
            AccountId = accountId;
            ReasonForAccount = reasonForAccount;
        }
        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public string AccountId { get; set; }
        public string ReasonForAccount { get; set; }

        #endregion

    }
}
