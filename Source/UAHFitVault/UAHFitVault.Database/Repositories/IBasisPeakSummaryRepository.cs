using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface to implement the basis peak summary repository
    /// </summary>
    public interface IBasisPeakSummaryRepository : IRepository<BasisPeakSummaryData>
    {
        /// <summary>
        /// Get a basis peak summary data records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        IEnumerable<BasisPeakSummaryData> GetBasisPeakSummaryByPatientDataId(string id);
    }
}