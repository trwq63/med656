using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class MSBandDistance
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MSBandDistance() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string MotionType { get; set; }
        [Required]
        public float Pace { get; set; }
        [Required]
        public float Speed { get; set; }
        [Required]
        public float Total { get; set; }

        #endregion

        #region Navigation Properties
        [Required]
        public Guid PatientDataId { get; set; }

        #endregion
    }
}
