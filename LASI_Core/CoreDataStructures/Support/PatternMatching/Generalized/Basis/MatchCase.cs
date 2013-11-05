using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Patternization.Generalized
{
    class MatchCase<TResult>
    {
        #region Constructors
        protected internal MatchCase(object value) { _value = value; }
        #endregion
        #region With Expressions
        public MatchCase<TResult> With<TCase>(Func<TResult> actn) {
            if (!_matchFound && _value is TCase) {
                _matchFound = true;
                _result = actn();
            }
            return this;
        }
        public MatchCase<TResult> With<TCase>(Func<TCase, TResult> actn) {
            if (!_matchFound && _value is TCase) {
                _matchFound = true;
                _result = actn((TCase)_value);
            }
            return this;
        }
        public MatchCase<TResult> With<TCase>(Func<TCase, bool> when, Func<TResult> actn) {
            if (!_matchFound && _value is TCase && when((TCase)_value)) {
                _matchFound = true;
                _result = actn();
            }
            return this;
        }
        public MatchCase<TResult> With<TCase>(Func<TCase, bool> when, Func<TCase, TResult> actn) {
            if (!_matchFound && _value is TCase && when((TCase)_value)) {
                _matchFound = true;
                _result = actn((TCase)_value);
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
    class MatchCase
    {
        #region Constructors
        protected internal MatchCase(object value) { _value = value; }
        #endregion

        public MatchCase<R> Yield<R>() {
            return new MatchCase<R>(_value);
        }
        #region With Expressions
        public MatchCase Case(Func<object, bool> when, Action actn) {
            if (!_matchFound && when(_value)) {
                actn();
            }
            return this;
        }
        public MatchCase Case(Func<object, bool> when, Action<object> actn) {
            if (!_matchFound && when(_value)) {
                actn(_value);
            }
            return this;
        }
        public MatchCase Case<TCase>(Action actn) {
            if (!_matchFound && _value is TCase) {
                _matchFound = true;
                actn();
            }
            return this;
        }
        public MatchCase Case<TCase>(Action<TCase> actn) {
            if (!_matchFound && _value is TCase) {
                _matchFound = true;
                actn((TCase)_value);
            }
            return this;
        }
        public MatchCase Case<TCase>(Func<TCase, bool> when, Action actn) {
            if (!_matchFound && _value is TCase && when((TCase)_value)) {
                _matchFound = true;
                actn();
            }
            return this;
        }
        public MatchCase Case<TCase>(Func<TCase, bool> when, Action<TCase> actn) {
            if (!_matchFound && _value is TCase && when((TCase)_value)) {
                _matchFound = true;
                actn((TCase)_value);
            }
            return this;
        }

        #endregion

        #region Default Expressions

        public MatchCase Perform(Action actn) {
            if (!_matchFound) {
                actn();
            }
            return this;
        }
        public MatchCase Perform(Action<object> actn) {
            if (!_matchFound) {
                actn(_value);
            }
            return this;
        }

        #endregion

        #region Fields
        private object _value;
        private bool _matchFound;
        #endregion

    }
}