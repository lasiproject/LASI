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

    public class Switch
    {
        public Switch(object switchOn) {
            SwitchOn = switchOn;
        }

        public object SwitchOn {
            get;
            private set;
        }
    }


    public static class SwitchExtensions
    {
        public static Switch Case<T>(this Switch s, Action<T> action) where T : class {
            return Case<T>(s, x => true, action, false);
        }
        public static Switch Case<T>(this Switch s, Action<T> action, bool fallThrough) where T : class {
            return Case<T>(s, x => true, action, fallThrough);
        }
        public static Switch Case<T>(this Switch s, Func<T, bool> condition, Action<T> action) where T : class {
            return Case<T>(s, condition, action, false);
        }
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



        public static void Default(this Switch s, Action<object> action) {
            if (s != null)
                action(s.SwitchOn);
        }
        public static void Default<T>(this Switch s, Action<T> action) where T : class {
            if (s != null && s.SwitchOn as T != null)
                action(s.SwitchOn as T);
        }
    }
    #endregion
}