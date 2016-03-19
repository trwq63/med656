using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Interface for the Microsoft Band HeartRate service
    /// </summary>
    public interface IMSBandHeartRateService
    {
        /// <summary>
        /// Add a new Microsoft Band HeartRate data record to the database
        /// </summary>
        /// <param name="msBandHeartRate">MSBandHeartRate object to add to the database</param>
        void CreateMSBandHeartRate(MSBandHeartRate msBandHeartRate);

        /// <summary>
        /// Get Microsoft Band HeartRate data from database using the Microsoft Band HeartRate id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band HeartRate data</param>
        /// <returns></returns>
        MSBandHeartRate GetMSBandHeartRateData(int id);

        /// <summary>
        /// Get the Microsoft Band HeartRate data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band HeartRate Data records</param>
        /// <returns></returns>
        IEnumerable<MSBandHeartRate> GetMSBandHeartRateData(PatientData patientData);

        /// <summary>
        /// Get the Microsoft Band HeartRate data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band HeartRate Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        IEnumerable<MSBandHeartRate> GetMSBandHeartRateData(PatientData patientData, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Bulk Insert Microsoft Band HeartRate Data into the database
        /// </summary>
        /// <param name="msBandHeartRate">Collection of Microsoft Band summary data to insert into database.</param>
        void BulkInsert(List<MSBandHeartRate> msBandHeartRate);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}