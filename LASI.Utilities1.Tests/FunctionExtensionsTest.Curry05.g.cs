using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// <copyright file="FunctionExtensionsTest.Curry05.g.cs">Copyright �  2013</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace LASI.Utilities
{
    public partial class FunctionExtensionsTest
    {
[TestMethod]
[PexGeneratedBy(typeof(FunctionExtensionsTest))]
public void Curry05235()
{
    Func<int, Func<int, Func<int, Func<int, Func<int, Func<int, Func<int, int>>>>>>>
       func;
    func = this.Curry05<int, int, int, int, int, int, int, int>
               ((Func<int, int, int, int, int, int, int, int>)null);
    Assert.IsNotNull((object)func);
}
    }
}
