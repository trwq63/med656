using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface used to implement Zephyr Event Data repository
    /// </summary>
    public interface IZephyrEventDataRepository : IRepository<ZephyrEventData>
    {
        /// <summary>
        /// Get a zephyr event data records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<ZephyrEventData> GetZephyrEventDataByPatientDataId(Guid id);
    }
}