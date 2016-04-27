using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Service operations used to access data for a user requesting an account
    /// </summary>
    public class AccountRequestService : IAccountRequestService
    {
        #region Private Properties

        private readonly IAccountRequestRepository _accountRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Account Request Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public AccountRequestService(IAccountRequestRepository repository, IUnitOfWork unitOfWork) {
            _accountRequestRepository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all account requests from the database
        /// </summary>
        /// <param name="ID">Get all account requests from the Db.</param>
        /// <returns></returns>
        public IEnumerable<AccountRequest> GetAccountRequests() {
            return _accountRequestRepository.GetAll();
        }

        /// <summary>
        /// Get account request from database using accountRequest Id
        /// </summary>
        /// <param name="id">Id of the account request</param>
        /// <returns></returns>
        public AccountRequest GetAccountRequest (int id) {
            AccountRequest accountRequest = null;
            if (id > 0)
            {
                accountRequest = _accountRequestRepository.GetAccountRequest(id);
            }
            return accountRequest;
        }

        /// <summary>
        /// Add a new account request to the database
        /// </summary>
        /// <param name="accountRequest">AccountRequest object to add to the database</param>
        public void CreateAccountRequest (AccountRequest accountRequest) {
            if(accountRequest != null) {
                _accountRequestRepository.Add(accountRequest);
            }
        }

        /// <summary>
        /// Delete an account request from the database.
        /// </summary>
        /// <param name="id">Id of the account request to delete</param>
        /// <returns></returns>
        public bool DeleteAccountRequest(int id) {
            bool result = false;
            if(id > 0) {
                //Find the account request object.
                AccountRequest request = GetAccountRequest(id);
                if(request != null) {
                    //Delete it.
                    _accountRequestRepository.Delete(request);
                    result = true;
                }                
            }

            return result;            
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void SaveChanges() {
            _unitOfWork.Commit();
        }

        #endregion
    }
}
