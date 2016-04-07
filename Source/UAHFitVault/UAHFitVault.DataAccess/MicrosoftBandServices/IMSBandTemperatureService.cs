using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Interface for the Microsoft Band Temperature service
    /// </summary>
    public interface IMSBandTemperatureService
    {
        /// <summary>
        /// Add a new Microsoft Band Temperature data record to the database
        /// </summary>
        /// <param name="msBandTemperature">MSBandTemperature object to add to the database</param>
        void CreateMSBandTemperature(MSBandTemperature msBandTemperature);

        /// <summary>
        /// Get Microsoft Band Temperature data from database using the Microsoft Band Temperature id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Temperature data</param>
        /// <returns></returns>
        MSBandTemperature GetMSBandTemperatureData(int id);

        /// <summary>
        /// Get the Microsoft Band Temperature data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Temperature Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<MSBandTemperature> GetMSBandTemperatureData(PatientData patientData, int skip = 0, int take = 0);

        /// <summary>
        /// Get the Microsoft Band Temperature data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Temperature Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<MSBandTemperature> GetMSBandTemperatureData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0);

        /// <summary>
        /// Bulk Insert Microsoft Band Temperature Data into the database
        /// </summary>
        /// <param name="msBandTemperature">Collection of Microsoft Band summary data to insert into database.</param>
        void BulkInsert(List<MSBandTemperature> msBandTemperature);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}