using LASI.Core.Binding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;
using System.Collections.Generic;
using LASI.Core.DocumentStructures;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for ObjectBinderTest and is intended
    ///to contain all ObjectBinderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObjectBinderTest
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
        ///A test for Bind
        ///</summary>
        [TestMethod()]
        public void BindTest() {
            ObjectBinder target = new ObjectBinder(); // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> contiguousPhrases = null; // TODO: Initialize to an appropriate value
            target.Bind(contiguousPhrases);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Bind
        ///</summary>
        [TestMethod()]
        public void BindTest1() {
            ObjectBinder target = new ObjectBinder(); // TODO: Initialize to an appropriate value
            Sentence sentence = null; // TODO: Initialize to an appropriate value
            target.Bind(sentence);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ObjectBinder Constructor
        ///</summary>
        [TestMethod()]
        public void ObjectBinderConstructorTest() {
            ObjectBinder target = new ObjectBinder();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
