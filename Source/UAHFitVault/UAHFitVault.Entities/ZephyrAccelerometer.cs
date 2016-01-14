using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Entities
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
        public Guid PatentDataId { get; set; }

        #endregion
    }
}
