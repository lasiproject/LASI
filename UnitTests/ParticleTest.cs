using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Algorithm.FundamentalSyntacticInterfaces;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is entity test class for ParticleTest and is intended
    ///to contain all ParticleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ParticleTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>C:\Users\Aluan\Desktop\LASI\LASI_v1\AlgorithmAssemblyUnitTestProject\ParticleTest.cs
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
        //Use ClassCleanup to run code after all tests in entity class have run
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
        ///entity test for Particle Constructor
        ///</summary>
        [TestMethod()]
        public void ParticleConstructorTest() {
            string text = "about";
            Particle target = new Particle(text);
            Assert.IsTrue(target.Text == "about" && target.OnLeftSide == null && target.OnRightSide == null && target.PrepositionalObject == null);
        }


        /// <summary>
        ///A test for BindObjectOfPreposition
        ///</summary>
        [TestMethod()]
        public void BindObjectOfPrepositionTest() {
            string text = "about";
            Particle target = new Particle(text);
            ILexical prepositionalObject = new NounPhrase(new[] { new ProperSingularNoun("Ayn"), new ProperSingularNoun("Rand") });
            target.BindObjectOfPreposition(prepositionalObject);
            Assert.IsTrue(target.PrepositionalObject == prepositionalObject);
        }

        /// <summary>
        ///A test for OnLeftSide
        ///</summary>
        [TestMethod()]
        public void OnLeftSideTest() {
            string text = "about";
            Particle target = new Particle(text);
            IPrepositionLinkable expected = new VerbPhrase(new[] { new PastTenseVerb("walked") });
            IPrepositionLinkable actual;
            target.OnLeftSide = expected;
            actual = target.OnLeftSide;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for OnRightSide
        ///</summary>
        [TestMethod()]
        public void OnRightSideTest() {
            string text = "about";
            Particle target = new Particle(text);
            IPrepositionLinkable expected = new NounPhrase(new Word[] { new Determiner("the"), new GenericPluralNoun("grounds") });
            IPrepositionLinkable actual;
            target.OnRightSide = expected;
            actual = target.OnRightSide;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PrepositionalRole
        ///</summary>
        [TestMethod()]
        public void PrepositionalRoleTest() {
            string text = "about";
            Particle target = new Particle(text);
            PrepositionalRole expected = PrepositionalRole.Undetermined;
            PrepositionalRole actual;
            actual = target.PrepositionalRole;
            Assert.AreEqual(expected, actual);
        }
    }
}
