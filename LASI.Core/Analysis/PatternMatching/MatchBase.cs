using System.Collections.Generic;
using System.Diagnostics;

namespace LASI.Core.Heuristics.PatternMatching
{
    /// <summary>
    /// Represents a type for the representation and free form structuring of Type based Pattern Matching expressions
    /// which match over a value of Type T and does not yield a result.
    /// </summary>
    /// <typeparam name="T">
    /// The Type of the value which the Pattern Matching expression will match with.
    /// </typeparam>
    /// <summary>
    /// Provides for the construction of flexible Typed Pattern Matching expressions.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class MatchBase<T> where T : ILexical
    {
        /// <summary>
        /// Initializes a new instance of the MatchBase&lt;T&gt; class which will match against the given value.
        /// </summary>
        /// <param name="value">The value to match against.</param>
        protected MatchBase(T value)
        {
            // TODO: replace with pattern when https://github.com/dotnet/csharplang/issues/766 is resolved.
            Unmatchable = EqualityComparer<T>.Default.Equals(value, default);
            Value = value;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the value indicating if a match was found or if the default value will be yielded by the Result method.
        /// </summary>
        protected bool Matched { get; set; }

        /// <summary>
        /// Gets or sets the value being matched against.
        /// </summary>
        protected T Value { get; }

        /// <summary>
        /// `true` if value being matched cannot be matched by any pattern;
        /// </summary>
        protected bool Unmatchable { get; }
        #endregion
    }
}
