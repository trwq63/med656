using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface for a Experiment Administrator repository class
    /// </summary>
    public interface IExperimentAdminRepository : IRepository<ExperimentAdministrator>
    {
        /// <summary>
        /// Get a Experiment Administrator using first and last name.
        /// </summary>
        /// <param name="firstName">First name of Experiment Administrator</param>
        /// <param name="lastName">Last name of Experiment Administrator</param>
        /// <returns></returns>
        ExperimentAdministrator GetExperimentAdministratorByName(string firstName, string lastName);
    }
}