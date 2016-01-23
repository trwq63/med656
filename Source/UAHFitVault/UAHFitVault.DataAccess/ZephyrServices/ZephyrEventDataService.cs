using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Service operations used to access Zephyr Event Data.
    /// </summary>
    public class ZephyrEventDataService : IZephyrEventDataService
    {
        #region Private Properties

        private readonly IRepository<ZephyrEventData> _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrEventDataService(IRepository<ZephyrEventData> repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Zephyr ECG Zephyr Event Data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Event Data records</param>
        /// <returns></returns>
        public IEnumerable<ZephyrEventData> GetZephyrEventData(PatientData patientData) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetAll().Where(r => r.PatientDataId == patientData.Id);
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
        /// Save changes to database
        /// </summary>
        public void SaveCategory() {
            _unitOfWork.Commit();
        }

        #endregion
    }
}
