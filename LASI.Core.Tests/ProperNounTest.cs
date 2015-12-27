using System.Collections.Generic;
using Xunit;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for ProperNounTest and is intended
    ///to contain all ProperNounTest Unit Tests
    /// </summary>
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
        [Fact]
        public void IsPersonalNameTest()
        {
            foreach (ProperNoun target in CreateProperNouns())
            {
                bool actual;
                actual = target.IsPersonalName;
                Assert.True(actual);
            }
        }
    }
}
