using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Interface for the patient service
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Add a new patient to the database
        /// </summary>
        /// <param name="patient">Patient object to add to the database</param>
        void CreatePatient(Patient patient);

        /// <summary>
        /// Get Patient from database using Patient Id
        /// </summary>
        /// <param name="id">Id of the patient</param>
        /// <returns></returns>
        Patient GetPatient(Guid id);

        /// <summary>
        /// Get all patients from the database
        /// </summary>
        /// <param name="physican">Optional parameter to get all patients that are patients of with a specific physician.</param>
        /// <returns></returns>
        IEnumerable<Patient> GetPatients(Physician physican = null);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}