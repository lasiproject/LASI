using System;

namespace LASI.Core.Analysis.PatternMatching
{
    public sealed class PredicatedMatchLifted<T, TCase> : PredicatedMatch<T>
        where T : ILexical
        where TCase : ILexical
    {

        public PredicatedMatchLifted(bool v, Match<T> inner) : base(v, inner)
        {
        }

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TResult">
        /// The Type of the result to be yielded by the Pattern Matching expression.
        /// </typeparam>
        /// <param name="f">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The <see cref="Match{T}"/> describing the Match expression so far.</returns>
        public Match<T, TResult> Then<TResult>(Func<TCase, TResult> f) => Accepted ? Expression.Case(f) : Expression;
    }
}