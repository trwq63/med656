using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class Activity
    {
        #region Public Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Activity() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public int DataActivity { get; set; }

        #endregion

    }
}
