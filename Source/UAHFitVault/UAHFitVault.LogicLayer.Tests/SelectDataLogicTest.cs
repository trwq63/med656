using LumenWorks.Framework.IO.Csv;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.LogicLayer.Enums;
// <copyright file="SelectDataLogicTest.cs">Copyright ©  2016</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAHFitVault.LogicLayer.LogicFiles;

namespace UAHFitVault.LogicLayer.LogicFiles.Tests
{
    [TestClass]
    [PexClass(typeof(SelectDataLogic))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SelectDataLogicTest
    {

        [PexMethod]
        public File_Type DetermineFileType(string fileName, MedicalDevice medicalDevice)
        {
            File_Type result = SelectDataLogic.DetermineFileType(fileName, medicalDevice);
            return result;
            // TODO: add assertions to method SelectDataLogicTest.DetermineFileType(String, MedicalDevice)
        }

        [PexMethod]
        public MedicalDevice DetermineDeviceType(List<MedicalDevice> medicalDeviceTypes, string deviceType)
        {
            MedicalDevice result = SelectDataLogic.DetermineDeviceType(medicalDeviceTypes, deviceType);
            return result;
            // TODO: add assertions to method SelectDataLogicTest.DetermineDeviceType(List`1<MedicalDevice>, String)
        }

        [PexMethod]
        public List<ZephyrSummaryData> BuildZephyrSummaryDataList(CsvReader csvReader, PatientData patientData)
        {
            List<ZephyrSummaryData> result = SelectDataLogic.BuildZephyrSummaryDataList(csvReader, patientData);
            return result;
            // TODO: add assertions to method SelectDataLogicTest.BuildZephyrSummaryDataList(CsvReader, PatientData)
        }

        [PexMethod]
        public List<ZephyrEventData> BuildZephyrEventDataList(CsvReader csvReader, PatientData patientData)
        {
            List<ZephyrEventData> result = SelectDataLogic.BuildZephyrEventDataList(csvReader, patientData);
            return result;
            // TODO: add assertions to method SelectDataLogicTest.BuildZephyrEventDataList(CsvReader, PatientData)
        }

        [PexMethod]
        public List<ZephyrECGWaveform> BuildZephyrEcgDataList(CsvReader csvReader, PatientData patientData)
        {
            List<ZephyrECGWaveform> result = SelectDataLogic.BuildZephyrEcgDataList(csvReader, patientData);
            return result;
            // TODO: add assertions to method SelectDataLogicTest.BuildZephyrEcgDataList(CsvReader, PatientData)
        }

        [PexMethod]
        public List<ZephyrBreathingWaveform> BuildZephyrBreathingDataList(CsvReader csvReader, PatientData patientData)
        {
            List<ZephyrBreathingWaveform> result
               = SelectDataLogic.BuildZephyrBreathingDataList(csvReader, patientData);
            return result;
            // TODO: add assertions to method SelectDataLogicTest.BuildZephyrBreathingDataList(CsvReader, PatientData)
        }

        [PexMethod]
        public List<ZephyrAccelerometer> BuildZephyrAccelDataList(CsvReader csvReader, PatientData patientData)
        {
            List<ZephyrAccelerometer> result = SelectDataLogic.BuildZephyrAccelDataList(csvReader, patientData);
            return result;
            // TODO: add assertions to method SelectDataLogicTest.BuildZephyrAccelDataList(CsvReader, PatientData)
        }

        [PexMethod]
        public List<BasisPeakSummaryData> BuildBasisPeakSummaryDataList(CsvReader csvReader, PatientData patientData)
        {
            List<BasisPeakSummaryData> result
               = SelectDataLogic.BuildBasisPeakSummaryDataList(csvReader, patientData);
            return result;
            // TODO: add assertions to method SelectDataLogicTest.BuildBasisPeakSummaryDataList(CsvReader, PatientData)
        }
    }
}
