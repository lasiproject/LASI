using LASI.Core.PatternMatching;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.Core;
using LASI.UnitTests;
using System.Collections;
using System.Collections.Generic;

namespace LASI.Core.Tests.Analysis.PatternMatching.LexicalSpecific
{
    [TestClass]
    public class MatchToEnumerableProviderTest
    {
        [TestMethod]
        public void EnumerateResultOfSuccessfulMatchTest() {
            Match<ILexical, string> m = CreateFruitfulMatch("reality");
            MatchToEnumerableProvider<ILexical, string> target = m;
            Assert.IsTrue(target.Any());
            Assert.AreEqual(m.Result(), target.First());
        }
        [TestMethod]
        public void EnumerateResultOfUnsuccessfulMatchTest1() {
            Match<ILexical, string> m = CreateFruitlessMatch<string>();
            MatchToEnumerableProvider<ILexical, string> target = m;
            Assert.IsFalse(target.Any());
        }
        [TestMethod]
        public void EnumerateResultOfSuccessfulMatchWithQueryExpressionTest1() {
            const string fruit = "okay";
            Match<ILexical, string> m = CreateFruitfulMatch(fruit);
            MatchToEnumerableProvider<ILexical, string> target = m;
            IEnumerable<string> q2 = from r in m
                                     select r;
            Assert.AreEqual(q2.First(), fruit);

        }
        [ExpectedInvalidOperationException]
        [TestMethod]
        public void EnumerateResultOfUnsuccessfulMatchTest2() {
            Match<ILexical, string> m = CreateFruitlessMatch<string>();
            m.Select(x => x).First();

        }


        #region Test Helpers
        private static Match<ILexical, TResult> CreateFruitlessMatch<TResult>() {
            var value = default(ILexical);
            return value.Match().Yield<TResult>();
        }
        private static Match<ILexical, TResult> CreateFruitfulMatch<TResult>(TResult fruit) {
            var result = new LexicalStub();
            return result.Match<ILexical>().When(true).Then(fruit);
        }

        private class LexicalStub : ILexical
        {
            public double MetaWeight { get; set; }

            public string Text => string.Empty;

            public double Weight { get; set; }
        }
        #endregion
    }
}
