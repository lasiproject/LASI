using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI;
using LASI.Core.DocumentStructures;
using LASI.Utilities.Contracts.Validators;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of various document level constructs.
    /// </summary>
    /// <see cref="DocumentStructures.Document"/>
    /// <seealso cref="DocumentStructures.Sentence"/>
    /// <seealso cref="DocumentStructures.Paragraph"/>
    public static partial class LexicalEnumerable
    {
        #region Sequential Implementations

        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Phrase> OfPhrase(this IEnumerable<Paragraph> paragraphs) {
            ArgumentValidator.ThrowIfNull(paragraphs, "paragraphs");
            return paragraphs.SelectMany(p => p.Phrases);
        }


        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Word> OfWord(this IEnumerable<Paragraph> paragraphs) {
            ArgumentValidator.ThrowIfNull(paragraphs, "paragraphs");
            return paragraphs.SelectMany(p => p.Words);

        }
        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Sentence instances.</returns>
        public static IEnumerable<Phrase> OfPhrase(this IEnumerable<Sentence> sentences) {
            ArgumentValidator.ThrowIfNull(sentences, "sentences");
            return sentences.SelectMany(s => s.Phrases);
        }
        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static IEnumerable<Word> OfWord(this IEnumerable<Sentence> sentences) {
            ArgumentValidator.ThrowIfNull(sentences, "sentences");
            return sentences.SelectMany(s => s.Words);
        }

        #endregion

        #region Parallel Implementations

        /// <summary>
        /// Gets the parallel aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The parallel aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static ParallelQuery<Phrase> OfPhrase(this ParallelQuery<Paragraph> paragraphs) {
            ArgumentValidator.ThrowIfNull(paragraphs, "paragraphs");
            return paragraphs.SelectMany(p => p.Phrases);

        }
        /// <summary>
        /// Gets the parallel aggregation of all Word instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The parallel aggregation of all Word instances contained within the sequence of Paragraph instances.</returns>
        public static ParallelQuery<Word> OfWord(this ParallelQuery<Paragraph> paragraphs) {
            ArgumentValidator.ThrowIfNull(paragraphs, "paragraphs");
            return paragraphs.SelectMany(p => p.Words);

        }
        /// <summary>
        /// Gets the parallel aggregation of all Phrase instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The parallel aggregation of all Phrase instances contained within the sequence of Sentence instances.</returns>
        public static ParallelQuery<Phrase> OfPhrase(this ParallelQuery<Sentence> sentences) {
            ArgumentValidator.ThrowIfNull(sentences, "sentences");
            return sentences.SelectMany(s => s.Phrases);

        }
        /// <summary>
        /// Gets the parallel aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The parallel aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static ParallelQuery<Word> OfWord(this ParallelQuery<Sentence> sentences) {
            ArgumentValidator.ThrowIfNull(sentences, "sentences");
            return sentences.SelectMany(s => s.Words);

        }

        #endregion


    }
}
