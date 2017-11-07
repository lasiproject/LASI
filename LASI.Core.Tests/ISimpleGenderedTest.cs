using LASI.Core;
using System;
using LASI.Core.Heuristics;
using Fact = Xunit.FactAttribute;
using NFluent;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for ISimpleGenderedTest and is intended
    ///to contain all ISimpleGenderedTest Unit Tests
    /// </summary>
    public class ISimpleGenderedTest
    {
        /// <summary>
        ///A test for Gender
        /// </summary>
        [Fact]
        public void KnownMaleFirstNameIsOfMaleGender()
        {
            ISimpleGendered jack = new ProperSingularNoun("Jack");
            Check.That(jack.Gender).IsEqualTo(Gender.Male).And.IsEqualTo(jack.GetGender());
        }
        /// <summary>
        ///A test for Gender
        /// </summary>
        [Fact]
        public void KnownFemaleIsOfFemaleGender()
        {
            ISimpleGendered jill = new ProperSingularNoun("Jill");
            Check.That(jill.Gender).IsEqualTo(Gender.Female).And.IsEqualTo(jill.GetGender());
        }
        /// <summary>
        ///A test for Gender
        /// </summary>
        [Fact]
        public void KnownLastNameIsOfNeutralGender()
        {
            ISimpleGendered carnegie = new ProperSingularNoun("Carnegie");
            Check.That(carnegie.Gender).IsEqualTo(Gender.Neutral).And.IsEqualTo(carnegie.GetGender());
        }
        /// <summary>
        ///A test for Gender
        /// </summary>
        [Fact]
        public void AcronymIsOfNeutralGender()
        {
            ISimpleGendered lasi = new ProperSingularNoun("LASI");
            Check.That(lasi.Gender).IsEqualTo(Gender.Neutral).And.IsEqualTo(lasi.GetGender());
        }
    }
}
