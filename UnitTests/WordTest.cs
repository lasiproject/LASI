using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
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
    [TestClass()]
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
        [TestMethod()]
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
        ///A test for EstablishParent
        ///</summary>
        [TestMethod()]
        public void EstablishParentTest() {
            Word target = CreateWord();
            Phrase phrase = new NounPhrase(new Word[] { new Adjective("Psychotic"), target });
            target.EstablishParent(phrase);
            Assert.IsTrue(target.Phrase == phrase);
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
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
        [TestMethod()]
        public void ToStringTest() {
            Word target = CreateWord();
            string expected = target.Type.Name + " \"" + target.Text + "\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NextWord
        ///</summary>
        [TestMethod()]
        public void NextWordTest() {
            Word target = CreateWord();
            Word expected = new ToLinker();
            Word actual;
            target.NextWord = expected;
            actual = target.NextWord;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Document
        ///</summary>
        [TestMethod()]
        public void ParentDocTest() {
            Word target = CreateWord();
            Document parent = new Document(new[] { new Paragraph(new[] { new Sentence(new[] { new Clause(new[] { new NounPhrase(new Word[] { target }) }) }, null) }, ParagraphKind.Default) });
            Document expected = parent;
            Document actual;
            actual = target.Document;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PreviousWord
        ///</summary>
        [TestMethod()]
        public void PreviousWordTest() {
            Word target = CreateWord();
            Word expected = new Determiner("the");
            Word actual;
            target.PreviousWord = expected;
            actual = target.PreviousWord;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest() {
            Word target = new Verb("run", VerbForm.Base);
            string expected = "run";
            string actual;
            actual = target.Text;
            Assert.AreEqual(expected, actual);

        }


        /// <summary>
        ///A test for Weight
        ///</summary>
        [TestMethod()]
        public void WeightTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for VerboseOutput
        ///</summary>
        [TestMethod()]
        public void VerboseOutputTest() {
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            Word.VerboseOutput = expected;
            actual = Word.VerboseOutput;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [TestMethod()]
        public void TypeTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
            Type actual;
            actual = target.Type;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

      
    }
}
