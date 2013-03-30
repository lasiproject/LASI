using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities.TypedSwitchExtensions
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


    public class Switch<T>
    {
        public Switch(T switchOn) {
            SwitchOn = switchOn;
        }

        public T SwitchOn {
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

    }
    #endregion



    #region Non Generic Switch Components




    public static class SwitchExtensions1
    {
        public static Switch Case(this  Switch s, object switchOn, Action<object> action) {
            return Case(s, switchOn, action, false);
        }
        public static Switch Case(this  Switch s, object switchOn, Action<object> action, bool fallThrough) {
            return Case(s, x => object.Equals(x, switchOn), action, fallThrough);
        }
        public static Switch Case(this  Switch s, Func<object, bool> condition, Action<object> action) {
            return Case(s, condition, action, false);
        }
        public static Switch Case(this Switch s, Func<object, bool> condition, Action<object> action, bool fallThrough) {
            if (s == null) {
                return null;
            } else if (condition(s.SwitchOn)) {
                action(s.SwitchOn);
                return fallThrough ? s : null;
            }
            return s;
        }
        public static void Default(this LASI.Utilities.TypedSwitchExtensions.Switch s, Action<object> action) {
            if (s != null)
                action(s.SwitchOn);
        }
    }
    #endregion




    #region Generic Switch Components


    public static class SwitchExtensions2
    {
        public static Switch<T> Case<T>(this Switch<T> s, T SwitchOn, Action<T> action) {
            return Case(s, SwitchOn, action, false);
        }
        public static Switch<T> Case<T>(this Switch<T> s, T SwitchOn, Action<T> action, bool fallThrough) {
            return Case(s, x => Object.Equals(x, SwitchOn), action, fallThrough);
        }
        public static Switch<T> Case<T>(this Switch<T> s, Func<T, bool> condition, Action<T> action) {
            return Case(s, condition, action, false);
        }
        public static Switch<T> Case<T>(this Switch<T> s, Func<T, bool> condition, Action<T> action, bool fallThrough) {
            if (s == null) {
                return null;
            } else if (condition(s.SwitchOn)) {
                action(s.SwitchOn);
                return fallThrough ? s : null;
            }
            return s;
        }
        public static void Default<T>(this Switch<T> s, Action<T> action) {
            if (s != null)
                action(s.SwitchOn);
        }



    #endregion
    }
}