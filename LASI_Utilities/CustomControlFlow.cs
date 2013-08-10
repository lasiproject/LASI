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


namespace LASI.Utilities.TypedSwitch
{

    #region Non Generic Switch Components

    /// <summary>
    /// Forms the head of the customized Typed Switching block and provides the switch control object.
    /// </summary>
    public class Switch
    {
        /// <summary>
        /// Initializes the head of a Typed Switch statement, specifying the value on which to switch.
        /// </summary>
        /// <param name="switchOn">The object on which to switch.</param>
        public Switch(object switchOn) {
            SwitchOn = switchOn;
        }
        /// <summary>
        /// Gets the object on which to switch.
        /// </summary>
        public object SwitchOn {
            get;
            private set;
        }

    }

    /// <summary>
    /// Defines extension methods which allow for the chaining of switch cases which are infact objects.
    /// </summary>
    public static class SwitchExtensions
    {
        /// <summary>
        /// Defines the head of a Typed Switch statement, specifying the value on which to switch.
        /// </summary>
        /// <typeparam name="T">The type of the value to switch on.</typeparam>
        /// <param name="switchOn">The value to switch on.</param>
        /// <returns>A Swictch object describing the new Switch statement.</returns>
        public static Switch Switch<T>(this T switchOn) where T : class {
            return new Switch(switchOn);
        }
        /// <summary>
        /// Appends a new case to the Typed Switch statement.
        /// </summary>
        /// <typeparam name="T">The type of the value to switch on.</typeparam>
        /// <param name="s">The Typed Switch statement to which to append the new case.</param>
        /// <param name="action">The void function defining the body of the new case.</param>
        /// <returns>The provided Typed Switch object with the new case appended.</returns>
        public static Switch Case<T>(this Switch s, Action<T> action) where T : class {
            return Case<T>(s, x => true, action, false);
        }
        /// <summary>
        /// Appends a new case to the Typed Switch statement.
        /// </summary>
        /// <typeparam name="T">The type of the value to switch on.</typeparam>
        /// <param name="s">The Typed Switch statement to which to append the new case.</param>
        /// <param name="action">The void function defining the body of the new case.</param>
        /// <returns>The provided Typed Switch object with the new case appended.</returns>
        public static Switch Case<T>(this Switch s, Action action) where T : class {
            return Case<T>(s, x => true, p => action(), false);
        }
        /// <summary>
        /// Appends a new case to the Typed Switch statement.
        /// </summary>
        /// <typeparam name="T">The type of the value to switch on.</typeparam>
        /// <param name="s">The Typed Switch statement to which to append the new case.</param>
        /// <param name="action">The void function defining the body of the new case.</param>
        /// <param name="fallThrough">Indicates if case fallthrough is allowed.</param>
        /// <returns>The provided Typed Switch object with the new case appended.</returns>
        public static Switch Case<T>(this Switch s, Action<T> action, bool fallThrough) where T : class {
            return Case<T>(s, x => true, action, fallThrough);
        }
        /// <summary>
        /// Appends a new case to the Typed Switch statement.
        /// </summary>
        /// <typeparam name="T">The type of the value to switch on.</typeparam>
        /// <param name="s">The Typed Switch statement to which to append the new case.</param>
        /// <param name="condition">The condition which the switch value must meet for the current case to be selected.</param>
        /// <param name="action">The void function defining the body of the new case.</param>
        /// <returns>The provided Typed Switch object with the new case appended.</returns>
        public static Switch Case<T>(this Switch s, Func<T, bool> condition, Action<T> action) where T : class {
            return Case<T>(s, condition, action, false);
        }
        /// <summary>
        /// Appends a new case to the Typed Switch statement.
        /// </summary>
        /// <typeparam name="T">The type of the value to switch on.</typeparam>
        /// <param name="s">The Typed Switch statement to which to append the new case.</param>
        /// <param name="condition">The condition which the switch value must meet for the current case to be selected.</param>
        /// <param name="action">The void function defining the body of the new case.</param>
        /// <param name="fallThrough">Indicates if case fallthrough is allowed.</param>
        /// <returns>The provided Typed Switch object with the new case appended.</returns>
        public static Switch Case<T>(this Switch s, Func<T, bool> condition, Action<T> action, bool fallThrough) where T : class {
            if (s == null) {
                return null;
            } else {
                T tCasted = s.SwitchOn as T;
                if (tCasted != null) {
                    if (condition(tCasted)) {
                        action(tCasted);
                        return fallThrough ? s : null;
                    }
                }
            }
            return s;
        }
        /// <summary>
        /// Defines and appends the default case to the Typed Switch statement.
        /// </summary>
        /// <param name="s">The Typed Switch statement to which to append the default case.</param>
        /// <param name="action">The void function defining the body of the new case.</param>
        public static void Default(this Switch s, Action action) {
            Default(s, x => action());
        }
        /// <summary>
        /// Defines and appends the default case to the Typed Switch statement.
        /// </summary>
        /// <param name="s">The Typed Switch statement to which to append the default case.</param>
        /// <param name="action">The void function defining the body of the new case.</param>
        public static void Default(this Switch s, Action<object> action) {
            if (s != null)
                action(s.SwitchOn);
        }
        /// <summary>
        /// Defines and appends the default case to the Typed Switch statement.
        /// </summary>
        /// <typeparam name="T">The type of the value to switch on.</typeparam>
        /// <param name="s">The Typed Switch statement to which to append the default case.</param>
        /// <param name="action">The void function defining the body of the new case.</param>
        public static void Default<T>(this Switch s, Action<T> action) where T : class {
            if (s != null && s.SwitchOn as T != null)
                action(s.SwitchOn as T);
        }
    }
    public static class Match<T, R> where T : class
    {
        public static M<T, R> On(T matchOn) { return new M<T, R>(matchOn); }
    }
    public static class Match<T> where T : class
    {
        public static M<T> On(T matchOn) { return new M<T>(matchOn); }
        public static TM<T> From(T matchOn) { return new TM<T>(matchOn); }
    }
    public static class Match
    {


