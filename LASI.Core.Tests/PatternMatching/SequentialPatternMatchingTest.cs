using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Tests.Analysis.Binding.Experimental
{
    using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
    [TestClass]
    public class SequentialPatternMatchingTest
    {
        static IEnumerable<ILexical> TestTarget =>
            new ILexical[] {
                new CommonPluralNoun("Dogs"),
                new Conjunction("or"),
                new CommonPluralNoun("cats"),
                new Preposition("but"),
                new Adverb("not"),
                new PersonalPronoun("both")
            };
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsExactTypesTest() {
            bool actual = false;
            TestTarget.Match()
                .BindWhen((CommonPluralNoun n1, Conjunction c, CommonPluralNoun n2, Preposition p, Adverb a, PersonalPronoun pp) => {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesTest() {
            bool actual = false;
            TestTarget.Match()
                .BindWhen((IEntity n1, IConjunctive c, CommonNoun n2, IPrepositional p, IAdverbial a, IReferencer pp) => {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesTest1() {
            bool actual = false;
            TestTarget.Match()
                .BindWhen((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity pp) => {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
    }
}
