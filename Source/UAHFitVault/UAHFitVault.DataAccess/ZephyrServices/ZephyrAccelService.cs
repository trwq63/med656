using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Service operations used to access patient data.
    /// </summary>
    public class ZephyrAccelService : IZephyrAccelService
    {
        #region Private Properties

        private readonly IRepository<ZephyrAccelerometer> _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrAccelService(IRepository<ZephyrAccelerometer> repository, IUnitOfWork unitOfWork) {
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
            ZephyrAccelerometer zephryAccel = null;

            if(id > 0) {
                zephryAccel = _repository.GetById(id);
            }

            return zephryAccel;

        }

        /// <summary>
        /// Add a new Zephyr Accelerometer data record to the database
        /// </summary>
        /// <param name="zephryAccel">ZephyrAccelerometer object to add to the database</param>
        public void CreateZephyrAccel(ZephyrAccelerometer zephryAccel) {
            if(zephryAccel != null) {
                _repository.Add(zephryAccel);
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
