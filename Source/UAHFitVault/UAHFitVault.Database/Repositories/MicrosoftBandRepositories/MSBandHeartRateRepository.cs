using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the MSBandHeartRate model
    /// </summary>
    public class MSBandHeartRateRepository : RepositoryBase<MSBandHeartRate>, IMSBandHeartRateRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MSBandHeartRateRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a Microsoft Band HeartRate records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<MSBandHeartRate> GetMSBandHeartRateByPatientDataId(string id) {
            IEnumerable<MSBandHeartRate> msBandHeartRate = this.DbContext.MSBandHeartRate.Where(p => p.PatientDataId == id);                                                    

            return msBandHeartRate;
        }

        #endregion
    }
}
