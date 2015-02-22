using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for SingleQuoteTest and is intended
    ///to contain all SingleQuoteTest Unit Tests
    /// </summary>
    [TestClass]
    public class SingleQuoteTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        /// <summary>
        ///A test for PairWith
        /// </summary>
        [TestMethod]
        public void PairWithTest() {
            SingleQuote target = new SingleQuote();
            SingleQuote complement = new SingleQuote();
            target.PairWith(complement);
            Assert.AreEqual(target, complement.PairedWith);
            Assert.AreEqual(complement.PairedWith, target);
        }

        /// <summary>
        ///A test for SingleQuote Constructor
        /// </summary>
        [TestMethod]
        public void SingleQuoteConstructorTest() {
            SingleQuote target = new SingleQuote();
            Assert.IsTrue(target.Text == "'");
            Assert.AreEqual(target.LiteralCharacter, '\'');
        }
    }
}
