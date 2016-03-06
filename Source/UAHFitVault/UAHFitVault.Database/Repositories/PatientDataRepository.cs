using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;
using System;
using System.Collections.Generic;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the patientdata model
    /// </summary>
    public class PatientDataRepository : RepositoryBase<PatientData>, IPatientDataRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public PatientDataRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a patient data records using patient id
        /// </summary>
        /// <param name="id">Id of the patient</param>
        /// <returns></returns>
        public IEnumerable<PatientData> GetPatientDataByPatientId(int id) {
            IEnumerable<PatientData> patientData = this.DbContext.PatientData.Where(p => p.Patient.Id == id);
                                                    

            return patientData;
        }

        /// <summary>
        /// Get a patient data record by id.
        /// </summary>
        /// <param name="id">Id of the patient data record.</param>
        /// <returns></returns>
        public PatientData GetById(string id) {
            PatientData data = null;
            if (!string.IsNullOrEmpty(id)) {
                data = DbContext.PatientData.FirstOrDefault(p => p.Id == id);
            }

            return data;
        }

        #endregion
    }
}
