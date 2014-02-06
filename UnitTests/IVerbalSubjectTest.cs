using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IVerbalSubjectTest and is intended
    ///to contain all IVerbalSubjectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IVerbalSubjectTest
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


        internal virtual IVerbalSubject CreateIVerbalSubject() {
            // TODO: Instantiate an appropriate concrete class.
            IVerbalSubject target = null;
            return target;
        }

        /// <summary>
        ///A test for SubjectOf
        ///</summary>
        [TestMethod()]
        public void SubjectOfTest() {
            IVerbalSubject target = CreateIVerbalSubject(); // TODO: Initialize to an appropriate value
            IVerbal expected = null; // TODO: Initialize to an appropriate value
            IVerbal actual;
            target.SubjectOf = expected;
            actual = target.SubjectOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
