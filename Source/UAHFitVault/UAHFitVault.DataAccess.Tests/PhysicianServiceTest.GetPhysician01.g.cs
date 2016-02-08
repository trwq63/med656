using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Repositories;
using UAHFitVault.Database.Entities;
using UAHFitVault.DataAccess;
// <copyright file="PhysicianServiceTest.GetPhysician01.g.cs">Copyright ©  2016</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace UAHFitVault.DataAccess.Tests
{
    public partial class PhysicianServiceTest
    {

[TestMethod]
[PexGeneratedBy(typeof(PhysicianServiceTest))]
public void GetPhysician01312()
{
    PhysicianService physicianService;
    Physician physician;
    physicianService =
      new PhysicianService((IPhysicianRepository)null, (IUnitOfWork)null);
    physician = this.GetPhysician01(physicianService, (string)null, (string)null);
    Assert.IsNull((object)physician);
    Assert.IsNotNull((object)physicianService);
}

[TestMethod]
[PexGeneratedBy(typeof(PhysicianServiceTest))]
public void GetPhysician01866()
{
    PhysicianService physicianService;
    Physician physician;
    physicianService =
      new PhysicianService((IPhysicianRepository)null, (IUnitOfWork)null);
    physician = this.GetPhysician01(physicianService, "\0", (string)null);
    Assert.IsNull((object)physician);
    Assert.IsNotNull((object)physicianService);
}

[TestMethod]
[PexGeneratedBy(typeof(PhysicianServiceTest))]
[PexRaisedException(typeof(NullReferenceException))]
public void GetPhysician01ThrowsNullReferenceException984()
{
    PhysicianService physicianService;
    Physician physician;
    physicianService =
      new PhysicianService((IPhysicianRepository)null, (IUnitOfWork)null);
    physician = this.GetPhysician01(physicianService, "\0", "\0");
}
    }
}
