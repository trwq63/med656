using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class Patient
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public Patient() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Location { get; set; }
        [Required]
        public int Ethnicity { get; set; }
        [Required]
        public int Race { get; set; }
        [Required]
        public int Gender { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Physician Physician { get; set; }

        public virtual List<PatientData> PatientData { get; set; }

        #endregion
    }
}
