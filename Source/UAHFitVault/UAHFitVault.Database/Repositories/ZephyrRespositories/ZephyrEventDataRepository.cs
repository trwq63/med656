using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.ZephyrRepositories
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
        public IEnumerable<ZephyrEventData> GetZephyrEventDataByPatientDataId(string id) {
            IEnumerable<ZephyrEventData> zephyrEvents = this.DbContext.ZephyrEventData.Where(p => p.PatientDataId == id);                                                    

            return zephyrEvents;
        }

        #endregion
    }
}
