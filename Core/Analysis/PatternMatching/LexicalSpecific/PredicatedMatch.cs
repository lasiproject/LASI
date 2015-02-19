using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching
{
    /// <summary>
    /// Provides for the representation and free-form structuring of a non result yielding Match expression which is predicated by an arbitrary condition.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam>
    [DebuggerStepThrough]
    public class PredicatedMatch<T> : PredicatedMatchBase<T> where T : class, ILexical
    {
        /// <summary>
        /// Initializes a new instance of the PredicatedMatch&lt;T&gt; class which will match attempt to match against the value of supplied Match if accepted argument is true.
        /// </summary>
        /// <param name="accepted">Indicates if match operations are to be tested. If false, Then expressions will have no effect and simply return the original match.</param>
        /// <param name="inner">The match which has been predicated.</param>
        internal PredicatedMatch(bool accepted, Match<T> inner) : base(accepted)
        {
            this.expression = inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. This expression will be selected, and the provided action invoked, if and only if the predicate has been satisfied and the value being matched over is of this type.</typeparam>
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> Then<TCase>(Action action) where TCase : class, ILexical
        {
            return Accepted ? expression.Case<TCase>(action) : expression;
        }

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. This expression will be selected, and the provided action invoked, if and only if the predicate has been satisfied and the value being matched over is of this type.</typeparam>
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> Then<TCase>(Action<TCase> action) where TCase : class, ILexical
        {
            return Accepted ? expression.Case(action) : expression;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. This expression will be selected, and the provided action invoked, if and only if the predicate has been satisfied and the value being matched over is of this type.</typeparam>
        /// <param name="f">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> Then<TCase, TResult>(Func<TCase, TResult> f) where TCase : class, ILexical
        {
            return Accepted ? expression.Yield<TResult>().Case(f) : expression.Yield<TResult>();
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary> 
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> Then(Action action) => Accepted ? expression.Case<T>(action) : expression;

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked on the value being matched over.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> Then(Action<T> action) => Accepted ? expression.Case(action) : expression;
        public Match<T, TResult> Then<TResult>(Func<T, TResult> f) => Accepted ?
            expression.Yield<TResult>().Case(f) :
            expression.Yield<TResult>();
        public Match<T, TResult> Then<TResult>(Func<TResult> f) => Accepted ?
            expression.Yield<TResult>().Case(f) :
            expression.Yield<TResult>();
        public Match<T, TResult> Then<TResult>(TResult value) => Accepted ?
            expression.Yield<TResult>().Case(value) :
            expression.Yield<TResult>();

        #region Fields
        private Match<T> expression;
        #endregion
    }
}
