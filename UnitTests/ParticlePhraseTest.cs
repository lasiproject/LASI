using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for ParticlePhraseTest and is intended
    ///to contain all ParticlePhraseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ParticlePhraseTest
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
        ///A test for ParticlePhrase Constructor
        ///</summary>
        [TestMethod()]
        public void ParticlePhraseConstructorTest() {
            IEnumerable<Word> composedWords = new[] { new Particle("away") };
            ParticlePhrase target = new ParticlePhrase(composedWords);
            Assert.IsTrue((from w1 in composedWords
                           join w2 in composedWords on w1 equals w2
                           select new
                           {
                               w1,
                               w2
                           }).Count() == composedWords.Count());
        }

        /// <summary>
        ///A test for BindObjectOfPreposition
        ///</summary>
        [TestMethod()]
        public void BindObjectOfPrepositionTest() {
            IEnumerable<Word> composedWords = new[] { new Particle("about") };
            ParticlePhrase target = new ParticlePhrase(composedWords);
            ILexical prepositionalObject = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("house") });
            target.BindObject(prepositionalObject);
            Assert.AreEqual(target.BoundObject, prepositionalObject);
        }

        /// <summary>
        ///A test for OnLeftSide
        ///</summary>
        [TestMethod()]
        public void OnLeftSideTest() {
            IEnumerable<Word> composedWords = new[] { new Particle("away") };
            ParticlePhrase target = new ParticlePhrase(composedWords);
            ILexical expected = new Verb("gave", VerbForm.Past);
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for OnRightSide
        ///</summary>
        [TestMethod()]
        public void OnRightSideTest() {
            IEnumerable<Word> composedWords = new[] { new Particle("away") };
            ParticlePhrase target = new ParticlePhrase(composedWords);
            ILexical expected = new Preposition("for");
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for ToTheRightOf
        ///</summary>
        [TestMethod()]
        public void ToTheRightOfTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            ParticlePhrase target = new ParticlePhrase(composedWords); // TODO: Initialize to an appropriate value
            ILexical expected = null; // TODO: Initialize to an appropriate value
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToTheLeftOf
        ///</summary>
        [TestMethod()]
        public void ToTheLeftOfTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            ParticlePhrase target = new ParticlePhrase(composedWords); // TODO: Initialize to an appropriate value
            ILexical expected = null; // TODO: Initialize to an appropriate value
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Role
        ///</summary>
        [TestMethod()]
        public void RoleTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            ParticlePhrase target = new ParticlePhrase(composedWords); // TODO: Initialize to an appropriate value
            PrepositionRole expected = new PrepositionRole(); // TODO: Initialize to an appropriate value
            PrepositionRole actual;
            target.Role = expected;
            actual = target.Role;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for BindObject
        ///</summary>
        [TestMethod()]
        public void BindObjectTest() {
            IEnumerable<Word> composedWords = null; // TODO: Initialize to an appropriate value
            ParticlePhrase target = new ParticlePhrase(composedWords); // TODO: Initialize to an appropriate value
            ILexical prepositionalObject = null; // TODO: Initialize to an appropriate value
            target.BindObject(prepositionalObject);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

      
    }
}
