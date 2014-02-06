using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IDescribableTest and is intended
    ///to contain all IDescribableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IDescribableTest
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


        internal virtual IDescribable CreateIDescribable() {
            // TODO: Instantiate an appropriate concrete class.
            IDescribable target = null;
            return target;
        }

        /// <summary>
        ///A test for Descriptors
        ///</summary>
        [TestMethod()]
        public void DescriptorsTest() {
            IDescribable target = CreateIDescribable(); // TODO: Initialize to an appropriate value
            IEnumerable<IDescriptor> actual;
            actual = target.Descriptors;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BindDescriptor
        ///</summary>
        [TestMethod()]
        public void BindDescriptorTest() {
            IDescribable target = CreateIDescribable(); // TODO: Initialize to an appropriate value
            IDescriptor descriptor = null; // TODO: Initialize to an appropriate value
            target.BindDescriptor(descriptor);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
