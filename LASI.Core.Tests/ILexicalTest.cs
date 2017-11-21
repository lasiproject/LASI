using NFluent;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for ILexicalTest and is intended
    ///to contain all ILexicalTest Unit Tests
    /// </summary>
    public class ILexicalTest
    {
        /// <summary>
        ///A test for MetaWeight
        /// </summary>
        [Fact]
        public void MetaWeightTest()
        {
            var target = CreateILexical();
            var expected = 1d;
            double actual;
            target.MetaWeight = expected;
            actual = target.MetaWeight;
            Check.That(actual).IsEqualTo(expected);
        }

        ///// <summary>
        /////A test for Text
        ///// </summary>
        //[Fact]
        //public void TextTest()
        //{
        //    ILexical target = CreateILexical();
        //    string actual;
        //    actual = target.Text;
        //    Check.That(target.Text).IsEqualTo(actual);
        //}

        /// <summary>
        ///A test for Weight
        /// </summary>
        [Fact]
        public void WeightTest()
        {
            var target = CreateILexical();
            var expected = 1d;
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Check.That(actual).IsEqualTo(expected);
        }

        internal virtual ILexical CreateILexical()
        {
            ILexical target = new AggregateEntity(
                new PersonalPronoun("him"),
                new ProperSingularNoun("Patrick"),
                new NounPhrase(new ProperSingularNoun("Brittany"))
            );
            return target;
        }
    }
}