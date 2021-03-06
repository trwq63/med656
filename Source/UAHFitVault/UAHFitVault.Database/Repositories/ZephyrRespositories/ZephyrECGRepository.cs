﻿using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.ZephyrRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the ZephyrECGWaveform model
    /// </summary>
    public class ZephyrECGRepository : RepositoryBase<ZephyrECGWaveform>, IZephyrECGRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ZephyrECGRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a zephyr ecg waveform records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<ZephyrECGWaveform> GetZephyrECGByPatientDataId(string id) {
            IEnumerable<ZephyrECGWaveform> zephyrEcg = this.DbContext.ZephyrECGWaveform.Where(p => p.PatientDataId == id);                                                    

            return zephyrEcg;
        }

        #endregion
    }
}
