using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Models
{
    /// <summary>
    /// Model for creating experiments
    /// </summary>
    public class ExperimentViewModel
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

        public string[] selectedGenders;        // Selected genders
        public string[] selectedRaces;          // Selected races
        public string[] selectedEthnicities;    // Selected ethnicities
        public string[] selectedLocations;      // Selected locations   
    }

    /// <summary>
    /// Model for viewing patient data
    /// </summary>
    public class ViewPatientViewModel
    {
        public string ActivityTagFilter { get; set; }

        public Patient patient { get; set; }

        public List<PatientData> patientData { get; set; }
    }

    /// <summary>
    /// Model for deleting an experiment
    /// </summary>
    public class DeleteExperimentViewModel
    {
        public string ExperimentName { get; set; }
    }

    /// <summary>
    /// Model for viewing matching criteria data for an experiment
    /// </summary>
    public class ViewExperimentCriteriaViewModel
    {
        public ExperimentViewModel experiment { get; set; }
    }

    /// <summary>
    /// Model for viewing experiments
    /// </summary>
    public class ViewExperimentViewModel
    {
        public string experimentName { get; set; }

        public ViewExperimentCriteriaViewModel criteriaModel { get; set; }

        public List<Patient> patientList { get; set; }
    }

    /// <summary>
    /// Model for viewing experiments
    /// </summary>
    public class ViewAllExperimentsViewModel
    {
        /// <summary>
        /// All of the experiments
        /// </summary>
        public List<Experiment> Experiments { get; set; }

        /// <summary>
        /// Store the data for easy access in the view
        /// </summary>
        public List<ExperimentViewModel> ExperimentCriteria { get; set; }
    }
}