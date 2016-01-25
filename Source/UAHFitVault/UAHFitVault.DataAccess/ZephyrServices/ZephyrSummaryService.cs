using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Service operations used to access Zephyr Summary data.
    /// </summary>
    public class ZephyrSummaryService : IZephyrSummaryService
    {
        #region Private Properties

        private readonly IZephyrSummaryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrSummaryService(IZephyrSummaryRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Zephyr Summary data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Summary Data records</param>
        /// <returns></returns>
        public IEnumerable<ZephyrSummaryData> GetZephyrSummaryData(PatientData patientData) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetAll().Where(r => r.PatientDataId == patientData.Id);
        }

        /// <summary>
        /// Get Zephyr Summary data from database using the Zephyr Summary id
        /// </summary>
        /// <param name="id">Id of the Zephyr Summary data</param>
        /// <returns></returns>
        public ZephyrSummaryData GetZephyrSummaryData(int id) {
            ZephyrSummaryData zephyrSummary = null;

            if(id > 0) {
                zephyrSummary = _repository.GetById(id);
            }

            return zephyrSummary;

        }

        /// <summary>
        /// Add a new Zephyr Summary data record to the database
        /// </summary>
        /// <param name="zephyrSummary">ZephyrSummaryData object to add to the database</param>
        public void CreateZephyrSummary(ZephyrSummaryData zephyrSummary) {
            if(zephyrSummary != null) {
                _repository.Add(zephyrSummary);
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
