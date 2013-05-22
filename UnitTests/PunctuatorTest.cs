using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is entity test class for PunctuatorTest and is intended
    ///to contain all PunctuatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PunctuatorTest
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
        ///entity test for Punctuation Constructor
        ///</summary>
        [TestMethod()]
        public void PunctuatorConstructorTest() {
            char puncChar = '\u0021';
            Punctuation target = new Punctuation(puncChar);
            Assert.IsTrue(target.ActualCharacter == puncChar, "Punctuator Character Constructor Works!");
        }

        /// <summary>
        ///entity test for Punctuation Constructor
        ///</summary>
        [TestMethod()]
        public void PunctuatorConstructorTest1() {
            string puncString = "!";
            Punctuation target = new Punctuation(puncString);
            Assert.IsTrue(target.AliasString == puncString, "Punctuator String Constructor Works!"); //possibly?
        }

        /// <summary>
        ///entity test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest() {
            char puncChar = '\u0021';
            Punctuation target = new Punctuation(puncChar);
            string expected = "!";
            string actual;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }
    }
}
