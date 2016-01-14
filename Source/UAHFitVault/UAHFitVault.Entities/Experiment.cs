using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Entities
{
    public class Experiment
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Experiment() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime LastModified { get; set; }
        [Required]
        public string QueryString { get; set; }

        #endregion

        #region Navigation Properties
        [Required]
        public virtual ExperimentAdministrator ExperimentAdministrator { get; set; }

        #endregion
    }
}
