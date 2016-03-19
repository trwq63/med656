using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Interface for the Microsoft Band UV service
    /// </summary>
    public interface IMSBandUVService
    {
        /// <summary>
        /// Add a new Microsoft Band UV data record to the database
        /// </summary>
        /// <param name="msBandUV">MSBandUV object to add to the database</param>
        void CreateMSBandUV(MSBandUV msBandUV);

        /// <summary>
        /// Get Microsoft Band UV data from database using the Microsoft Band UV id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band UV data</param>
        /// <returns></returns>
        MSBandUV GetMSBandUVData(int id);

        /// <summary>
        /// Get the Microsoft Band UV data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band UV Data records</param>
        /// <returns></returns>
        IEnumerable<MSBandUV> GetMSBandUVData(PatientData patientData);

        /// <summary>
        /// Get the Microsoft Band UV data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band UV Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        IEnumerable<MSBandUV> GetMSBandUVData(PatientData patientData, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Bulk Insert Microsoft Band UV Data into the database
        /// </summary>
        /// <param name="msBandUV">Collection of Microsoft Band summary data to insert into database.</param>
        void BulkInsert(List<MSBandUV> msBandUV);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}