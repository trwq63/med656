using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class AspNetUser
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public AspNetUser() {

        }

        #endregion

        #region Scalar Properties
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public int Status { get; set; }

        #endregion

        #region Navigation Properties
        public virtual AspNetRole AspNetRole { get; set; }
        public virtual Patient Parent { get; set; }
        public virtual Physician Physician { get; set; }
        public virtual ExperimentAdministrator ExperimentAdministrator { get; set; }

        #endregion
    }
}
