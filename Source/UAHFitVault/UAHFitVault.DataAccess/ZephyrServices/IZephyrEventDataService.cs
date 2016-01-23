using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Interface for Zephyr Event Data Service
    /// </summary>
    public interface IZephyrEventDataService
    {
        /// <summary>
        /// Add a new Zephyr Event Data record to the database
        /// </summary>
        /// <param name="zephyrEvent">ZephyrECGWaveForm object to add to the database</param>
        void CreateZephyrEventData(ZephyrEventData zephyrEvent);

        /// <summary>
        /// Get the Zephyr ECG Zephyr Event Data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Event Data records</param>
        /// <returns></returns>
        IEnumerable<ZephyrEventData> GetZephyrEventData(PatientData patientData);

        /// <summary>
        /// Get Zephyr Event Data from database using the Zephyr Event Data id
        /// </summary>
        /// <param name="id">Id of the Zephyr Event Data</param>
        /// <returns></returns>
        ZephyrEventData GetZephyrEventData(int id);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveCategory();
    }
}