using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Interface for the Zephyr Accelerometer service
    /// </summary>
    public interface IZephyrAccelService
    {
        /// <summary>
        /// Add a new Zephyr Accelerometer data record to the database
        /// </summary>
        /// <param name="zephryAccel">ZephyrAccelerometer object to add to the database</param>
        void CreateZephyrAccel(ZephyrAccelerometer zephryAccel);

        /// <summary>
        /// Get Zephyr Accelerometer data from database using the Zephyr Accelerometer id
        /// </summary>
        /// <param name="id">Id of the Zephyr Accelerometer data</param>
        /// <returns></returns>
        ZephyrAccelerometer GetZephyrAccelerometerData(int id);

        /// <summary>
        /// Get the Zephyr Accelerometer data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Accelerometer Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<ZephyrAccelerometer> GetZephyrAccelerometerData(PatientData patientData, int skip = 0, int take = 0);

        /// <summary>
        /// Get the Zephyr Accelerometer data for the given a patient data record or all records for all patients.
        /// Filter what is returned by time.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Accelerometer Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<ZephyrAccelerometer> GetZephyrAccelerometerData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0);

        /// <summary>
        /// Bulk Insert Zephyr Acceleromater Data into the database
        /// </summary>
        /// <param name="zephyrAccel">Collection of Zephyr summary data to insert into database.</param>
        void BulkInsert(List<ZephyrAccelerometer> zephyrAccel);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}