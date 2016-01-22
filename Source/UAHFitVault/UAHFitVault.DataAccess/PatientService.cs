using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Service operations used to access data for a patient user
    /// </summary>
    public class PatientService : IPatientService
    {
        #region Private Properties

        private readonly IRepository<Patient> _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Patient Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public PatientService(IRepository<Patient> repository, IUnitOfWork unitOfWork) {
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
                patient = _repository.GetById(id);
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
        /// Save changes to database
        /// </summary>
        public void SaveCategory() {
            _unitOfWork.Commit();
        }

        #endregion
    }
}
