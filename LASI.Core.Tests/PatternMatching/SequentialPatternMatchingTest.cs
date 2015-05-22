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
        public void EnumerableOfLexicalWithoutFiltersOrGuardsExactTypesActionTest()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((CommonPluralNoun n1, Conjunction c, CommonPluralNoun n2, Preposition p, Adverb a, PersonalPronoun e) =>
                {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesActionTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, CommonNoun n2, IPrepositional p, IAdverbial a, IReferencer e) =>
                {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesActionTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardFalseActionTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .When(false) // this guard is impossible to satisfy, thus the following match must fail even though the pattern is applicable.
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardFalseActionTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .When(() => false) // this guard is impossible to satisfy
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardTrueActionTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .When(true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardTrueActionTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .When(() => true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterActionTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial>()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IEntity e) =>
                {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterActionTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive>()
                .Case((IEntity n1, IEntity n2, IPrepositional p, IEntity e) =>
                {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterActionTest3()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive, IPrepositional>()
                .Case((IEntity n1, IEntity n2, IEntity e) =>
                {
                    actual = true;
                });
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsExactTypesFuncTest()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((CommonPluralNoun n1, Conjunction c, CommonPluralNoun n2, Preposition p, Adverb a, PersonalPronoun e) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesFuncTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, CommonNoun n2, IPrepositional p, IAdverbial a, IReferencer e) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesFuncTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardFalse1FuncTest()
        {
            bool actual = false;
            TestTarget.Match()
                .When(false) // this guard is impossible to satisfy, thus the following match must fail even though the pattern is applicable.
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardFalse2FuncTest()
        {
            bool actual = false;
            TestTarget.Match()
                .When(() => false) // this guard is impossible to satisfy
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardTrueFuncTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .When(true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithGuardTrueFuncTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .When(() => true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterFuncTest1()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial>()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IEntity e) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterFuncTest2()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive>()
                .Case((IEntity n1, IEntity n2, IPrepositional p, IEntity e) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterFuncTest3()
        {
            bool actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive, IPrepositional>()
                .Case((IEntity n1, IEntity n2, IEntity e) => actual = true);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }
    }
}
