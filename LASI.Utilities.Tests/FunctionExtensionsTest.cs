using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Utilities.Tests
{
    /// <summary>
    ///This is A test class for FunctionExtensionsTest and is intended
    ///to contain all FunctionExtensionsTest Unit Tests
    /// </summary>
    [TestClass]
    public class FunctionExtensionsTest
    {
        /// <summary>
        ///A test for Compose
        /// </summary>
        public void ComposeTest1Helper<R, U, T>()
        {
            Func<R, T> f = r => default(T);
            Func<U, R> g = u => default(R);
            Func<U, T> expected = u => default(T);
            Func<U, T> actual;
            var y = f.Compose(g);
            actual = FunctionExtensions.Compose(f, g);
            Assert.AreEqual(expected(default(U)), default(T));

        }

        [TestMethod]
        public void ComposeTest1()
        {
            ComposeTest1Helper<GenericParameterHelper, GenericParameterHelper, GenericParameterHelper>();
        }

    }




}
