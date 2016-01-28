using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Service operations used to access data for a medical device
    /// </summary>
    public class MedicalDeviceService : IMedicalDeviceService
    {
        #region Private Properties

        private readonly IMedicalDeviceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Physician Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public MedicalDeviceService(IMedicalDeviceRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all medical devices from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MedicalDevice> GetMedicalDevices() {
             return _repository.GetAll();
        }


        /// <summary>
        /// Add a new medical device to the database
        /// </summary>
        /// <param name="medicalDevice">MedicalDevice object to add to the database</param>
        public void CreateMedicalDevice(MedicalDevice medicalDevice) {
            if(medicalDevice != null) {
                _repository.Add(medicalDevice);
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
