using System.IO;
// <copyright file="XmlConfigTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities
{
    [TestClass]
    [PexClass(typeof(XmlConfig))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class XmlConfigTest
    {
        [PexMethod]
        [PexAllowedException(typeof(FileNotFoundException))]
        internal XmlConfig Constructor(string filePath)
        {
            XmlConfig target = new XmlConfig(filePath);
            return target;
            // TODO: add assertions to method XmlConfigTest.Constructor(String)
        }
    }
}
