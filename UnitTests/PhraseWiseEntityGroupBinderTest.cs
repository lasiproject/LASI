using LASI.Core.Binding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;
using System.Collections.Generic;
using LASI.Core.DocumentStructures;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for PhraseWiseEntityGroupBinderTest and is intended
    ///to contain all PhraseWiseEntityGroupBinderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhraseWiseEntityGroupBinderTest
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
        ///A test for EntityGroups
        ///</summary>
        [TestMethod()]
        public void EntityGroupsTest() {
            PhraseWiseEntityGroupBinder target = new PhraseWiseEntityGroupBinder(); // TODO: Initialize to an appropriate value
            IEnumerable<IAggregateEntity> actual;
            actual = target.EntityGroups;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Bind
        ///</summary>
        [TestMethod()]
        public void BindTest() {
            PhraseWiseEntityGroupBinder target = new PhraseWiseEntityGroupBinder(); // TODO: Initialize to an appropriate value
            Sentence sentence = null; // TODO: Initialize to an appropriate value
            target.Bind(sentence);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PhraseWiseEntityGroupBinder Constructor
        ///</summary>
        [TestMethod()]
        public void PhraseWiseEntityGroupBinderConstructorTest() {
            PhraseWiseEntityGroupBinder target = new PhraseWiseEntityGroupBinder();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
