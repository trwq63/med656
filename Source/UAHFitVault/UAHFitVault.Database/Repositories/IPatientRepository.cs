using System;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface to implement patient repository.
    /// </summary>
    public interface IPatientRepository : IRepository<Patient>
    {
        /// <summary>
        /// Get a patient by their guid id.
        /// </summary>
        /// <param name="id">Id of the patient</param>
        /// <returns></returns>
        Patient GetPatientById(int id);
    }
}