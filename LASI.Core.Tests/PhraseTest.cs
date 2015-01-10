using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core.DocumentStructures;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for PhraseTest and is intended
    ///to contain all PhraseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhraseTest
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


        internal virtual Phrase CreatePhrase() {
            // TODO: Instantiate an appropriate concrete class.
            Phrase target = null;
            return target;
        }

        /// <summary>
        ///A test for Weight
        ///</summary>
        [TestMethod()]
        public void WeightTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
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
            Phrase.VerboseOutput = expected;
            actual = Phrase.VerboseOutput;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        [TestMethod()]
        public void TypeTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            Type actual;
            actual = target.Type;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Text;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Sentence
        ///</summary>
        [TestMethod()]
        public void SentenceTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            Sentence expected = null; // TODO: Initialize to an appropriate value
            Sentence actual;
            target.Sentence = expected;
            actual = target.Sentence;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PreviousPhrase
        ///</summary>
        [TestMethod()]
        public void PreviousPhraseTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            Phrase expected = null; // TODO: Initialize to an appropriate value
            Phrase actual;
            target.PreviousPhrase = expected;
            actual = target.PreviousPhrase;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PrepositionOnRight
        ///</summary>
        [TestMethod()]
        public void PrepositionOnRightTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
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
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            IPrepositional expected = null; // TODO: Initialize to an appropriate value
            IPrepositional actual;
            target.PrepositionOnLeft = expected;
            actual = target.PrepositionOnLeft;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Paragraph
        ///</summary>
        [TestMethod()]
        public void ParagraphTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            Paragraph actual;
            actual = target.Paragraph;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NextPhrase
        ///</summary>
        [TestMethod()]
        public void NextPhraseTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            Phrase expected = null; // TODO: Initialize to an appropriate value
            Phrase actual;
            target.NextPhrase = expected;
            actual = target.NextPhrase;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MetaWeight
        ///</summary>
        [TestMethod()]
        public void MetaWeightTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            target.MetaWeight = expected;
            actual = target.MetaWeight;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Clause
        ///</summary>
        [TestMethod()]
        public void ClauseTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            Clause expected = null; // TODO: Initialize to an appropriate value
            Clause actual;
            target.Clause = expected;
            actual = target.Clause;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest() {
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
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
            Phrase target = CreatePhrase(); // TODO: Initialize to an appropriate value
            Clause parent = null; // TODO: Initialize to an appropriate value
            target.EstablishParent(parent);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
