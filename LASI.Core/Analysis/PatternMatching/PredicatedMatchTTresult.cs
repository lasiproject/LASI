using System;
using System.Diagnostics;

namespace LASI.Core.Heuristics.PatternMatching
{  /// <summary>
   /// Provides for the representation and free-form structuring of a result yielding Match
   /// expression which is predicated by an arbitrary condition. </summary> <typeparam name="T">The
   /// Type of the value which the Pattern Matching expression will match with.</typeparam>
   /// <typeparam name="TResult">The Type of the result to be yielded by the Pattern Matching expression.</typeparam>
    [DebuggerStepThrough]
    public class PredicatedMatch<T, TResult> : PredicatedMatchBase<T> where T : ILexical
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PredicatedMatchBase&lt;T, TResult&gt; class.
        /// </summary>
        /// <param name="predicateSucceeded">
        /// A value indicating if the predicate is true for the value being matched over.
        /// </param>
        /// <param name="inner">The Match&lt;T, TResult&gt; which created the current instance.</param>
        internal PredicatedMatch(bool predicateSucceeded, Match<T, TResult> inner)
           : base(predicateSucceeded) => Expression = inner;

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="func">
        /// The function whose which will be called on the value being matched over to produce the
        /// result of the Then expression.
        /// </param>
        /// <returns>The <see cref="Match{T}"/> describing the Match expression so far.</returns>
        public Match<T, TResult> Then(Func<T, TResult> func) => Accepted ? Expression.Case(func) : Expression;

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="result">
        /// The value which, if this Then expression is Matched, will be the result of the Pattern Match.
        /// </param>
        /// <returns>The Match&lt;T, R&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> Then(TResult result) => Accepted ? Expression.Case<T>(result) : Expression;

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="func">The function whose result will be the result of the Then match expression.</param>
        /// <returns>The <see cref="Match{T}"/> describing the Match expression so far.</returns>
        public Match<T, TResult> Then(Func<TResult> func) => Accepted ? Expression.Case<T>(func) : Expression;

        protected Match<T, TResult> Expression { get; }

        #endregion Methods
    }
}