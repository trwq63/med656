using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface to implement zephyr accel repository.
    /// </summary>
    public interface IZephyrAccelRepository : IRepository<ZephyrAccelerometer>
    {
        /// <summary>
        /// Get a zephyr accelerometer records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<ZephyrAccelerometer> GetZephyrAccelByPatientDataId(string id);
    }
}