        public static M<T> On<T>(T matchOn) where T : class { return new M<T>(matchOn); }
        public static M<T, R> On<T, R>(T matchOn) where T : class { return new M<T, R>(matchOn); }
        public static TM<T> From<T>(T matchOn) where T : class { return new TM<T>(matchOn); }
    }
    public class M<T, R> where T : class
    {
        protected internal M(T matchOn) { toMatch = matchOn; }
        public M<T, R> With<TCase>(Func<R> func) where TCase : class,T {
            if (result == null) {
                if (toMatch is TCase) {
                    result = func();
                }
            }
            return this;
        }
        public M<T, R> With<TCase>(Func<TCase, bool> condition, Func<R> func) where TCase : class,T {
            if (result == null) {
                var matched = toMatch as TCase;
                if (matched != null && condition(matched)) {
                    result = func();
                }
            }
            return this;
        }
        public M<T, R> With<TCase>(Func<TCase, R> func) where TCase : class,T {
            if (result == null) {
                var matched = toMatch as TCase;
                if (matched != null) {
                    result = func(matched);
                }
            }
            return this;
        }
        public M<T, R> With<TCase>(Func<TCase, bool> condition, Func<TCase, R> func) where TCase : class,T {
            if (result == null) {
                var matched = toMatch as TCase;
                if (matched != null && condition(matched)) {
                    result = func(matched);
                }
            }
            return this;
        }
        public M<T, R> Default(Func<R> func) {
            if (result == null) {
                result = func();
            }
            return this;
        }
        public M<T, R> Default(Func<T, R> func) {
            if (result == null) {
                result = func(toMatch);
            }
            return this;
        }
        public R Result { get { return result; } }

        private T toMatch;
        private R result;
    }
    public class M<T> where T : class
    {

        protected internal M(T matchOn) { toMatch = matchOn; }
        public M<T> With<TCase>(Action action) where TCase : class ,T {
            if (!matchFound) {
                if (toMatch is TCase) {
                    matchFound = true;
                    action();
                }
            }
            return this;
        }
        public M<T> With<TCase>(Action<TCase> action) where TCase : class ,T {
            if (!matchFound) {
                var matched = toMatch as TCase;
                if (matched != null) {
                    matchFound = true;
                    action(matched);
                }
            }
            return this;
        }
        public void Default(Action action) {
            if (!matchFound) {
                action();
            }
        }
        public void Default(Action<T> action) {
            if (!matchFound) {
                action(toMatch);
            }
        }
        private bool matchFound;
        private T toMatch;

    }
    public struct TM<T> where T : class
    {
        internal TM(T toMatch) {
            matchOn = toMatch;
        }
        public M<T, R> To<R>() {
            return new M<T, R>(matchOn);
        }
        private T matchOn;
    }
    #endregion
}