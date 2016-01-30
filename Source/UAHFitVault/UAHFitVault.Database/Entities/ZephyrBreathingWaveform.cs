using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class ZephyrBreathingWaveform
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ZephyrBreathingWaveform() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int Data { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public Guid PatientDataId { get; set; }

        #endregion
    }
}
