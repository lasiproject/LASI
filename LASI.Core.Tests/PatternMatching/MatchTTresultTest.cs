using System;
using System.Linq;
using LASI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.TestUtilities;
using LASI.Core;
using LASI.Utilities.SupportTypes;

namespace LASI.Core.Tests.PatternMatching
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
                 .Case((IVerbal v) => new[] { v }.AsEnumerable().OfType<ILexical>())
                 .Result();
            Assert.AreNotEqual(result, null);
            EnumerableAssert.AreSequenceEqual(result, ((Phrase)target).Words);
        }
        [TestMethod]
        public void MatchChangingMidwayToLessDerivedResultType2()
        {
            ILexical target = new VerbPhrase(new BaseVerb("walk"), new Adverb("briskly"));
            var result = target.Match()
                 .Case((IVerbal v) => v.ToOption())
                 .Case((VerbPhrase v) => v.Words.AsEnumerable().OfType<ILexical>())
                 .Result();
            Assert.AreNotEqual(result, null);
            EnumerableAssert.AreSequenceEqual(result, target.ToOption());
        }
    }
}
