using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.PatternMatching
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
        internal PredicatedMatch(bool accepted, Match<T> inner) : base(accepted) {
            this.inner = inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. This expression will be selected, and the provided action invoked, if and only if the predicate has been satisfied and the value being matched over is of this type.</typeparam>
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> Then<TPattern>(Action action) where TPattern : class, ILexical {
            return ConditionMet ? inner.Case<TPattern>(action) : inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. This expression will be selected, and the provided action invoked, if and only if the predicate has been satisfied and the value being matched over is of this type.</typeparam>
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> Then<TPattern>(Action<TPattern> action) where TPattern : class, ILexical {
            return ConditionMet ? inner.Case(action) : inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary> 
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> Then(Action action) {
            return ConditionMet ? inner.Case<T>(action) : inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked on the value being matched over.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T> Then(Action<T> action) {
            return ConditionMet ? inner.Case(action) : inner;
        }
        #region Fields
        private Match<T> inner;
        #endregion
    }
    /// <summary>
    /// Provides for the representation and free-form structuring of a result yielding Match expression which is predicated by an arbitrary condition.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam>
    /// <typeparam name="TResult">The Type of the result to be yielded by the Pattern Matching expression.</typeparam> 
    [DebuggerStepThrough]
    public class PredicatedMatch<T, TResult> : PredicatedMatchBase<T> where T : class, ILexical
    {
        #region Constructors
        /// <summary>
        /// Initializes a new isntance of the PredicatedMatchBase&lt;T, TResult&gt; class.
        /// </summary>
        /// <param name="predicateSucceeded">A value indicating if the predicate is true for the value being matched over.</param>
        /// <param name="inner">The Match&lt;T, TResult&gt; which created the current instance.</param>
        internal PredicatedMatch(bool predicateSucceeded, Match<T, TResult> inner)
          : base(predicateSucceeded) {
            this.inner = inner;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this Case expression will be selected and executed.</typeparam>
        /// <param name="result">The value which, if this Case expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The Match&lt;T, R&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> Then<TPattern>(TResult result)
             where TPattern : class, ILexical {
            return ConditionMet ? inner.Case<TPattern>(result) : inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. This expression will be selected, and the provided function invoked, if and only if the predicate has been satisfied and the value being matched over is of this type.</typeparam>
        /// <param name="func">The function whose result will be the result of the Then match expression.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> Then<TPattern>(Func<TResult> func)
            where TPattern : class, ILexical {
            return ConditionMet ? inner.Case<TPattern>(func) : inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this Case expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this Case expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TPattern.</param>
        /// <returns>The Match&lt;T, R&gt; describing the Match expression so far.</returns> 
        public Match<T, TResult> Then<TPattern>(Func<TPattern, TResult> func)
            where TPattern : class, ILexical {
            return ConditionMet ? inner.Case(func) : inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="func">The function whose which will be called on the value being matched over to produce the result of the Then expression.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> Then(Func<T, TResult> func) {
            return ConditionMet ? inner.Case(func) : inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="result">The value which, if this Then expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The Match&lt;T, R&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> Then(TResult result) {
            return ConditionMet ? inner.Case<T>(result) : inner;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <param name="func">The function whose result will be the result of the Then match expression.</param>
        /// <returns>The Match&lt;T&gt; describing the Match expression so far.</returns>
        public Match<T, TResult> Then(Func<TResult> func) {
            return ConditionMet ? inner.Case<T>(func) : inner;
        }

        #endregion

        #region Fields
        private Match<T, TResult> inner;
        #endregion

    }
}
