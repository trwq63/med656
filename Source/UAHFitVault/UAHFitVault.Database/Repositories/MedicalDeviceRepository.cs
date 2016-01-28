using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the medical device model
    /// </summary>
    public class MedicalDeviceRepository : RepositoryBase<MedicalDevice>, IMedicalDeviceRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public MedicalDeviceRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

    }
}
