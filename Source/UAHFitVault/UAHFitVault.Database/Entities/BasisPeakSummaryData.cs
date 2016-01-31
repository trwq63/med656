using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAHFitVault.Database.Entities
{
    public class BasisPeakSummaryData
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public BasisPeakSummaryData() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float? Calories { get; set; }
        public float? GSR { get; set; }
        public int? HeartRate { get; set; }
        public float? SkinTemp { get; set; }
        public int Steps { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public virtual Guid PatientDataId { get; set; }

        [ForeignKey("PatientDataId")]
        public virtual PatientData PatientData { get; set; }

        #endregion
    }
}
