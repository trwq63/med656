using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class MSBandAccelerometer
    {
        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MSBandAccelerometer() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public float Vertical { get; set; }
        [Required]
        public float Lateral { get; set; }
        [Required]
        public float Sagittal { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public Guid PatientDataId { get; set; }

        #endregion
    }
}
