using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the physician model
    /// </summary>
    public class AccountRequestRepository : RepositoryBase<AccountRequest>, IAccountRequestRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public AccountRequestRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get an account request using ID.
        /// </summary>
        /// <param name="accountID">ID of account</param>
        /// <returns>Account Request</returns>
        public AccountRequest GetAccountRequest(int ID)
        {
            AccountRequest accountRequest = this.DbContext.AccountRequests.Where(p => p.Id == ID).FirstOrDefault();
            return accountRequest;
        }

        #endregion
    }
}
