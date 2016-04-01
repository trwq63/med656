using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Service operations used to access activity data
    /// </summary>
    public class ActivityService : IActivityService
    {
        #region Private Properties

        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Activity Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ActivityService(IActivityRepository repository, IUnitOfWork unitOfWork) {
            _activityRepository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all actvities from the database
        /// </summary>
        /// <param name="ID">Get all activities from the Db.</param>
        /// <returns></returns>
        public IEnumerable<Activity> GetActivities() {
            return _activityRepository.GetAll();
        }

        /// <summary>
        /// Get actvitiy from database using accountRequest Id
        /// </summary>
        /// <param name="id">Id of the activity</param>
        /// <returns></returns>
        public Activity GetActivity (int id) {
            Activity activity = null;
            if (id > 0)
            {
                activity = _activityRepository.GetActivity(id);
            }
            return activity;
        }

        /// <summary>
        /// Add a new activity to the database
        /// </summary>
        /// <param name="activity">Activity object to add to the database</param>
        public void CreateActivity (Activity activity) {
            if(activity != null) {
                _activityRepository.Add(activity);
            }
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void SaveChanges() {
            _unitOfWork.Commit();
        }

        #endregion
    }
}
