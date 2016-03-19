using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the MSBandAccelerometer model
    /// </summary>
    public class MSBandAccelRepository : RepositoryBase<MSBandAccelerometer>, IMSBandAccelRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MSBandAccelRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a Microsoft Band accelerometer records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<MSBandAccelerometer> GetMSBandAccelByPatientDataId(string id) {
            IEnumerable<MSBandAccelerometer> msBandAccel = this.DbContext.MSBandAccelerometer.Where(p => p.PatientDataId == id);                                                    

            return msBandAccel;
        }

        #endregion
    }
}
