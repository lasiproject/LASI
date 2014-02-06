using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IPairedPunctuatorTest and is intended
    ///to contain all IPairedPunctuatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IPairedPunctuatorTest
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
        ///A test for PairedInstance
        ///</summary>
        public void PairedInstanceTestHelper<TPunctuator>()
            where TPunctuator : IPairedPunctuator<TPunctuator> {
            IPairedPunctuator<TPunctuator> target = CreateIPairedPunctuator<TPunctuator>(); // TODO: Initialize to an appropriate value
            TPunctuator actual;
            actual = target.PairedInstance;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        internal virtual IPairedPunctuator<TPunctuator> CreateIPairedPunctuator<TPunctuator>()
            where TPunctuator : IPairedPunctuator<TPunctuator> {
            // TODO: Instantiate an appropriate concrete class.
            IPairedPunctuator<TPunctuator> target = null;
            return target;
        }

        [TestMethod()]
        public void PairedInstanceTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TPu" +
                    "nctuator. Please call PairedInstanceTestHelper<TPunctuator>() with appropriate t" +
                    "ype parameters.");
        }

        /// <summary>
        ///A test for PairWith
        ///</summary>
        public void PairWithTestHelper<TPunctuator>()
            where TPunctuator : IPairedPunctuator<TPunctuator> {
            IPairedPunctuator<TPunctuator> target = CreateIPairedPunctuator<TPunctuator>(); // TODO: Initialize to an appropriate value
            TPunctuator complement = default(TPunctuator); // TODO: Initialize to an appropriate value
            target.PairWith(complement);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void PairWithTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TPu" +
                    "nctuator. Please call PairWithTestHelper<TPunctuator>() with appropriate type pa" +
                    "rameters.");
        }
    }
}
