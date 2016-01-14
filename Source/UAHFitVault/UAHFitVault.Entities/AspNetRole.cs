using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Entities
{
    public class AspNetRole
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public AspNetRole() {

        }

        #endregion

        #region Scalar Properties
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        #endregion
    }
}
