using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for QuotationMarkTest and is intended
    ///to contain all QuotationMarkTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QuotationMarkTest
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
        public void ToStringTestHelper<TQuote>()
            where TQuote : QuotationMark<TQuote>, IPairedPunctuator<TQuote> {
            QuotationMark<TQuote> target = CreateQuotationMark<TQuote>(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        internal virtual QuotationMark<TQuote> CreateQuotationMark<TQuote>()
            where TQuote : QuotationMark<TQuote>, IPairedPunctuator<TQuote > {
            // TODO: Instantiate an appropriate concrete class.
            QuotationMark<TQuote> target = null;
            return target;
        }

        [TestMethod()]
        public void ToStringTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TQu" +
                    "ote. Please call ToStringTestHelper<TQuote>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for PairWith
        ///</summary>
        public void PairWithTestHelper<TQuote>()
            where TQuote : QuotationMark<TQuote>, IPairedPunctuator<TQuote> {
            QuotationMark<TQuote> target = CreateQuotationMark<TQuote>(); // TODO: Initialize to an appropriate value
            TQuote complement = default(TQuote); // TODO: Initialize to an appropriate value
            target.PairWith(complement);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void PairWithTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TQu" +
                    "ote. Please call PairWithTestHelper<TQuote>() with appropriate type parameters.");
        }
    }
}
