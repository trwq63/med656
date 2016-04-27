using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAHFitVault.Database.Entities
{
    public class ZephyrAccelerometer
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ZephyrAccelerometer() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int Vertical { get; set; }
        [Required]
        public int Lateral { get; set; }
        [Required]
        public int Sagittal { get; set; }

        #endregion

        #region Navigation Properties
        [Required]
        public virtual string PatientDataId { get; set; }

        [ForeignKey("PatientDataId")]
        public virtual PatientData PatientData { get; set; }

        #endregion
    }
}
