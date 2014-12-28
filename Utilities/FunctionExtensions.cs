using System;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines extension methods for the System.Func&lt;T, TResult&gt; family of delegate types.
    /// </summary>
    public static class FunctionExtensions
    {

        /// <summary>
        /// Composes two functions returning a new function which represents the application of the
        /// first function to the result of the application of the second function. In other words
        /// f.Compose(g)(x) is equivalent to f(g(x)).
        /// </summary>
        /// <typeparam name="T1">
        /// The input type of the second function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The input type of the second function and the result type of the second function.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The return type of the first function and result type of the their composition.
        /// </typeparam>
        /// <param name="first">
        /// The outer function f, of the composition to perform f(g)
        /// </param>
        /// <param name="second">
        /// The inner function g, of the composition to perform f(g)
        /// </param>
        /// <returns>
        /// a new function which when invoked is equivalent to invoking the first function on the
        /// result of invoking the second on some arbitrary B, b.
        /// </returns>
        /// <example>
        /// <code>
        /// Func&lt;double, double&gt;&gt; cube = x =&gt; Math.Pow(x, 3);
        /// Func&lt;double, double&gt;&gt; sqrt = x =&gt; Math.Sqrt(x);
        /// var sqrtOfCube = sqrt.Compose(cube);
        /// var cubeOfSqrt = cube.Compose(sqrt);
        /// var v1 = sqrtOfCube(5); // about 11.18
        /// var v2 = cubeOfSqrt(5); // about 11.18
        /// </code>
        /// </example>
        /// <remarks>
        /// </remarks>
        public static Func<T2, T3> Compose<T1, T2, T3>(this Func<T1, T3> first, Func<T2, T1> second) {
            return x => first(second(x));
        }
        public static Func<T1, T2> Compose<T1, T2>(this Func<T1, T2> first, Func<T1, T1> second) {
            return x => first(second(x));
        }
        public static Func<T1, T1> Compose<T1, T2>(this Func<T2, T1> first, Func<T1, T2> second) {
            return x => first(second(x));
        }

        public static Func<T2, T1> Compose<T1, T2>(this Func<T1, T1> first, Func<T2, T1> second) {
            return x => first(second(x));
        }


        public static Func<T3, T1> AndThen<T1, T2, T3>(this Func<T3, T2> first, Func<T2, T1> second) {
            return x => {
                var y = first(x);
                return second(y);
            };
        }
        public static Func<T1, T3> AndThen<T1, T2, T3>(this Func<T1, T2> first, Func<T2, T3> second) {
            return x => {
                var y = first(x);
                return second(y);
            };
        }

        #region Currying

        public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(this Func<T1, T2, TResult> fn) {
            return a => b => fn(a, b);
        }

        public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> fn) => a => b => c => fn(a, b, c);

        public static Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> fn) => a => b => c => d => fn(a, b, c, d);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> Curry<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> fn) => a => b => c => d => e => fn(a, b, c, d, e);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>> Curry<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> fn) => a => b => c => d => e => g => fn(a, b, c, d, e, g);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TResult>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> fn) => a => b => c => d => e => f => g => fn(a, b, c, d, e, f, g);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TResult>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> fn) {
            return a => b => c => d => e => f => g => h => fn(a, b, c, d, e, f, g, h);
        }

        #endregion Currying

        #region Partial Application

        ///// <summary>
        ///// Partially applies a function taking 1 arguments, of the form (T1) =&gt; TResult, by
        ///// binding the supplied value as the first argument and returning a new function of the
        ///// form () =&gt; TResult.
        ///// </summary>
        ///// <typeparam name="T1">
        ///// The type of the first argument of the function.
        ///// </typeparam>
        ///// <typeparam name="TResult">
        ///// The type of the result of the function.
        ///// </typeparam>
        ///// <param name="function">
        ///// The function to partially apply.
        ///// </param>
        ///// <param name="value">
        ///// The value to bind as the argument.
        ///// </param>
        ///// <returns>
        ///// A new function, of the form () =&gt; TResult, produced by binding the supplied value as
        ///// the argument.
        ///// </returns>
        //public static Func<TResult> Apply<T1, TResult>(this Func<T1, TResult> function, T1 value) {
        //    return function(value);
        //}

        /// <summary>
        /// Partially applies a function taking 2 arguments, of the form (T1, T2) =&gt; TResult, by
        /// binding the supplied value as the first argument and returning a new function of the
        /// form (T2) =&gt; TResult.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="function">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2) =&gt; TResult, produced by binding the supplied value
        /// as the first argument.
        /// </returns>
        public static Func<T2, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> function, T1 value) {
            return y => function(value, y);
        }

        /// <summary>
        /// Partially applies a function taking 3 arguments, of the form (T1, T2, T3) =&gt; TResult,
        /// by binding the supplied value as the first argument and returning a new function of the
        /// form (T2, T3) =&gt; TResult.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the function.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="function">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3) =&gt; TResult, produced by binding the supplied
        /// value as the first argument.
        /// </returns>
        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> function, T1 value) {
            return (x, y) => function(value, x, y);
        }

        /// <summary>
        /// Partially applies a function taking 4 arguments, of the form (T1, T2, T3, T4) =&gt;
        /// TResult, by binding the supplied value as the first argument and returning a new
        /// function of the form (T2, T3, T4) =&gt; TResult.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the function.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the function.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="function">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4) =&gt; TResult, produced by binding the supplied
        /// value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> function, T1 value) {
            return (x, y, z) => function(value, x, y, z);
        }

        /// <summary>
        /// Partially applies a function taking 5 arguments, of the form (T1, T2, T3, T4, T5) =&gt;
        /// TResult, by binding the supplied value as the first argument and returning a new
        /// function of the form (T2, T3, T4, T5) =&gt; TResult.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the function.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the function.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the function.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="function">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4, T5) =&gt; TResult, produced by binding the
        /// supplied value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> function, T1 value) {
            return (b, c, d, e) => function(value, b, c, d, e);
        }

        /// <summary>
        /// Partially applies a function taking 6 arguments, of the form (T1, T2, T3, T4, T5, T6)
        /// = &gt; TResult, by binding the supplied value as the first argument and returning a new
        /// function of the form (T2, T3, T4, T5, T6) =&gt; TResult.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the function.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The type of the third argument of the function.
        /// </typeparam>
        /// <typeparam name="T4">
        /// The type of the fourth argument of the function.
        /// </typeparam>
        /// <typeparam name="T5">
        /// The type of the fifth argument of the function.
        /// </typeparam>
        /// <typeparam name="T6">
        /// The type of the sixth argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="function">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4, T5, T6) =&gt; TResult, produced by binding the
        /// supplied value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> function, T1 value) {
            return (b, c, d, e, f) => function(value, b, c, d, e, f);
        }

        #endregion Partial Application

        public static T Identity<T>(T t) => t;

        public static System.Diagnostics.Stopwatch InvokeTimed(this Action action) {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            action(); return stopwatch;
        }

        public static T InvokeAndTime<T>(this Func<T> function, out System.Diagnostics.Stopwatch stopwatch) {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var value = function();
            stopwatch.Stop();
            return value;
        }
    }
}