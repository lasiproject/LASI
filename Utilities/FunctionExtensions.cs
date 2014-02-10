using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines extension methods for System.Func&lt;T, TResult&gt; instances.
    /// </summary>
    public static class FunctionExtensions
    {
        /// <summary>
        /// Composes 2 functions, f and g, producing a new function, result, such that the result of invoking f.Compose(g), 
        /// where f and g are function such that return type of g is the same as the argument type of f,is equivalent to the result of calling them as f(g(x)).
        /// </summary>
        /// <typeparam name="A">The generic return type of the first function.</typeparam>
        /// <typeparam name="B">The generic input type of the second function and the generic input type of the resulting function</typeparam>
        /// <typeparam name="C">The generic input type of the first function and the generic result type of the second function</typeparam>
        /// <param name="f">The outer function f, of the composition to perform f(g)</param>
        /// <param name="g">The inner function g, of the composition to perform f(g)</param>
        /// <returns>a new function which when invoked is equivalent to invoking the first function on the result of invoking the second on some arbitrary B, b</returns>
        public static Func<A, C> Compose<A, B, C>(this Func<B, C> f, Func<A, B> g) {
            return t => f(g(t));
        }

        static Func<T, TResult2> Select<T, TResult, TResult2>(
                this Func<T, TResult> func,
                Func<TResult, TResult2> selector) {
            return x => selector(func(x));
        }

    }

}
