﻿using System;
using System.Diagnostics;

namespace LASI.Core.Analysis.PatternMatching
{
    /// <summary>
    /// Provides for the representation and free-form structuring of a non result yielding Match
    /// expression which is predicated by an arbitrary condition.
    /// </summary>
    /// <typeparam name="T">
    /// The Type of the value which the Pattern Matching expression will match with.
    /// </typeparam>
    [DebuggerStepThrough]
    public class PredicatedMatch<T> : PredicatedMatchBase<T> where T : ILexical
    {
        /// <summary>
        /// Initializes a new instance of the Predicated <see cref="Match{T}"/> class which will
        /// match attempt to match against the value of supplied Match if accepted argument is true.
        /// </summary>
        /// <param name="predicateSucceeded">
        /// Indicates if match operations are to be tested. If false, Then expressions will have no
        /// effect and simply return the original match.
        /// </param>
        /// <param name="inner">The match which has been predicated.</param>
        internal PredicatedMatch(bool predicateSucceeded, Match<T> inner) : base(predicateSucceeded) => expression = inner;

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">
        /// The Type to match with. This expression will be selected, and the provided action
        /// invoked, if and only if the predicate has been satisfied and the value being matched over
        /// is of this type.
        /// </typeparam>
        /// <param name="action">
        /// The Action which, if this Case expression is Matched, will be invoked.
        /// </param>
        /// <returns>The <see cref="Match{T}"/> describing the Match expression so far.</returns>
        public Match<T> Then<TCase>(Action action) where TCase : class, ILexical => Accepted ? expression.Case<TCase>(action) : expression;

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">
        /// The Type to match with. This expression will be selected, and the provided action
        /// invoked, if and only if the predicate has been satisfied and the value being matched over
        /// is of this type.
        /// </typeparam>
        /// <param name="action">
        /// The Action which, if this Case expression is Matched, will be invoked.
        /// </param>
        /// <returns>The <see cref="Match{T}"/> describing the Match expression so far.</returns>
        public Match<T> Then<TCase>(Action<TCase> action) where TCase : class, ILexical => Accepted ? expression.Case(action) : expression;



        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="action">
        /// The Action which, if this Case expression is Matched, will be invoked.
        /// </param>
        /// <returns>The <see cref="Match{T}"/> describing the Match expression so far.</returns>
        public Match<T> Then(Action action) => Accepted ? expression.Case<T>(action) : expression;

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="action">
        /// The Action which, if this Case expression is Matched, will be invoked on the value being
        /// matched over.
        /// </param>
        /// <returns>The <see cref="Match{T}"/> describing the Match expression so far.</returns>
        public Match<T> Then(Action<T> action) => Accepted ? expression.Case(action) : expression;

        public Match<T, TResult> Then<TResult>(Func<T, TResult> f) => Accepted ? expression.Case(f) : expression;

        public Match<T, TResult> Then<TResult>(Func<TResult> f) => Accepted ? expression.Case(f) : expression;

        public Match<T, TResult> Then<TResult>(TResult value) => Accepted ? expression.Yield<TResult>().Case(value) : expression;

        #region Fields

        readonly Match<T> expression;

        protected Match<T> Expression => expression;

        #endregion Fields
    }
}