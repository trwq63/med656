using System;

namespace UAHFitVault.Models
{
    /// <summary>
    /// Model for each activity to be tagged to data being uploaded.
    /// </summary>
    public class ActivityModel
    {
        #region Private Members

        /// <summary>
        /// Start time of the activity.
        /// </summary>
        private DateTime _startTime;

        /// <summary>
        /// End time of the activity.
        /// </summary>
        private DateTime _endTime;

        #endregion

        #region Public Properties
        /// <summary>
        /// Name of the activity type performed for the data set.
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// Date the activity took place on.
        /// </summary>
        public DateTime ActivityDate { get; set; }

        /// <summary>
        /// Start time of the activity.
        /// </summary>
        public DateTime StartTime {
            get {
                DateTime startTime = new DateTime(ActivityDate.Year, ActivityDate.Month, ActivityDate.Day, _startTime.Hour, _startTime.Minute, _startTime.Second);
                return startTime;
            }
            set {
                _startTime = value;
            }
        }

        /// <summary>
        /// End time of the activity.
        /// </summary>
        public DateTime EndTime {
            get {
                DateTime endTime = new DateTime(ActivityDate.Year, ActivityDate.Month, ActivityDate.Day, _endTime.Hour, _endTime.Minute, _endTime.Second);
                return endTime;
            }
            set {
                _endTime = value;
            }
        }
            #endregion
        }
}
