using System.Collections.Generic;
namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    using Logger = Utilities.Logger;
    /// <summary>
    /// Provides extension methods for acquiring a <see cref="SequenceMatch"/> object to perform a operation.
    /// </summary>
    public static class MatchExtensions
    {
        /// <summary>
        /// Begins a pattern match expression over the <see cref="Sentence"/>.
        /// </summary>
        /// <param name="sentence">The sentence to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing the match expression.</returns>
        public static SequenceMatch Match(this Sentence sentence) => new SequenceMatch(sentence).AddLogger(Logger.Log);

        /// <summary>
        /// Begins a pattern match expression over the sequence of <see cref="ILexical"/>s.
        /// </summary>
        /// <param name="lexicalSequence">The sequence of to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing the match expression.</returns>
        public static SequenceMatch Match(this IEnumerable<ILexical> lexicalSequence) => new SequenceMatch(lexicalSequence).AddLogger(Logger.Log);
        /// <summary>
        /// Begins a pattern match expression over the sequence of <see cref="Word"/>s in the <see cref="Phrase"/>.
        /// </summary>
        /// <param name="phrase">The phrase to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing the match expression.</returns>
        public static SequenceMatch MatchSequence(this Phrase phrase) => phrase.Words.Match();
        /// <summary>
        /// Begins a pattern match expression over the sequence of <see cref="Phrase"/>s in the <see cref="Clause"/>.
        /// </summary>
        /// <param name="clause">The clause to match against.</param>
        /// <returns>A <see cref="SequenceMatch"/> instance representing the match expression.</returns>
        public static SequenceMatch MatchSequence(this Clause clause) => clause.Phrases.Match();
    }
}