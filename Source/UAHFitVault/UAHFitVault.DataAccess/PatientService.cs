using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;
using System;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Service operations used to access data for a patient user
    /// </summary>
    public class PatientService : IPatientService
    {
        #region Private Properties

        private readonly IPatientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Patient Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public PatientService(IPatientRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all patients from the database
        /// </summary>
        /// <param name="physician">Optional parameter to get all patients belonging to a specific physician</param>
        /// <returns></returns>
        public IEnumerable<Patient> GetPatients(Physician physician = null) {
            if (physician == null)
                return _repository.GetAll();
            else
                return _repository.GetAll().Where(p => p.Physician.Id == physician.Id);
        }

        /// <summary>
        /// Get patient from database using patient Id
        /// </summary>
        /// <param name="id">Id of the patient</param>
        /// <returns></returns>
        public Patient GetPatient(int id) {
            Patient patient = null;

            if(id > 0) {
                patient = _repository.GetPatientById(id);
            }

            return patient;

        }

        /// <summary>
        /// Add a new patient to the database
        /// </summary>
        /// <param name="patient">Patient object to add to the database</param>
        public void CreatePatient(Patient patient) {
            if(patient != null) {
                _repository.Add(patient);
            }
        }

        /// <summary>
        /// Delete a patient from the database.
        /// </summary>
        /// <param name="patient">Patient to delete from the database</param>
        public void DeletePatient (Patient patient)
        {
            if (patient != null)
            {
                _repository.Delete(patient);
            }
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void SaveChanges() {
            _unitOfWork.Commit();
        }

        #endregion
    }
}
