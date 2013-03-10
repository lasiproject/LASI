using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for GenericPluralNounTest and is intended
    ///to contain all GenericPluralNounTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GenericPluralNounTest
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
        ///a test for GenericPluralNoun Constructor
        ///</summary>
        [TestMethod()]
        public void GenericPluralNounConstructorTest() {
            string text = "clowns";
            GenericPluralNoun target = new GenericPluralNoun(text);
            Assert.IsTrue(target.Text == text);
        }

        /// <summary>
        ///a test for Quantifier
        ///</summary>
        [TestMethod()]
        public void QuantifierTest() {
            string text = "clowns";
            GenericPluralNoun target = new GenericPluralNoun(text);
            Quantifier expected = new Quantifier("22");
            Quantifier actual;
            target.Quantifier = expected;
            actual = target.Quantifier;
            Assert.AreEqual(expected, actual);
        }
    }
}
