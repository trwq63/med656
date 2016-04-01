using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the activity model
    /// </summary>
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ActivityRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get an activity using its ID.
        /// </summary>
        /// <param name="id">ID of the activity</param>
        /// <returns>Account Request</returns>
        public Activity GetActivity(int id) {
            Activity Activity = this.DbContext.Activities.Where(p => p.Id == id).FirstOrDefault();
            return Activity;
        }

        #endregion
    }
}
