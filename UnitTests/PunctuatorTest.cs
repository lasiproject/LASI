using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for PunctuatorTest and is intended
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
        ///A test for Punctuation Constructor
        ///</summary>
        [TestMethod()]
        public void PunctuatorConstructorTest() {
            char puncChar = '\u0021';
            Punctuator target = new Punctuator(puncChar);
            Assert.IsTrue(target.LiteralCharacter == puncChar, "Punctuator Character Constructor Works!");
        }


        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest() {
            char puncChar = '\u0021';
            Punctuator target = new Punctuator(puncChar);
            string expected = "!";
            string actual;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
       } 

        /// <summary>
        ///A test for Punctuator Constructor
        ///</summary>
        [TestMethod()]
        public void PunctuatorConstructorTest2() {
            string punctuation = "!";
            Punctuator target = new Punctuator(punctuation);
            string expected = "!";
            string actual;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Punctuator Constructor
        ///</summary>
        [TestMethod()]
        public void PunctuatorConstructorTest3() {
            char punctuation = '.';
            Punctuator target = new Punctuator(punctuation);
            char expected = '.';
            char actual = target.LiteralCharacter;
            Assert.AreEqual(expected, actual);
        }
    }
}
