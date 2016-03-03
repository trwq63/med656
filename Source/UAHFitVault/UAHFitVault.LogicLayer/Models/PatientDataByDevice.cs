using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.LogicLayer.Enums;

namespace UAHFitVault.LogicLayer.Models
{
    /// <summary>
    /// Class used to store patient data records by device type.
    /// </summary>
    public class PatientDataByDevice
    {
        #region 
        
        /// <summary>
        /// Medical device type
        /// </summary>
        public string MedicalDevice { get; set; }

        /// <summary>
        /// List of patient data records for the device.
        /// </summary>
        public List<PatientData> DataRecords { get; set; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PatientDataByDevice() {

        }

        /// <summary>
        /// Constructor to initialize object properties at construction.
        /// </summary>
        /// <param name="deviceType">Medical device type</param>
        /// <param name="patientdata">List of patient data records for the device.</param>
        public PatientDataByDevice(string deviceType, List<PatientData> patientdata) {
            if(IsValid(deviceType, patientdata)) {
                MedicalDevice = deviceType;
                DataRecords = patientdata;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Verify the current object properties have valid values.
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return IsValid(MedicalDevice, DataRecords);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Check the input parameters for valid values.
        /// </summary>
        /// <param name="deviceType">Medical device type</param>
        /// <param name="patientData">List of patient data records for the device.</param>
        /// <returns></returns>
        protected bool IsValid(string deviceType, List<PatientData> patientData) {
            bool valid = false;

            if(!string.IsNullOrEmpty(deviceType) && patientData != null && patientData.Count > 0) {
                valid = true;
            }

            return valid;
        }

        #endregion


    }
}
