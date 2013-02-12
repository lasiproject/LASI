using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    public static class FunctionExtensions
    {
        /// <summary>
        /// Composes 2 functions, f and g, producing a new function, result, such that the result of invoking f.Compose(g), 
        /// where f and g are function such that return type of g is the same as the argument type of f,is equivalent to the result of calling them as f(g(x)).
        /// </summary>
        /// <typeparam name="T">The generic return type of the first function.</typeparam>
        /// <typeparam name="U">The generic input type of the second function and the generic input type of the resulting function</typeparam>
        /// <typeparam name="R">The generic input type of the first function and the generic result type of the second function</typeparam>
        /// <param name="f">The outer function f, of the composition to perform f(g)</param>
        /// <param name="g">The inner function g, of the composition to perform f(g)</param>
        /// <returns>A new function which when invoked is equivalent to invoking the first function on the result of invoking the second on some arbitrary U, u</returns>
        public static Func<U, T> Compose<T, U, R>(this Func<R, T> f, Func<U, R> g) {
            return t => f(g(t));
        }

        public static Func<T, T> Compose<T>(this Func<T, T> func, params Func<T, T>[] fs) {
            Func<T, T> result = func;
            foreach (var f in fs)
                result = f.Compose(result);
            return result;
        }

        public static T Identity<T>(this T value) {
            return value;
        }
    }
}
