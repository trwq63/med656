using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess
{
    /// <summary>
    /// Interface for the experiment administrator service
    /// </summary>
    public interface IExperimentAdminService
    {
        /// <summary>
        /// Add a new experiment administrator to the database
        /// </summary>
        /// <param name="experimentAdmin">experiment administrator object to add to the database</param>
        void CreateExperimentAdministrator(ExperimentAdministrator experimentAdmin);

        /// <summary>
        /// Get experiment administrator from database using experiment administrator Id
        /// </summary>
        /// <param name="id">Id of the experiment administrator</param>
        /// <returns></returns>
        ExperimentAdministrator GetExperimentAdministrator(int id);

        /// <summary>
        /// Get experiment administrator from database using experiment administrator Id
        /// </summary>
        /// <param name="firstName">First name of the experiment administrator</param>
        /// <param name="lastName">Last name of the experiment administrator</param>
        /// <returns></returns>
        ExperimentAdministrator GetExperimentAdministrator(string firstName, string lastName);

        /// <summary>
        /// Get all experiment administrators from the database
        /// </summary>
        /// <param name="lastName">Optional parameter to get all experiment administrator with a specific name</param>
        /// <returns></returns>
        IEnumerable<ExperimentAdministrator> GetExperimentAdministrators(string lastName = null);

        /// <summary>
        /// Delete an experiment administrator user from the database.
        /// </summary>
        /// <param name="id"></param>
        void DeleteExperimentAdministrator(int id);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}