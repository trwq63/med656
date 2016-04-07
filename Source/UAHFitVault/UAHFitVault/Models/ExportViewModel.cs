using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Models
{
    /// <summary>
    /// View model used for displaying data in patient export view.
    /// </summary>
    public class ExportViewModel
    {
        #region Public Properties

        /// <summary>
        /// The patient's id in the system.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Patient's username in the system.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// List of all of the patient's data records that have been uploaded into the system.
        /// </summary>
        public List<PatientData> PatientData { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ExportViewModel() {

        }

        /// <summary>
        /// Constructor used to initialize object properties at construction.
        /// </summary>
        /// <param name="patientId">The patient's id in the system.</param>
        /// <param name="username">Patient's username in the system.</param>
        /// <param name="patientData">List of all of the patient's data records that have been uploaded into the system.</param>
        public ExportViewModel(int patientId, string username, List<PatientData> patientData) {
            if(IsValid(patientId, username, patientData)) {
                PatientId = patientId;
                Username = username;
                PatientData = patientData;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Verify that the object's current properties are valid;
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return IsValid(PatientId, Username, PatientData);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Validate the input parameters that will be or are already set as the objects properties.
        /// </summary>
        /// <param name="patientId">The patient's id in the system.</param>
        /// <param name="username">Patient's username in the system.</param>
        /// <param name="patientData">List of all of the patient's data records that have been uploaded into the system.</param>
        /// <returns></returns>
        protected bool IsValid(int patientId, string username, List<PatientData> patientData) {
            bool valid = false;

            if(patientId > 0 && !string.IsNullOrEmpty(username) && patientData != null && patientData.Count > 0) {
                valid = true;
            }

            return valid;
        }

        #endregion
    }
}