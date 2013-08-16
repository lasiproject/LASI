/*
 * Concepts and tutorials for these customized switch statement functions were provided 
 * by Bart De Smet via his exceptional programming blog. 
 * Entry: A FUNCTIONAL C# (TYPE)SWITCH  http://community.bartdesmet.net/blogs/bart/archive/2008/03/30/a-functional-c-type-switch.aspx
 * Main Blog: http://community.bartdesmet.net/blogs/bart/default.aspx
 * Thank you!
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Utilities.PatternMatching
{

    /// <summary>
    /// Provides for the construction of flexible Pattern Matching expressions.
    /// </summary>
    public static class MatchingExtensions
    {
        /// <summary>
        /// Constructs the head of a non result returning Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="t">The value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression with the specified value.</returns>
        public static PatternMatching<T> Match<T>(this T t) where T : class { return new PatternMatching<T>(t); }
        //public static PatternToFromTransition<T> MatchFrom<T>(this T t) where T : class { return new PatternToFromTransition<T>(t); }
        //public static M<object, R> MatchTo<R>(this object t) { return new M<object, R>(t); }
    }
    /// <summary>
    /// Provides for the construction of flexible Pattern Matching expressions.
    /// </summary>
    public static class Match
    {
        /// <summary>
        /// Constructs the head of a non result returning Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="matchOn">The value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression with the specified value.</returns>
        public static PatternMatching<T> On<T>(T matchOn) where T : class { return new PatternMatching<T>(matchOn); }
        /// <summary>
        /// Constructs the head of a result-yielding, Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <typeparam name="R">The Type of the result to be yielded.</typeparam> 
        /// <param name="matchOn">The value to match with.</param>     
        /// <returns>The head of a result-yielding, Type based Pattern Matching expression with the specified value.</returns>
        public static PatternMatching<T, R> On<T, R>(T matchOn) where T : class { return new PatternMatching<T, R>(matchOn); }
        /// <summary>
        /// Constructs the intermediate "From" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a From...To expression.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="matchOn">The value to match with.</param>
        /// <returns>The intermediate "From" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a From...To expression.
        /// </returns>
        public static TransitionHelper<T> From<T>(T matchOn) where T : class { return new TransitionHelper<T>(matchOn); }
        /// <summary>
        /// Represents the intermediate "From" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a From...To expression.
        /// </summary>
        /// <typeparam name="T">The Type of the value that will be matched with.</typeparam>
        public struct TransitionHelper<T> where T : class
        {
            internal TransitionHelper(T toMatch) {
                matchOn = toMatch;
            }
            /// <summary>
            /// Completes the From...To expression by specifying the type of the Result and constructing and returning the head of a result-yielding, Type based Pattern Matching expression.
            /// </summary>
            /// <typeparam name="R">The Type of the Results which may be returned by the expressions appended to the newly created expression.</typeparam>
            /// <returns>
            ///  The head of a result-yielding, Type based Pattern Matching expression.
            ///  </returns>  
            public PatternMatching<T, R> To<R>() {
                return new PatternMatching<T, R>(matchOn);
            }
            private T matchOn;
        }
    }
    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions from a match with value of Type T to a Result of Type R.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam>
    /// <typeparam name="R">The Type of the result to be yielded by the Pattern Matching expression.</typeparam> 
    public class PatternMatching<T, R> where T : class
    {
        /// <summary>
        /// Initailizes a new instance of the PatternMatching&lt;T&gt;&lt;R&gt; which will allow for Pattern Matching with the provided value.
        /// </summary>
        /// <param name="matchOn">The value to match with.</param>
        protected internal PatternMatching(T matchOn) { toMatch = matchOn; }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match with TCase.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public PatternMatching<T, R> With<TCase>(Func<R> func) where TCase : class,T {
            if (!matchFound) {
                if (toMatch is TCase) {
                    result = func();
                    matchFound = true;
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="condition">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match with TCase.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public PatternMatching<T, R> With<TCase>(Func<TCase, bool> condition, Func<R> func) where TCase : class,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null && condition(matched)) {
                    result = func();
                    matchFound = true;
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TCase.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public PatternMatching<T, R> With<TCase>(Func<TCase, R> func) where TCase : class,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null) {
                    result = func(matched);
                    matchFound = true;
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="condition">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TCase.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        public PatternMatching<T, R> With<TCase>(Func<TCase, bool> condition, Func<TCase, R> func) where TCase : class,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null && condition(matched)) {
                    result = func(matched);
                    matchFound = true;
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public PatternMatching<T, R> Default(Func<R> func) {
            if (!matchFound) {
                result = func();
                matchFound = true;
            }
            return this;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public PatternMatching<T, R> Default(Func<T, R> func) {
            if (!matchFound) {
                result = func(toMatch);
                matchFound = true;
            }
            return this;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="defaultValue">The desired default value.</param>
        /// <returns>The PatternMatching&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public PatternMatching<T, R> Default(R defaultValue) {
            if (!matchFound) {
                result = defaultValue;
                matchFound = true;
            }
            return this;
        }
        /// <summary>
        /// Returns the result of the Pattern Matching expression. 
        /// The result will be one of 3 possibilities: 
        /// 1. The result specified for the first Match with Type expression which succeeded. 
        /// 2. If no matches succeeded, and a Default expression was provided, the result specified in the Default expression.
        /// 3. If no matches succeeded, and a Default expression was not provided, the default value for type the Result Type of the Match Expression.
        /// </summary>
        /// <returns>Returns the result of the Pattern Matching expression.</returns>
        public R Result() { return result; }
        private bool matchFound;
        /// <summary>
        /// Gets a value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        public bool MatchFound {
            get { return matchFound; }
        }
        private T toMatch;
        private R result;
    }







    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions which match with a value of Type T and does not yield a result.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam> 
    public class PatternMatching<T> where T : class
    {

        internal PatternMatching(T matchOn) { toMatch = matchOn; }
        public PatternMatching<T> With<TCase>(Action action) where TCase : class ,T {
            if (!matchFound) {
                if (toMatch is TCase) {
                    matchFound = true;
                    action();
                }
            }
            return this;
        }
        public PatternMatching<T> With<TCase>(Action<TCase> action) where TCase : class ,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null) {
                    matchFound = true;
                    action(matched);
                }
            }
            return this;
        }
        public PatternMatching<T> With<TCase>(Func<TCase, bool> condition, Action action) where TCase : class ,T {
            if (!matchFound) {
                if (toMatch is TCase) {
                    matchFound = true;
                    action();
                }
            }
            return this;
        }
        public PatternMatching<T> With<TCase>(Func<TCase, bool> condition, Action<TCase> action) where TCase : class ,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null) {
                    matchFound = true;
                    action(matched);
                }
            }
            return this;
        }
        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="action">The function to invoke if no matches in the expression succeeded.</param>
        public void Default(Action action) {
            if (!matchFound) {
                action();
            }
        }
        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="action">The function to invoke on the match with value if no matches in the expression succeeded.</param>
        public void Default(Action<T> action) {
            if (!matchFound) {
                action(toMatch);
            }
        }
        private bool matchFound;
        /// <summary>
        /// Gets a value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        public bool MatchFound {
            get { return matchFound; }
        }
        private T toMatch;

    }

}