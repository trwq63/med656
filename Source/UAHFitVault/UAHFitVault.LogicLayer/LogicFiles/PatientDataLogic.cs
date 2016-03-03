using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.LogicLayer.Models;

namespace UAHFitVault.LogicLayer.LogicFiles
{
    /// <summary>
    /// Class contains the logic functions needed to work with Patient data functionality
    /// </summary>
    public static class PatientDataLogic
    {
        #region Public Methods

        /// <summary>
        /// Sort the patient records by the device type specified.
        /// </summary>
        /// <param name="patientData">List of all patient data records.</param>
        /// <param name="deviceType">Name of the medical device</param>
        /// <returns></returns>
        public static PatientDataByDevice SortPatientData(List<PatientData> patientData, string deviceType) {
            PatientDataByDevice deviceData = null;

            if(patientData != null && patientData.Count > 0 && !string.IsNullOrEmpty(deviceType)) {
                List<PatientData> data = patientData.Where(p => p.MedicalDevice.Name == deviceType).ToList();
                if(data != null && data.Count > 0) {
                    deviceData = new PatientDataByDevice() {
                        MedicalDevice = deviceType,
                        DataRecords = data
                    };
                }
            }

            return deviceData;
        }

        #endregion
    }
}
