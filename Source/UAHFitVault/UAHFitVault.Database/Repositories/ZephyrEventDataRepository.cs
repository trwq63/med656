using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using System;
using System.Collections.Generic;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the ZephyrEventData model
    /// </summary>
    public class ZephyrEventDataRepository : RepositoryBase<ZephyrEventData>, IZephyrEventDataRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ZephyrEventDataRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a zephyr event data records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<ZephyrEventData> GetZephyrEventDataByPatientDataId(Guid id) {
            IEnumerable<ZephyrEventData> zephyrEvents = this.DbContext.ZephyrEventData.Where(p => p.PatientDataId == id);                                                    

            return zephyrEvents;
        }

        #endregion
    }
}
