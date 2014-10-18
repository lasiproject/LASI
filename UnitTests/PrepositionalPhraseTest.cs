using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for PrepositionalPhraseTest and is intended
    ///to contain all PrepositionalPhraseTest Unit Tests
    ///</summary>
    [TestClass]
    public class PrepositionalPhraseTest
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

        private static PrepositionalPhrase CreatePrepositionPhrase() {
            var composedWords = new[] { new Preposition("on") };
            return new PrepositionalPhrase(composedWords);
        }

        /// <summary>
        ///A test for PrepositionalPhrase Constructor
        ///</summary>
        [TestMethod]
        public void PrepositionalPhraseConstructorTest() {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);

            Assert.IsTrue(target.Words.Count() == composedWords.Count());
            Assert.IsTrue(target.Text == "on" && target.ToTheLeftOf == null && target.ToTheRightOf == null && target.BoundObject == null);

        }


        /// <summary>
        ///A test for BindObjectOfPreposition
        ///</summary>
        [TestMethod]
        public void BindObjectOfPrepositionTest() {
            PrepositionalPhrase target = CreatePrepositionPhrase();
            ILexical prepositionalObject = new VerbPhrase(new Word[] { new Verb("have", VerbForm.Base) });
            target.BindObject(prepositionalObject);
            Assert.IsTrue(target.BoundObject == prepositionalObject);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest() {
            Phrase.VerboseOutput = true;
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            target.ToTheLeftOf = new NounPhrase(new[] { new PersonalPronoun("it") });
            target.ToTheRightOf = new VerbPhrase(new[] { new PresentParticipleVerb("slamming") });
            string expected = String.Format("PrepositionalPhrase \"for\"\n\tleft linked: {0}\n\tright linked: {1}", target.ToTheLeftOf, target.ToTheRightOf);
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for PrepositionalRole
        ///</summary>
        [TestMethod]
        public void PrepositionalRoleTest() {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            PrepositionRole expected = PrepositionRole.Undetermined;
            PrepositionRole actual;
            actual = target.Role;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for OnLeftSide
        ///</summary>
        [TestMethod]
        public void OnLeftSideTest() {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical expected = new NounPhrase(new[] { new PersonalPronoun("it") });
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for OnRightSide
        ///</summary>
        [TestMethod]
        public void OnRightSideTest() {
            IEnumerable<Word> composedWords = new Word[] { new Preposition("for") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical expected = new VerbPhrase(new[] { new PresentParticipleVerb("slamming") });
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToTheRightOf
        ///</summary>
        [TestMethod]
        public void ToTheRightOfTest() {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical expected = new Verb("is", VerbForm.ThirdPersonSingularPresent);
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToTheLeftOf
        ///</summary>
        [TestMethod]
        public void ToTheLeftOfTest() {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("table") });
            ILexical actual;
            target.ToTheLeftOf = expected;
            actual = target.ToTheLeftOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Role
        ///</summary>
        [TestMethod]
        public void RoleTest() {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);

            PrepositionRole expected = PrepositionRole.SpatialSpecifier;
            PrepositionRole actual;
            target.Role = expected;
            actual = target.Role;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for BindObject
        ///</summary>
        [TestMethod]
        public void BindObjectTest() {
            var composedWords = new[] { new Preposition("on") };
            PrepositionalPhrase target = new PrepositionalPhrase(composedWords);
            ILexical prepositionalObject = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("table") });
            target.BindObject(prepositionalObject);
            Assert.AreEqual(prepositionalObject, target.BoundObject);
            IVerbal verbal = new Verb("is", VerbForm.ThirdPersonSingularPresent);
            verbal.AttachObjectViaPreposition(target);
            Assert.AreEqual(prepositionalObject, verbal.ObjectOfThePreposition);
        }


    }
}
