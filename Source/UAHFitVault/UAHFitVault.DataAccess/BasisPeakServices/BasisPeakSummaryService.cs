using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories.BasisPeakRepositories;
using UAHFitVault.Database;
using EntityFramework.BulkInsert.Extensions;
using System;

namespace UAHFitVault.DataAccess.BasisPeakServices
{
    /// <summary>
    /// Service operations used to access BasisPeak Summary data.
    /// </summary>
    public class BasisPeakSummaryService : IBasisPeakSummaryService
    {
        #region Private Properties

        private readonly IBasisPeakSummaryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public BasisPeakSummaryService(IBasisPeakSummaryRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the BasisPeak Summary data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the BasisPeak Summary Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<BasisPeakSummaryData> GetBasisPeakSummaryData(PatientData patientData, int skip = 0 , int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id, r => r.Date, skip, take);
        }

        /// <summary>
        /// Get the BasisPeak Summary data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the BasisPeak Summary Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<BasisPeakSummaryData> GetBasisPeakSummaryData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id && r.Date >= startTime && r.Date <= endTime, r => r.Date, skip, take);
        }


        /// <summary>
        /// Get BasisPeak Summary data from database using the BasisPeak Summary id
        /// </summary>
        /// <param name="id">Id of the BasisPeak Summary data</param>
        /// <returns></returns>
        public BasisPeakSummaryData GetBasisPeakSummaryData(int id) {
            BasisPeakSummaryData BasisPeakSummary = null;

            if(id > 0) {
                BasisPeakSummary = _repository.GetById(id);
            }

            return BasisPeakSummary;

        }

        /// <summary>
        /// Add a new BasisPeak Summary data record to the database
        /// </summary>
        /// <param name="BasisPeakSummary">BasisPeakSummaryData object to add to the database</param>
        public void CreateBasisPeakSummary(BasisPeakSummaryData BasisPeakSummary) {
            if(BasisPeakSummary != null) {
                _repository.Add(BasisPeakSummary);
            }
        }

        /// <summary>
        /// Bulk Insert BasisPeak Summary Data into the database
        /// </summary>
        /// <param name="basisPeakSummary">Collection of Zephyr summary data to insert into database.</param>
        public void BulkInsert(List<BasisPeakSummaryData> basisPeakSummary) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(basisPeakSummary);

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
