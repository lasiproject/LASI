using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IAdverbialModifiableTest and is intended
    ///to contain all IAdverbialModifiableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IAdverbialModifiableTest
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


        internal virtual IAdverbialModifiable CreateIAdverbialModifiable() {
            // TODO: Instantiate an appropriate concrete class.
            IAdverbialModifiable target = null;
            return target;
        }

        /// <summary>
        ///A test for Modifiers
        ///</summary>
        [TestMethod()]
        public void ModifiersTest() {
            IAdverbialModifiable target = CreateIAdverbialModifiable(); // TODO: Initialize to an appropriate value
            IEnumerable<IAdverbial> actual;
            actual = target.Modifiers;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ModifyWith
        ///</summary>
        [TestMethod()]
        public void ModifyWithTest() {
            IAdverbialModifiable target = CreateIAdverbialModifiable(); // TODO: Initialize to an appropriate value
            IAdverbial adv = null; // TODO: Initialize to an appropriate value
            target.ModifyWith(adv);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
