using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.DocumentStructures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for CrossDocumentJoinerTest and is intended
    ///to contain all CrossDocumentJoinerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CrossDocumentJoinerTest
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
        ///A test for GetCommonResultsAsnyc
        ///</summary>
        [TestMethod()]
        public void GetCommonResultsAsnycTest() {
            CrossDocumentJoiner target = new CrossDocumentJoiner(); // TODO: Initialize to an appropriate value
            IEnumerable<Document> documents = null; // TODO: Initialize to an appropriate value
            Task<IEnumerable<SVORelationship>> expected = null; // TODO: Initialize to an appropriate value
            Task<IEnumerable<SVORelationship>> actual;
            actual = target.GetCommonResultsAsnyc(documents);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCommonResults
        ///</summary>
        [TestMethod()]
        public void GetCommonResultsTest() {
            CrossDocumentJoiner target = new CrossDocumentJoiner(); // TODO: Initialize to an appropriate value
            IEnumerable<Document> documents = null; // TODO: Initialize to an appropriate value
            IEnumerable<SVORelationship> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<SVORelationship> actual;
            actual = target.GetCommonResults(documents);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CrossDocumentJoiner Constructor
        ///</summary>
        [TestMethod()]
        public void CrossDocumentJoinerConstructorTest() {
            CrossDocumentJoiner target = new CrossDocumentJoiner();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
