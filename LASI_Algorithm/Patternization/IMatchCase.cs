using System;
namespace LASI.Algorithm.Patternization
{
    /// <summary>
    /// Specifies the required behavior for Pattern Matching expression component that does not yield a value.
    /// </summary>
    /// <typeparam name="T">The Type of the value being matched over.</typeparam>
    public interface IMatchCase<T>
       where T : class,ILexical
    {
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action which, if this With expression is Matched, will be invoked.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        IMatchCase<T> With<TCase>(Action action) where TCase : class,ILexical;
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="action">The Action&lt;TCase&gt; which, if this With expression is Matched, will be invoked on the value being matched over by the PatternMatching expression.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        IMatchCase<T> With<TCase>(Action<TCase> action) where TCase : class,ILexical;
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="when">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="action">The Action which, if this With expression is Matched, will be invoked.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        IMatchCase<T> With<TCase>(Func<TCase, bool> when, Action action) where TCase : class,ILexical;
        /// <summary>
        /// Appends a Match with Type expression to the current PatternMatching Expression.
        /// </summary>
        /// <typeparam name="TCase">The Type to match with. If the value being matched is of this type, this With expression will be selected and the provided action invoked.</typeparam>
        /// <param name="when">Specifies an additional condition which the value being matched must conform to in order for a Match with TCase to succeed.</param>
        /// <param name="action">The Action&lt;TCase&gt; which, if this With expression is Matched, will be invoked on the value being matched over by the PatternMatching expression.</param>
        /// <returns>The ICase&lt;T, R&gt; describing the Match expression so far.</returns>
        IMatchCase<T> With<TCase>(Func<TCase, bool> when, Action<TCase> action) where TCase : class,ILexical;
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
}
