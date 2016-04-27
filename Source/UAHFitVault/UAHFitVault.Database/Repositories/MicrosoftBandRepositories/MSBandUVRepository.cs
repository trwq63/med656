using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the MSBandUV model
    /// </summary>
    public class MSBandUVRepository : RepositoryBase<MSBandUV>, IMSBandUVRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MSBandUVRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a Microsoft Band UV records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<MSBandUV> GetMSBandUVByPatientDataId(string id) {
            IEnumerable<MSBandUV> msBandUV = this.DbContext.MSBandUV.Where(p => p.PatientDataId == id);                                                    

            return msBandUV;
        }

        #endregion
    }
}
