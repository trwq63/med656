using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Interface to implement Microsoft Band accel repository.
    /// </summary>
    public interface IMSBandAccelRepository : IRepository<MSBandAccelerometer>
    {
        /// <summary>
        /// Get a Microsoft Band accelerometer records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<MSBandAccelerometer> GetMSBandAccelByPatientDataId(string id);
    }
}