using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgorithmAssemblyUnitTestProject
{
    
    
    /// <summary>
    ///This is a test class for ForeignWordTest and is intended
    ///to contain all ForeignWordTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ForeignWordTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
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
        ///A test for ForeignWord Constructor
        ///</summary>
        [TestMethod()]
        public void ForeignWordConstructorTest()
        {
            string text = "Bonjour"; 
            ForeignWord target = new ForeignWord(text);
            Assert.IsTrue(target.Text == text);
            Assert.IsTrue(target.UsedAsType == null);
        }

        /// <summary>
        ///A test for UsedAsType
        ///</summary>
        [TestMethod()]
        public void UsedAsTypeTest()
        {
            string text = "Bonjour"; 
            ForeignWord target = new ForeignWord(text); 
            Type expected = typeof(Interjection);
            Type actual;
            target.UsedAsType = expected;
            actual = target.UsedAsType;
            Assert.AreEqual(expected, actual);
        }
    }
}
