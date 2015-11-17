using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.Utilities.Specialized;
using Shared.Test.Assertions;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.Universal;

namespace LASI.Core.PatternMatching.Tests
{
    [TestClass]
    public class MatchTTresultTest
    {
        [TestMethod]
        public void MatchChangingMidwayToLessDerivedResultType1()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = target.Match()
                 .Case((VerbPhrase v) => v.Words)
                 .Case((IVerbal v) => new[] { v }.OfType<ILexical>())
                 .Result();
            Assert.AreNotEqual(result, null);
            EnumerableAssert.AreSequenceEqual(result, ((Phrase)target).Words);
        }
        [TestMethod]
        public void MatchChangingMidwayToLessDerivedResultType2()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = target.Match()
                 .Case((IVerbal v) => v.Lift())
                 .Case((VerbPhrase v) => v.Words.OfType<ILexical>())
                 .Result();
            Assert.AreNotEqual(result, null);
            EnumerableAssert.AreSequenceEqual(result, target.Lift());
        }
    }
}
