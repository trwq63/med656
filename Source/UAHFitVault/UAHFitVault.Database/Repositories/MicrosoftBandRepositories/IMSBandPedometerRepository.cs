using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Interface to implement Microsoft Band calories repository.
    /// </summary>
    public interface IMSBandPedometerRepository : IRepository<MSBandPedometer>
    {
        /// <summary>
        /// Get a Microsoft Band Pedometer records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<MSBandPedometer> GetMSBandPedometerByPatientDataId(string id);
    }
}