using System;
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
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<ZephyrSummaryData> GetZephyrSummaryData(PatientData patientData, int skip = 0, int take = 0);

        /// <summary>
        /// Get the Zephyr Summary data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Summary Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<ZephyrSummaryData> GetZephyrSummaryData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0);

        /// <summary>
        /// Get Zephyr Summary data from database using the Zephyr Summary id
        /// </summary>
        /// <param name="id">Id of the Zephyr Summary data</param>
        /// <returns></returns>
        ZephyrSummaryData GetZephyrSummaryData(int id);

        /// <summary>
        /// Bulk Insert Zephyr Summary Data into the database
        /// </summary>
        /// <param name="zephyrSummaryData">Collection of Zephyr summary data to insert into database.</param>
        void BulkInsert(List<ZephyrSummaryData> zephyrSummaryData);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}