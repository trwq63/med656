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
    /// Service operations used to access Microsoft Band HeartRate Data.
    /// </summary>
    public class MSBandHeartRateService : IMSBandHeartRateService
    {
        #region Private Properties

        private readonly IMSBandHeartRateRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public MSBandHeartRateService(IMSBandHeartRateRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Microsoft Band HeartRate data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band HeartRate Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<MSBandHeartRate> GetMSBandHeartRateData(PatientData patientData, int skip = 0, int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id, r => r.Date, skip, take);
        }

        /// <summary>
        /// Get the Microsoft Band HeartRate data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band HeartRate Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<MSBandHeartRate> GetMSBandHeartRateData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id && r.Date >= startTime && r.Date <= endTime, r => r.Date, skip, take);
        }

        /// <summary>
        /// Get Microsoft Band HeartRate data from database using the Microsoft Band HeartRate id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band HeartRate data</param>
        /// <returns></returns>
        public MSBandHeartRate GetMSBandHeartRateData(int id) {
            MSBandHeartRate msBandHeartRate = null;

            if(id > 0) {
                msBandHeartRate = _repository.GetById(id);
            }

            return msBandHeartRate;

        }

        /// <summary>
        /// Add a new Microsoft Band HeartRate data record to the database
        /// </summary>
        /// <param name="msBandHeartRate">MSBandHeartRate object to add to the database</param>
        public void CreateMSBandHeartRate(MSBandHeartRate msBandHeartRate) {
            if(msBandHeartRate != null) {
                _repository.Add(msBandHeartRate);
            }
        }

        /// <summary>
        /// Bulk Insert Microsoft Band HeartRate Data into the database
        /// </summary>
        /// <param name="msBandHeartRate">Collection of Microsoft Band summary data to insert into database.</param>
        public void BulkInsert(List<MSBandHeartRate> msBandHeartRate) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(msBandHeartRate);

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
