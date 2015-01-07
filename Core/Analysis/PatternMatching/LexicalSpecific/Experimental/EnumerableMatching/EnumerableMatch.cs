using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.EnumerableMatching
{
    using LASI.Utilities.Advaned;
    using Enumerable;
    using IL = ILexical;

    public class EnumerableMatchBase<T, TResult> where T : class, IL
    {
        protected EnumerableMatchBase(IEnumerable<T> values) {
            Values = values;
            Action setMatchedToTrue = delegate { Matched = true; };
            Action removeSetMatchedToTrue = null;
            removeSetMatchedToTrue = delegate {
                SetIsMatched -= setMatchedToTrue;
                SetIsMatched -= removeSetMatchedToTrue;
            };
            SetIsMatched += setMatchedToTrue + removeSetMatchedToTrue;
        }

        /// <summary> The delegate called to establish that a .Case clause has been matched. </summary>
        protected Action SetIsMatched { get; private set; }

        protected IEnumerable<T> Values { get; }

        protected IEnumerable<TResult> CaseResults { get; set; } = Empty<TResult>();

        /// <summary> Gets the value indicating wether or not a .Case clause has matched for any <typeparamref name="T" /> in Values. </summary>
        /// <remarks>
        /// This value is used to determine if the Match should continue. 
        /// Derrived types may set this value a single time via the <see cref="SetIsMatched" /> property.
        /// </remarks>
        protected bool Matched { get; private set; }
        public IEnumerable<TResult> Results() => CaseResults;
        public IEnumerable<TResult> Results(IEnumerable<TResult> defaultValues) => CaseResults.Any() ? CaseResults : defaultValues;
        public IEnumerable<TResult> Results(Func<IEnumerable<TResult>> defaultValuesFactory) => CaseResults.Any() ? CaseResults : defaultValuesFactory();
        public IEnumerable<TResult> Results(TResult defaultValue) => CaseResults.Any() ? CaseResults : defaultValue.Lift();
        public IEnumerable<TResult> Results(Func<TResult> defaultValueFactory) => CaseResults.Any() ? CaseResults : defaultValueFactory().Lift();
    }

    public class EnumerableMatch<T, TResult> : EnumerableMatchBase<T, TResult> where T : class, IL
    {
        public EnumerableMatch(IEnumerable<T> values) : base(values) { }

        public EnumerableMatch<T, TResult> Case<TCase>(Func<TCase, TResult> f) where TCase : class, IL => Apply(f);

        private EnumerableMatch<T, TResult> Apply<TCase>(Func<TCase, TResult> f) where TCase : class, IL {
            if (Matched) { return this; }
            CaseResults = Values.OfType<TCase>()
                            .Select(c => {
                                SetIsMatched();
                                return f(c);
                            });
            return this;
        }
    }
}