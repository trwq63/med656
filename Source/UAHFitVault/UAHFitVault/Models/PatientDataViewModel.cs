using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UAHFitVault.Models
{
    /// <summary>
    /// View model for data displayed to the patient or physician for selecting medical data to view.
    /// </summary>
    public class PatientDataViewModel
    {
        #region Public Properties

        public SelectList Medical { get; set; }

        #endregion
    }
}