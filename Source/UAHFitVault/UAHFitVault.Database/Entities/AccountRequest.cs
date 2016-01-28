using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace UAHFitVault.Database.Entities
{
    public class AccountRequest
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountRequest() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public string Request { get; set; }

        #endregion

    }
}
