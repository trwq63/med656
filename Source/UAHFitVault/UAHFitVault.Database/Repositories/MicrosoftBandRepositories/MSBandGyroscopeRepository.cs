using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the MSBandGryoscope model
    /// </summary>
    public class MSBandGyroscopeRepository : RepositoryBase<MSBandGryoscope>, IMSBandGyroscopeRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MSBandGyroscopeRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a Microsoft Band Gyroscope records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<MSBandGryoscope> GetMSBandGyroscopeByPatientDataId(string id) {
            IEnumerable<MSBandGryoscope> msBandGyroscope = this.DbContext.MSBandGyroscrope.Where(p => p.PatientDataId == id);                                                    

            return msBandGyroscope;
        }

        #endregion
    }
}
