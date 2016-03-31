using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the MSBandTemperature model
    /// </summary>
    public class MSBandTemperatureRepository : RepositoryBase<MSBandTemperature>, IMSBandTemperatureRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MSBandTemperatureRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a Microsoft Band Temperature records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<MSBandTemperature> GetMSBandTemperatureByPatientDataId(string id) {
            IEnumerable<MSBandTemperature> msBandTemperature = this.DbContext.MSBandTemperature.Where(p => p.PatientDataId == id);                                                    

            return msBandTemperature;
        }

        #endregion
    }
}
