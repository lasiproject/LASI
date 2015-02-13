// <copyright file="StringExtensionsTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities
{
    [TestClass]
    [PexClass(typeof(StringExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class StringExtensionsTest
    {
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public string[] SplitRemoveEmpty01(string value, string[] seperator)
        {
            string[] result = StringExtensions.SplitRemoveEmpty(value, seperator);
            return result;
            // TODO: add assertions to method StringExtensionsTest.SplitRemoveEmpty01(String, String[])
        }
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public string[] SplitRemoveEmpty(string value, char[] seperator)
        {
            string[] result = StringExtensions.SplitRemoveEmpty(value, seperator);
            return result;
            // TODO: add assertions to method StringExtensionsTest.SplitRemoveEmpty(String, Char[])
        }
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public string RemoveSubstrings(string value, string[] remove)
        {
            string result = StringExtensions.RemoveSubstrings(value, remove);
            return result;
            // TODO: add assertions to method StringExtensionsTest.RemoveSubstrings(String, String[])
        }
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public string RemoveAnyOf(string value, char[] anyOf)
        {
            string result = StringExtensions.RemoveAnyOf(value, anyOf);
            return result;
            // TODO: add assertions to method StringExtensionsTest.RemoveAnyOf(String, Char[])
        }
        [PexMethod(MaxRunsWithoutNewTests = 200)]
        public bool IsNullOrWhiteSpace(string value)
        {
            bool result = StringExtensions.IsNullOrWhiteSpace(value);
            return result;
            // TODO: add assertions to method StringExtensionsTest.IsNullOrWhiteSpace(String)
        }
    }
}
