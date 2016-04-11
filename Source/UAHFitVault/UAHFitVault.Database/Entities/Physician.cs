using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class Physician
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public Physician() {
            Patients = new HashSet<Patient>();
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

        public virtual ICollection<Patient> Patients { get; set; }

        #endregion
    }
}
