using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface for a physician repository class
    /// </summary>
    public interface IAccountRequestRepository : IRepository<AccountRequest>
    {
        /// <summary>
        /// Get an account request using ID.
        /// </summary>
        /// <param name="accountID">ID of account</param>
        /// <returns>Account Request</returns>
        AccountRequest GetAccountRequest(int ID);
    }
}