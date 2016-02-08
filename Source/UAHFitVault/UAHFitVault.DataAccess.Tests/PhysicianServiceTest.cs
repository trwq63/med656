using System.Collections.Generic;
using UAHFitVault.Database.Entities;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Repositories;
// <copyright file="PhysicianServiceTest.cs">Copyright ©  2016</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAHFitVault.DataAccess;

namespace UAHFitVault.DataAccess.Tests
{
    [TestClass]
    [PexClass(typeof(PhysicianService))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PhysicianServiceTest
    {

        [PexMethod]
        public PhysicianService Constructor(IPhysicianRepository repository, IUnitOfWork unitOfWork)
        {
            PhysicianService target = new PhysicianService(repository, unitOfWork);
            return target;
            // TODO: add assertions to method PhysicianServiceTest.Constructor(IPhysicianRepository, IUnitOfWork)
        }

        [PexMethod]
        public void CreatePhysician([PexAssumeUnderTest]PhysicianService target, Physician physician)
        {
            target.CreatePhysician(physician);
            // TODO: add assertions to method PhysicianServiceTest.CreatePhysician(PhysicianService, Physician)
        }

        [PexMethod]
        public Physician GetPhysician([PexAssumeUnderTest]PhysicianService target, int id)
        {
            Physician result = target.GetPhysician(id);
            return result;
            // TODO: add assertions to method PhysicianServiceTest.GetPhysician(PhysicianService, Int32)
        }

        [PexMethod]
        public Physician GetPhysician01(
            [PexAssumeUnderTest]PhysicianService target,
            string firstName,
            string lastName
        )
        {
            Physician result = target.GetPhysician(firstName, lastName);
            return result;
            // TODO: add assertions to method PhysicianServiceTest.GetPhysician01(PhysicianService, String, String)
        }

        [PexMethod]
        public IEnumerable<Physician> GetPhysicians([PexAssumeUnderTest]PhysicianService target, string lastName)
        {
            IEnumerable<Physician> result = target.GetPhysicians(lastName);
            return result;
            // TODO: add assertions to method PhysicianServiceTest.GetPhysicians(PhysicianService, String)
        }

        [PexMethod]
        public void SaveChanges([PexAssumeUnderTest]PhysicianService target)
        {
            target.SaveChanges();
            // TODO: add assertions to method PhysicianServiceTest.SaveChanges(PhysicianService)
        }
    }
}
