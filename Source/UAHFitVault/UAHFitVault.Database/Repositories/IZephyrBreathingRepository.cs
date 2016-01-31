using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface used to implement zephyr breating repository
    /// </summary>
    public interface IZephyrBreathingRepository : IRepository<ZephyrBreathingWaveform>
    {
        /// <summary>
        /// Get a zephyr breathing waveform records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<ZephyrBreathingWaveform> GetZephyrBreathingByPatientDataId(Guid id);
    }
}