using LASI;
using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is A test class for AdverbTest and is intended
    ///to contain all AdverbTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdverbTest
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
        //Use ClassCleanup to run code after all tests in A class have run
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
        ///</summary>
        [TestMethod()]
        public void ModiffiedTest() {
            string text = "quickly";
            Adverb target = new Adverb(text);
            IAdverbialModifiable expected = new Verb("run", VerbForm.Base);
            IAdverbialModifiable actual;
            target.Modifies = expected;
            actual = target.Modifies;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Adverb Constructor
        ///</summary>
        [TestMethod()]
        public void AdverbConstructorTest() {
            string text = "quickly";
            Adverb target = new Adverb(text);
            Assert.IsTrue(target.Text == "quickly" && target.Modifies == null);
        }
    }
}
