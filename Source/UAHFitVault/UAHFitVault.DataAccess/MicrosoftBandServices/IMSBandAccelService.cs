using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.MicrosoftBandServices
{
    /// <summary>
    /// Interface for the Microsoft Band Accelerometer service
    /// </summary>
    public interface IMSBandAccelService
    {
        /// <summary>
        /// Add a new Microsoft Band Accelerometer data record to the database
        /// </summary>
        /// <param name="msBandAccel">MSBandAccelerometer object to add to the database</param>
        void CreateMSBandAccel(MSBandAccelerometer msBandAccel);

        /// <summary>
        /// Get Microsoft Band Accelerometer data from database using the Microsoft Band Accelerometer id
        /// </summary>
        /// <param name="id">Id of the Microsoft Band Accelerometer data</param>
        /// <returns></returns>
        MSBandAccelerometer GetMSBandAccelerometerData(int id);

        /// <summary>
        /// Get the Microsoft Band Accelerometer data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Accelerometer Data records</param>
        /// <returns></returns>
        IEnumerable<MSBandAccelerometer> GetMSBandAccelerometerData(PatientData patientData);

        /// <summary>
        /// Get the Microsoft Band Accelerometer data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Microsoft Band Accelerometer Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <returns></returns>
        IEnumerable<MSBandAccelerometer> GetMSBandAccelerometerData(PatientData patientData, DateTime startTime, DateTime endTime);

        /// <summary>
        /// Bulk Insert Microsoft Band Acceleromater Data into the database
        /// </summary>
        /// <param name="msBandAccel">Collection of Microsoft Band summary data to insert into database.</param>
        void BulkInsert(List<MSBandAccelerometer> msBandAccel);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}