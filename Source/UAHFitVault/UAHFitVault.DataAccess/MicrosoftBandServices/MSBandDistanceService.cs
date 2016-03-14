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
    /// Service operations used to access Microsoft Band Distance Data.
    /// </summary>
    public class MSBandDistanceService : IMSBandDistanceService
    {
        #region Private Properties

        private readonly IMSBandDistanceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public MSBandDistanceService(IMSBandDistanceRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Microsoft Band Distance data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Distance Data records</param>
        /// <returns></returns>
        public IEnumerable<MSBandDistance> GetMSBandDistanceData(PatientData patientData) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id);
        }

        /// <summary>
        /// Get the Microsoft Band Distance data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Distance Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        public IEnumerable<MSBandDistance> GetMSBandDistanceData(PatientData patientData, DateTime startTime, DateTime endTime) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id && r.Date >= startTime && r.Date <= endTime);
        }

        /// <summary>
        /// Get Microsoft Band Distance data from database using the Microsoft Band Distance id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Distance data</param>
        /// <returns></returns>
        public MSBandDistance GetMSBandDistanceData(int id) {
            MSBandDistance msBandDistance = null;

            if(id > 0) {
                msBandDistance = _repository.GetById(id);
            }

            return msBandDistance;

        }

        /// <summary>
        /// Add a new Microsoft Band Distance data record to the database
        /// </summary>
        /// <param name="msBandDistance">MSBandDistance object to add to the database</param>
        public void CreateMSBandDistance(MSBandDistance msBandDistance) {
            if(msBandDistance != null) {
                _repository.Add(msBandDistance);
            }
        }

        /// <summary>
        /// Bulk Insert Microsoft Band Distance Data into the database
        /// </summary>
        /// <param name="msBandDistance">Collection of Microsoft Band summary data to insert into database.</param>
        public void BulkInsert(List<MSBandDistance> msBandDistance) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(msBandDistance);

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
