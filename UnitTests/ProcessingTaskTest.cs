using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for ProcessingTaskTest and is intended
    ///to contain all ProcessingTaskTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProcessingTaskTest
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
        ///A test for ProcessingTask Constructor
        ///</summary>
        [TestMethod()]
        public void ProcessingTaskConstructorTest() {
            Action workToPerform = null; // TODO: Initialize to an appropriate value
            string initializationMessage = string.Empty; // TODO: Initialize to an appropriate value
            string completionMessage = string.Empty; // TODO: Initialize to an appropriate value
            double percentWorkRepresented = 0F; // TODO: Initialize to an appropriate value
            ProcessingTask target = new ProcessingTask(workToPerform, initializationMessage, completionMessage, percentWorkRepresented);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for ProcessingTask Constructor
        ///</summary>
        [TestMethod()]
        public void ProcessingTaskConstructorTest1() {
            Task workToPerform = null; // TODO: Initialize to an appropriate value
            string initializationMessage = string.Empty; // TODO: Initialize to an appropriate value
            string completionMessage = string.Empty; // TODO: Initialize to an appropriate value
            double percentWorkRepresented = 0F; // TODO: Initialize to an appropriate value
            ProcessingTask target = new ProcessingTask(workToPerform, initializationMessage, completionMessage, percentWorkRepresented);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
