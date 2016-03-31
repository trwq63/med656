using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Interface for the Microsoft Band Calories service
    /// </summary>
    public interface IMSBandCaloriesService
    {
        /// <summary>
        /// Add a new Microsoft Band Calories data record to the database
        /// </summary>
        /// <param name="msBandCalories">MSBandCalories object to add to the database</param>
        void CreateMSBandCalories(MSBandCalories msBandCalories);

        /// <summary>
        /// Get Microsoft Band Calories data from database using the Microsoft Band Calories id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Calories data</param>
        /// <returns></returns>
        MSBandCalories GetMSBandCaloriesData(int id);

        /// <summary>
        /// Get the Microsoft Band Calories data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Calories Data records</param>
        /// <returns></returns>
        IEnumerable<MSBandCalories> GetMSBandCaloriesData(PatientData patientData);

        /// <summary>
        /// Get the Microsoft Band Calories data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Calories Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        IEnumerable<MSBandCalories> GetMSBandCaloriesData(PatientData patientData, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Bulk Insert Microsoft Band Calories Data into the database
        /// </summary>
        /// <param name="msBandCalories">Collection of Microsoft Band summary data to insert into database.</param>
        void BulkInsert(List<MSBandCalories> msBandCalories);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}