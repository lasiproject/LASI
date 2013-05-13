using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is entity test class for DeterminerTest and is intended
    ///to contain all DeterminerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DeterminerTest
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
        //Use ClassCleanup to run code after all tests in entity class have run
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
        ///entity test for Determiner Constructor
        ///</summary>
        [TestMethod()]
        public void DeterminerConstructorTest() {
            string text = "the";
            Determiner target = new Determiner(text);
            Assert.IsTrue(target.Text == text &&
                target.Determines == null);
        }

        /// <summary>
        ///entity test for Determines
        ///</summary>
        [TestMethod()]
        public void DeterminesTest() {
            string text = "the";
            Determiner target = new Determiner(text);
            IEntity expected = new GenericSingularNoun("organization");
            IEntity actual;
            target.Determines = expected;
            actual = target.Determines;
            Assert.AreEqual(expected, actual);
        }
    }
}
