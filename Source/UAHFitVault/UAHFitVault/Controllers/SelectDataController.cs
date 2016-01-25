using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.DataAccess.ZephyrServices;
using UAHFitVault.DataAccess;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.LogicLayer.LogicFiles;

namespace UAHFitVault.Controllers
{
    public class SelectDataController : Controller
    {
        #region Private Members

        /// <summary>
        /// Service object for accessing patient data database functions.
        /// </summary>
        private readonly IPatientDataService _patientDataService;

        /// <summary>
        /// Service object for accessing Zephyr Accelerometer database functions.
        /// </summary>
        private readonly IZephyrAccelService _accelService;

        /// <summary>
        /// Service object for accessing Zephyr Breathing Waveform database functions.
        /// </summary>
        private readonly IZephyrBreathingService _breathingService;

        /// <summary>
        /// Service object for accessing Zephyr ECG Waveform database functions.
        /// </summary>
        private readonly IZephyrECGService _ecgService;

        /// <summary>
        /// Service object for accessing Zephyr Event Data database functions.
        /// </summary>
        private readonly IZephyrEventDataService _eventDataService;

        /// <summary>
        /// Service object for accessing Zephyr Summary database functions.
        /// </summary>
        private readonly IZephyrSummaryService _summaryService;

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public SelectDataController(IPatientDataService patientDataService, IZephyrAccelService accelService,
                                    IZephyrBreathingService breathingService, IZephyrECGService ecgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService) {

            _patientDataService = patientDataService;
            _accelService = accelService;
            _breathingService = breathingService;
            _ecgService = ecgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
        }
        #endregion

        /// <summary>
        /// Load the initial select data view when first navigating to the page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Process the data submitted from the form on the view.
        /// </summary>
        /// <param name="files">Comma delimited list of strings of file names.</param>
        /// <returns></returns>
        public ActionResult ProcessData(string files) {

            string[] fileList = files.Split(',');

            foreach(string file in fileList) {
                Device_Type deviceType = SelectDataLogic.DetermineDeviceType(file);
                File_Type fileType = SelectDataLogic.DetermineFileType(file, deviceType);

                switch (deviceType) {
                    case Device_Type.BasisPeak:
                        ProcessBasisPeakData(file);
                        break;
                    case Device_Type.Zephyr:
                        break;
                    case Device_Type.MicrosoftBand:
                        break;
                    default:
                        break;
                }
            }
            //Determine device type

            //Read file

            //Create objects

            //Write to db.

            return new JsonResult();
        }

        #region Protected Methods

        //protected string ReadFile() {

        //}

        protected void ProcessBasisPeakData(string fileName) {
            //Read a line of the file
         //   using(Tex)
        }

        #endregion
    }
}