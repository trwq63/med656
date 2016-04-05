using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Interface for the Microsoft Band Pedometer service
    /// </summary>
    public interface IMSBandPedometerService
    {
        /// <summary>
        /// Add a new Microsoft Band Pedometer data record to the database
        /// </summary>
        /// <param name="msBandPedometer">MSBandPedometer object to add to the database</param>
        void CreateMSBandPedometer(MSBandPedometer msBandPedometer);

        /// <summary>
        /// Get Microsoft Band Pedometer data from database using the Microsoft Band Pedometer id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Pedometer data</param>
        /// <returns></returns>
        MSBandPedometer GetMSBandPedometerData(int id);

        /// <summary>
        /// Get the Microsoft Band Pedometer data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Pedometer Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<MSBandPedometer> GetMSBandPedometerData(PatientData patientData, int skip = 0, int take = 0);

        /// <summary>
        /// Get the Microsoft Band Pedometer data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Pedometer Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<MSBandPedometer> GetMSBandPedometerData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0);

        /// <summary>
        /// Bulk Insert Microsoft Band Pedometer Data into the database
        /// </summary>
        /// <param name="msBandPedometer">Collection of Microsoft Band summary data to insert into database.</param>
        void BulkInsert(List<MSBandPedometer> msBandPedometer);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}