using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using System;
using System.Collections.Generic;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the ZephyrBreathingWaveform model
    /// </summary>
    public class ZephyrBreathingRepository : RepositoryBase<ZephyrBreathingWaveform>, IZephyrBreathingRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ZephyrBreathingRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a zephyr breathing waveform records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<ZephyrBreathingWaveform> GetZephyrBreathingByPatientDataId(string id) {
            IEnumerable<ZephyrBreathingWaveform> zephyrBreathing = this.DbContext.ZephyrBreathingWaveform.Where(p => p.PatientDataId == id);                                                    

            return zephyrBreathing;
        }

        #endregion
    }
}
