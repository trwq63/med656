﻿using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Interface to implement Microsoft Band Temperature repository.
    /// </summary>
    public interface IMSBandTemperatureRepository : IRepository<MSBandTemperature>
    {
        /// <summary>
        /// Get a Microsoft Band Temperature records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<MSBandTemperature> GetMSBandTemperatureByPatientDataId(string id);
    }
}