using System.Diagnostics;

namespace LASI.Core.Heuristics.PatternMatching
{
    /// <summary>
    /// Provides for the representation and free-form structuring of a non result yielding Match
    /// expression which is predicated by an arbitrary condition.
    /// </summary>
    /// <typeparam name="T">
    /// The Type of the value which the Pattern Matching expression will match with.
    /// </typeparam>
    [DebuggerStepThrough]
    public abstract class PredicatedMatchBase<T> where T : ILexical
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PredicatedMatchBase{T}"/> class.
        /// </summary>
        /// <param name="predicateSucceeded">
        /// A value indicating whether or not the preceding When clause succeeded.
        /// </param>
        protected PredicatedMatchBase(bool predicateSucceeded) => Accepted = predicateSucceeded;

        /// <summary>
        /// Gets a value indicating if condition upon which the match is predicated has was satisfied.
        /// </summary>
        /// <returns>
        /// <c>true</c> if condition upon which the match is predicated has was satisfied; false otherwise.
        /// </returns>
        protected bool Accepted { get; }
    }
}
