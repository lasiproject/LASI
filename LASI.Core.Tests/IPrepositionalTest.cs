using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IPrepositionalTest and is intended
    ///to contain all IPrepositionalTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IPrepositionalTest
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


        internal virtual IPrepositional CreateIPrepositional() {
            // TODO: Instantiate an appropriate concrete class.
            IPrepositional target = null;
            return target;
        }

        /// <summary>
        ///A test for ToTheRightOf
        ///</summary>
        [TestMethod()]
        public void ToTheRightOfTest() {
            IPrepositional target = CreateIPrepositional(); // TODO: Initialize to an appropriate value
            ILexical expected = null; // TODO: Initialize to an appropriate value
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToTheLeftOf
        ///</summary>
        [TestMethod()]
        public void ToTheLeftOfTest() {
            IPrepositional target = CreateIPrepositional(); // TODO: Initialize to an appropriate value
            ILexical expected = null; // TODO: Initialize to an appropriate value
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Role
        ///</summary>
        [TestMethod()]
        public void RoleTest() {
            IPrepositional target = CreateIPrepositional(); // TODO: Initialize to an appropriate value
            PrepositionRole actual;
            actual = target.Role;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BoundObject
        ///</summary>
        [TestMethod()]
        public void BoundObjectTest() {
            IPrepositional target = CreateIPrepositional(); // TODO: Initialize to an appropriate value
            ILexical actual;
            actual = target.BoundObject;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BindObject
        ///</summary>
        [TestMethod()]
        public void BindObjectTest() {
            IPrepositional target = CreateIPrepositional(); // TODO: Initialize to an appropriate value
            ILexical prepositionalObject = null; // TODO: Initialize to an appropriate value
            target.BindObject(prepositionalObject);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
