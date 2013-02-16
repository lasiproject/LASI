using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for WordTest and is intended
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


        internal virtual Word CreateWord() {
            Word target = new GenericSingularNoun("dog");
            return target;
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
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
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest1() {
            Word target = CreateWord();
            Word other = new GenericSingularNoun("dog");
            bool expected = true;
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
            other = new GenericSingularNoun("cat");
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
            other = null;
            expected = false;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EstablishParent
        ///</summary>
        [TestMethod()]
        public void EstablishParentTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
            Phrase phrase = new VerbPhrase(new Word[] { }); // TODO: Initialize to an appropriate value
            target.EstablishParent(phrase);
            Assert.IsTrue(target.ParentPhrase == phrase);
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
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
            string expected = "LASI.Algorithm.GenericSingularNoun " + target.Text;
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest() {
            Word A = null;
            Word B = null;
            bool expected = true;
            bool actual;
            actual = A == B && B == A;
            Assert.AreEqual(expected, actual);
            A = CreateWord();
            B = CreateWord();
            expected = true;
            actual = A == B && B == A;
            Assert.AreEqual(expected, actual);
            A = null;
            B = CreateWord();
            expected = false;
            actual = A == B && B == A;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest() {
            Word A = CreateWord();
            Word B = CreateWord();
            bool expected = false;
            bool actual;
            actual = A != B;
            Assert.AreEqual(expected, actual);
            A = null;
            B = null;
            expected = false;
            actual = A != B;
            Assert.AreEqual(expected, actual);

            B = null;
            A = CreateWord();
            expected = true;
            actual = A != B;
            Assert.AreEqual(actual, expected);
        }

        /// <summary>
        ///A test for NextWord
        ///</summary>
        [TestMethod()]
        public void NextWordTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
            Word expected = new ToLinker("to"); // TODO: Initialize to an appropriate value
            Word actual;
            target.NextWord = expected;
            actual = target.NextWord;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ParentDocument
        ///</summary>
        [TestMethod()]
        public void ParentDocTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
            Document expected = new Document(new Word[] { target });// TODO: Initialize to an appropriate value
            Document actual;
            target.ParentDocument = expected;
            actual = target.ParentDocument;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ParentPhrase
        ///</summary>
        [TestMethod()]
        public void ParentPhraseTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
            Phrase expected = new VerbPhrase(new Word[] { });
            Phrase actual;
            target.ParentPhrase = expected;
            actual = target.ParentPhrase;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for PreviousWord
        ///</summary>
        [TestMethod()]
        public void PreviousWordTest() {
            Word target = CreateWord(); // TODO: Initialize to an appropriate value
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
            Word target = new Verb("run");
            string expected = "run";
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);

        }
    }
}
