using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Models
{
    /*
    public class PhysicianViewModels
    {
    }
    */

    /// <summary>
    /// CreatePatientViewModel - Model for the create patient form for the physician view.
    /// </summary>
    public class CreatePatientViewModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Birthdate")]
        public string Birthdate { get; set; }

        [Display(Name = "Weight (pounds)")]
        public int Weight { get; set; }

        [Display(Name = "Height (inches)")]
        public int Height { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Race")]
        public string Race { get; set; }

        [Display(Name = "Ethnicity")]
        public string Ethnicity { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

    }
}