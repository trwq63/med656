using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface to implement patient data repository.
    /// </summary>
    public interface IPatientDataRepository : IRepository<PatientData>
    {
        /// <summary>
        /// Get a patient data records using patient id
        /// </summary>
        /// <param name="id">Id of the patient</param>
        /// <returns></returns>
        IEnumerable<PatientData> GetPatientDataByPatientId(Guid id);
    }
}