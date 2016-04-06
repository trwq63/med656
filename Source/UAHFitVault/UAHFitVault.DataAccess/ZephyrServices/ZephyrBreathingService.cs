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
    /// Service operations used to access ZephyrBreathing Waveform data.
    /// </summary>
    public class ZephyrBreathingService : IZephyrBreathingService
    {
        #region Private Properties

        private readonly IZephyrBreathingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrBreathingService(IZephyrBreathingRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Zephyr Breathing Waveform data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Breathing Waveform Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<ZephyrBreathingWaveform> GetZephyrBreathingWaveformData(PatientData patientData, int skip = 0, int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id, r => r.Time, skip, take);
        }

        /// <summary>
        /// Get the Zephyr Breathing Waveform data for the given a patient data record or all records for all patients
        /// during the time provided.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Breathing Waveform Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        public IEnumerable<ZephyrBreathingWaveform> GetZephyrBreathingWaveformData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.PatientDataId == patientData.Id && r.Time >= startTime && r.Time <= endTime, r => r.Time, skip, take);
        }

        /// <summary>
        /// Get Zephyr Breathing Waveform data from database using the Zephyr Breathing Waveform id
        /// </summary>
        /// <param name="id">Id of the Zephyr Breathing Waveform data</param>
        /// <returns></returns>
        public ZephyrBreathingWaveform GetZephyrBreathingWaveformData(int id) {
            ZephyrBreathingWaveform zephyrBreathing = null;

            if(id > 0) {
                zephyrBreathing = _repository.GetById(id);
            }

            return zephyrBreathing;

        }

        /// <summary>
        /// Add a new Zephyr Breathing Waveform data record to the database
        /// </summary>
        /// <param name="zephyrBreathing">ZephyrBreathingWaveform object to add to the database</param>
        public void CreateZephyrBreathingWaveform(ZephyrBreathingWaveform zephyrBreathing) {
            if(zephyrBreathing != null) {
                _repository.Add(zephyrBreathing);
            }
        }

        /// <summary>
        /// Bulk Insert Zephyr Breathing Waveform Data into the database
        /// </summary>
        /// <param name="zephyrBreathingData">Collection of Zephyr summary data to insert into database.</param>
        public void BulkInsert(List<ZephyrBreathingWaveform> zephyrBreathingData) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(zephyrBreathingData);

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
