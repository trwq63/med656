using EntityFramework.BulkInsert.Extensions;
using System;
using System.Collections.Generic;
using UAHFitVault.Database;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Repositories.MicrosoftBandRepositories;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Service operations used to access Microsoft Band Accelerometer Data.
    /// </summary>
    public class MSBandAccelService : IMSBandAccelService
    {
        #region Private Properties

        private readonly IMSBandAccelRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public MSBandAccelService(IMSBandAccelRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Microsoft Band Accelerometer data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Accelerometer Data records</param>
        /// <returns></returns>
        public IEnumerable<MSBandAccelerometer> GetMSBandAccelerometerData(PatientData patientData) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id);
        }

        /// <summary>
        /// Get the Microsoft Band Accelerometer data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Accelerometer Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        public IEnumerable<MSBandAccelerometer> GetMSBandAccelerometerData(PatientData patientData, DateTime startTime, DateTime endTime) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id && r.Date >= startTime && r.Date <= endTime);
        }

        /// <summary>
        /// Get Microsoft Band Accelerometer data from database using the Microsoft Band Accelerometer id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Accelerometer data</param>
        /// <returns></returns>
        public MSBandAccelerometer GetMSBandAccelerometerData(int id) {
            MSBandAccelerometer msBandAccel = null;

            if(id > 0) {
                msBandAccel = _repository.GetById(id);
            }

            return msBandAccel;

        }

        /// <summary>
        /// Add a new Microsoft Band Accelerometer data record to the database
        /// </summary>
        /// <param name="msBandAccel">MSBandAccelerometer object to add to the database</param>
        public void CreateMSBandAccel(MSBandAccelerometer msBandAccel) {
            if(msBandAccel != null) {
                _repository.Add(msBandAccel);
            }
        }

        /// <summary>
        /// Bulk Insert Microsoft Band Acceleromater Data into the database
        /// </summary>
        /// <param name="msBandAccel">Collection of Microsoft Band summary data to insert into database.</param>
        public void BulkInsert(List<MSBandAccelerometer> msBandAccel) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(msBandAccel);

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
