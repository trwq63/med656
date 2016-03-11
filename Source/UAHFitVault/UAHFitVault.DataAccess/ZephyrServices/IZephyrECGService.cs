using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Interface for the Zephyr ECG Service
    /// </summary>
    public interface IZephyrECGService
    {
        /// <summary>
        /// Add a new Zephyr ECG WaveForm data record to the database
        /// </summary>
        void CreateZephyrECGData(ZephyrECGWaveform zephryEcg);

        /// <summary>
        /// Get the Zephyr ECG WaveForm data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr ECG WaveForm Data records</param>
        /// <returns></returns>
        IEnumerable<ZephyrECGWaveform> GetZephyrECGWaveFormData(PatientData patientData);

        /// <summary>
        /// Get the Zephyr ECG WaveForm data for the given a patient data record or all records for all patients during
        /// the given time period.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr ECG WaveForm Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        IEnumerable<ZephyrECGWaveform> GetZephyrECGWaveFormData(PatientData patientData, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Get Zephyr ECG WaveForm data from database using the Zephyr ECG WaveForm id
        /// </summary>
        /// <param name="id">Id of the Zephyr ECG WaveForm data</param>
        /// <returns></returns>
        ZephyrECGWaveform GetZephyrECGWaveFormData(int id);

        /// <summary>
        /// Bulk Insert Zephyr ECG Waveform Data into the database
        /// </summary>
        /// <param name="zephyrEcgData">Collection of Zephyr summary data to insert into database.</param>
        void BulkInsert(List<ZephyrECGWaveform> zephyrEcgData);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}