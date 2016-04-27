using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Interface to implement Microsoft Band Gyroscope repository.
    /// </summary>
    public interface IMSBandGyroscopeRepository : IRepository<MSBandGyroscope>
    {
        /// <summary>
        /// Get a Microsoft Band Gyroscope records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<MSBandGyroscope> GetMSBandGyroscopeByPatientDataId(string id);
    }
}