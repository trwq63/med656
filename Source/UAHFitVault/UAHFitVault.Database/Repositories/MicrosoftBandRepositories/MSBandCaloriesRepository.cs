using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories.MicrosoftBandRepositories
{
    /// <summary>
    /// Implementation of the repository base class for the MSBandCalories model
    /// </summary>
    public class MSBandCaloriesRepository : RepositoryBase<MSBandCalories>, IMSBandCaloriesRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MSBandCaloriesRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a Microsoft Band calories records using patient data record id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public IEnumerable<MSBandCalories> GetMSBandCaloriesByPatientDataId(string id) {
            IEnumerable<MSBandCalories> msBandCalories = this.DbContext.MSBandCalories.Where(p => p.PatientDataId == id);                                                    

            return msBandCalories;
        }

        #endregion
    }
}
