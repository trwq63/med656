using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using System;
using System.Collections.Generic;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the ZephyrAccelerometer model
    /// </summary>
    public class ZephyrAccelRepository : RepositoryBase<ZephyrAccelerometer>, IZephyrAccelRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ZephyrAccelRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a zephyr accelerometer records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<ZephyrAccelerometer> GetZephyrAccelByPatientDataId(Guid id) {
            IEnumerable<ZephyrAccelerometer> zephyrAccel = this.DbContext.ZephyrAccelerometer.Where(p => p.PatentDataId == id);                                                    

            return zephyrAccel;
        }

        #endregion
    }
}
