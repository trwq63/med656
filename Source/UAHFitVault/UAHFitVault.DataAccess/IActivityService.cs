using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Interface for the account request service
    /// </summary>
    public interface IActivityService
    {
        /// <summary>
        /// Add a new activity to the database
        /// </summary>
        /// <param name="activity">Activity object to add to the database</param>
        void CreateActivity(Activity activity);

        /// <summary>
        /// Get Activity from database using activity Id
        /// </summary>
        /// <param name="id">Id of the activity</param>
        /// <returns></returns>
        Activity GetActivity (int id);
        
        /// <summary>
        /// Get all activities from the database
        /// </summary>
        /// <returns></returns>
        IEnumerable<Activity> GetActivities();

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}