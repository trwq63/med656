using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Interface for a Experiment repository class
    /// </summary>
    public interface IExperimentRepository : IRepository<Experiment>
    {
        /// <summary>
        /// Get a Experiment using experiment name.
        /// </summary>
        /// <param name="name">Name of Experiment</param>
        /// <returns></returns>
        Experiment GetExperimentByName(string name);
    }
}