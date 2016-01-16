using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the physician model
    /// </summary>
    public class ExperimentAdminRepository : RepositoryBase<ExperimentAdministrator>, IExperimentAdminRepository
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbFactory">Interface for dbFactory</param>
        public ExperimentAdminRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a experiment administrator using first and last name.
        /// </summary>
        /// <param name="firstName">First name of experiment administrator</param>
        /// <param name="lastName">Last name of experiment administrator</param>
        /// <returns></returns>
        public ExperimentAdministrator GetExperimentAdministratorByName(string firstName, string lastName) {
            ExperimentAdministrator experimentAdmin = this.DbContext.ExperimentAdministrators.Where(p => p.FirstName == firstName)
                                                    .Where(p => p.LastName == lastName)
                                                    .FirstOrDefault();

            return experimentAdmin;
        }

        #endregion
    }
}
