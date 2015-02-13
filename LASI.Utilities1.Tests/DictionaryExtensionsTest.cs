using System.Collections.Generic;
// <copyright file="DictionaryExtensionsTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities
{
    [TestClass]
    [PexClass(typeof(DictionaryExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class DictionaryExtensionsTest
    {
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public TValue GetValueOrDefault01<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue
        )
        {
            TValue result = DictionaryExtensions.GetValueOrDefault<TKey, TValue>(dictionary, key, defaultValue);
            return result;
            // TODO: add assertions to method DictionaryExtensionsTest.GetValueOrDefault01(IDictionary`2<!!0,!!1>, !!0, !!1)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public TValue GetValueOrDefault02<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueFactory
        )
        {
            TValue result
               = DictionaryExtensions.GetValueOrDefault<TKey, TValue>(dictionary, key, defaultValueFactory);
            return result;
            // TODO: add assertions to method DictionaryExtensionsTest.GetValueOrDefault02(IDictionary`2<!!0,!!1>, !!0, Func`1<!!1>)
        }
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue result = DictionaryExtensions.GetValueOrDefault<TKey, TValue>(dictionary, key);
            return result;
            // TODO: add assertions to method DictionaryExtensionsTest.GetValueOrDefault(IDictionary`2<!!0,!!1>, !!0)
        }
    }
}
