using LASI.Core.Patternization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for MatcherTest and is intended
    ///to contain all MatcherTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MatcherTest
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
        //Use ClassCleanup to run code after all tests in a class have run
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
        ///A test for Match
        ///</summary>
        public void MatchTestHelper<T>()
            where T : class , ILexical {
            T value = null; // TODO: Initialize to an appropriate value
            Match<T> expected = null; // TODO: Initialize to an appropriate value
            Match<T> actual;
            actual = Matcher.Match<T>(value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void MatchTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call MatchTestHelper<T>() with appropriate type parameters.");
        }
    }
}
