using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using System;
using System.Collections.Generic;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the BasisPeakSummaryData model
    /// </summary>
    public class BasisPeakSummaryRepository : RepositoryBase<BasisPeakSummaryData>, IBasisPeakSummaryRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public BasisPeakSummaryRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a basis peak summary data records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<BasisPeakSummaryData> GetBasisPeakSummaryByPatientDataId(string id) {
            IEnumerable<BasisPeakSummaryData> basisSummaries = this.DbContext.BasisPeakSummaryData.Where(p => p.PatientDataId == id);                                                    

            return basisSummaries;
        }

        #endregion
    }
}
