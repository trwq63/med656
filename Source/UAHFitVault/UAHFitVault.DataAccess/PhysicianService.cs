using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Service operations used to access data for a physician user
    /// </summary>
    public class PhysicianService : IPhysicianService
    {
        #region Private Properties

        private readonly IPhysicianRepository _physicianRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Physician Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public PhysicianService(IPhysicianRepository repository, IUnitOfWork unitOfWork) {
            _physicianRepository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all physicians from the database
        /// </summary>
        /// <param name="lastName">Optional parameter to get all physicians with a specific name</param>
        /// <returns></returns>
        public IEnumerable<Physician> GetPhysicians(string lastName = null) {
            if (string.IsNullOrEmpty(lastName))
                return _physicianRepository.GetAll();
            else
                return _physicianRepository.GetAll().Where(c => c.LastName == lastName);
        }

        /// <summary>
        /// Get Physician from database using Physician Id
        /// </summary>
        /// <param name="id">Id of the physician</param>
        /// <returns></returns>
        public Physician GetPhysician(int id) {
            Physician physician = null;

            if(id > 0) {
                physician = _physicianRepository.GetById(id);
            }

            return physician;

        }

        /// <summary>
        /// Get Physician from database using Physician Id
        /// </summary>
        /// <param name="firstName">First name of the physician</param>
        /// <param name="lastName">Last name of the physician</param>
        /// <returns></returns>
        public Physician GetPhysician(string firstName, string lastName) {
            Physician physician = null;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)) {
                physician = _physicianRepository.GetPhysicianByName(firstName, lastName);
            }

            return physician;

        }

        /// <summary>
        /// Add a new physician to the database
        /// </summary>
        /// <param name="physician">Physician object to add to the database</param>
        public void CreatePhysician(Physician physician) {
            if(physician != null) {
                _physicianRepository.Add(physician);
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
