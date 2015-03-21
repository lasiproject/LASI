using System.Collections.Generic;

namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    /// <summary>
    /// Provides extension methods for acquiring a <see cref="SequenceMatch"/> object to perform a operation.
    /// </summary>
    public static class MatchExtensions
    {
        /// <summary>
        /// Begins a pattern match expression over the <see cref="Sentence"/>.
        /// </summary>
        /// <param name="sentence">The sentence to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing defining the match expression.</returns>
        public static SequenceMatch Match(this Sentence sentence) =>
            new SequenceMatch(sentence).AddLoggingBehavior(message => Utilities.Logger.Log(message));

        /// <summary>
        /// Begins a pattern match expression over the sequence of <see cref="ILexical"/>s.
        /// </summary>
        /// <param name="lexicalSequence">The sequence of to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing defining the match expression.</returns>
        public static SequenceMatch Match(this IEnumerable<ILexical> lexicalSequence) =>
            new SequenceMatch(lexicalSequence).AddLoggingBehavior(message => Utilities.Logger.Log(message));
    }
}