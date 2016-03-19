using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the MSBandPedometer model
    /// </summary>
    public class MSBandPedometerRepository : RepositoryBase<MSBandPedometer>, IMSBandPedometerRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MSBandPedometerRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a Microsoft Band Pedometer records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<MSBandPedometer> GetMSBandPedometerByPatientDataId(string id) {
            IEnumerable<MSBandPedometer> msBandPedometer = this.DbContext.MSBandPedometer.Where(p => p.PatientDataId == id);                                                    

            return msBandPedometer;
        }

        #endregion
    }
}
