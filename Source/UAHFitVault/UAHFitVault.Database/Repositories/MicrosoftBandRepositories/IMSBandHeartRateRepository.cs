using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Interface to implement Microsoft Band HeartRate repository.
    /// </summary>
    public interface IMSBandHeartRateRepository : IRepository<MSBandHeartRate>
    {
        /// <summary>
        /// Get a Microsoft Band HeartRate records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<MSBandHeartRate> GetMSBandHeartRateByPatientDataId(string id);
    }
}