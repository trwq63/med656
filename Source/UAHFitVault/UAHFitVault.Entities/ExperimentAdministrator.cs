using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Entities
{
    public class ExperimentAdministrator
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ExperimentAdministrator() {
            Experiments = new HashSet<Experiment>();
        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<Experiment> Experiments { get; set; }
        #endregion
    }
}
