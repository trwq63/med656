using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAHFitVault.Database.Infrastructure;
using UAHFitVault.Database.Repositories;
using UAHFitVault.DataAccess;
// <copyright file="PhysicianServiceTest.Constructor.g.cs">Copyright ©  2016</copyright>
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
public void Constructor669()
{
    PhysicianService physicianService;
    physicianService =
      this.Constructor((IPhysicianRepository)null, (IUnitOfWork)null);
    Assert.IsNotNull((object)physicianService);
}
    }
}
