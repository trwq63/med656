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
        /// Get an experiment using the experiment Id
        /// </summary>
        /// <param name="experimentId">Id of the experiment</param>
        /// <param name="experimentAdminId">Id of the experiment administrator</param>
        /// <returns></returns>
        Experiment GetExperimentById(int experimentId, int experimentAdminId);

        /// <summary>
        /// Get a Experiment using experiment name.
        /// </summary>
        /// <param name="name">Name of Experiment</param>
        /// <returns></returns>
        Experiment GetExperimentByName(string name, int experimentAdminId);
    }
}