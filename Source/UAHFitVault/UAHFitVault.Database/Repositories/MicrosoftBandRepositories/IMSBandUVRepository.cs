﻿using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Interface to implement Microsoft Band UV repository.
    /// </summary>
    public interface IMSBandUVRepository : IRepository<MSBandUV>
    {
        /// <summary>
        /// Get a Microsoft Band UV records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<MSBandUV> GetMSBandUVByPatientDataId(string id);
    }
}