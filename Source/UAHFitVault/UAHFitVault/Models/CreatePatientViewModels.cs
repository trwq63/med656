using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Models
{
    /// <summary>
    /// CreatePatientViewModel - Model for the create patient form for the physician view.
    /// </summary>
    public class CreatePatientViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Birthdate")]
        public string Birthdate { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public int? Weight { get; set; }

        [Required]
        [Display(Name = "Height")]
        public int? Height { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Race")]
        public string Race { get; set; }

        [Required]
        [Display(Name = "Ethnicity")]
        public string Ethnicity { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

    }
}