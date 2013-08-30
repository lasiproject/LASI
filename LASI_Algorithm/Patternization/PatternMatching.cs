using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LASI.Algorithm.Patternization
{
    /// <summary>
    /// Provides for the construction of flexible Pattern Matching expressions.
    /// </summary>
    public static class Match
    {
        /// <summary>
        /// Constructs the head of a non result returning Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="value">The value to match with.</param>
        /// <returns>The head of a non result yielding Type based Pattern Matching expression with the specified value.</returns>
        public static MatchCase<T> MatchOn<T>(this T value) where T : class, ILexical { return new MatchCase<T>(value); }
        /// <summary>
        /// Constructs the head of a result-yielding, Type based Pattern Matching expression with the specified value.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <typeparam name="R">The Type of the result to be yielded.</typeparam> 
        /// <param name="value">The value to match with.</param>     
        /// <returns>The head of a result-yielding, Type based Pattern Matching expression with the specified value.</returns>
        public static MatchCase<T, R> MatchTo<T, R>(T value) where T : class, ILexical { return new MatchCase<T, R>(value); }
        /// <summary>
        /// Constructs the intermediate "On" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a On...To expression.
        /// </summary>
        /// <typeparam name="T">The Type of the value to match with.</typeparam>
        /// <param name="value">The value to match with.</param>
        /// <returns>The intermediate "On" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a On...To expression.
        /// </returns>
        public static OnTo<T> On<T>(T value) where T : class, ILexical { return new OnTo<T>(value); }


        /// <summary>
        /// Represents the intermediate "From" portion of a Pattern Matching expression providing the value to match with.
        /// The result is a bridge to the "To" portion in a From...To expression.
        /// </summary>
        /// <typeparam name="T">The Type of the value that will be matched with.</typeparam>
        public class OnTo<T> where T : class, ILexical
        {
            internal OnTo(T value) {
                _value = value;
            }
            /// <summary>
            /// Completes the From...To expression by specifying the type of the Result and constructing and returning the head of a result-yielding, Type based Pattern Matching expression.
            /// </summary>
            /// <typeparam name="R">The Type of the Results which may be returned by the expressions appended to the newly created expression.</typeparam>
            /// <returns>
            ///  The head of a result-yielding, Type based Pattern Matching expression.
            ///  </returns>  
            public MatchCase<T, R> To<R>() {
                return new MatchCase<T, R>(_value);
            }
            private T _value;
        }
    }

    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions from a match with value of Type T to a Result of Type R.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam>
    /// <typeparam name="R">The Type of the result to be yielded by the Pattern Matching expression.</typeparam> 
    public class MatchCase<T, R> where T : class, ILexical
    {
        #region Constructors

        /// <summary>
        /// Initailizes a new instance of the Case&lt;T,R&gt; which will allow for Pattern Matching with the provided value.
        /// </summary>
        /// <param name="value">The value to match with.</param>
        protected internal MatchCase(T value) { _value = value; }

        #endregion

        #region With Expressions

        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TCase.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, R> func) where TCase : class, ILexical {
            if (_value != null) {
                if (!matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        result = func(matched);
                        matchFound = true;
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match with TCase.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<R> func) where TCase : class, ILexical {
            if (_value != null) {
                if (!matchFound) {
                    if (_value is TCase) {
                        result = func();
                        matchFound = true;
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="caseResult">The value which, if this With expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(R caseResult) where TCase : class , ILexical {
            if (_value != null) {
                if (!matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        result = caseResult;
                        matchFound = true;
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="when">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TCase.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, bool> when, Func<TCase, R> func) where TCase : class, ILexical {
            if (_value != null) {
                if (!matchFound) {
                    var matched = _value as TCase;
                    if (matched != null && when(matched)) {
                        result = func(matched);
                        matchFound = true;
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="when">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match with TCase.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, bool> when, Func<R> func) where TCase : class, ILexical {
            if (_value != null) {
                if (!matchFound) {
                    var matched = _value as TCase;
                    if (matched != null && when(matched)) {
                        result = func();
                        matchFound = true;
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="when">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="func">The value which, if this With expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, bool> when, R func) where TCase : class, ILexical {
            if (_value != null) {
                if (!matchFound) {
                    var matched = _value as TCase;
                    if (matched != null && when(matched)) {
                        result = func;
                        matchFound = true;
                    }
                }
            }
            return this;
        }

        #endregion

        #region Default Expressions
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public R Result(Func<R> func) {
            if (!matchFound) {
                result = func();
                matchFound = true;
            }
            return this.result;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public R Result(Func<T, R> func) {
            if (_value != null) {
                if (!matchFound) {
                    result = func(_value);
                    matchFound = true;
                }
            }
            return this.result;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="value">The desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public R Result(R value) {
            if (!matchFound) {
                result = value;
                matchFound = true;
            }
            return this.result;
        }

        #endregion

        #region Additional Public Methods

        /// <summary>
        /// Returns the result of the Pattern Matching expression. 
        /// The result will be one of 3 possibilities: 
        /// 1. The result specified for the first Match with Type expression which succeeded. 
        /// 2. If no matches succeeded, and a Default expression was provided, the result specified in the Default expression.
        /// 3. If no matches succeeded, and a Default expression was not provided, the default value for type the Result Type of the Match Expression.
        /// </summary>
        /// <returns>Returns the result of the Pattern Matching expression.</returns>
        public R Result() { return result; }

        #endregion

        #region Fields
        /// <summary>
        /// Gets a value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        private bool matchFound;
        private T _value;
        private R result = default(R);
        #endregion
    }


    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions which match with a value of Type T and does not yield a result.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam> 
    public class MatchCase<T> : ICase<T> where T : class, ILexical
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the MatchCase&lt;T&gt; class which will match against the supplied value.
        /// </summary>
        /// <param name="value">The value to match against.</param>
        protected internal MatchCase(T value) { _value = value; }
        #endregion

        /// <summary>
        /// Promotes the current non result returning expression of type Case&lt;T&gt; into a result returning expression of Case&lt;T, R&gt;
        /// Such that subsequent With expressions appended are now to yield a result value of the supplied Type R.
        /// </summary>
        /// <typeparam name="R">The Type of the result which the match expression may now return.</typeparam>
        /// <returns>A Case&lt;T, R&gt; describing the Match expression so far.</returns> 
        public MatchCase<T, R> Yield<R>() { return new MatchCase<T, R>(_value); }

        #region With Expressions
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action which, if this With expression is Matched, will be invoked.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public ICase<T> With<TCase>(Action action) where TCase : class ,ILexical {
            if (_value != null) {
                if (!matchFound) {
                    if (_value is TCase) {
                        matchFound = true;
                        action();
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action&lt;TCase&gt; which, if this With expression is Matched, will be invoked on the value being matched over by the PatternMatching expression.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public ICase<T> With<TCase>(Action<TCase> action) where TCase : class,ILexical {
            if (_value != null) {
                if (!matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        matchFound = true;
                        action(matched);
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="when">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="action">The Action which, if this With expression is Matched, will be invoked.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public ICase<T> With<TCase>(Func<TCase, bool> when, Action action) where TCase : class ,ILexical {
            if (_value != null) {
                if (!matchFound) {
                    if (_value is TCase) {
                        matchFound = true;
                        action();
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="when">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="action">The Action&lt;TCase&gt; which, if this With expression is Matched, will be invoked on the value being matched over by the PatternMatching expression.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public ICase<T> With<TCase>(Func<TCase, bool> when, Action<TCase> action) where TCase : class ,ILexical {
            if (_value != null) {
                if (!matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        matchFound = true;
                        action(matched);
                    }
                }
            }
            return this;
        }

        #endregion

        #region Default Expressions

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
            if (_value != null) {
                if (!matchFound) {
                    action(_value);
                }
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// The value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        private bool matchFound;
        private T _value;
        #endregion
    }

}