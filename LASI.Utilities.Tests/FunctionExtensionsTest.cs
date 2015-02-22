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
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in A class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        #endregion




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
