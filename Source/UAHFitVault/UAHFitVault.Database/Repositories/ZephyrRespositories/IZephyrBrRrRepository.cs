using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.ZephyrRepositories
{
    /// <summary>
    /// Interface used to implement zephyr br rr repository
    /// </summary>
    public interface IZephyrBrRrRepository : IRepository<ZephyrBRRR>
    {
        /// <summary>
        /// Get a zephyr br rr waveform records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<ZephyrBRRR> GetZephyrBrRrByPatientDataId(string id);
    }
}