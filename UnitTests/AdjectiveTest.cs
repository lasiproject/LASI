using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LASI.Core.Tests
{


    /// <summary>
    ///This is A test class for AdjectiveTest and is intended
    ///to contain all AdjectiveTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdjectiveTest
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
        ///A test for Adjective Constructor
        ///</summary>
        [TestMethod()]
        public void AdjectiveConstructorTest() {
            string text = "orangish";
            Adjective target = new Adjective(text);
            Assert.AreEqual(text, target.Text);
        }

        /// <summary>
        ///A test for ModifyWith
        ///</summary>
        [TestMethod()]
        public void ModifyWithTest() {
            Adjective target = new Adjective("orangish");
            Adverb adv = new Adverb("demonstrably");
            AdverbPhrase advp = new AdverbPhrase(new[] { adv });
            target.ModifyWith(adv);
            target.ModifyWith(advp);
            Assert.IsTrue(target.Modifiers.Contains(adv) && target.Modifiers.Contains(advp));
        }

        /// <summary>
        ///A test for Describes
        ///</summary>
        [TestMethod()]
        public void DescribesTest() {
            string text = "funny";
            Adjective target = new Adjective(text);
            IDescribable expected = new CommonSingularNoun("man");
            IDescribable actual;
            target.Describes = expected;
            actual = target.Describes;
            Assert.AreEqual(expected, actual);
            expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonSingularNoun("woman") });
            target.Describes = expected;
            actual = target.Describes;
            Assert.AreEqual(expected, actual);
        }
    }
}
