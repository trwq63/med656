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
        /// Get Zephyr Breathing Waveform data from database using the Zephyr Breathing Waveform id
        /// </summary>
        /// <param name="id">Id of the Zephyr Breathing Waveform data</param>
        /// <returns></returns>
        ZephyrBreathingWaveform GetZephyrBreathingWaveformData(int id);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}