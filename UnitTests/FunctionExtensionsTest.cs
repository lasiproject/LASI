using LASI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmAssemblyUnitTestProject
{
    /// <summary>
    ///This is A test class for FunctionExtensionsTest and is intended
    ///to contain all FunctionExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FunctionExtensionsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
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
        //
        #endregion


        /// <summary>
        ///A test for Identity
        ///</summary>
        public void IdentityTestHelper<T>() {
            T value = default(T);
            T expected = default(T);
            T actual;
            actual = FunctionExtensions.Identity<T>(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IdentityTest() {
            IdentityTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Compose
        ///</summary>
        public void ComposeTestHelper<T>() {


        }

        [TestMethod()]
        public void ComposeTest() {
            Func<int, int> func = x => x * x * x;//cubes an int, A value type
            Func<int, int>[] fs = new Func<int, int>[] { x => x / x, x => x + 1, x => x * 3 };
            var function = FunctionExtensions.Compose(func, fs);
            for (int i = 1; i < 100; i++) {
                int k = i % 2 == 0 ? new Random().Next(1, 200) : new Random().Next(-199, -1);
                int a = i * 3;
                int b = a + 1;
                int c = b / b;
                int expected = c * c * c;
                Assert.AreEqual(expected, function(k));

            }
        }

        /// <summary>
        ///A test for Compose
        ///</summary>
        public void ComposeTest1Helper<T, U, R>() {
            Func<R, T> f = r => default(T);
            Func<U, R> g = u => default(R);
            Func<U, T> expected = u => default(T);
            Func<U, T> actual;
            actual = FunctionExtensions.Compose<T, U, R>(f, g);
            Assert.AreEqual(expected(default(U)), default(T));

        }

        [TestMethod()]
        public void ComposeTest1() {
            ComposeTest1Helper<GenericParameterHelper, GenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///A test for AsEnumerable
        ///</summary>
        public void AsEnumerableTestHelper<T>() {
            T t = default(T);

            IEnumerable<T> actual = FunctionExtensions.AsEnumerable<T>(t);
            Assert.AreEqual(t, actual.First());
            Assert.IsTrue(actual.Count() == 1);

        }

        [TestMethod()]

        public void AsEnumerableTest() {
            AsEnumerableTestHelper<GenericParameterHelper>();
        }
    }




}
