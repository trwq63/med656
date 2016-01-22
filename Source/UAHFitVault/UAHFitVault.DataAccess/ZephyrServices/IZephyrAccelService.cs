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
        /// <returns></returns>
        IEnumerable<ZephyrAccelerometer> GetZephyrAccelerometerData(PatientData patientData);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveCategory();
    }
}