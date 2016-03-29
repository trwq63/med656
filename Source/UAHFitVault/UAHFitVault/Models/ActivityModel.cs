using System;

namespace UAHFitVault.Models
{
    /// <summary>
    /// Model for each activity to be tagged to data being uploaded.
    /// </summary>
    public class ActivityModel
    {
        #region Public Properties
        /// <summary>
        /// Name of the activity type performed for the data set.
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// Start time of the activity.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End time of the activity.
        /// </summary>
        public DateTime EndDate { get; set; }

        #endregion
    }
}
