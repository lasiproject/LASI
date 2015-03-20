using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.PatternMatching.Tests
{
    using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
    [TestClass]
    public class SequentialPatternMatchingTest
    {
        private static IEnumerable<ILexical> TestTarget
        {
            get
            {
                yield return new CommonPluralNoun("Dogs");
                yield return new Conjunction("or");
                yield return new CommonPluralNoun("cats");
                yield return new Preposition("but");
                yield return new Adverb("not");
                yield return new PersonalPronoun("both");
            }
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsExactTypesTest()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((CommonPluralNoun n1, Conjunction c, CommonPluralNoun n2, Preposition p, Adverb a, PersonalPronoun pp) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, CommonNoun n2, IPrepositional p, IAdverbial a, IReferencer pp) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity pp) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardFalse1()
        {
            bool actual = false;
            TestTarget.Match()
                .Guard(false) // this guard is impossible to satisfy, thus the following match must fail even though the pattern is applicable.
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity pp) => actual = true);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardFalse2()
        {
            bool actual = false;
            TestTarget.Match()
                .Guard(() => false) // this guard is impossible to satisfy
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity pp) => actual = true);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardTrue1()
        {
            bool actual = false;
            TestTarget.Match()
                .Guard(true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity pp) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardTrue2()
        {
            bool actual = false;
            TestTarget.Match()
                .Guard(() => true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity pp) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial>()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IEntity pp) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive>()
                .Case((IEntity n1, IEntity n2, IPrepositional p, IEntity pp) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterTest3()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive, IPrepositional>()
                .Case((IEntity n1, IEntity n2, IEntity pp) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
    }
}
