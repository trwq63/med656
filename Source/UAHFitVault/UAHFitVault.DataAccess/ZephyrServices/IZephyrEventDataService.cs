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
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<ZephyrEventData> GetZephyrEventData(PatientData patientData, int skip = 0, int take = 0);

        /// <summary>
        /// Get Zephyr Event Data from database using the Zephyr Event Data id
        /// </summary>
        /// <param name="id">Id of the Zephyr Event Data</param>
        /// <returns></returns>
        ZephyrEventData GetZephyrEventData(int id);

        /// <summary>
        /// Bulk Insert Zephyr Event Data into the database
        /// </summary>
        /// <param name="zephyrEventData">Collection of Zephyr summary data to insert into database.</param>
        void BulkInsert(List<ZephyrEventData> zephyrEventData);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}