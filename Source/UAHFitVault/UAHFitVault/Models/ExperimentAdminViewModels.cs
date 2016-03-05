using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UAHFitVault.LogicLayer.Enums;
using System;

namespace UAHFitVault.Models
{
    /// <summary>
    /// Model for creating experiments
    /// </summary>
    public class CreateExperimentViewModel
    {
        /// <summary>
        /// The name of the experiment
        /// </summary>
        [Required]
        [Display(Name = "Experiment Name")]
        public string ExperimentName { get; set; }

        [Display(Name = "Start of Age Range")]
        public int ageRangeStart { get; set; }

        [Display(Name = "End of Age Range")]
        public int ageRangeEnd { get; set; }

        [Display(Name = "Start of Weight Range (pounds)")]
        public int weightRangeBegin { get; set; }

        [Display(Name = "End of Weight Range (pounds)")]
        public int weightRangeEnd { get; set; }

        [Display(Name = "Start of Height Range (inches)")]
        public int heightRangeBegin { get; set; }

        [Display(Name = "End of Height Range (inches)")]
        public int heightRangeEnd { get; set; }

        public List<bool> races; // Races

        public List<bool> genders; // Sexes

        public List<int> locations; // Select location

        public List<int> ethnicities; // Ethnicities

        public List<int> activities; // Activity tags
        
    }
}