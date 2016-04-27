using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the MSBandDistance model
    /// </summary>
    public class MSBandDistanceRepository : RepositoryBase<MSBandDistance>, IMSBandDistanceRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MSBandDistanceRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a Microsoft Band distance records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<MSBandDistance> GetMSBandDistanceByPatientDataId(string id) {
            IEnumerable<MSBandDistance> msBandDistance = this.DbContext.MSBandDistance.Where(p => p.PatientDataId == id);                                                    

            return msBandDistance;
        }

        #endregion
    }
}
