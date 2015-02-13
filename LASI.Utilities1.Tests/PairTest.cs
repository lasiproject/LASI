// <copyright file="PairTest.cs">Copyright ©  2013</copyright>

using System;
using LASI.Utilities;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.Utilities
{
    [TestClass]
    [PexClass(typeof(Pair))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PairTest
    {
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        public Pair<T1, T2> Create<T1, T2>(T1 first, T2 second)
        {
            Pair<T1, T2> result = Pair.Create<T1, T2>(first, second);
            return result;
            // TODO: add assertions to method PairTest.Create(!!0, !!1)
        }
    }
}
