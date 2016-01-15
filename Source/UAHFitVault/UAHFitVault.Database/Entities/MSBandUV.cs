using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault..Database.Entities
{
    public class MSBandUV
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MSBandUV() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int UVIndex { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public Guid PatientDataId { get; set; }

        #endregion
    }
}
