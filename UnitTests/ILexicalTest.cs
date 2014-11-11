using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LASI.UnitTests
{


    /// <summary>
    ///This is a test class for ILexicalTest and is intended
    ///to contain all ILexicalTest Unit Tests
    ///</summary>
    [TestClass]
    public class ILexicalTest
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


        internal virtual ILexical CreateILexical() {
            ILexical target = new AggregateEntity(new IEntity[] {
                    new PersonalPronoun("him"),
                    new ProperSingularNoun("Patrick"),
                    new NounPhrase(new Word[] { new ProperSingularNoun("Brittany") })
                });
            return target;
        }

        /// <summary>
        ///A test for MetaWeight
        ///</summary>
        [TestMethod]
        public void MetaWeightTest() {
            ILexical target = CreateILexical();
            double expected = 1d;
            double actual;
            target.MetaWeight = expected;
            actual = target.MetaWeight;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PrepositionOnLeft
        ///</summary>
        [TestMethod]
        public void PrepositionOnLeftTest() {
            ILexical target = CreateILexical();
            IPrepositional expected = new Preposition("with");
            IPrepositional actual;
            target.PrepositionOnLeft = expected;
            actual = target.PrepositionOnLeft;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PrepositionOnRight
        ///</summary>
        [TestMethod]
        public void PrepositionOnRightTest() {
            ILexical target = CreateILexical();
            IPrepositional expected = new Preposition("with");
            IPrepositional actual;
            target.PrepositionOnRight = expected;
            actual = target.PrepositionOnRight;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod]
        public void TextTest() {
            ILexical target = CreateILexical();
            string actual;
            actual = target.Text;
            Assert.AreEqual(target.Text, actual);
        }


        /// <summary>
        ///A test for Weight
        ///</summary>
        [TestMethod]
        public void WeightTest() {
            ILexical target = CreateILexical();
            double expected = 1d;
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Assert.AreEqual(expected, actual);
        }

    }
}
