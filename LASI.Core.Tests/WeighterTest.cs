using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.DocumentStructures;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for WeighterTest and is intended
    ///to contain all WeighterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WeighterTest
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
        ///A test for WeightAsync
        ///</summary>
        [TestMethod()]
        public void WeightAsyncTest() {
            Document document = null; // TODO: Initialize to an appropriate value
            Task expected = null; // TODO: Initialize to an appropriate value
            Task actual;
            actual = Weighter.WeightAsync(document);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Weight
        ///</summary>
        [TestMethod()]
        public void WeightTest() {
            Document document = null; // TODO: Initialize to an appropriate value
            Weighter.Weight(document);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetWeightingTasks
        ///</summary>
        [TestMethod()]
        public void GetWeightingTasksTest() {
            Document document = null; // TODO: Initialize to an appropriate value
            IEnumerable<ProcessingTask> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ProcessingTask> actual;
            actual = Weighter.GetWeightingTasks(document);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
