using System;
using System.ComponentModel.DataAnnotations;

namespace UAHFitVault.Database.Entities
{
    public class ZephyrEventData
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ZephyrEventData() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public int EventCode { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public int EventId { get; set; }
        public string EventSpecificData { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public Guid PatientDataId { get; set; }

        #endregion
    }
}
