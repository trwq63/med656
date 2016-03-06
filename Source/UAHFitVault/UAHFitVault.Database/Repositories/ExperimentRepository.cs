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
        /// Get a Experiment using experiment name.
        /// </summary>
        /// <param name="name">Name of Experiment</param>
        /// <returns></returns>
        public Experiment GetExperimentByName(string name) {
            Experiment experiment = this.DbContext.Experiments.Where(p => p.Name == name).FirstOrDefault();

            return experiment;
        }

        #endregion
    }
}
