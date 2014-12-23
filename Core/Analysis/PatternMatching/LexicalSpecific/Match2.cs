using System;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific
{
    public class Match<T1, T2> where T1 : class, ILexical where T2 : class, ILexical
    {
        T1 first;
        T2 second;
        internal Match(T1 first, T2 second) {
            this.first = first;
            this.second = second;
        }
        public Match<T1, T2, TResult> Yield<TResult>() {
            return new Match<T1, T2, TResult>(first, second, false);
        }
    }

    public class Match<T1, T2, TResult>
        where T1 : class, ILexical
        where T2 : class, ILexical
    {
        bool accepted;
        private T1 first;
        private T2 second;
        private TResult result = default(TResult);

        internal Match(T1 first, T2 second) {
            this.first = first;
            this.second = second;
        }

        internal Match(T1 first, T2 second, bool accepted) {
            this.first = first;
            this.second = second;
            this.accepted = accepted;
        }

        public Match<T1, T2, TResult> Case<TPattern1, TPattern2>(Func<TPattern1, TPattern2, TResult> f)
            where TPattern1 : class, ILexical
            where TPattern2 : class, ILexical {
            if (!accepted) {
                var maybe1 = first as TPattern1;
                var maybe2 = second as TPattern2;
                if (maybe1 != null && maybe2 != null) {
                    this.result = f(maybe1, maybe2);
                    accepted = true;
                }
            }
            return this;
        }

        public Match<T1, T2, TResult> Case(Func<T1, T2, TResult> f) => this.Case<T1, T2>(f);
        public TResult Result(Func<T1, T2, TResult> defaultValueFactory) => result = defaultValueFactory(first, second);
        public TResult Result(Func<TResult> defaultValueFactory) => result = defaultValueFactory();
        public TResult Result(TResult defaultValue) => result = defaultValue;
    }
}
