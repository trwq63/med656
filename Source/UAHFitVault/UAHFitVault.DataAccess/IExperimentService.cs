using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess
{
    public interface IExperimentService
    {
        /// <summary>
        /// Create an experiment
        /// </summary>
        /// <param name="experiment">Experiment to create</param>
        void CreateExperiment(Experiment experiment);

        /// <summary>
        /// Delete an experiment
        /// </summary>
        /// <param name="experiment">Id of the experiment to delete</param>
        /// <param name="experimentAdminId">Id of the experiment administrator</param>
        void DeleteExperiment(int experimentId, int experimentAdminId);


        /// <summary>
        /// Get the Experiment
        /// </summary>
        /// <param name="experimentId">Id of the experiment to get</param>
        /// <param name="experimentAdminId">Id of the experiment administrator</param>
        /// <returns>Experiment</returns>
        Experiment GetExperimentById(int experimentId, int experimentAdminId);

        /// <summary>
        /// Get the experiment by name
        /// </summary>
        /// <param name="experimentName">Name of the experiment</param>
        /// <param name="experimentAdminId">Id of the experiment administrator</param>
        /// <returns></returns>
        Experiment GetExperimentByName(string experimentName, int experimentAdminId);

        /// <summary>
        /// Get all experiments for a specified experiment administrator
        /// </summary>
        /// <param name="experimentAdminId">Id of the experiment administrator</param>
        /// <returns></returns>
        IEnumerable<Experiment> GetExperiments(int experimentAdminId);

        /// <summary>
        /// Get all of the patients of the input type
        /// </summary>
        /// <param name="type">Input Type</param>
        /// <param name="value">Value to look for</param>
        /// <returns></returns>
        IEnumerable<Patient> GetPatientsOfType(Type type, string value);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}
