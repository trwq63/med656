using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class MSBandCalories
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MSBandCalories() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Total { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public string PatientDataId { get; set; }

        #endregion
    }
}
