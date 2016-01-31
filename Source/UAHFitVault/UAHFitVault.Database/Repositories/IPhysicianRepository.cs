using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface for a physician repository class
    /// </summary>
    public interface IPhysicianRepository : IRepository<Physician>
    {
        /// <summary>
        /// Get a physician using first and last name.
        /// </summary>
        /// <param name="firstName">First name of physician</param>
        /// <param name="lastName">Last name of physician</param>
        /// <returns></returns>
        Physician GetPhysicianByName(string firstName, string lastName);
    }
}