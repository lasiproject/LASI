using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LASI.Core.DocumentStructures;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for ClauseTest and is intended
    ///to contain all ClauseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ClauseTest
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
        ///A test for Words
        ///</summary>
        [TestMethod()]
        public void WordsTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            IEnumerable<Word> actual;
            actual = target.Words;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Weight
        ///</summary>
        [TestMethod()]
        public void WeightTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [TestMethod()]
        public void TypeTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            Type actual;
            actual = target.Type;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Text;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PrepositionOnRight
        ///</summary>
        [TestMethod()]
        public void PrepositionOnRightTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            IPrepositional expected = null; // TODO: Initialize to an appropriate value
            IPrepositional actual;
            target.PrepositionOnRight = expected;
            actual = target.PrepositionOnRight;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PrepositionOnLeft
        ///</summary>
        [TestMethod()]
        public void PrepositionOnLeftTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            IPrepositional expected = null; // TODO: Initialize to an appropriate value
            IPrepositional actual;
            target.PrepositionOnLeft = expected;
            actual = target.PrepositionOnLeft;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ParentParagraph
        ///</summary>
        [TestMethod()]
        public void ParentParagraphTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            Paragraph actual;
            actual = target.ParentParagraph;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MetaWeight
        ///</summary>
        [TestMethod()]
        public void MetaWeightTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            target.MetaWeight = expected;
            actual = target.MetaWeight;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Document
        ///</summary>
        [TestMethod()]
        public void DocumentTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            Document actual;
            actual = target.Document;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EstablishParent
        ///</summary>
        [TestMethod()]
        public void EstablishParentTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases); // TODO: Initialize to an appropriate value
            Sentence sentence = null; // TODO: Initialize to an appropriate value
            target.EstablishParent(sentence);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Clause Constructor
        ///</summary>
        [TestMethod()]
        public void ClauseConstructorTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Clause target = new Clause(phrases);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
