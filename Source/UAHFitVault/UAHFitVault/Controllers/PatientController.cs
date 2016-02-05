﻿using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using UAHFitVault.DataAccess;
using UAHFitVault.DataAccess.BasisPeakServices;
using UAHFitVault.DataAccess.ZephyrServices;
using UAHFitVault.Database.Entities;
using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.LogicLayer.LogicFiles;
using UAHFitVault.Helpers;

namespace UAHFitVault.Controllers
{
    [Authorization("ROLES_PATIENT")]
    public class PatientController : Controller
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

        /// <summary>
        /// Service object for accessing patient database functions.
        /// </summary>
        private readonly IPatientService _patientService;

        /// <summary>
        /// Service object for accessing basis peak summary database functions.
        /// </summary>
        private readonly IBasisPeakSummaryService _basisPeakService;

        /// <summary>
        /// Service for accessing medical devices.
        /// </summary>
        private readonly IMedicalDeviceService _medicalDeviceService;       

        #endregion

        #region Public Properties

        /// <summary>
        /// Session property used to store the medical devices available in the system.
        /// </summary>
        public List<MedicalDevice> MedicalDevices {
            get {
                if (Session["sMedicalDevices"] == null) {
                    Session["sMedicalDevices"] = new List<MedicalDevice>();
                }
                return (List<MedicalDevice>)Session["sMedicalDevices"];
            }
            private set {
                Session["sMedicalDevices"] = value;
            }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public PatientController(IPatientDataService patientDataService, IZephyrAccelService accelService,
                                    IZephyrBreathingService breathingService, IZephyrECGService ecgService,
                                    IZephyrEventDataService eventDataService, IZephyrSummaryService summaryService,
                                    IPatientService patientService, IBasisPeakSummaryService basisPeakService,
                                    IMedicalDeviceService medicalDeviceService) {

            _patientDataService = patientDataService;
            _accelService = accelService;
            _breathingService = breathingService;
            _ecgService = ecgService;
            _eventDataService = eventDataService;
            _summaryService = summaryService;
            _patientService = patientService;
            _basisPeakService = basisPeakService;
            _medicalDeviceService = medicalDeviceService;

        }
        #endregion

        /// <summary>
        /// Load the initial select data view when first navigating to the page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<MedicalDevice> medicalDevices = (List<MedicalDevice>)_medicalDeviceService.GetMedicalDevices();
            if(medicalDevices != null && medicalDevices.Count > 0) {
                //Save session property
                MedicalDevices = medicalDevices;
            }
            return View();
        }

        /// <summary>
        /// Process the data submitted from the form on the view.
        /// </summary>
        /// <param name="files">Comma delimited list of strings of file names.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessData(HttpPostedFileBase[] files, string medicalDeviceType) {

            MedicalDevice medicalDevice = PatientLogic.DetermineDeviceType(MedicalDevices, medicalDeviceType);

            foreach (HttpPostedFileBase file in files) { 
                
                File_Type fileType = PatientLogic.DetermineFileType(file.FileName, medicalDevice);

                //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();

                Patient patient = _patientService.GetPatient(1);               

                PatientData patientData = new PatientData() {
                    Id = Guid.NewGuid(),
                    DataType = (int)fileType,
                    Name = file.FileName,
                    UploadDate = DateTime.Now,
                    Date = DateTime.Now,                    
                    MedicalDeviceId = medicalDevice.Id,
                    Patient = patient
                };

                if(medicalDevice.Name == Device_Type.BasisPeak.ToString()) {
                    ProcessBasisPeakData(file, patientData);
                }
                else if(medicalDevice.Name == Device_Type.Zephyr.ToString()) { 
                    switch (fileType) {
                        case File_Type.Accel:
                            ProcessZephyrAccelData(file, patientData);
                            break;
                        case File_Type.Breathing:
                            ProcessZephyrBreathingData(file, patientData);
                            break;
                        case File_Type.ECG:
                            ProcessZephyrECGData(file, patientData);
                            break;
                        case File_Type.EventData:
                            ProcessZephyrEventData(file, patientData);
                            break;
                        case File_Type.Summary:
                            ProcessZephyrSummaryData(file, patientData);
                            break;
                        default:
                            break;
                    }
                }
                else if(medicalDevice.Name.Trim() == Device_Type.MicrosoftBand.ToString()) {
                    //TODO: Create msband process methods.
                }
            }           

