using System.Linq;
using NFluent;
using Xunit;

namespace LASI.Core.Tests.PatternMatching
{
    public class MatchTTResultTest
    {
        [Fact]
        public void MatchChangingMidwayToLessDerivedResultType1()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = target.Match()
                 .Case((VerbPhrase v) => v.Words)
                 .Case((IVerbal v) => new[] { v }.OfType<ILexical>())
                 .Result();
            Check.That(result).IsNotNull().And.Contains(((Phrase)target).Words).Only().InThatOrder();
        }
        [Fact]
        public void MatchChangingMidwayToLessDerivedResultType2()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = target.Match()
                 .Case((IVerbal v) => new[] { v })
                 .Case((VerbPhrase v) => v.Words.OfType<ILexical>())
                 .Result();
            Check.That(result).IsNotNull().And.Contains(target).Only().InThatOrder();
        }
    }
}
