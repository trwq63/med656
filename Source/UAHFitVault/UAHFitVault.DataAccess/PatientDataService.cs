using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Service operations used to access patient data.
    /// </summary>
    public class PatientDataService : IPatientDataService
    {
        #region Private Properties

        private readonly IPatientDataRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public PatientDataService(IPatientDataRepository repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the patient data records for the given patient or all records for all patients.
        /// </summary>
        /// <param name="patient">Patient object used to retrieve the patient's data records</param>
        /// <returns></returns>
        public IEnumerable<PatientData> GetPatientData(Patient patient) { 
            if (patient == null)
                return _repository.GetAll();
            else
                return _repository.GetMany(r => r.Patient.Id == patient.Id);
        }

        /// <summary>
        /// Get Patient Data Record from database using the patient data record Id
        /// </summary>
        /// <param name="id">Id of the patient data record</param>
        /// <returns></returns>
        public PatientData GetPatientData(string id) {
            PatientData patientData = null;

            if(!string.IsNullOrEmpty(id)) {
                patientData = _repository.GetById(id);
            }

            return patientData;

        }

        /// <summary>
        /// Add a new patient data record to the database
        /// </summary>
        /// <param name="patientData">PatientData object to add to the database</param>
        public void CreatePatientData(PatientData patientData) {
            if(patientData != null) {
                _repository.Add(patientData);
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
