using System.Collections.Generic;
using UAHFitVault.LogicLayer.Models;

namespace UAHFitVault.Models
{
    /// <summary>
    /// View model for data displayed to the patient or physician for selecting medical data to view.
    /// </summary>
    public class PatientDataViewModel
    {
        #region Public Properties

        public List<LineGraphModel> LineGraphModels { get; set; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PatientDataViewModel() {
            LineGraphModels = new List<LineGraphModel>();
        }

        /// <summary>
        /// Constructor used to initialize object properties at construction.
        /// </summary>
        /// <param name="lineGraphModels"></param>
        public PatientDataViewModel(List<LineGraphModel> lineGraphModels) {
            LineGraphModels = lineGraphModels;
        }
        #endregion
    }
}