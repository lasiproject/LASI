using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for ModalAuxilaryTest and is intended
    ///to contain all ModalAuxilaryTest Unit Tests
    /// </summary>
    [TestClass]
    public class ModalAuxilaryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
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
        ///A test for Modifies
        /// </summary>
        [TestMethod]
        public void ModifiesTest() {
            string text = "can";
            ModalAuxilary target = new ModalAuxilary(text);
            IModalityModifiable expected = new BaseVerb("do");
            IModalityModifiable actual;
            target.Modifies = expected;
            actual = target.Modifies;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ModalAuxilary Constructor
        /// </summary>
        [TestMethod]
        public void ModalAuxilaryConstructorTest() {
            string text = "cannot";
            ModalAuxilary target = new ModalAuxilary(text);
            Assert.IsTrue(target.Text == text);
            Assert.IsTrue(target.Modifies == null);
        }
    }
}
