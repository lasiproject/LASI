using System;

namespace LASI.Utilities
{
    /// <summary>
    /// Defines extension methods for the System.Func&lt;T, TResult&gt; and System.Action&lt;T&gt; family of delegate types.
    /// </summary>
    public static class FunctionExtensions
    {
        #region Compose

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
        /// <param name="f">
        /// The outer function f, of the composition to perform f(g)
        /// </param>
        /// <param name="g">
        /// The inner function g, of the composition to perform f(g)
        /// </param>
        /// <returns>
        /// a new function which when invoked is equivalent to invoking the first function on the
        /// result of invoking the second.
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
        public static Func<T2, T3> Compose<T1, T2, T3>(this Func<T1, T3> f, Func<T2, T1> g) => x => f(g(x));

        #endregion

        #region AndThen
        /// <summary>
        /// Composes two functions returning a new function which represents the application of the
        /// second function to the result of the application of the first function. In other words
        /// f.AndThen(g)(x) is equivalent to g(f(x)).
        /// </summary>
        /// <typeparam name="T2">
        /// The input type of the second function.
        /// </typeparam>
        /// <typeparam name="T1">
        /// The input type of the second function and the result type of the second function.
        /// </typeparam>
        /// <typeparam name="T3">
        /// The return type of the first function and result type of the their composition.
        /// </typeparam>
        /// <param name="f">
        /// The outer function f, of the composition to perform f(g)
        /// </param>
        /// <param name="g">
        /// The inner function g, of the composition to perform f(g)
        /// </param>
        /// <returns>
        /// a new function which when invoked is equivalent to invoking the second function on the
        /// result of invoking the first.
        /// </returns>
        public static Func<T1, T3> AndThen<T2, T1, T3>(this Func<T1, T2> f, Func<T2, T3> g) => x => g(f(x));

        /// <summary>
        /// Composes a pair of curried predicates having the form
        /// <typeparamref name="T1"/> => <typeparamref name="T2"/> => <see cref="bool"/> using short-circuiting logical and (<c>&amp;&amp;</c>).
        /// </summary>
        /// <typeparam name="T1">The type of the parameter of both <paramref name="f"/> and <paramref name="g"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the parameter of both
        /// <c><paramref name="f"/>(<typeparamref name="T1"/>)</c> and <c><paramref name="g"/>(<typeparamref name="T1"/>)</c>.
        /// </typeparam>
        /// <param name="f">A function having the form <typeparamref name="T1"/> => <typeparamref name="T2"/> => <see cref="bool"/>.</param>
        /// <param name="g">A function having the form <typeparamref name="T1"/> => <typeparamref name="T2"/> => <see cref="bool"/>.</param>
        /// <returns><c><paramref name="f"/>(<typeparamref name="T1"/>)(<typeparamref name="T2"/>) &amp;&amp; <paramref name="g"/>(<typeparamref name="T1"/>)(<typeparamref name="T2"/>)</c>.</returns>
        public static Func<T1, Func<T2, bool>> And<T1, T2>(this Func<T1, Func<T2, bool>> f, Func<T1, Func<T2, bool>> g) => CombinePredicate(f, g, x => y => x && y);

        /// <summary>
        /// Composes a pair of curried predicates having the form
        /// <typeparamref name="T1"/> => <typeparamref name="T2"/> => <see cref="bool"/> using short-circuiting logical or (<c>||</c>).
        /// </summary>
        /// <typeparam name="T1">The type of the parameter of both <paramref name="f"/> and <paramref name="g"/>.
        /// </typeparam>
        /// <typeparam name="T2">
        /// The type of the parameter of both
        /// <c><paramref name="f"/>(<typeparamref name="T1"/>)</c> and <c><paramref name="g"/>(<typeparamref name="T1"/>)</c>.
        /// </typeparam>
        /// <param name="f">A function having the form <typeparamref name="T1"/> => <typeparamref name="T2"/> => <see cref="bool"/>.</param>
        /// <param name="g">A function having the form <typeparamref name="T1"/> => <typeparamref name="T2"/> => <see cref="bool"/>.</param>
        /// <returns><c><paramref name="f"/>(<typeparamref name="T1"/>)(<typeparamref name="T2"/>) || <paramref name="g"/>(<typeparamref name="T1"/>)(<typeparamref name="T2"/>)</c>.</returns>
        public static Func<T1, Func<T2, bool>> Or<T1, T2>(this Func<T1, Func<T2, bool>> f, Func<T1, Func<T2, bool>> g) => CombinePredicate(f, g, x => y => x || y);

        static Func<T1, Func<T2, bool>> CombinePredicate<T1, T2>(
            Func<T1, Func<T2, bool>> f,
            Func<T1, Func<T2, bool>> g,
            Func<bool, Func<bool, bool>> op) =>
            x => y => op(f(x)(y))(g(x)(y));

        #region Experimental

