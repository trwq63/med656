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
    }
}
