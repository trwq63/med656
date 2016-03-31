using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Interface for the Microsoft Band Distance service
    /// </summary>
    public interface IMSBandDistanceService
    {
        /// <summary>
        /// Add a new Microsoft Band Distance data record to the database
        /// </summary>
        /// <param name="msBandDistance">MSBandDistance object to add to the database</param>
        void CreateMSBandDistance(MSBandDistance msBandDistance);

        /// <summary>
        /// Get Microsoft Band Distance data from database using the Microsoft Band Distance id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Distance data</param>
        /// <returns></returns>
        MSBandDistance GetMSBandDistanceData(int id);

        /// <summary>
        /// Get the Microsoft Band Distance data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Distance Data records</param>
        /// <returns></returns>
        IEnumerable<MSBandDistance> GetMSBandDistanceData(PatientData patientData);

        /// <summary>
        /// Get the Microsoft Band Distance data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Distance Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        IEnumerable<MSBandDistance> GetMSBandDistanceData(PatientData patientData, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Bulk Insert Microsoft Band Distance Data into the database
        /// </summary>
        /// <param name="msBandDistance">Collection of Microsoft Band summary data to insert into database.</param>
        void BulkInsert(List<MSBandDistance> msBandDistance);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}