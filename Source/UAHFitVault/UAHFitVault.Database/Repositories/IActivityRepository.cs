using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface for the Activity repository.
    /// </summary>
    public interface IActivityRepository : IRepository<Activity>
    {
        /// <summary>
        /// Get an activity using its ID.
        /// </summary>
        /// <param name="id">ID of the activity</param>
        /// <returns>Account Request</returns>
        Activity GetActivity(int id);
    }
}