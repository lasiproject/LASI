using System;

namespace LASI.Core.Analysis.PatternMatching
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCase">
    /// The Type to match with. If the value being matched is of this type, this Case expression
    /// will be selected and executed.
    /// </typeparam>
    /// <typeparam name="TResult">The Type of the result to be yielded by the Pattern Matching expression.</typeparam>
    public sealed class PredicatedMatch<T, TCase, TResult> : PredicatedMatch<T, TResult>
        where T : ILexical
        where TCase : ILexical
    {
        internal PredicatedMatch(bool succeeded, Match<T, TResult> match) : base(succeeded, match) { }

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="func">
        /// The function which, if this Case expression is Matched, will be invoked on the value
        /// being matched with to produce the desired result for a Match with TPattern.
        /// </param>
        /// <returns>The <see cref="Match{T, TResult}"/> describing the Match expression so far.</returns>
        public Match<T, TResult> Then(Func<TCase, TResult> func) => Accepted ? Expression.Case(func) : Expression;
    }
}