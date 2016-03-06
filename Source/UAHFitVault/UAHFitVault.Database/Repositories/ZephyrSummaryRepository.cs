using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using System;
using System.Collections.Generic;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the ZephyrSummaryData model
    /// </summary>
    public class ZephyrSummaryRepository : RepositoryBase<ZephyrSummaryData>, IZephyrSummaryRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ZephyrSummaryRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a zephyr summary data records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<ZephyrSummaryData> GetZephyrSummaryByPatientDataId(string id) {
            IEnumerable<ZephyrSummaryData> zephyrSummaries = this.DbContext.ZephyrSummaryData.Where(p => p.PatientDataId == id);                                                    

            return zephyrSummaries;
        }

        #endregion
    }
}
