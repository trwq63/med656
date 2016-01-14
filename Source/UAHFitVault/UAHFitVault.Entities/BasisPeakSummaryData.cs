using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Entities
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
        public float Calories { get; set; }
        public float GSR { get; set; }
        public int HeartRate { get; set; }
        public float SkinTemp { get; set; }
        public int Steps { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public Guid PatientDataId { get; set; }

        #endregion
    }
}
