using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface to implement zephyr ecg repository
    /// </summary>
    public interface IZephyrECGRepository : IRepository<ZephyrECGWaveform>
    {
        /// <summary>
        /// Get a zephyr ecg waveform records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<ZephyrECGWaveform> GetZephyrECGByPatientDataId(string id);
    }
}