        public static Action<T> AndThen<T>(this Action<T> a1, Action<T> a2) => x => { a1(x); a2(x); };
        public static Action<T> AndThen<T>(this Action<T> a1, Action a2) => x => { a1(x); a2(); };
        public static Action<T> AndThen<T>(this Action a2, Action<T> a1) => x => { a1(x); a2(); };
        public static Action<T1, T2, T3, T4> AndThen<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> a1, Action<T1, T2, T3, T4> a2) =>
            (a, b, c, d) => { a1(a, b, c, d); a2(a, b, c, d); };
        public static Action<T1, T2, T3, T4, T5> AndThen<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> a1, Action<T1, T2, T3, T4, T5> a2) =>
             (a, b, c, d, e) => { a1(a, b, c, d, e); a2(a, b, c, d, e); };
        public static Action AndThen(this Action a1, Action a2) => a1 + a2;

        #endregion

        #region Curry

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
        /// Curries a function of the form (T1, T2, T3, T4, T5, T6, T7, T8, T9) => TResult, yielding a function of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => (T8) => (T9) => TResult.
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
        /// <typeparam name="T9">
        /// The type of the eight argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="fn">The function to curry.</param>
        /// <returns>
        /// A new function, of the form (T1) => (T2) => (T3) => (T4) => (T5) => (T6) => (T7) => (T8) => TResult.
        /// </returns>
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, TResult>>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>
            (this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> fn) => a => b => c => d => e => f => g => h => i => fn(a, b, c, d, e, f, g, h, i);

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
            (this Action<T1, T2, T3, T4, T5, T6> fn) => a => b => c => d => e => f => fn(a, b, c, d, e, f);

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

        #endregion

        #region Lifted Negation
        public static Func<T1, bool> Negate<T1>(this Func<T1, bool> f) => x => !f(x);
        public static Func<T1, Func<T2, bool>> Negate<T1, T2>(this Func<T1, Func<T2, bool>> f) => x => f(x).Negate();
        public static Func<T1, T2, bool> Negate<T1, T2>(this Func<T1, T2, bool> f) => (x, y) => !f(x, y);
        #endregion

        #region Partial Application

        /// <summary>
        /// Partially applies a function taking 1 argument, of the form (T1) => TResult, by
        /// binding the supplied value as the first argument and returning a new function of the
        /// form () => TResult.
        /// </summary>
        /// <typeparam name="T1">
        /// The type of the first argument of the function.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the result of the function.
        /// </typeparam>
        /// <param name="f">
        /// The function to partially apply.
        /// </param>
        /// <param name="x">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form () => TResult, produced by binding the supplied value
        /// as the first argument.
        /// </returns>
        public static Func<TResult> Apply<T1, TResult>(this Func<T1, TResult> f, T1 x) => () => f(x);
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
        /// <param name="f">
        /// The function to partially apply.
        /// </param>
        /// <param name="first">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2) => TResult, produced by binding the supplied value
        /// as the first argument.
        /// </returns>
        public static Func<T2, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> f, T1 first) => y => f(first, y);
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
        /// <param name="f">
        /// The function to partially apply.
        /// </param>
        /// <param name="first">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3) => TResult, produced by binding the supplied
        /// value as the first argument.
        /// </returns>
        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> f, T1 first) => (x, y) => f(first, x, y);
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
        /// <param name="f">
        /// The function to partially apply.
        /// </param>
        /// <param name="first">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4) => TResult, produced by binding the supplied
        /// value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> f, T1 first) => (x, y, z) => f(first, x, y, z);
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
        /// <param name="f">
        /// The function to partially apply.
        /// </param>
        /// <param name="first">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4, T5) => TResult, produced by binding the
        /// supplied value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> f, T1 first) => (b, c, d, e) => f(first, b, c, d, e);
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
        /// <param name="first">
        /// The value to bind as the first argument.
        /// </param>
        /// <returns>
        /// A new function, of the form (T2, T3, T4, T5, T6) => TResult, produced by binding the
        /// supplied value as the first argument.
        /// </returns>
        public static Func<T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> fn, T1 first) => (b, c, d, e, f) => fn(first, b, c, d, e, f);

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
        /// and sets up an invocation context such that after a call to <paramref name="function"/> it will track the elapsed execution time.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the function.</typeparam>
        /// <param name="function">the function to time.</param>
        /// <param name="stopwatch">The stopwatch reference which will be bound.</param>
        /// <returns>A function which will invoke the specified function and reveal elapsed execution time by way of <paramref name="stopwatch"/>.</returns>
        public static Func<TResult> WithTimer<TResult>(this Func<TResult> function, out System.Diagnostics.Stopwatch stopwatch)
        {
            var proxy = new System.Diagnostics.Stopwatch();
            stopwatch = proxy;
            return () =>
             {
                 proxy.Start();
                 var result = function();
                 proxy.Stop();
                 return result;
             };
        }
        /// <summary>
        /// Binds a new <see cref="System.Diagnostics.Stopwatch"/>to <paramref name="stopwatch"/>
        /// and sets up an invocation context such that after a call to <paramref name="function"/> it will track the elapsed execution time.
        /// </summary>
        /// <typeparam name="T">The type of argument of the function.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by the function</typeparam>
        /// <param name="function">the function to time.</param>
        /// <param name="stopwatch">The stopwatch reference which will be bound.</param>
        /// <returns>A function which will invoke the specified function and reveal elapsed execution time by way of <paramref name="stopwatch"/>.</returns>
        public static Func<T, TResult> WithTimer<T, TResult>(this Func<T, TResult> function, out System.Diagnostics.Stopwatch stopwatch)
        {
            var proxy = new System.Diagnostics.Stopwatch();
            stopwatch = proxy;
            return x =>
            {
                proxy.Start();
                var result = function(x);
                proxy.Stop();
                return result;
            };
        }

        /// <summary>
        /// Returns a function wrapping the specified function with a timer.
        /// </summary>
        /// <param name="action">The function to time.</param>
        /// <param name="stopwatch">The <see cref="System.Diagnostics.Stopwatch"/> which will be used to track execution time.</param>
        /// <returns>The specified function, wrapped with a timer.</returns>
        public static Action WithTimer(this Action action, out System.Diagnostics.Stopwatch stopwatch)
        {
            var proxy = new System.Diagnostics.Stopwatch();
            stopwatch = proxy;
            return () =>
            {
                proxy.Start();
                action();
                proxy.Stop();
            };
        }
    }
    #endregion
}
