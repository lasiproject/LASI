using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace AlgorithmAssemblyUnitTestProject
{
    
    
    /// <summary>
    ///This is A test class for ConjunctionTest and is intended
    ///to contain all ConjunctionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConjunctionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
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
        ///A test for Conjunction Constructor
        ///</summary>
        [TestMethod()]
        public void ConjunctionConstructorTest()
        {
            string text = "and" ;
            Conjunction target = new Conjunction(text);
            Assert.AreEqual(target.Text, text );
        }

        /// <summary>
        ///A test for OnLeft
        ///</summary>
        [TestMethod()]
        public void OnLeftTest()
        {
            string text = "and";
            Conjunction target = new Conjunction(text);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new GenericSingularNoun("program") });
            ILexical actual;
            target.JoinedLeft = expected;
            actual = target.JoinedLeft;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for OnRight
        ///</summary>
        [TestMethod()]
        public void OnRightTest()
        {
            string text = "and";
            Conjunction target = new Conjunction(text);
            ILexical expected = new NounPhrase(new Word[] { new Determiner("the"), new GenericSingularNoun("program") });
            ILexical actual;
            target.JoinedRight = expected;
            actual = target.JoinedRight;
            Assert.AreEqual(expected, actual);
        }
    }
}
