using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.TermBased
{
    using Validator = LASI.Utilities.Contracts.Validators.ArgumentValidator;
    static class TermTest
    {
        public class ValueTerm<TValue> : ITerm
        {
            public bool Matched { get; protected set; }

            private readonly TValue value;
            public ValueTerm(TValue value) {
                Validator.ThrowIfNull(value, nameof(value));
                this.value = value;
            }

            public bool Matches(TValue actual) => Matched = value.Equals(actual);
            public bool Matches(Func<TValue> actualValueProvider) => Matched = Matches(actualValueProvider());
            public static implicit operator ValueTerm<TValue>(TValue value) => new ValueTerm<TValue>(value);
            public static bool operator ==(ValueTerm<TValue> term, TValue value) => term.value.Equals(value);
            public static bool operator !=(ValueTerm<TValue> term, TValue value) => !(term == value);
            public static bool operator ==(TValue value, ValueTerm<TValue> term) => (term == value);
            public static bool operator !=(TValue value, ValueTerm<TValue> term) => !(term == value);

        }
        public class TypeTerm<TMatch> : ITerm
        {
            public bool Matched { get; protected set; }
            public virtual bool Matches<TTest>(TTest test) => Matched = test is TMatch;
            public virtual bool Matches<TTest>(Func<TTest> testValueProvider) => Matched = testValueProvider() is TMatch;

        }

        public static Func<IVerbal, ValueTerm<TValue>, TResult> Verbal<TValue, TResult>(IVerbal match, TValue value, Func<IVerbal, ValueTerm<TValue>, TResult> f) => new Func<IVerbal, ValueTerm<TValue>, TResult>((x, y) => { return default(TResult); });
        public class VerbalX : ComplexTerm<TypeTerm<IVerbal>, IVerbal, ValueTerm<string>, string>
        {
            public static VerbalX Factory() { return new VerbalX(new TypeTerm<IVerbal>(), "attach"); }
            public VerbalX(TypeTerm<IVerbal> typeTerm, ValueTerm<string> valueTerm) : base(typeTerm, valueTerm) {
            }
        }

        public abstract class ComplexTerm<TTypeTerm, TType, TValueTerm, TValue> : ITerm where TTypeTerm : TypeTerm<TType> where TValueTerm : ValueTerm<TValue>
        {
            public ComplexTerm(TTypeTerm typeTerm, TValueTerm valueTerm) {
                TypeTerm = typeTerm;
                ValueTerm = valueTerm;
            }
            public bool Matches(TType test, TValue value) =>
                Matched = TypeTerm.Matches(test) && ValueTerm.Matches(value);

            protected TTypeTerm TypeTerm { get; }
            protected TValueTerm ValueTerm { get; }

            public bool Matched { get; protected set; }
        }
        internal interface ITerm
        {
            bool Matched { get; }
        }
        static void SimpleTest(IVerbal verbal) {
            Verbal(verbal, "attach", (v, t) => v.Text == t);
        }
    }
}