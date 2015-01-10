using LASI.Core.Binding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.DocumentStructures;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for SubjectBinderTest and is intended
    ///to contain all SubjectBinderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SubjectBinderTest
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
        ///A test for Display
        ///</summary>
        [TestMethod()]
        public void DisplayTest() {
            SubjectBinder target = new SubjectBinder(); // TODO: Initialize to an appropriate value
            target.Display();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Bind
        ///</summary>
        [TestMethod()]
        public void BindTest() {
            SubjectBinder target = new SubjectBinder(); // TODO: Initialize to an appropriate value
            Sentence s = null; // TODO: Initialize to an appropriate value
            target.Bind(s);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SubjectBinder Constructor
        ///</summary>
        [TestMethod()]
        public void SubjectBinderConstructorTest() {
            SubjectBinder target = new SubjectBinder();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
