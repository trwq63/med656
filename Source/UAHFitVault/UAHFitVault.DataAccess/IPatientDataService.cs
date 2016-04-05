using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Interface for the patient data service
    /// </summary>
    public interface IPatientDataService
    {
        /// <summary>
        /// Add a new patient data record to the database
        /// </summary>
        /// <param name="patientData">Patient Data Record object to add to the database</param>
        void CreatePatientData(PatientData patientData);

        /// <summary>
        /// Get Patient Data Record from database using the patient data record Id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        PatientData GetPatientData(string id);

        /// <summary>
        /// Get the patient data records for the given patient.
        /// </summary>
        /// <param name="patient">Patient object used to retrieve the patient's data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<PatientData> GetPatientData(Patient patient, int skip = 0, int take = 0);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}