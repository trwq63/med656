using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace UAHFitVault.Models
{
    /// <summary>
    /// Model to used to get data between view and controller
    /// </summary>
    public class UploadDataViewModel
    {
        #region Public Properties

        /// <summary>
        /// List of files to be uploaded
        /// </summary>
        [Required]
        public HttpPostedFileBase[] Files { get; set; }

        /// <summary>
        /// Beginning date of the date range for when the data being uploaded was collected.
        /// </summary
        [Required]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// End date of the date range for when the data being uploaded was collected.
        /// </summary>
        [Required]
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Device type being uploaded
        /// </summary>
        [Required]
        public string MedicalDeviceType { get; set; }

        #endregion

    }
}