using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault..Database.Entities
{
    public class ZephyrECGWaveform
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ZephyrECGWaveform() {

        }

        #endregion

        #region Scalar Properties

        public Guid Id { get; set; }
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
