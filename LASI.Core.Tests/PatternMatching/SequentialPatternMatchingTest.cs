using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.PatternMatching.Tests
{
    using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
    using NFluent;
    using Xunit;
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
        [Fact]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsExactTypesActionTest()
        {
            var actual = false;
            TestTarget.Match()
                .Case((CommonPluralNoun n1, Conjunction c, CommonPluralNoun n2, Preposition p, Adverb a, PersonalPronoun e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesActionTest1()
        {
            var actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, CommonNoun n2, IPrepositional p, IAdverbial a, IReferencer e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesActionTest2()
        {
            var actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithGuardFalseActionTest1()
        {
            var actual = false;
            TestTarget.Match()
                .When(false) // this guard is impossible to satisfy, thus the following match must fail even though the pattern is applicable.
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsFalse();
        }
        [Fact]
        public void EnumerableOfLexicalWithGuardFalseActionTest2()
        {
            var actual = false;
            TestTarget.Match()
                .When(() => false) // this guard is impossible to satisfy
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsFalse();
        }
        [Fact]
        public void EnumerableOfLexicalWithGuardTrueActionTest1()
        {
            var actual = false;
            TestTarget.Match()
                .When(true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithGuardTrueActionTest2()
        {
            var actual = false;
            TestTarget.Match()
                .When(() => true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterActionTest1()
        {
            var actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial>()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IEntity e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterActionTest2()
        {
            var actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive>()
                .Case((IEntity n1, IEntity n2, IPrepositional p, IEntity e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterActionTest3()
        {
            var actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive, IPrepositional>()
                .Case((IEntity n1, IEntity n2, IEntity e) =>
                {
                    actual = true;
                });
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsExactTypesFuncTest()
        {
            var actual = false;
            TestTarget.Match()
                .Case((CommonPluralNoun n1, Conjunction c, CommonPluralNoun n2, Preposition p, Adverb a, PersonalPronoun e) => actual = true);
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesFuncTest1()
        {
            var actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, CommonNoun n2, IPrepositional p, IAdverbial a, IReferencer e) => actual = true);
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithoutFiltersOrGuardsAbstractedTypesFuncTest2()
        {
            var actual = false;
            TestTarget.Match()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithGuardFalse1FuncTest()
        {
            var actual = false;
            TestTarget.Match()
                .When(false) // this guard is impossible to satisfy, thus the following match must fail even though the pattern is applicable.
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            var expected = false;
            Check.That(actual).IsEqualTo(expected);
        }
        [Fact]
        public void EnumerableOfLexicalWithGuardFalse2FuncTest()
        {
            var actual = false;
            TestTarget.Match()
                .When(() => false) // this guard is impossible to satisfy
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            Check.That(actual).IsFalse();
        }
        [Fact]
        public void EnumerableOfLexicalWithGuardTrueFuncTest1()
        {
            var actual = false;
            TestTarget.Match()
                .When(true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithGuardTrueFuncTest2()
        {
            var actual = false;
            TestTarget.Match()
                .When(() => true) // this guard is always satisfied
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IAttributive<IVerbal> a, IEntity e) => actual = true);
            var expected = true;
            Check.That(actual).IsEqualTo(expected);
        }
        [Fact]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterFuncTest1()
        {
            var actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial>()
                .Case((IEntity n1, IConjunctive c, IEntity n2, IPrepositional p, IEntity e) => actual = true);
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterFuncTest2()
        {
            var actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive>()
                .Case((IEntity n1, IEntity n2, IPrepositional p, IEntity e) => actual = true);
            Check.That(actual).IsTrue();
        }
        [Fact]
        public void EnumerableOfLexicalWithIgnoreOnceTypeFilterFuncTest3()
        {
            var actual = false;
            TestTarget.Match()
                .IgnoreOnce<IAdverbial, IConjunctive, IPrepositional>()
                .Case((IEntity n1, IEntity n2, IEntity e) => actual = true);
            Check.That(actual).IsTrue();
        }
    }
}
