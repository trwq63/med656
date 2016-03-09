using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the Experiment model
    /// </summary>
    public class ExperimentRepository : RepositoryBase<Experiment>, IExperimentRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ExperimentRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get an experiment using the experiment id
        /// </summary>
        /// <param name="experimentId">Experiment Id</param>
        /// <param name="experimentAdminId">Experiment Administrator Id</param>
        /// <returns></returns>
        public Experiment GetExperimentById (int experimentId, int experimentAdminId)
        {
            // Only return experiments that belong to the current user
            Experiment experiment = this.DbContext.Experiments.Where(p => p.Id == experimentId)
                .Where(p => p.ExperimentAdministrator.Id == experimentId)
                .FirstOrDefault();

            return experiment;
        }

        /// <summary>
        /// Get a Experiment using experiment name.
        /// </summary>
        /// <param name="name">Name of Experiment</param>
        /// <param name="experimentAdminId">Id of the Experiment Administrator</param>
        /// <returns>Experiment</returns>
        public Experiment GetExperimentByName(string name, int experimentAdminId) {
            // Only return experiments that belong to the current user
            Experiment experiment = this.DbContext.Experiments.Where(p => p.Name == name)
                .Where(p => p.ExperimentAdministrator.Id == experimentAdminId)
                .FirstOrDefault();

            return experiment;
        }

        #endregion
    }
}
