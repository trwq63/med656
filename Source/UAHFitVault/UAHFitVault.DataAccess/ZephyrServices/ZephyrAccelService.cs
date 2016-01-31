using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Service operations used to access Zephyr Accelerometer Data.
    /// </summary>
    public class ZephyrAccelService : IZephyrAccelService
    {
        #region Private Properties

        private readonly IZephyrAccelRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrAccelService(IZephyrAccelRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Zephyr Accelerometer data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Accelerometer Data records</param>
        /// <returns></returns>
        public IEnumerable<ZephyrAccelerometer> GetZephyrAccelerometerData(PatientData patientData) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetAll().Where(r => r.PatentDataId == patientData.Id);
        }

        /// <summary>
        /// Get Zephyr Accelerometer data from database using the Zephyr Accelerometer id
        /// </summary>
        /// <param name="id">Id of the Zephyr Accelerometer data</param>
        /// <returns></returns>
        public ZephyrAccelerometer GetZephyrAccelerometerData(int id) {
            ZephyrAccelerometer zephyrAccel = null;

            if(id > 0) {
                zephyrAccel = _repository.GetById(id);
            }

            return zephyrAccel;

        }

        /// <summary>
        /// Add a new Zephyr Accelerometer data record to the database
        /// </summary>
        /// <param name="zephryAccel">ZephyrAccelerometer object to add to the database</param>
        public void CreateZephyrAccel(ZephyrAccelerometer zephyrAccel) {
            if(zephyrAccel != null) {
                _repository.Add(zephyrAccel);
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
