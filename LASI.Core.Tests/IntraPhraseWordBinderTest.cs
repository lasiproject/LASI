using LASI.Core.Binding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LASI.Core;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for IntraPhraseWordBinderTest and is intended
    ///to contain all IntraPhraseWordBinderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IntraPhraseWordBinderTest
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
        ///A test for Bind
        ///</summary>
        [TestMethod()]
        public void BindTest() {
            VerbPhrase vp = null; // TODO: Initialize to an appropriate value
            IntraPhraseWordBinder.Bind(vp);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Bind
        ///</summary>
        public void BindTest1Helper<TNounPhrase>()
            where TNounPhrase : NounPhrase {
            TNounPhrase np = default(TNounPhrase); // TODO: Initialize to an appropriate value
            IntraPhraseWordBinder.Bind<TNounPhrase>(np);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void BindTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TNo" +
                    "unPhrase. Please call BindTest1Helper<TNounPhrase>() with appropriate type param" +
                    "eters.");
        }
    }
}
