using System;
namespace LASI.Algorithm.Patternization
{
    public interface IPredicatedPatternMatching<T, R>
      where T : class, LASI.Algorithm.ILexical
    {
        IPatternMatching<T, R> Then<TCase>(R caseResult) where TCase : class, T;
        IPatternMatching<T, R> Then<TCase>(Func<R> func) where TCase : class, T;
        IPatternMatching<T, R> Then<TCase>(Func<TCase, R> func) where TCase : class, T; /// <summary>
        /// Returns the result of the Pattern Matching expression. 
        /// The result will be one of 3 possibilities: 
        /// 1. The result specified for the first Match with Type expression which succeeded. 
        /// 2. If no matches succeeded, and a Default expression was provided, the result specified in the Default expression.
        /// 3. If no matches succeeded, and a Default expression was not provided, the default value for type the Result Type of the Match Expression.
        /// </summary>
        /// <returns>Returns the result of the Pattern Matching expression.</returns>
        R Result();
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="defaultValue">The desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        R Result(R defaultValue);
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Default should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        R Result(Func<R> func);
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The MatchCase&lt;T, R&gt; describing the Match expression so far.</returns>
        /// <remarks>Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between With clauses.</remarks>
        R Result(Func<T, R> func);
       
    }
    public interface IPredicatedPatternMatching<T>
    where T : class, LASI.Algorithm.ILexical
    {
        IPatternMatching<T> Then<TCase>(Action actn) where TCase : class, T;
        IPatternMatching<T> Then<TCase>(Action<TCase> actn) where TCase : class, T;
    }
}
