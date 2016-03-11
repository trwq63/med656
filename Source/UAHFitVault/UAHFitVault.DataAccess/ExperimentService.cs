using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Repositories;

namespace UAHFitVault.DataAccess
{
    class ExperimentService : IExperimentService
    {
        #region Private Properties

        private readonly IExperimentRepository _experimentRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Experiment Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ExperimentService(IExperimentRepository repository, IUnitOfWork unitOfWork)
        {
            _experimentRepository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Create an experiment
        /// </summary>
        /// <param name="experiment">Experiment to create</param>
        public void CreateExperiment (Experiment experiment)
        {
            if (experiment != null)
            {
                _experimentRepository.Add(experiment);
            }

        }

        /// <summary>
        /// Delete an experiment
        /// </summary>
        /// <param name="experimentId">Id of the experiment to delete</param>
        /// <param name="experimentAdminId">Id of the experiment administrator</param>
        public void DeleteExperiment (int experimentId, int experimentAdminId)
        {
            if (experimentId > 0)
            {
                Experiment experiment = _experimentRepository.GetExperimentById(experimentId, experimentAdminId);
                if (experiment != null)
                {
                    _experimentRepository.Delete(experiment);
                }
            }
        }

        /// <summary>
        /// Get the Experiment
        /// </summary>
        /// <param name="experimentId">Id of the experiment to get</param>
        /// <param name="experimentAdminId">Id of the experiment administrator</param>
        /// <returns>Experiment</returns>
        public Experiment GetExperimentById (int experimentId, int experimentAdminId)
        {
            Experiment experiment = null;
            if (experimentId > 0)
            {
                experiment = _experimentRepository.GetExperimentById(experimentId, experimentAdminId);
            }
            return experiment;
        }

        /// <summary>
        /// Get the experiment using the experiment name
        /// </summary>
        /// <param name="experimentName">Name of the experiment</param>
        /// <param name="experimentAdminId">Id of the experiment administrator</param>
        /// <returns></returns>
        public Experiment GetExperimentByName (string experimentName, int experimentAdminId)
        {
            Experiment experiment = null;
            if (!string.IsNullOrEmpty(experimentName))
            {
                experiment = _experimentRepository.GetExperimentByName(experimentName, experimentAdminId);
            }
            return experiment;
        }

        /// <summary>
        /// Get all of the experiments for the experiment administrator
        /// </summary>
        /// <param name="experimentAdminId">Experiment administrator Id</param>
        /// <returns></returns>
        public IEnumerable<Experiment> GetExperiments (int experimentAdminId)
        {
            if (experimentAdminId > 0)
            {
                return _experimentRepository.GetAll().Where(p => p.ExperimentAdministrator.Id == experimentAdminId);
            }
            return null;
        }
        
        /// <summary>
        /// Save changes to database
        /// </summary>
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
        #endregion
    }
}
