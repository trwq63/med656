using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Database.Repositories
{
    /// <summary>
    /// Implementation of the repository base class for the physician model
    /// </summary>
    public class PhysicianRepository : RepositoryBase<Physician>, IPhysicianRepository
    {
        #region Public Constructor

        public PhysicianRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        #endregion

        #region Repository Public Functions

        /// <summary>
        /// Get a physician using first and last name.
        /// </summary>
        /// <param name="firstName">First name of physician</param>
        /// <param name="lastName">Last name of physician</param>
        /// <returns></returns>
        public Physician GetPhysicianByName(string firstName, string lastName) {
            Physician physician = this.DbContext.Physicians.Where(p => p.FirstName == firstName)
                                                    .Where(p => p.LastName == lastName)
                                                    .FirstOrDefault();

            return physician;
        }

        #endregion
    }
}
