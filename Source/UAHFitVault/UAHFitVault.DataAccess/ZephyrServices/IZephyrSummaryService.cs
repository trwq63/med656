using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Interface for Zephyr Summary Service
    /// </summary>
    public interface IZephyrSummaryService
    {
        /// <summary>
        /// Add a new Zephyr Summary data record to the database
        /// </summary>
        /// <param name="zephyrSummary">ZephyrSummaryData object to add to the database</param>
        void CreateZephyrSummary(ZephyrSummaryData zephyrSummary);

        /// <summary>
        /// Get the Zephyr Summary data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Summary Data records</param>
        /// <returns></returns>
        IEnumerable<ZephyrSummaryData> GetZephyrSummaryData(PatientData patientData);

        /// <summary>
        /// Get Zephyr Summary data from database using the Zephyr Summary id
        /// </summary>
        /// <param name="id">Id of the Zephyr Summary data</param>
        /// <returns></returns>
        ZephyrSummaryData GetZephyrSummaryData(int id);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveCategory();
    }
}