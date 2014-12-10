using LASI;
using LASI.Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using System.Xml.Schema;

namespace LASI.UnitTests
{

    /// <summary>
    ///This is A test class for WordTest and is intended
    ///to contain all WordTest Unit Tests
    ///</summary>
    [TestClass]
    public class WordTest
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


        internal virtual Word CreateWord() {
            Word target = new CommonSingularNoun("dog");
            return target;
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void EqualsTest() {
            Word target = CreateWord();
            object obj = CreateWord();
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            obj = target;
            expected = true;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod]
        public void GetHashCodeTest() {
            Word target = CreateWord();
            int expected = (target).GetHashCode();
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest() {
            Word target = CreateWord();
            string expected = target.GetType().Name + " \"" + target.Text + "\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for Document
        ///</summary>
        [TestMethod]
        public void ParentDocTest() {
            Word target = CreateWord();
            Document parent = new Document(new[] { new Paragraph(new[] { new Sentence(new[] { new Clause(new[] { new NounPhrase(new Word[] { target }) }) }, null) }, ParagraphKind.Default) });
            Document expected = parent;
            Document actual;
            actual = target.Document;
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod]
        public void TextTest() {
            Word target = new SimpleVerb("run");
            string expected = "run";
            string actual;
            actual = target.Text;
            Assert.AreEqual(expected, actual);

        }


        /// <summary>
        ///A test for Weight
        ///</summary>
        [TestMethod]
        public void WeightTest() {
            Word target = CreateWord();
            double expected = new Random().NextDouble() * double.MaxValue;
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for VerboseOutput
        ///</summary>
        [TestMethod]
        public void VerboseOutputTest() {
            bool expected = false;
            bool actual;
            Word.VerboseOutput = expected;
            actual = Word.VerboseOutput;
            Assert.AreEqual(expected, actual);

        }


    }
}
