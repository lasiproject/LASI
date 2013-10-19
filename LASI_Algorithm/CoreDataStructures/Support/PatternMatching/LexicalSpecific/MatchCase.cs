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
        /// <typeparam name="TResult">The Type of the result which the match expression may now return.</typeparam>
        /// <returns>A Case&lt;T, R&gt; describing the Match expression so far.</returns> 
        public MatchCase<T, TResult> Yield<TResult>() { return new MatchCase<T, TResult>(_value); }
        #endregion
        #region When Expressions
        /// <summary>
        /// Appends a When expression to the current PatternMatching Expression. The When expression applies a predicate to the value being matched over. 
        /// It must be followed by a Then expression which is only considered if the predicate applied here returns true.
        /// </summary>
        /// <param name="predicate">The predicate to test the value being matched over.</param>
        /// <returns>The IPredicatedPatternMatching&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public IPredicatedMatchCase<T> When(Func<T, bool> predicate) { return new TestedMatchCase<T>(predicate(_value), this); }
        /// <summary>
        /// Appends a When expression to the current PatternMatching Expression. The When expression applies a predicate to the value being matched over. 
        /// It must be followed by a Then expression which is only considered if the predicate applied here returns true.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. That the value being matched is of this type is also necessary for the following then expression to be selected.</typeparam>
        /// <param name="predicate">The predicate to test the value being matched over.</param>
        /// <returns>The IPredicatedPatternMatching&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public IPredicatedMatchCase<T> When<TCase>(Func<TCase, bool> predicate) where TCase : class,ILexical {
            var typed = _value as TCase;
            return new TestedMatchCase<T>(typed != null && predicate(typed), this);
        }
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched suched that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <param name="condition">The predicate to test the value being matched.</param>
        /// <returns>The IPredicatedPatternMatching&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public IPredicatedMatchCase<T> When(bool condition) {
            return new TestedMatchCase<T>(condition, this);
        }
        #endregion
        #region With Expressions
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action which, if this With expression is Matched, will be invoked.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        public IMatchCase<T> Case<TCase>(Action action) where TCase : class ,T {
            if (_value != null) {
                if (!_matchFound) {
                    if (_value is TCase) {
                        _matchFound = true;
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
        public IMatchCase<T> Case<TCase>(Action<TCase> action) where TCase : class,T {
            if (_value != null) {
                if (!_matchFound) {
                    var matched = _value as TCase;
                    if (matched != null) {
                        _matchFound = true;
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
    /// <typeparam name="TResult">The Type of the result to be yielded by the Pattern Matching expression.</typeparam> 
    public class MatchCase<T, TResult> : IMatchCase<T, TResult> where T : class, ILexical
    {
        #region Constructors

        /// <summary>
        /// Initailizes a new instance of the Case&lt;T,R&gt; which will allow for Pattern Matching with the provided value.
        /// </summary>
        /// <param name="value">The value to match with.</param>
        protected internal MatchCase(T value) { _value = value; }

        #endregion

        #region When Expressions
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched suched that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <param name="predicate">The predicate to test the value being matched.</param>
        /// <returns>The IPredicatedPatternMatching&lt;T, R&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public IPredicatedMatchCase<T, TResult> When(Func<T, bool> predicate) { return new TestedMatchCase<T, TResult>(predicate(_value), this); }
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched suched that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. That the value being matched is of this type is also necessary for the following then expression to be selected.</typeparam>
        /// <param name="predicate">The predicate to test the value being matched.</param>
        /// <returns>The IPredicatedPatternMatching&lt;T, R&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public IPredicatedMatchCase<T, TResult> When<TCase>(Func<TCase, bool> predicate) where TCase : class, T {
            var typed = _value as TCase;
            return new TestedMatchCase<T, TResult>(typed != null && predicate(typed), this);
        }
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched suched that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <param name="condition">The predicate to test the value being matched.</param>
        /// <returns>The IPredicatedPatternMatching&lt;T, R&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        public IPredicatedMatchCase<T, TResult> When(bool condition) {
            return new TestedMatchCase<T, TResult>(condition, this);
        }
        #endregion

        #region Case Expressions
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and executed.</typeparam>
        /// <param name="func">The function which, if this With expression is Matched, will be invoked to produce the corresponding desired result for a Match with TCase.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        public IMatchCase<T, TResult> Case<TCase>(Func<TResult> func) where TCase : class, T {
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
        public IMatchCase<T, TResult> Case<TCase>(Func<TCase, TResult> func) where TCase : class, T {
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
        public IMatchCase<T, TResult> Case<TCase>(TResult result) where TCase : class, T {
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
        #endregion

        #region Result Expressions
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public TResult Result(Func<TResult> func) {
            if (!_matchFound) {
                _result = func();
                _matchFound = true;
            }
            return this._result;
        }
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public TResult Result(Func<T, TResult> func) {
            if (_value != null) {
                if (!_matchFound) {
                    _result = func(_value);
                    _matchFound = true;
                }
            }
            return this._result;
        }
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="defaultValue">The desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        public TResult Result(TResult defaultValue) {
            if (!_matchFound) {
                _result = defaultValue;
                _matchFound = true;
            }
            return this._result;
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
        public TResult Result() { return _result; }

        #endregion

        #region Fields
        /// <summary>
        /// Gets a value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        private bool _matchFound;
        private T _value;
        private TResult _result = default(TResult);
        #endregion




    }
    public class TestedMatchCase<T> : IPredicatedMatchCase<T> where T : class, ILexical
    {
        private IMatchCase<T> _inner;
        private bool _accepted;
        protected internal TestedMatchCase(bool accepted, MatchCase<T> inner) { _accepted = accepted; _inner = inner; }
        public IMatchCase<T> Then<TCase>(Action action) where TCase : class, T {
            return _accepted ? this._inner.Case<TCase>(action) : this._inner;
        }
        public IMatchCase<T> Then<TCase>(Action<TCase> action) where TCase : class, T {
            return _accepted ? this._inner.Case(action) : this._inner;
        }


        public IMatchCase<T> Then(Action action) {
            return _accepted ? this._inner.Case<T>(action) : this._inner;
        }
        public IMatchCase<T> Then(Action<T> action) {
            return _accepted ? this._inner.Case<T>(action) : this._inner;
        }
    }

    public class TestedMatchCase<T, TResult> : IPredicatedMatchCase<T, TResult> where T : class, ILexical
    {
        protected internal TestedMatchCase(bool accepted, MatchCase<T, TResult> inner) { _accepted = accepted; _inner = inner; }
        protected bool _accepted;
        protected internal IMatchCase<T, TResult> _inner;
        public IMatchCase<T, TResult> Then<TCase>(TResult result)
             where TCase : class, T {
            return _accepted ? this._inner.Case<TCase>(result) : this._inner;
        }
        public IMatchCase<T, TResult> Then<TCase>(Func<TResult> func)
            where TCase : class, T {
            return _accepted ? this._inner.Case<TCase>(func) : this._inner;
        }
        public IMatchCase<T, TResult> Then<TCase>(Func<TCase, TResult> func)
            where TCase : class, T {
            return _accepted ? this._inner.Case<TCase>(func) : this._inner;
        }

        public IMatchCase<T, TResult> Then(Func<T, TResult> func) {
            return _accepted ? this._inner.Case<T>(func) : this._inner;
        }

        public IMatchCase<T, TResult> Then(TResult resultValue) {
            return _accepted ? _inner.Case<T>(resultValue) : this._inner;
        }
        public IMatchCase<T, TResult> Then(Func<TResult> func) {
            return _accepted ? this._inner.Case<T>(func) : this._inner;
        }
        public TResult Result() {
            return _inner.Result();
        }

        public TResult Result(TResult defaultValue) {
            return _inner.Result(defaultValue);
        }

        public TResult Result(Func<TResult> func) {
            return _inner.Result(func);
        }

        public TResult Result(Func<T, TResult> func) {
            return _inner.Result(func);
        }
    }

}