﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class MSBandTemperature
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MSBandTemperature() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public float Temperature { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public string PatientDataId { get; set; }

        #endregion
    }
}
