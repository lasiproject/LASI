using System;
using NFluent;
using Xunit;

namespace LASI.Utilities.Tests
{
    using GenericParameterHelper = Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper;
    /// <summary>
    ///This is A test class for FunctionExtensionsTest and is intended
    ///to contain all FunctionExtensionsTest Unit Tests
    /// </summary>
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

            Check.That(expected(default(U))).IsEqualTo(default(T));

        }

        [Fact]
        public void ComposeTest1()
        {
            ComposeTest1Helper<GenericParameterHelper, GenericParameterHelper, GenericParameterHelper>();
        }

    }




}
