using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Interface for the account request service
    /// </summary>
    public interface IAccountRequestService
    {
        /// <summary>
        /// Add a new account request to the database
        /// </summary>
        /// <param name="accountRequest">Account request object to add to the database</param>
        void CreateAccountRequest(AccountRequest accountRequest);

        /// <summary>
        /// Get Account Request from database using Account Request Id
        /// </summary>
        /// <param name="id">Id of the account request</param>
        /// <returns></returns>
        AccountRequest GetAccountRequest (int id);
        
        /// <summary>
        /// Get all account requests from the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<AccountRequest> GetAccountRequests();

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}