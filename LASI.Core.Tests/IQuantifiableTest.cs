using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for IQuantifiableTest and is intended
    ///to contain all IQuantifiableTest Unit Tests
    /// </summary>
    [TestClass]
    public class IQuantifiableTest
    {
        internal virtual IQuantifiable CreateIQuantifiable()
        {
            // TODO: Instantiate an appropriate concrete class.
            IQuantifiable target = new CommonPluralNoun("mittens");
            return target;
        }

        /// <summary>
        ///A test for QuantifiedBy
        /// </summary>
        [TestMethod]
        public void QuantifiedByTest()
        {
            IQuantifiable target = CreateIQuantifiable(); // TODO: Initialize to an appropriate value
            IQuantifier expected = new Quantifier("all");
            IQuantifier actual;
            target.QuantifiedBy = expected;
            actual = target.QuantifiedBy;
            Assert.AreEqual(expected, actual);
        }
    }
}
