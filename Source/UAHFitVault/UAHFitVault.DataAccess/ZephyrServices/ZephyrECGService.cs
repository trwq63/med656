﻿using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;
using UAHFitVault.Database;
using EntityFramework.BulkInsert.Extensions;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Service operations used to access Zephyr ECG Waveform data.
    /// </summary>
    public class ZephyrECGService : IZephyrECGService
    {
        #region Private Properties

        private readonly IZephyrECGRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrECGService(IZephyrECGRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Zephyr ECG WaveForm data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr ECG WaveForm Data records</param>
        /// <returns></returns>
        public IEnumerable<ZephyrECGWaveform> GetZephyrECGWaveFormData(PatientData patientData) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetAll().Where(r => r.PatientDataId == patientData.Id);
        }

        /// <summary>
        /// Get Zephyr ECG WaveForm data from database using the Zephyr ECG WaveForm id
        /// </summary>
        /// <param name="id">Id of the Zephyr ECG WaveForm data</param>
        /// <returns></returns>
        public ZephyrECGWaveform GetZephyrECGWaveFormData(int id) {
            ZephyrECGWaveform zephyrEcg = null;

            if(id > 0) {
                zephyrEcg = _repository.GetById(id);
            }

            return zephyrEcg;

        }

        /// <summary>
        /// Add a new Zephyr ECG WaveForm data record to the database
        /// </summary>
        /// <param name="zephyrEcg">ZephyrECGWaveForm object to add to the database</param>
        public void CreateZephyrECGData(ZephyrECGWaveform zephyrEcg) {
            if(zephyrEcg != null) {
                _repository.Add(zephyrEcg);
            }
        }

        /// <summary>
        /// Bulk Insert Zephyr ECG Waveform Data into the database
        /// </summary>
        /// <param name="zephyrEcgData">Collection of Zephyr summary data to insert into database.</param>
        public void BulkInsert(List<ZephyrECGWaveform> zephyrEcgData) {
            using (FitVaultContext context = new FitVaultContext()) {
                context.BulkInsert(zephyrEcgData);

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