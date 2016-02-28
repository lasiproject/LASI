using System.Linq;
using LASI.Utilities.Specialized.Enhanced.Universal;
using NFluent;
using Xunit;

namespace LASI.Core.PatternMatching.Tests
{
    public class MatchTTresultTest
    {
        [Fact]
        public void MatchChangingMidwayToLessDerivedResultType1()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = target.Match()
                 .Case((VerbPhrase v) => v.Words)
                 .Match((IVerbal v) => new[] { v }.OfType<ILexical>())
                 .Result();
            Check.That(result).IsNotNull().And.Contains(((Phrase)target).Words).Only().InThatOrder();
        }
        [Fact]
        public void MatchChangingMidwayToLessDerivedResultType2()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = target.Match()
                 .Case((IVerbal v) => v.Lift())
                 .Match((VerbPhrase v) => v.Words.OfType<ILexical>())
                 .Result();
            Check.That(result).IsNotNull().And.Contains(target.Lift()).Only().InThatOrder();
        }
    }
}
