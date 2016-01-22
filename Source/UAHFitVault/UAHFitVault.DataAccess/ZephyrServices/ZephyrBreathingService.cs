using System.Collections.Generic;
using System.Linq;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.ZephyrServices
{
    /// <summary>
    /// Service operations used to access patient data.
    /// </summary>
    public class ZephyrBreathingService : IZephyrBreathingService
    {
        #region Private Properties

        private readonly IRepository<ZephyrBreathingWaveform> _repository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor with dependencies
        /// </summary>
        /// <param name="repository">Repository interface dependency</param>
        /// <param name="unitOfWork">UnitOfWork interface dependency</param>
        public ZephyrBreathingService(IRepository<ZephyrBreathingWaveform> repository, IUnitOfWork unitOfWork) {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the Zephyr Breathing Waveform data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the Zephyr Breathing Waveform Data records</param>
        /// <returns></returns>
        public IEnumerable<ZephyrBreathingWaveform> GetZephyrBreathingWaveformData(PatientData patientData) {
            if (patientData == null)
                return _repository.GetAll();
            else
                return _repository.GetAll().Where(r => r.PatientDataId == patientData.Id);
        }

        /// <summary>
        /// Get Zephyr Breathing Waveform data from database using the Zephyr Breathing Waveform id
        /// </summary>
        /// <param name="id">Id of the Zephyr Breathing Waveform data</param>
        /// <returns></returns>
        public ZephyrBreathingWaveform GetZephyrBreathingWaveformData(int id) {
            ZephyrBreathingWaveform zephryBreathing = null;

            if(id > 0) {
                zephryBreathing = _repository.GetById(id);
            }

            return zephryBreathing;

        }

        /// <summary>
        /// Add a new Zephyr Breathing Waveform data record to the database
        /// </summary>
        /// <param name="zephryBreathing">ZephyrBreathingWaveform object to add to the database</param>
        public void CreateZephyrAccel(ZephyrBreathingWaveform zephryBreathing) {
            if(zephryBreathing != null) {
                _repository.Add(zephryBreathing);
            }
        }

        /// <summary>
        /// Save changes to database
        /// </summary>
        public void SaveCategory() {
            _unitOfWork.Commit();
        }

        #endregion
    }
}
