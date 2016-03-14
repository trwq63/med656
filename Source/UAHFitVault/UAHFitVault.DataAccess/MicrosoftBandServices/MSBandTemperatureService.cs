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
    /// Service operations used to access Microsoft Band Temperature Data.
    /// </summary>
    public class MSBandTemperatureService : IMSBandTemperatureService
    {
        #region Private Properties

        private readonly IMSBandTemperatureRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public MSBandTemperatureService(IMSBandTemperatureRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Microsoft Band Temperature data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Temperature Data records</param>
        /// <returns></returns>
        public IEnumerable<MSBandTemperature> GetMSBandTemperatureData(PatientData patientData) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id);
        }

        /// <summary>
        /// Get the Microsoft Band Temperature data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Temperature Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        public IEnumerable<MSBandTemperature> GetMSBandTemperatureData(PatientData patientData, DateTime startTime, DateTime endTime) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id && r.Date >= startTime && r.Date <= endTime);
        }

        /// <summary>
        /// Get Microsoft Band Temperature data from database using the Microsoft Band Temperature id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Temperature data</param>
        /// <returns></returns>
        public MSBandTemperature GetMSBandTemperatureData(int id) {
            MSBandTemperature msBandTemperature = null;

            if(id > 0) {
                msBandTemperature = _repository.GetById(id);
            }

            return msBandTemperature;

        }

        /// <summary>
        /// Add a new Microsoft Band Temperature data record to the database
        /// </summary>
        /// <param name="msBandTemperature">MSBandTemperature object to add to the database</param>
        public void CreateMSBandTemperature(MSBandTemperature msBandTemperature) {
            if(msBandTemperature != null) {
                _repository.Add(msBandTemperature);
            }
        }

        /// <summary>
        /// Bulk Insert Microsoft Band Temperature Data into the database
        /// </summary>
        /// <param name="msBandTemperature">Collection of Microsoft Band summary data to insert into database.</param>
        public void BulkInsert(List<MSBandTemperature> msBandTemperature) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(msBandTemperature);

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
