using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    public interface IZephyrBreathingService
    {
        /// <summary>
        /// Add a new Zephyr Breathing Waveform data record to the database
        /// </summary>
        /// <param name="zephryBreathing">ZephyrBreathingWaveform object to add to the database</param>
        void CreateZephyrBreathingWaveform(ZephyrBreathingWaveform zephryBreathing);

        /// <summary>
        /// Get the Zephyr Breathing Waveform data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Breathing Waveform Data records</param>
        /// <returns></returns>
        IEnumerable<ZephyrBreathingWaveform> GetZephyrBreathingWaveformData(PatientData patientData);

        /// <summary>
        /// Get the Zephyr Breathing Waveform data for the given a patient data record or all records for all patients
        /// during the time provided.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Breathing Waveform Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        IEnumerable<ZephyrBreathingWaveform> GetZephyrBreathingWaveformData(PatientData patientData, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Get Zephyr Breathing Waveform data from database using the Zephyr Breathing Waveform id
        /// </summary>
        /// <param name="id">Id of the Zephyr Breathing Waveform data</param>
        /// <returns></returns>
        ZephyrBreathingWaveform GetZephyrBreathingWaveformData(int id);

        /// <summary>
        /// Bulk Insert Zephyr Breathing Waveform Data into the database
        /// </summary>
        /// <param name="zephyrBreathingData">Collection of Zephyr summary data to insert into database.</param>
        void BulkInsert(List<ZephyrBreathingWaveform> zephyrBreathingData);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}