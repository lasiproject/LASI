using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for SentenceTest and is intended
    ///to contain all SentenceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SentenceTest
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
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Phrase[] phrases = new Phrase[] {new NounPhrase(new Word[] {new ProperSingularNoun("LASI")}), new VerbPhrase (new Word[] {new PastTenseVerb ("found")}), new NounPhrase (new Word[] {new ProperPluralNoun ("TIMIS")})};
            Sentence target = new Sentence(phrases, new SentencePunctuation('.'));
            string expected = "LASI.Algorithm.Sentence \"LASI found TIMIS.\"";
            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
