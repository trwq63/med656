using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAHFitVault.Database.Entities
{
    public class ZephyrBRRR
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ZephyrBRRR() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        public float? BR { get; set; }
        public float? RR { get; set; }

        #endregion

        #region Navigation Properties
        [Required]
        public virtual string PatientDataId { get; set; }

        [ForeignKey("PatientDataId")]
        public virtual PatientData PatientData { get; set; }

        #endregion
    }
}
