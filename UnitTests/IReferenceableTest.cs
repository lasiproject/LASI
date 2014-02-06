using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IReferenceableTest and is intended
    ///to contain all IReferenceableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IReferenceableTest
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


        internal virtual IReferenceable CreateIReferenceable() {
            // TODO: Instantiate an appropriate concrete class.
            IReferenceable target = null;
            return target;
        }

        /// <summary>
        ///A test for Referees
        ///</summary>
        [TestMethod()]
        public void RefereesTest() {
            IReferenceable target = CreateIReferenceable(); // TODO: Initialize to an appropriate value
            IEnumerable<IReferencer> actual;
            actual = target.Referees;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BindReferencer
        ///</summary>
        [TestMethod()]
        public void BindReferencerTest() {
            IReferenceable target = CreateIReferenceable(); // TODO: Initialize to an appropriate value
            IReferencer referee = null; // TODO: Initialize to an appropriate value
            target.BindReferencer(referee);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
