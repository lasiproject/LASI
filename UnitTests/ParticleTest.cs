using LASI;
using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace LASI.UnitTests
{


    /// <summary>
    ///This is A test class for ParticleTest and is intended
    ///to contain all ParticleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ParticleTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>wd:\Users\Aluan\Desktop\LASI\LASI_v1\AlgorithmAssemblyUnitTestProject\ParticleTest.cs
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
        ///A test for Particle Constructor
        ///</summary>
        [TestMethod()]
        public void ParticleConstructorTest() {
            string text = "about";
            Particle target = new Particle(text);
            Assert.IsTrue(target.Text == "about" && target.ToTheLeftOf == null && target.ToTheRightOf == null && target.BoundObject == null);
        }


        /// <summary>
        ///A test for BindObjectOfPreposition
        ///</summary>
        [TestMethod()]
        public void BindObjectOfPrepositionTest() {
            string text = "about";
            Particle target = new Particle(text);
            ILexical prepositionalObject = new NounPhrase(new[] { new ProperSingularNoun("Ayn"), new ProperSingularNoun("Rand") });
            target.BindObject(prepositionalObject);
            Assert.IsTrue(target.BoundObject == prepositionalObject);
        }

        /// <summary>
        ///A test for OnLeftSide
        ///</summary>
        [TestMethod()]
        public void OnLeftSideTest() {
            string text = "about";
            Particle target = new Particle(text);
            ILexical expected = new VerbPhrase(new[] { new PastTenseVerb("walked") });
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
            string text = "about";
            Particle target = new Particle(text);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new CommonPluralNoun("grounds") });
            ILexical actual;
            target.ToTheRightOf = expected;
            actual = target.ToTheRightOf;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PrepositionalRole
        ///</summary>
        [TestMethod()]
        public void PrepositionalRoleTest() {
            string text = "about";
            Particle target = new Particle(text);
            PrepositionRole expected = PrepositionRole.Undetermined;
            PrepositionRole actual;
            actual = target.Role;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToTheRightOf
        ///</summary>
        [TestMethod()]
        public void ToTheRightOfTest() {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Particle target = new Particle(text); // TODO: Initialize to an appropriate value
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
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Particle target = new Particle(text); // TODO: Initialize to an appropriate value
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
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Particle target = new Particle(text); // TODO: Initialize to an appropriate value
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
            string text = string.Empty; // TODO: Initialize to an appropriate value
            Particle target = new Particle(text); // TODO: Initialize to an appropriate value
            ILexical prepositionalObject = null; // TODO: Initialize to an appropriate value
            target.BindObject(prepositionalObject);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

    
    }
}
