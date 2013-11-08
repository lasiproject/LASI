using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for IEntityTest and is intended
    ///to contain all IEntityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IEntityTest
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


        internal virtual IEntity CreateIEntity() {
            // TODO: Instantiate an appropriate concrete class.
            IEntity target = null;
            return target;
        }

        /// <summary>
        ///A test for EntityKind
        ///</summary>
        [TestMethod()]
        public void EntityKindTest() {
            IEntity target = CreateIEntity(); // TODO: Initialize to an appropriate value
            EntityKind actual;
            actual = target.EntityKind;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
