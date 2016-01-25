using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using System;
using System.Linq;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the Patient model
    /// </summary>
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public PatientRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a patient by their guid id.
        /// </summary>
        /// <param name="id">Guid id of the patient</param>
        /// <returns></returns>
        public Patient GetPatientById(Guid id) {
            Patient patient = this.DbContext.Patients.FirstOrDefault(p => p.Id == id);

            return patient;
        }

        #endregion
    }
}
