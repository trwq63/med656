using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAHFitVault.Database.Entities
{
    public class PatientData
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public PatientData() {
            Activities = new HashSet<Activity>();
        }

        #endregion

        #region Scalar Properties

        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        [Required]
        public int DataType { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public virtual Patient Patient { get; set; }
        [Required]        
        public virtual int MedicalDeviceId { get; set; }
        [ForeignKey("MedicalDeviceId")]
        public virtual MedicalDevice MedicalDevice { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }

        #endregion
    }
}
