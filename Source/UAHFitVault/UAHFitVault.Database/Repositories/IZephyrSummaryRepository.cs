using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface to implement the zephyr summary data repository
    /// </summary>
    public interface IZephyrSummaryRepository : IRepository<ZephyrSummaryData>
    {
        /// <summary>
        /// Get a zephyr summary data records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<ZephyrSummaryData> GetZephyrSummaryByPatientDataId(string id);
    }
}