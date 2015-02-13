// <copyright file="CharacterExtensionsTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities
{
    [TestClass]
    [PexClass(typeof(CharacterExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CharacterExtensionsTest
    {
        [PexMethod]
        public bool EqualsIgnoreCase(char c, char other)
        {
            bool result = CharacterExtensions.EqualsIgnoreCase(c, other);
            return result;
            // TODO: add assertions to method CharacterExtensionsTest.EqualsIgnoreCase(Char, Char)
        }
        [PexMethod]
        public bool IsAlphabetic(char c)
        {
            bool result = CharacterExtensions.IsAlphabetic(c);
            return result;
            // TODO: add assertions to method CharacterExtensionsTest.IsAlphabetic(Char)
        }
        [PexMethod]
        public bool IsConsonant(char c)
        {
            bool result = CharacterExtensions.IsConsonant(c);
            return result;
            // TODO: add assertions to method CharacterExtensionsTest.IsConsonant(Char)
        }
        [PexMethod]
        public bool IsLower(char c)
        {
            bool result = CharacterExtensions.IsLower(c);
            return result;
            // TODO: add assertions to method CharacterExtensionsTest.IsLower(Char)
        }
        [PexMethod]
        public bool IsUpper(char c)
        {
            bool result = CharacterExtensions.IsUpper(c);
            return result;
            // TODO: add assertions to method CharacterExtensionsTest.IsUpper(Char)
        }
        [PexMethod]
        public char ToLower(char c)
        {
            char result = CharacterExtensions.ToLower(c);
            return result;
            // TODO: add assertions to method CharacterExtensionsTest.ToLower(Char)
        }
        [PexMethod]
        public char ToUpper(char c)
        {
            char result = CharacterExtensions.ToUpper(c);
            return result;
            // TODO: add assertions to method CharacterExtensionsTest.ToUpper(Char)
        }
    }
}
