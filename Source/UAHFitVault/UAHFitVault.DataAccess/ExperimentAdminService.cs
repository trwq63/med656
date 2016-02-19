using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Service operations used to access data for a experiment administrator user
    /// </summary>
    public class ExperimentAdminService : IExperimentAdminService
    {
        #region Private Properties

        private readonly IExperimentAdminRepository _experimentAdminRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Experiment Administrator Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ExperimentAdminService(IExperimentAdminRepository repository, IUnitOfWork unitOfWork) {
            _experimentAdminRepository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all Experiment Administrators from the database
        /// </summary>
        /// <param name="lastName">Optional parameter to get all Experiment Administrators with a specific name</param>
        /// <returns></returns>
        public IEnumerable<ExperimentAdministrator> GetExperimentAdministrators(string lastName = null) {
            if (string.IsNullOrEmpty(lastName))
                return _experimentAdminRepository.GetAll();
            else
                return _experimentAdminRepository.GetAll().Where(c => c.LastName == lastName);
        }

        /// <summary>
        /// Get ExperimentAdministrator from database using ExperimentAdministrator Id
        /// </summary>
        /// <param name="id">Id of the experiment administrator</param>
        /// <returns></returns>
        public ExperimentAdministrator GetExperimentAdministrator(int id) {
            ExperimentAdministrator experimentAdmin = null;

            if(id > 0) {
                experimentAdmin = _experimentAdminRepository.GetById(id);
            }

            return experimentAdmin;

        }

        /// <summary>
        /// Get Experiment Administrator from database using Experiment Administrator Id
        /// </summary>
        /// <param name="firstName">First name of the Experiment Administrator</param>
        /// <param name="lastName">Last name of the Experiment Administrator</param>
        /// <returns></returns>
        public ExperimentAdministrator GetExperimentAdministrator(string firstName, string lastName) {
            ExperimentAdministrator experimentAdmin = null;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)) {
                experimentAdmin = _experimentAdminRepository.GetExperimentAdministratorByName(firstName, lastName);
            }

            return experimentAdmin;

        }

        /// <summary>
        /// Add a new ExperimentAdministrator to the database
        /// </summary>
        /// <param name="experimentAdmin">Experiment Administrator object to add to the database</param>
        public void CreateExperimentAdministrator(ExperimentAdministrator experimentAdmin) {
            if(experimentAdmin != null) {
                _experimentAdminRepository.Add(experimentAdmin);
            }
        }

        /// <summary>
        /// Delete an experiment administrator user from the database.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteExperimentAdministrator(int id) {
            if (id > 0) {
                ExperimentAdministrator expAdmin = GetExperimentAdministrator(id);
                if (expAdmin != null) {
                    _experimentAdminRepository.Delete(expAdmin);
                }
            }
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
