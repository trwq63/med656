using UAHFitVault.LogicLayer.Enums;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;
// <copyright file="PatientLogicTest.cs">Copyright ©  2016</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAHFitVault.LogicLayer.LogicFiles;

namespace UAHFitVault.LogicLayer.LogicFiles.Tests
{
    [TestClass]
    [PexClass(typeof(PatientLogic))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PatientLogicTest
    {

        [PexMethod]
        public MedicalDevice DetermineDeviceType(List<MedicalDevice> medicalDeviceTypes, string deviceType)
        {
            MedicalDevice result = PatientLogic.DetermineDeviceType(medicalDeviceTypes, deviceType);
            return result;
            // TODO: add assertions to method PatientLogicTest.DetermineDeviceType(List`1<MedicalDevice>, String)
        }

        [PexMethod]
        public File_Type DetermineFileType(string fileName, MedicalDevice medicalDevice)
        {
            File_Type result = PatientLogic.DetermineFileType(fileName, medicalDevice);
            return result;
            // TODO: add assertions to method PatientLogicTest.DetermineFileType(String, MedicalDevice)
        }
    }
}
