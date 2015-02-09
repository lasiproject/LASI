﻿using System;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines extension methods for the System.Func&lt;T, TResult&gt; and System.Action&lt;T&gt; family of delegate types.
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
        /// Func&lt;double, double&gt;&gt; cube = x => Math.Pow(x, 3);
        /// Func&lt;double, double&gt;&gt; sqrt = x => Math.Sqrt(x);
        /// var sqrtOfCube = sqrt.Compose(cube);
        /// var cubeOfSqrt = cube.Compose(sqrt);
        /// var v1 = sqrtOfCube(5); // about 11.18
        /// var v2 = cubeOfSqrt(5); // about 11.18
        /// </code>
        /// </example>
        /// <remarks>
        /// </remarks>
        public static Func<T2, T3> Compose<T1, T2, T3>(this Func<T1, T3> first, Func<T2, T1> second) => x => first(second(x));

        public static Func<T1, T2> Compose<T1, T2>(this Func<T1, T2> first, Func<T1, T1> second) => x => first(second(x));

        public static Func<T1, T1> Compose<T1, T2>(this Func<T2, T1> first, Func<T1, T2> second) => x => first(second(x));

        public static Func<T2, T1> Compose<T1, T2>(this Func<T1, T1> first, Func<T2, T1> second) => x => first(second(x));


        #region Currying

        /// <summary>
        /// Curries a function of the form (T1, T2) => TResult, yielding a function of the form (T1) => (T2) => TResult.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function, of the form (T1) => (T2) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(this Func<T1, T2, TResult> fn) => a => b => fn(a, b);

        /// <summary>
        /// Curries a function of the form (T1, T2, T3) => TResult, yielding a function of the form (T1) => (T2) => (T3) => TResult.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function, of the form (T1) => (T2) => (T3) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>
            (this Func<T1, T2, T3, TResult> fn) => a => b => c => fn(a, b, c);

        /// <summary>
        /// Curries a function of the form (T1, T2, T3, T4) => TResult, yielding a function of the form (T1) => (T2) => (T3) => (T4) => TResult.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function, of the form (T1) => (T2) => (T3) => (T4) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T1, T2, T3, T4, TResult>
            (this Func<T1, T2, T3, T4, TResult> fn) => a => b => c => d => fn(a, b, c, d);

        /// <summary>
        /// Curries a function of the form (T1, T2, T3, T4, T5) => TResult, yielding a function of the form (T1) => (T2) => (T3) => (T4) => (T5) => TResult.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function, of the form (T1) => (T2) => (T3) => (T4) => (T5) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> Curry<T1, T2, T3, T4, T5, TResult>
            (this Func<T1, T2, T3, T4, T5, TResult> fn) => a => b => c => d => e => fn(a, b, c, d, e);

        /// <summary>
        /// Curries a function of the form (T1, T2, T3, T4, T5, T6) => TResult, yielding a function of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => TResult.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function, of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>> Curry<T1, T2, T3, T4, T5, T6, TResult>
            (this Func<T1, T2, T3, T4, T5, T6, TResult> fn) => a => b => c => d => e => g => fn(a, b, c, d, e, g);

        /// <summary>
        /// Curries a function of the form (T1, T2, T3, T4, T5, T6, T7) => TResult, yielding a function of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => TResult.
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
        /// <typeparam name="T7">
        /// The type of the seventh argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function, of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TResult>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, TResult>
            (this Func<T1, T2, T3, T4, T5, T6, T7, TResult> fn) => a => b => c => d => e => f => g => fn(a, b, c, d, e, f, g);

        /// <summary>
        /// Curries a function of the form (T1, T2, T3, T4, T5, T6, T7, T8) => TResult, yielding a function of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => (T8) => TResult.
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
        /// <typeparam name="T7">
        /// The type of the seventh argument of the function.
        /// </typeparam>
        /// <typeparam name="T8">
        /// The type of the eight argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function, of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => (T8) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TResult>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, TResult>
            (this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> fn) => a => b => c => d => e => f => g => h => fn(a, b, c, d, e, f, g, h);

        /// <summary>
        /// Curries a function of the form (T1, T2) => void, yielding an action of the form (T1) => (T2) => void.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the function.
        /// </typeparam>
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new action of the form (T1) => (T2) => void.
        /// </returns>
        public static Func<T1, Action<T2>> Curry<T1, T2>(this Action<T1, T2> fn) => a => b => fn(a, b);

        /// <summary>
        /// Curries an action of the form (T1, T2, T3) => void, yielding an action of the form (T1) => (T2) => (T3) => void.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new action of the form (T1) => (T2) => (T3) => void.
        /// </returns>
        public static Func<T1, Func<T2, Action<T3>>> Curry<T1, T2, T3>
            (this Action<T1, T2, T3> fn) => a => b => c => fn(a, b, c);

        /// <summary>
        /// Curries an action of the form (T1, T2, T3, T4) => void, yielding a function of the form (T1) => (T2) => (T3) => (T4) => TResult.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function of the form (T1) => (T2) => (T3) => (T4) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Action<T4>>>> Curry<T1, T2, T3, T4>
            (this Action<T1, T2, T3, T4> fn) => a => b => c => d => fn(a, b, c, d);

        /// <summary>
        /// Curries a function of the form (T1, T2, T3, T4, T5) => TResult, yielding a function of the form (T1) => (T2) => (T3) => (T4) => (T5) => TResult.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function of the form (T1) => (T2) => (T3) => (T4) => (T5) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> Curry<T1, T2, T3, T4, T5>
            (this Action<T1, T2, T3, T4, T5> fn) => a => b => c => d => e => fn(a, b, c, d, e);

        /// <summary>
        /// Curries an action of the form (T1, T2, T3, T4, T5, T6) => void, yielding an action of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => void.
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
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new action of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => void.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> Curry<T1, T2, T3, T4, T5, T6>
            (this Action<T1, T2, T3, T4, T5, T6> fn) => a => b => c => d => e => g => fn(a, b, c, d, e, g);

        /// <summary>
        /// Curries an action of the form (T1, T2, T3, T4, T5, T6, T7) => TResult, yielding an action of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => void.
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
        /// <typeparam name="T7">
        /// The type of the seventh argument of the function.
        /// </typeparam>
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new action of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => void.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7>
            (this Action<T1, T2, T3, T4, T5, T6, T7> fn) => a => b => c => d => e => f => g => fn(a, b, c, d, e, f, g);

        /// <summary>
        /// Curries an action of the form (T1, T2, T3, T4, T5, T6, T7, T8) => void, yielding an action of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => (T8) => void.
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
        /// <typeparam name="T7">
        /// The type of the seventh argument of the function.
        /// </typeparam>
        /// <typeparam name="T8">
        /// The type of the eight argument of the function.
        /// </typeparam>
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new action of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => (T8) => void.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8>
            (this Action<T1, T2, T3, T4, T5, T6, T7, T8> fn) => a => b => c => d => e => f => g => h => fn(a, b, c, d, e, f, g, h);

        #endregion Action Currying

        #region Partial Application

        /// <summary>
        /// Partially applies a function taking 2 arguments, of the form (T1, T2) => TResult, by
        /// binding the supplied value as the first argument and returning a new function of the
        /// form (T2) => TResult.
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
        /// <param name="fn">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2) => TResult, produced by binding the supplied value
        /// as the first argument.
        /// </returns>
        public static Func<T2, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> fn, T1 value) => y => fn(value, y);

        /// <summary>
        /// Partially applies a function taking 2 arguments, of the form (T1, T2) => TResult, by
        /// binding the supplied value as the second argument and returning a new function of the
        /// form (T1) => TResult.
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
        /// <param name="fn">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the second argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T1) => TResult, produced by binding the supplied value
        /// as the first argument.
        /// </returns>
        public static Func<T1, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> fn, T2 value) => x => fn(x, value);

        /// <summary>
        /// Partially applies a function taking 3 arguments, of the form (T1, T2, T3) => TResult,
        /// by binding the supplied value as the first argument and returning a new function of the
        /// form (T2, T3) => TResult.
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
        /// <param name="fn">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3) => TResult, produced by binding the supplied
        /// value as the first argument.
        /// </returns>
        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> fn, T1 value)
        {
            return (x, y) => fn(value, x, y);
        }

        /// <summary>
        /// Partially applies a function taking 3 arguments, of the form (T1, T2, T3) => TResult,
        /// by binding the supplied value as the second argument and returning a new function of the
        /// form (T1, T3) => TResult.
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
        /// <param name="fn">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the second argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T1, T3) => TResult, produced by binding the supplied
        /// value as the second argument.
        /// </returns>
        public static Func<T1, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> fn, T2 value) => (x, z) => fn(x, value, z);

        /// <summary>
        /// Partially applies a function taking 3 arguments, of the form (T1, T2, T3) => TResult,
        /// by binding the supplied value as the third argument and returning a new function of the
        /// form (T1, T2) => TResult.
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
        /// <param name="fn">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the third argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T1, T2) => TResult, produced by binding the supplied
        /// value as the third argument.
        /// </returns>
        public static Func<T1, T2, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> fn, T3 value) => (x, y) => fn(x, y, value);

        /// <summary>
        /// Partially applies a function taking 4 arguments, of the form (T1, T2, T3, T4) =>
        /// TResult, by binding the supplied value as the first argument and returning a new
        /// function of the form (T2, T3, T4) => TResult.
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
        /// <param name="fn">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4) => TResult, produced by binding the supplied
        /// value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> fn, T1 value)
        {
            return (x, y, z) => fn(value, x, y, z);
        }

        /// <summary>
        /// Partially applies a function taking 5 arguments, of the form (T1, T2, T3, T4, T5) =>
        /// TResult, by binding the supplied value as the first argument and returning a new
        /// function of the form (T2, T3, T4, T5) => TResult.
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
        /// <param name="fn">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4, T5) => TResult, produced by binding the
        /// supplied value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> fn, T1 value)
        {
            return (b, c, d, e) => fn(value, b, c, d, e);
        }

        /// <summary>
        /// Partially applies a function taking 6 arguments, of the form (T1, T2, T3, T4, T5, T6)
        /// = &gt; TResult, by binding the supplied value as the first argument and returning a new
        /// function of the form (T2, T3, T4, T5, T6) => TResult.
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
        /// <param name="fn">
        /// The function to partially apply.
        /// </param>
        /// <param name="value">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4, T5, T6) => TResult, produced by binding the
        /// supplied value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> fn, T1 value)
        {
            return (b, c, d, e, f) => fn(value, b, c, d, e, f);
        }
        /// <summary>
        /// Partially applies an action taking 2 arguments of the form (T1, T2) => void, by
        /// binding the supplied value as the first argument and returning a new action of the
        /// form (T2) => void.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the function.
        /// </typeparam>
        /// <param name="a">The action to partially apply.</param>
        /// <param name="value">The value to bind as the first argument.</param>
        /// <returns>
        /// A new action of the form (T2) => void, produced by binding the supplied value as the first argument.
        /// </returns>
        public static Action<T2> Apply<T1, T2>(this Action<T1, T2> a, T1 value) => y => a(value, y);
        /// <summary>
        /// Partially applies an action taking 2 arguments of the form (T1, T2) => void, by
        /// binding the supplied value as the second argument and returning a new action of the
        /// form (T1) => void.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the second argument of the function.
        /// </typeparam>
        /// <param name="a">The action to partially apply.</param>
        /// <param name="value">The value to bind as the second argument.</param>
        /// <returns>
        /// A new action of the form (T1) => void, produced by binding the supplied value as the second argument.
        /// </returns>
        public static Action<T1> Apply<T1, T2>(this Action<T1, T2> a, T2 value) => y => a(y, value);
        /// <summary>
        /// Partially applies an action taking 3 arguments of the form (T1, T2, T3) => void, by
        /// binding the supplied value as the first argument and returning a new action of the
        /// form (T2, T3) => void.
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
        /// <param name="a">The action to partially apply.</param>
        /// <param name="value">The value to bind as the first argument.</param>
        /// <returns>
        /// A new action of the form (T2, T3) => void, produced by binding the supplied value as the first argument.
        /// </returns>
        public static Action<T2, T3> Apply<T1, T2, T3>(this Action<T1, T2, T3> a, T1 value) => (y, z) => a(value, y, z);
        /// <summary>
        /// Partially applies an action taking 3 arguments of the form (T1, T2, T3) => void, by
        /// binding the supplied value as the second argument and returning a new action of the
        /// form (T1, T3) => void.
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
        /// <param name="a">The action to partially apply.</param>
        /// <param name="value">The value to bind as the second argument.</param>
        /// <returns>
        /// A new action of the form (T1, T3) => void, produced by binding the supplied value as the second argument.
        /// </returns>
        public static Action<T1, T3> Apply<T1, T2, T3>(this Action<T1, T2, T3> a, T2 value) => (x, z) => a(x, value, z);
        /// <summary>
        /// Partially applies an action taking 3 arguments of the form (T1, T2, T3) => void, by
        /// binding the supplied value as the third argument and returning a new action of the
        /// form (T1, T2) => void.
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
        /// <param name="a">The action to partially apply.</param>
        /// <param name="value">The value to bind as the third argument.</param>
        /// <returns>
        /// A new action of the form (T1, T2) => void, produced by binding the supplied value as the third argument.
        /// </returns>
        public static Action<T1, T2> Apply<T1, T2, T3>(this Action<T1, T2, T3> a, T3 value) => (x, y) => a(x, y, value);

        #endregion Partial Application


        /// <summary>
        /// Performs an identity projection on the value, returning it.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to project.</param>
        /// <returns>The value.</returns>
        public static T Identity<T>(T value) => value;

        /// <summary>
        /// Binds a new <see cref="System.Diagnostics.Stopwatch"/>to <paramref name="stopwatch"/> 
        /// and sets up an invokation context such that after a call to <paramref name="function"/> it will track the elapsed execution time. 
        /// </summary>
        /// <typeparam name="T">The type of the value returned by the function.</typeparam>
        /// <param name="function">the function to time.</param>
        /// <param name="stopwatch">The stopwatch reference which will be bound.</param>
        /// <returns>A function which will invoke the specified function and reveal elasped execution time by way of <paramref name="stopwatch"/>.</returns>
        public static Func<T> WithTimer<T>(this Func<T> function, out System.Diagnostics.Stopwatch stopwatch)
        {
            var proxy = new[] { new System.Diagnostics.Stopwatch() };
            stopwatch = proxy[0];
            return () =>
             {
                 proxy[0].Start();
                 var value = function();
                 proxy[0].Stop();
                 return value;
             };
        }
        /// <summary>
        /// Binds a new <see cref="System.Diagnostics.Stopwatch"/>to <paramref name="stopwatch"/> 
        /// and sets up an invokation context such that after a call to <paramref name="function"/> it will track the elapsed execution time. 
        /// </summary>
        /// <typeparam name="T">The type of argument of the function.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the function</typeparam>
        /// <param name="function">the function to time.</param>
        /// <param name="stopwatch">The stopwatch reference which will be bound.</param>
        /// <returns>A function which will invoke the specified function and reveal elasped execution time by way of <paramref name="stopwatch"/>.</returns>
        public static Func<T, TResult> WithTimer<T, TResult>(this Func<T, TResult> function, out System.Diagnostics.Stopwatch stopwatch)
        {
            var proxy = new[] { new System.Diagnostics.Stopwatch() };
            stopwatch = proxy[0];
            return x =>
            {
                proxy[0].Start();
                return function(x);
            };
        }
        private static void InitializeTimer(out System.Diagnostics.Stopwatch stopwatch) => stopwatch = System.Diagnostics.Stopwatch.StartNew();
    }
}