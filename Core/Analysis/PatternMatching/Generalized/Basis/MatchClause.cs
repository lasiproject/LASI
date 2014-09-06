using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///// <summary>
///// Contains constructs for type based pattern matching over any Type.
///// </summary>
namespace LASI.Core.PatternMatching.Generalized
{
    class MatchClause<TResult>
    {
        #region Constructors
        protected internal MatchClause(object value) { _value = value; }
        #endregion
        #region With Expressions
        public MatchClause<TResult> With<TCase>(Func<TResult> action) {
            if (!_matchFound && _value is TCase) {
                _matchFound = true;
                _result = action();
            }
            return this;
        }
        public MatchClause<TResult> With<TCase>(Func<TCase, TResult> action) {
            if (!_matchFound && _value is TCase) {
                _matchFound = true;
                _result = action((TCase)_value);
            }
            return this;
        }
        public MatchClause<TResult> With<TCase>(Func<TCase, bool> when, Func<TResult> action) {
            if (!_matchFound && _value is TCase && when((TCase)_value)) {
                _matchFound = true;
                _result = action();
            }
            return this;
        }
        public MatchClause<TResult> With<TCase>(Func<TCase, bool> when, Func<TCase, TResult> action) {
            if (!_matchFound && _value is TCase && when((TCase)_value)) {
                _matchFound = true;
                _result = action((TCase)_value);
            }
            return this;
        }
        #endregion

        public TResult Result() { return _result; }

        #region Fields
        private TResult _result;
        private object _value;
        private bool _matchFound;
        #endregion

    }
    class MatchClause
    {
        #region Constructors
        protected internal MatchClause(object value) { this.value = value; }
        #endregion

        public MatchClause<R> Yield<R>() {
            return new MatchClause<R>(value);
        }
        #region With Expressions
        public MatchClause Case(Func<object, bool> when, Action action) {
            if (!_matchFound && when(value)) {
                action();
            }
            return this;
        }
        public MatchClause Case(Func<object, bool> when, Action<object> action) {
            if (!_matchFound && when(value)) {
                action(value);
            }
            return this;
        }
        public MatchClause Case<TCase>(Action action) {
            if (!_matchFound && value is TCase) {
                _matchFound = true;
                action();
            }
            return this;
        }
        public MatchClause Case<TCase>(Action<TCase> action) {
            if (!_matchFound && value is TCase) {
                _matchFound = true;
                action((TCase)value);
            }
            return this;
        }
        public MatchClause Case<TCase>(Func<TCase, bool> when, Action action) {
            if (!_matchFound && value is TCase && when((TCase)value)) {
                _matchFound = true;
                action();
            }
            return this;
        }
        public MatchClause Case<TCase>(Func<TCase, bool> when, Action<TCase> action) {
            if (!_matchFound && value is TCase && when((TCase)value)) {
                _matchFound = true;
                action((TCase)value);
            }
            return this;
        }

        #endregion

        #region Default Expressions

        public MatchClause Perform(Action action) {
            if (!_matchFound) {
                action();
            }
            return this;
        }
        public MatchClause Perform(Action<object> actn) {
            if (!_matchFound) {
                actn(value);
            }
            return this;
        }

        #endregion

        #region Fields
        private object value;
        private bool _matchFound;
        #endregion

    }
}