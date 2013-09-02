using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Patternization
{
    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions which match with a value of Type T and does not yield a result.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam> 
    public class MatchCase<T> : IMatchCase<T> where T : class, ILexical
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the MatchCase&lt;T&gt; class which will match against the supplied value.
        /// </summary>
        /// <param name="value">The value to match against.</param>
        protected internal MatchCase(T value) { _value = value; }
        #endregion

        #region Expression Transformations
        /// <summary>
        /// Promotes the current non result returning expression of type Case&lt;T&gt; into a result returning expression of Case&lt;T, R&gt;
        /// Such that subsequent With expressions appended are now to yield a result value of the supplied Type R.
        /// </summary>
        /// <typeparam name="R">The Type of the result which the match expression may now return.</typeparam>
        /// <returns>A Case&lt;T, R&gt; describing the Match expression so far.</returns> 
        public MatchCase<T, R> To<R>() { return new MatchCase<T, R>(_value); }
        #endregion

        #region With Expressions
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="actn">The Action which, if this With expression is Matched, will be invoked.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public IMatchCase<T> With<TCase>(Action actn) where TCase : class ,ILexical {
            if (_value != null) {
                if (!_matchFound) {
                    if (_value is TCase) {
                        _matchFound = true;
                        actn();
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="actn">The Action&lt;TCase&gt; which, if this With expression is Matched, will be invoked on the value being matched over by the PatternMatching expression.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public IMatchCase<T> With<TCase>(Action<TCase> actn) where TCase : class,ILexical {
            if (_value != null) {
                if (!_matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        _matchFound = true;
                        actn(matched);
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
        /// <param name="actn">The Action which, if this With expression is Matched, will be invoked.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public IMatchCase<T> With<TCase>(Func<TCase, bool> when, Action actn) where TCase : class ,ILexical {
            if (_value != null) {
                if (!_matchFound) {
                    if (_value is TCase) {
                        _matchFound = true;
                        actn();
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
        /// <param name="actn">The Action&lt;TCase&gt; which, if this With expression is Matched, will be invoked on the value being matched over by the PatternMatching expression.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public IMatchCase<T> With<TCase>(Func<TCase, bool> when, Action<TCase> actn) where TCase : class ,ILexical {
            if (_value != null) {
                if (!_matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        _matchFound = true;
                        actn(matched);
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
        /// <param name="actn">The function to invoke if no matches in the expression succeeded.</param>
        public void Default(Action actn) {
            if (!_matchFound) {
                actn();
            }
        }
        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="actn">The function to invoke on the match with value if no matches in the expression succeeded.</param>
        public void Default(Action<T> actn) {
            if (_value != null) {
                if (!_matchFound) {
                    actn(_value);
                }
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// The value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        private bool _matchFound;
        private T _value;
        #endregion
    }
    /// <summary>
    /// Provides for the representation and free form structuring of Type based Pattern Matching expressions from a match with value of Type T to a Result of Type R.
    /// </summary>
    /// <typeparam name="T">The Type of the value which the the Pattern Matching expression will match with.</typeparam>
    /// <typeparam name="R">The Type of the result to be yielded by the Pattern Matching expression.</typeparam> 
    public class MatchCase<T, R> : ITestedMatchCase<T, R> where T : class, ILexical
    {
        #region Constructors

        /// <summary>
        /// Initailizes a new instance of the Case&lt;T,R&gt; which will allow for Pattern Matching with the provided value.
        /// </summary>
        /// <param name="value">The value to match with.</param>
        protected internal MatchCase(T value) { _value = value; }

        #endregion

        #region When Expressions
        public ITestedMatchCase<T, R> When(Func<T, bool> when) { return new TestedMatchCase<T, R>(when(_value), this); }
        public ITestedMatchCase<T, R> When<TCase>(Func<TCase, bool> when) where TCase : class, ILexical {
            var typed = _value as TCase;
            return new TestedMatchCase<T, R>(typed != null && when(typed), this);
        }
        #endregion

        #region With Expressions
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match with TCase.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<R> func) where TCase : class, ILexical {
            if (_value != null) {
                if (!_matchFound) {
                    if (_value is TCase) {
                        _result = func();
                        _matchFound = true;
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TCase.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, R> func) where TCase : class, ILexical {
            if (_value != null) {
                if (!_matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        _result = func(matched);
                        _matchFound = true;
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="result">The value which, if this With expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(R result) where TCase : class , ILexical {
            if (_value != null) {
                if (!_matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        _result = result;
                        _matchFound = true;
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
                if (!_matchFound) {
                    var matched = _value as TCase;
                    if (matched != null && when(matched)) {
                        _result = func(matched);
                        _matchFound = true;
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
                if (!_matchFound) {
                    var matched = _value as TCase;
                    if (matched != null && when(matched)) {
                        _result = func();
                        _matchFound = true;
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
        /// <param name="result">The value which, if this With expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public MatchCase<T, R> With<TCase>(Func<TCase, bool> when, R result) where TCase : class, ILexical {
            if (_value != null) {
                if (!_matchFound) {
                    var matched = _value as TCase;
                    if (matched != null && when(matched)) {
                        _result = result;
                        _matchFound = true;
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
            if (!_matchFound) {
                _result = func();
                _matchFound = true;
            }
            return this._result;
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
                if (!_matchFound) {
                    _result = func(_value);
                    _matchFound = true;
                }
            }
            return this._result;
        }
        /// <summary>
        /// Appends a Default Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enformed by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="value">The desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public R Result(R value) {
            if (!_matchFound) {
                _result = value;
                _matchFound = true;
            }
            return this._result;
        }

        #endregion

        #region Additional Public Methods
        public bool TestValue(Func<T, bool> test) {
            return _value != null && test(_value);
        }
        /// <summary>
        /// Returns the result of the Pattern Matching expression. 
        /// The result will be one of 3 possibilities: 
        /// 1. The result specified for the first Match with Type expression which succeeded. 
        /// 2. If no matches succeeded, and a Default expression was provided, the result specified in the Default expression.
        /// 3. If no matches succeeded, and a Default expression was not provided, the default value for type the Result Type of the Match Expression.
        /// </summary>
        /// <returns>Returns the result of the Pattern Matching expression.</returns>
        public R Result() { return _result; }

        #endregion

        #region Fields
        /// <summary>
        /// Gets a value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        private bool _matchFound;
        private T _value;
        private R _result = default(R);
        #endregion

        #region Operators
        public static implicit operator R(MatchCase<T, R> matchCase) { return matchCase.Result(); }
        #endregion
    }
    internal class TestedMatchCase<T, R> : ITestedMatchCase<T, R> where T : class, ILexical
    {
        protected internal TestedMatchCase(bool accepted, MatchCase<T, R> inner) { _accepted = accepted; _inner = inner; }
        protected bool _accepted;
        protected internal MatchCase<T, R> _inner;
        public MatchCase<T, R> With<TCase>(R result)
             where TCase : class , ILexical {
            return _accepted ? this._inner.With<TCase>(result) : this._inner;
        }
        public MatchCase<T, R> With<TCase>(Func<R> func)
            where TCase : class , ILexical {
            return _accepted ? this._inner.With<TCase>(func) : this._inner;
        }
        public MatchCase<T, R> With<TCase>(Func<TCase, R> func)
          where TCase : class , ILexical {
            return _accepted ? this._inner.With<TCase>(func) : this._inner;
        }
    }
    public static class PatternizedCaseMatchExtensions
    {

    }
}