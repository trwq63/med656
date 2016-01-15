using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault..Database.Entities
{
    public class MSBandHeartRate
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MSBandHeartRate() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string ReadStatus { get; set; }
        [Required]
        public int HeartRate { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public Guid PatientDataId { get; set; }

        #endregion
    }
}
