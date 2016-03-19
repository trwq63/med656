using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Interface for the Microsoft Band Gyroscope service
    /// </summary>
    public interface IMSBandGyroscopeService
    {
        /// <summary>
        /// Add a new Microsoft Band Gyroscope data record to the database
        /// </summary>
        /// <param name="msBandGyroscope">MSBandGyroscope object to add to the database</param>
        void CreateMSBandGyroscope(MSBandGyroscope msBandGyroscope);

        /// <summary>
        /// Get Microsoft Band Gyroscope data from database using the Microsoft Band Gyroscope id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Gyroscope data</param>
        /// <returns></returns>
        MSBandGyroscope GetMSBandGyroscopeData(int id);

        /// <summary>
        /// Get the Microsoft Band Gyroscope data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Gyroscope Data records</param>
        /// <returns></returns>
        IEnumerable<MSBandGyroscope> GetMSBandGyroscopeData(PatientData patientData);

        /// <summary>
        /// Get the Microsoft Band Gyroscope data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Gyroscope Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        IEnumerable<MSBandGyroscope> GetMSBandGyroscopeData(PatientData patientData, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Bulk Insert Microsoft Band Gyroscope Data into the database
        /// </summary>
        /// <param name="msBandGyroscope">Collection of Microsoft Band summary data to insert into database.</param>
        void BulkInsert(List<MSBandGyroscope> msBandGyroscope);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}