using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for ModalTest and is intended
    ///to contain all ModalTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ModalTest
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
        ///A test for Modal Constructor
        ///</summary>
        [TestMethod()]
        public void ModalConstructorTest() {
            string text = "can";
            Modal target = new Modal(text);
            Assert.IsTrue(target.Modifies == null && target.Text == text);
        }

        /// <summary>
        ///A test for Modifies
        ///</summary>
        [TestMethod()]
        public void ModifiesTest() {
            string text = "can";
            Modal target = new Modal(text);
            IModalityModifiable expected = new PresentTenseVerb("capitulate");
            IModalityModifiable actual;
            target.Modifies = expected;
            actual = target.Modifies;
            Assert.AreEqual(expected, actual);
        }
    }
}
