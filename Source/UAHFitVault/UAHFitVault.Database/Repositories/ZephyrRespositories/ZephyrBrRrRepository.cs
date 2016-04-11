using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.ZephyrRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the IZephyrBrRrRepository model
    /// </summary>
    public class ZephyrBrRrRepository : RepositoryBase<ZephyrBRRR>, IZephyrBrRrRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ZephyrBrRrRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a zephyr br rr waveform records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<ZephyrBRRR> GetZephyrBrRrByPatientDataId(string id) {
            IEnumerable<ZephyrBRRR> zephyrBrRr = this.DbContext.ZephyrBRRRData.Where(p => p.PatientDataId == id);                                                    

            return zephyrBrRr;
        }

        #endregion
    }
}
