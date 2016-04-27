using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Repositories;
using UAHFitVault.LogicLayer.Enums;

namespace UAHFitVault.DataAccess
{
    class ExperimentService : IExperimentService
    {
        #region Private Properties

        private readonly IExperimentRepository _experimentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _patientRepository;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Experiment Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ExperimentService(IExperimentRepository repository, IUnitOfWork unitOfWork, IPatientRepository patientRepository)
        {
            _experimentRepository = repository;
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Create an experiment
        /// </summary>
        /// <param name="experiment">Experiment to create</param>
        public void CreateExperiment(Experiment experiment)
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
        public void DeleteExperiment(int experimentId, int experimentAdminId)
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
        public Experiment GetExperimentById(int experimentId, int experimentAdminId)
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
        public Experiment GetExperimentByName(string experimentName, int experimentAdminId)
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
        public IEnumerable<Experiment> GetExperiments(int experimentAdminId)
        {
            if (experimentAdminId > 0)
            {
                return _experimentRepository.GetAll().Where(p => p.ExperimentAdministrator.Id == experimentAdminId);
            }
            return null;
        }

        /// <summary>
        /// Gets all of the experiments in the database
        /// </summary>
        public IEnumerable<Experiment> GetAllExperiments ()
        {
            return _experimentRepository.GetAll();
        }


        /// <summary>
        /// Retrieve the patients for an experiment
        /// </summary>
        /// <param name="criteria">Patient criteria to match</param>
        /// <returns></returns>
        public IEnumerable<Patient> GetPatientsForExperiment (ExperimentCriteria criteria)
        {
            // Need to get all of the patients here in the database and return the list
            char delimiter = '.';
            string genderString = delimiter.ToString(), raceString = delimiter.ToString(),
                ethnicityString = delimiter.ToString(), locationString = delimiter.ToString();

            foreach (string str in criteria.selectedGenders)
            {
                genderString += str + delimiter.ToString();
            }
            foreach (string str in criteria.selectedRaces)
            {
                raceString += str + delimiter.ToString();
            }
            foreach (string str in criteria.selectedEthnicities)
            {
                ethnicityString += str + delimiter.ToString();
            }
            foreach (string str in criteria.selectedLocations)
            {
                locationString += str + delimiter.ToString();
            }

            IEnumerable<Patient> patientList;
            patientList = _patientRepository.GetAll().Where(p => genderString.Contains(delimiter.ToString() + Enum.GetName(typeof(PatientGender), p.Gender) + delimiter.ToString()))
                .Where(p => raceString.Contains(delimiter.ToString() + Enum.GetName(typeof(PatientRace), p.Race) + delimiter.ToString()))
                .Where(p => ethnicityString.Contains(delimiter.ToString() + Enum.GetName(typeof(PatientEthnicity), p.Ethnicity) + delimiter.ToString()))
                .Where(p => locationString.Contains(delimiter.ToString() + Enum.GetName(typeof(Location), p.Location) + delimiter.ToString()))
                .Where(p => ((DateTime.Now-p.Birthdate).Days <= (365*criteria.ageRangeEnd)) &&
                ((DateTime.Now-p.Birthdate).Days >= (365*criteria.ageRangeStart)))
                .Where(p => ((p.Height <= criteria.heightRangeEnd) && (p.Height >= criteria.heightRangeBegin)))
                .Where(p => ((p.Weight <= criteria.weightRangeEnd) && (p.Weight >= criteria.weightRangeBegin)));

            return patientList;
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

    /// <summary>
    /// Class for holding all of the experiment criteria members
    /// </summary>
    public class ExperimentCriteria
    {
        public string[] selectedGenders, selectedRaces, selectedEthnicities, selectedLocations;
        public int ageRangeStart, ageRangeEnd, weightRangeBegin, weightRangeEnd, heightRangeBegin, heightRangeEnd;
    }
}
