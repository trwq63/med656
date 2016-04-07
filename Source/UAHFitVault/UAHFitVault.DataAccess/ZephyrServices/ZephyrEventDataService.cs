using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories.ZephyrRepositories;
using UAHFitVault.Database;
using EntityFramework.BulkInsert.Extensions;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Service operations used to access Zephyr Event Data.
    /// </summary>
    public class ZephyrEventDataService : IZephyrEventDataService
    {
        #region Private Properties

        private readonly IZephyrEventDataRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrEventDataService(IZephyrEventDataRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Zephyr ECG Zephyr Event Data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Event Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<ZephyrEventData> GetZephyrEventData(PatientData patientData, int skip = 0, int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id, r => r.Time, skip, take);
        }

        /// <summary>
        /// Get Zephyr Event Data from database using the Zephyr Event Data id
        /// </summary>
        /// <param name="id">Id of the Zephyr Event Data</param>
        /// <returns></returns>
        public ZephyrEventData GetZephyrEventData(int id) {
            ZephyrEventData zephyrEvent = null;

            if(id > 0) {
                zephyrEvent = _repository.GetById(id);
            }

            return zephyrEvent;

        }

        /// <summary>
        /// Add a new Zephyr Event Data record to the database
        /// </summary>
        /// <param name="zephyrEvent">ZephyrECGWaveForm object to add to the database</param>
        public void CreateZephyrEventData(ZephyrEventData zephyrEvent) {
            if(zephyrEvent != null) {
                _repository.Add(zephyrEvent);
            }
        }

        /// <summary>
        /// Bulk Insert Zephyr Event Data into the database
        /// </summary>
        /// <param name="zephyrEventData">Collection of Zephyr summary data to insert into database.</param>
        public void BulkInsert(List<ZephyrEventData> zephyrEventData) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(zephyrEventData);

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
