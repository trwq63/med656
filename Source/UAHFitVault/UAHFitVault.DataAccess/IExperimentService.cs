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
        /// <param name="experiment"></param>
        void DeleteExperiment(int experimentId);


        /// <summary>
        /// Get the Experiment
        /// </summary>
        /// <param name="experimentId">Id of the experiment to get</param>
        /// <returns>Experiment</returns>
        Experiment GetExperiment(int experimentId);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}
