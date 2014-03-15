using LASI.ContentSystem.TaggerEncapsulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LASI.Core;
using System.Collections.Generic;
using System.Reflection;
using LASI.ContentSystem;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for PhraseTagsetMapTest and is intended
    ///to contain all PhraseTagsetMapTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhraseTagsetMapTest
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
        class TestPhraseTagsetMap : PhraseTagsetMap
        {

            public override Func<IEnumerable<Word>, Phrase> this[string tag] {
                get {
                    try { return mapping[tag]; }
                    catch (KeyNotFoundException) { throw new UnknownPhraseTagException(tag); }
                }
            }

            public override string this[Func<IEnumerable<Word>, Phrase> mappedConstructor] {
                get {
                    return (from tm in mapping
                            where tm.Value.GetMethodInfo().ReturnParameter.ParameterType == mappedConstructor.GetMethodInfo().ReturnType
                            select tm.Key).Single();
                }
            }

            public override string this[Phrase phrase] {
                get {
                    return (from tm in mapping
                            where (tm.Value.Method.GetCustomAttributes()).GetType() == phrase.GetType()
                            select tm.Key).First();
                }
            }
            protected override IReadOnlyDictionary<string, Func<IEnumerable<Word>, Phrase>> TypeDictionary {
                get { return mapping; }
            }
            private readonly IReadOnlyDictionary<string, Func<IEnumerable<Word>, Phrase>> mapping = new Dictionary<string, Func<IEnumerable<Word>, Phrase>> {
                { "NP", ws => new NounPhrase(ws) },
                { "VP", ws => new VerbPhrase(ws) },
            };

        }

        internal virtual PhraseTagsetMap CreatePhraseTagsetMap() {
            // TODO: Instantiate an appropriate concrete class.
            PhraseTagsetMap target = new TestPhraseTagsetMap();
            return target;
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest() {
            PhraseTagsetMap target = CreatePhraseTagsetMap();
            string tag = "NP";
            Func<IEnumerable<Word>, Phrase> actual;
            actual = target[tag];
            var phrase = actual(new Word[] { new PersonalPronoun("he") });
            Assert.IsTrue(target[actual] == target[phrase]);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest1() {
            PhraseTagsetMap target = CreatePhraseTagsetMap(); // TODO: Initialize to an appropriate value
            Func<IEnumerable<Word>, Phrase> mappedConstructor = null; // TODO: Initialize to an appropriate value
            string actual;
            actual = target[mappedConstructor];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest2() {
            PhraseTagsetMap target = CreatePhraseTagsetMap(); // TODO: Initialize to an appropriate value
            Phrase phrase = null; // TODO: Initialize to an appropriate value
            string actual;
            actual = target[phrase];
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
