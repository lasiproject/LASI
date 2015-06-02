using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for ProperNounTest and is intended
    ///to contain all ProperNounTest Unit Tests
    /// </summary>
    [TestClass]
    public class ProperNounTest
    {
        private IEnumerable<ProperNoun> CreateProperNouns()
        {
            yield return new ProperSingularNoun("Patrick");
            yield return new ProperPluralNoun("Roberts");
            yield return new ProperPluralNoun("James");
            yield return new ProperPluralNoun("Rachels");
        }

        /// <summary>
        ///A test for IsPersonalName
        /// </summary>
        [TestMethod]
        public void IsPersonalNameTest()
        {
            foreach (ProperNoun target in CreateProperNouns())
            {
                bool actual;
                actual = target.IsPersonalName;
                Assert.IsTrue(actual);
            }
        }
    }
}
