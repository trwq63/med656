using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Interface for implementing the medical device service.
    /// </summary>
    public interface IMedicalDeviceService
    {
        /// <summary>
        /// Add a new Medical Device to the database
        /// </summary>
        /// <param name="medicalDevice">MedicalDevice object to add to the database</param>
        void CreateMedicalDevice(MedicalDevice medicalDevice);

        /// <summary>
        /// Get all medical devices from the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<MedicalDevice> GetMedicalDevices();

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}