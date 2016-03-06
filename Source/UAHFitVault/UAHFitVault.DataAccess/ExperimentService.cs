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
        public void DeleteExperiment (int experimentId)
        {
            if (experimentId > 0)
            {
                _experimentRepository.Delete(GetExperiment(experimentId));
            }
        }

        /// <summary>
        /// Get the Experiment
        /// </summary>
        /// <param name="experimentId">Id of the experiment to get</param>
        /// <returns>Experiment</returns>
        public Experiment GetExperiment (int experimentId)
        {
            Experiment experiment = null;
            if (experimentId > 0)
            {
                experiment = _experimentRepository.GetById(experimentId);
            }
            return experiment;
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
