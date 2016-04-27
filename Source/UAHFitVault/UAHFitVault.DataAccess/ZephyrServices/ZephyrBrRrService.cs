using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories.ZephyrRepositories;
using UAHFitVault.Database;
using EntityFramework.BulkInsert.Extensions;
using System;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Service operations used to access ZephyrBrRrService Waveform data.
    /// </summary>
    public class ZephyrBrRrService : IZephyrBrRrService
    {
        #region Private Properties

        private readonly IZephyrBrRrRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrBrRrService(IZephyrBrRrRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Zephyr BR RR data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr BR RR Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<ZephyrBRRR> GetZephyrBRRRData(PatientData patientData, int skip = 0, int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id, r => r.TimeStamp, skip, take);
        }

        /// <summary>
        /// Get the Zephyr BR RR data for the given a patient data record or all records for all patients
        /// during the time provided.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr BR RR Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<ZephyrBRRR> GetZephyrBRRRData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id && r.TimeStamp >= startTime && r.TimeStamp <= endTime, r => r.TimeStamp, skip, take);
        }

        /// <summary>
        /// Get Zephyr BR RR data from database using the Zephyr BR RR id
        /// </summary>
        /// <param name="id">Id of the Zephyr BR RR data</param>
        /// <returns></returns>
        public ZephyrBRRR GetZephyrBRRRData(int id) {
            ZephyrBRRR ZephyrBRRR = null;

            if(id > 0) {
                ZephyrBRRR = _repository.GetById(id);
            }

            return ZephyrBRRR;

        }

        /// <summary>
        /// Add a new Zephyr BR RR data record to the database
        /// </summary>
        /// <param name="ZephyrBRRR">ZephyrBRRR object to add to the database</param>
        public void CreateZephyrBRRR(ZephyrBRRR zephyrBRRR) {
            if(zephyrBRRR != null) {
                _repository.Add(zephyrBRRR);
            }
        }

        /// <summary>
        /// Bulk Insert Zephyr BR RR Data into the database
        /// </summary>
        /// <param name="zephyrBRRR">Collection of Zephyr BR RR data to insert into database.</param>
        public void BulkInsert(List<ZephyrBRRR> zephyrBRRR) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(zephyrBRRR);

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
