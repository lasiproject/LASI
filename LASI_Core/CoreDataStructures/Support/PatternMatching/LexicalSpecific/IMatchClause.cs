using System;
namespace LASI.Core.Patternization
{
    /// <summary>
    /// Specifies the required behavior for Pattern Matching expression component that does not yield a value.
    /// </summary>
    /// <typeparam name="T">The Type of the value being matched over.</typeparam>
    public interface IMatchClause<T>
       where T : class,ILexical
    {
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this Case expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action which, if this Case expression is Matched, will be invoked.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T> With<TPattern>(Action action) where TPattern : class, T;
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this Case expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action&lt;TPattern&gt; which, if this Case expression is Matched, will be invoked on the value being matched over by the PatternMatching expression.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T> With<TPattern>(Action<TPattern> action) where TPattern : class, T;
        /// <summary>
        /// Appends a When expression to the current PatternMatching Expression. The When expression applies a predicate to the value being matched over. 
        /// It must be followed by a Then expression which is only considered if the predicate applied here returns true.
        /// </summary>
        /// <param name="predicate">The predicate to test the value being matched over.</param>
        /// <returns>The IPredicatedMatchClause&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        IPredicatedMatchClause<T> When(Func<T, bool> predicate);
        /// <summary>
        /// Appends a When expression to the current PatternMatching Expression. The When expression applies a predicate to the value being matched over. 
        /// It must be followed by a Then expression which is only considered if the predicate applied here returns true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. That the value being matched is of this type is also necessary for the following then expression to be selected.</typeparam>
        /// <param name="predicate">The predicate to test the value being matched over.</param>
        /// <returns>The IPredicatedMatchClause&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        IPredicatedMatchClause<T> When<TPattern>(Func<TPattern, bool> predicate) where TPattern : class,ILexical;
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This tests a boolean condition that the subsequent Then expression will only be chosen if the condition is true.
        /// </summary>
        /// <param name="condition">The condition determining if the subsequent Then expression will only be chosen.</param>
        /// <returns>The IPredicatedMatchClause&lt;T&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        IPredicatedMatchClause<T> When(bool condition);
        /// <summary>
        /// Promotes the current non result returning expression of type Case&lt;T&gt; into a result returning expression of Case&lt;T, R&gt;
        /// Such that subsequent Case expressions appended are now to yield a result value of the supplied Type R.
        /// </summary>
        /// <typeparam name="TResult">The Type of the result which the match expression may now return.</typeparam>
        /// <returns>A IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns> 
        IMatchClause<T, TResult> Yield<TResult>();

        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="action">The function to invoke if no matches in the expression succeeded.</param>
        void Default(Action action);
        /// <summary>
        /// Appends the Default expression to the current Pattern Matching expression.
        /// </summary>
        /// <param name="action">The function to invoke on the match with value if no matches in the expression succeeded.</param>
        void Default(Action<T> action);
    }
    /// <summary>
    /// Specifies the required behavior for Pattern Matching expression component that does not yield a value.
    /// </summary>
    /// <typeparam name="T">The Type of the value being matched over.</typeparam>
    /// <typeparam name="TResult">The Type of Results the matching expression will yield.</typeparam>
    public interface IMatchClause<T, TResult>
    where T : class, LASI.Core.ILexical
    {
        /// <summary>
        /// Returns the result of the Pattern Matching expression. 
        /// The result will be one of 3 possibilities: 
        /// 1. The result specified for the first Match with Type expression which succeeded. 
        /// 2. If no matches succeeded, and a Default expression was provided, the result specified in the Default expression.
        /// 3. If no matches succeeded, and a Default expression was not provided, the default value for type the Result Type of the Match Expression.
        /// </summary>
        /// <returns>Returns the result of the Pattern Matching expression.</returns>
        TResult Result();
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between Case clauses.
        /// </summary>
        /// <param name="defaultValue">The desired default value.</param>
        /// <returns>The result corresponding to the first matched Case expression or the supplied default value if no Cases were matched.</returns> 
        TResult Result(TResult defaultValue);
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between Case clauses.
        /// </summary>
        /// <param name="valueSelector">The factory function returning a desired default value.</param>
        /// <returns>The result corresponding to the first matched Case expression or the result of invoking the supplied factory function if no Cases were matched.</returns> 
        TResult Result(Func<TResult> valueSelector);
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// Although not enforced by the compiler, Result should only be used as the last clause in the match expression, never in between Case clauses.
        /// </summary>
        /// <param name="valueSelector">The factory function returning a desired default value.</param>
        /// <returns>The result corresponding to the first matched Case expression or the result of invoking the supplied function on the value being matched if no Cases were matched.</returns> 
        TResult Result(Func<T, TResult> valueSelector);
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched suched that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <param name="predicate">The predicate to test the value being matched.</param>
        /// <returns>The IPredicatedMatchClause&lt;T, TResult&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        IPredicatedMatchClause<T, TResult> When(Func<T, bool> predicate);
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This tests a boolean condition that the subsequent Then expression will only be chosen if the condition is true.
        /// </summary>
        /// <param name="condition">The condition determining if the subsequent Then expression will only be chosen.</param>
        /// <returns>The IPredicatedMatchClause&lt;T, TResult&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        IPredicatedMatchClause<T, TResult> When(bool condition);
        /// <summary>
        /// Appends a When expression to the current pattern. 
        /// This applies a predicate to the value being matched suched that the subsequent Then expression will only be chosen if the predicate returns true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. That the value being matched is of this type is also necessary for the following then expression to be selected.</typeparam>
        /// <param name="predicate">The predicate to test the value being matched.</param>
        /// <returns>The IPredicatedMatchClause&lt;T, TResult&gt; describing the Match expression so far. This must be followed by a single Then expression.</returns>
        IPredicatedMatchClause<T, TResult> When<TPattern>(Func<TPattern, bool> predicate) where TPattern : class, T;
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this Case expression will be selected and executed.</typeparam>
        /// <param name="result">The value which, if this Case expression is Matched, will be the result of the Pattern Match.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> With<TPattern>(TResult result) where TPattern : class, T;
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this Case expression will be selected and executed.</typeparam>
        /// <param name="valueSelector">The function which, if this Case expression is Matched, will be invoked to produce the corresponding desired result for a Match with TPattern.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> With<TPattern>(Func<TResult> valueSelector) where TPattern : class, T;
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. If the value being matched is of this type, this Case expression will be selected and executed.</typeparam>
        /// <param name="valueSelector">The function which, if this Case expression is Matched, will be invoked on the value being matched with to produce the desired result for a Match with TPattern.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> With<TPattern>(Func<TPattern, TResult> valueSelector) where TPattern : class, T;
    }/// <summary>
    /// Specifies the required behavior for a pattern matching expression clause which immediately follows a When expression.
    /// </summary>
    /// <typeparam name="T">The Type of the value being matched over.</typeparam>
    public interface IPredicatedMatchClause<T>
    where T : class, LASI.Core.ILexical
    {
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. 
        /// If the value being matched is of this type and the immediately preceding When expression evaluated to true, 
        /// this Then expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">Action to invoke if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T&gt; describing the Match expression so far.</returns>
        IMatchClause<T> Then<TPattern>(Action action) where TPattern : class, T;
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. 
        /// If the value being matched is of this type and the immediately preceding When expression evaluated to true, 
        /// this Then expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">Action taking to invoke on the matched value if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T&gt; describing the Match expression so far.</returns>
        IMatchClause<T> Then<TPattern>(Action<TPattern> action) where TPattern : class, T;
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary> 
        /// <param name="action">The Action to invoke if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T R&gt; describing the Match expression so far.</returns>
        IMatchClause<T> Then(Action action);
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary> 
        /// <param name="action">The Action&lt;T R&gt; to invoke if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T R&gt; describing the Match expression so far.</returns>
        IMatchClause<T> Then(Action<T> action);

    }
    /// <summary>
    /// Specifies the required behavior for a pattern matching expression clause which immediately follows a When expression.
    /// </summary>
    /// <typeparam name="T">The Type of the value being matched over.</typeparam>
    /// <typeparam name="TResult">The Type of the result which the match expression may return.</typeparam>
    public interface IPredicatedMatchClause<T, TResult>
      where T : class, LASI.Core.ILexical
    {
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. 
        /// If the value being matched is of this type and the immediately preceding When expression evaluated to true, 
        /// this Then expression will be selected and the provided action invoked.</typeparam>
        /// <param name="resultValue">The result value to select if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> Then<TPattern>(TResult resultValue) where TPattern : class, T;
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary> 
        /// <param name="resultValue">The value to select if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> Then(TResult resultValue);
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. 
        /// If the value being matched is of this type and the immediately preceding When expression evaluated to true, 
        /// this Then expression will be selected and the provided action invoked.</typeparam>
        /// <param name="func">The function returning the value to select if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> Then<TPattern>(Func<TResult> func) where TPattern : class, T;
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary>
        /// <typeparam name="TPattern">The Type to match with. 
        /// If the value being matched is of this type and the immediately preceding When expression evaluated to true, 
        /// this Then expression will be selected and the provided action invoked.</typeparam>
        /// <param name="func">The function from TPattern -> R which will be invoked to generate the value if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> Then<TPattern>(Func<TPattern, TResult> func) where TPattern : class, T;
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary>
        /// <param name="func">The function from () -> R which will be invoked to generate the value if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> Then(Func<TResult> func);
        /// <summary>
        /// Appends a Then expression to the current pattern. Then expressions work exactly like Case expressions but are only matched if they immediately follow a When expression which evaluates to true.
        /// </summary>
        /// <param name="func">The function from T -> R which will be invoked to generate the value if this Then expression is matched.</param>
        /// <returns>The IMatchClause&lt;T, TResult&gt; describing the Match expression so far.</returns>
        IMatchClause<T, TResult> Then(Func<T, TResult> func);


        /// <summary>
        /// Returns the result of the Pattern Matching expression. 
        /// The result will be one of 3 possibilities: 
        /// 1. The result specified for the first Match with Type expression which succeeded. 
        /// 2. If no matches succeeded, and a Default expression was provided, the result specified in the Default expression.
        /// 3. If no matches succeeded, and a Default expression was not provided, the default value for type the Result Type of the Match Expression.
        /// </summary>
        /// <returns>The result of the Match expression.</returns> 

        TResult Result();
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// </summary>
        /// <param name="defaultValue">The desired default value.</param>
        /// <returns>The result of the Match expression.</returns> 
        TResult Result(TResult defaultValue);
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The result of the Match expression.</returns> 
        TResult Result(Func<TResult> func);
        /// <summary>
        /// Appends a Result Expression to the current pattern, thus specifying the default result to yield when no other patterns have been matched.
        /// </summary>
        /// <param name="func">The factory function returning a desired default value.</param>
        /// <returns>The result of the Match expression.</returns> 
        TResult Result(Func<T, TResult> func);

    }
}
