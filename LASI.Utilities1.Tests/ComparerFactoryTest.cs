using System.Collections.Generic;
// <copyright file="ComparerFactoryTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities
{
    [TestClass]
    [PexClass(typeof(ComparerFactory))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ComparerFactoryTest
    {
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEqualityComparer<T> CreateEquality01<T>(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            IEqualityComparer<T> result = ComparerFactory.CreateEquality<T>(equals, getHashCode);
            return result;
            // TODO: add assertions to method ComparerFactoryTest.CreateEquality01(Func`3<!!0,!!0,Boolean>, Func`2<!!0,Int32>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEqualityComparer<T> CreateEquality02<T>(Func<T, T, bool> equals, Func<T, object>[] hashValueSelectors)
        {
            IEqualityComparer<T> result = ComparerFactory.CreateEquality<T>(equals, hashValueSelectors);
            return result;
            // TODO: add assertions to method ComparerFactoryTest.CreateEquality02(Func`3<!!0,!!0,Boolean>, Func`2<!!0,Object>[])
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEqualityComparer<T> CreateEquality<T>(Func<T, T, bool> equals)
        {
            IEqualityComparer<T> result = ComparerFactory.CreateEquality<T>(equals);
            return result;
            // TODO: add assertions to method ComparerFactoryTest.CreateEquality(Func`3<!!0,!!0,Boolean>)
        }
    }
}
