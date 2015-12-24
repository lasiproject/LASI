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
            ILexical target = CreateILexical();
            double expected = 1d;
            double actual;
            target.MetaWeight = expected;
            actual = target.MetaWeight;
            Check.That(expected).IsEqualTo(actual);
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
            ILexical target = CreateILexical();
            double expected = 1d;
            double actual;
            target.Weight = expected;
            actual = target.Weight;
            Check.That(expected).IsEqualTo(actual);
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