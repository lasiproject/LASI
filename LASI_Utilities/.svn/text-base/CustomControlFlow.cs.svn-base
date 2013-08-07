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
    /// Forms the head of the customized Typed Switching block, Determining the object on which to switch.
    /// </summary>
    public class Switch
    {
        /// <summary>
        /// Initializes a new instance of the TypedSwitch class head.
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

    #endregion
}