            return RedirectToAction("Index", "Patient");
        }

        #region Protected Methods

        /// <summary>
        /// Insert Basis Peakk Summary data records from file to database.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="patientData"></param>
        protected void ProcessBasisPeakData(HttpPostedFileBase file, PatientData patientData) {
            List<BasisPeakSummaryData> basisPeakSummaryData = null;

            Stream stream = file.InputStream;

            //Note: Excel Reader is disposable per wiki on github
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {
                basisPeakSummaryData = PatientLogic.BuildBasisPeakSummaryDataList(csvReader, patientData);              
            }

            if(basisPeakSummaryData != null && basisPeakSummaryData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _basisPeakService.BulkInsert(basisPeakSummaryData);
            }
        }

        /// <summary>
        /// Insert Zephyr accel records from file into database
        /// </summary>
        /// <param name="file">Zephyr accel file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected void ProcessZephyrAccelData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrAccelerometer> zephyrAccelData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrAccelData = PatientLogic.BuildZephyrAccelDataList(csvReader, patientData);
            }
            
            if(zephyrAccelData != null && zephyrAccelData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _accelService.BulkInsert(zephyrAccelData);
            }
        }

        /// <summary>
        /// Insert Zephyr ECG records from file into database.
        /// </summary>
        /// <param name="file">Zephyr ecg file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected void ProcessZephyrECGData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrECGWaveform> zephyrEcgData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrEcgData = PatientLogic.BuildZephyrEcgDataList(csvReader, patientData);
            }

            if (zephyrEcgData != null && zephyrEcgData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _ecgService.BulkInsert(zephyrEcgData);
            }
        }

        /// <summary>
        /// Insert Zephyr Breathing records from file into database.
        /// </summary>
        /// <param name="file">Zephyr breathing file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected void ProcessZephyrBreathingData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrBreathingWaveform> zephyrBreathingData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrBreathingData = PatientLogic.BuildZephyrBreathingDataList(csvReader, patientData);
            }

            if (zephyrBreathingData != null && zephyrBreathingData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _breathingService.BulkInsert(zephyrBreathingData);
            }
        }


        /// <summary>
        /// Insert Zephyr Event Data records from file into database.
        /// </summary>
        /// <param name="file">Zephyr breathing file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected void ProcessZephyrEventData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrEventData> zephyrEventData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrEventData = PatientLogic.BuildZephyrEventDataList(csvReader, patientData);
            }

            if (zephyrEventData != null && zephyrEventData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _eventDataService.BulkInsert(zephyrEventData);
            }
        }


        /// <summary>
        /// Insert Zephyr Summary Data records from file into database.
        /// </summary>
        /// <param name="file">Zephyr breathing file selected for upload from view.</param>
        /// <param name="patientData">Patient data record created for the patient for this accel data.</param>
        protected void ProcessZephyrSummaryData(HttpPostedFileBase file, PatientData patientData) {
            List<ZephyrSummaryData> zephyrSummaryData = null;

            Stream stream = file.InputStream;
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream), true)) {

                zephyrSummaryData = PatientLogic.BuildZephyrSummaryDataList(csvReader, patientData);
            }

            if (zephyrSummaryData != null && zephyrSummaryData.Count > 0) {
                //Write data to database
                _patientDataService.CreatePatientData(patientData);
                _patientDataService.SaveChanges();

                //Bulk insert zephyr excel records
                _summaryService.BulkInsert(zephyrSummaryData);
            }
        }



        #endregion
    }
}