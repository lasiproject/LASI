using LASI.Core.Heuristics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for ExpressionExtensionsTest and is intended
    ///to contain all ExpressionExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExpressionExtensionsTest
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
        ///A test for SetRelationshipLookup
        ///</summary>
        [TestMethod()]
        public void SetRelationshipLookupTest() {
            IEntity entity = null; // TODO: Initialize to an appropriate value
            IRelationshipLookup<IEntity, IVerbal> relationshipLookup = null; // TODO: Initialize to an appropriate value
            ExpressionExtensions.SetRelationshipLookup(entity, relationshipLookup);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for On
        ///</summary>
        [TestMethod()]
        public void OnTest() {
            Nullable<ActionsRelatedOn> relatorSet = new Nullable<ActionsRelatedOn>(); // TODO: Initialize to an appropriate value
            IVerbal relator = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = ExpressionExtensions.On(relatorSet, relator);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsRelatedTo
        ///</summary>
        [TestMethod()]
        public void IsRelatedToTest() {
            IEntity performer = null; // TODO: Initialize to an appropriate value
            IEntity receiver = null; // TODO: Initialize to an appropriate value
            Nullable<ActionsRelatedOn> expected = new Nullable<ActionsRelatedOn>(); // TODO: Initialize to an appropriate value
            Nullable<ActionsRelatedOn> actual;
            actual = ExpressionExtensions.IsRelatedTo(performer, receiver);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
