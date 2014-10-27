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
    using Validator = ArgumentValidator;

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
        /// Gets the linear aggregation of all Paragraph instances contained within the sequence of Document instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Document instances.</param>
        /// <returns>The linear aggregation of all Paragraph instances contained within the sequence of Document instances.</returns>
        public static IEnumerable<Paragraph> AllParagraphs(this IEnumerable<Document> documents) {
            return documents.SelectMany(d => d.Paragraphs);
        }

        /// <summary>
        /// Gets the linear aggregation of all Paragraph instances contained within the sequence of Document.Page instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Document.Page instances.</param>
        /// <returns>The linear aggregation of all Paragraph instances contained within the sequence of Document.Page instances.</returns>
        public static IEnumerable<Paragraph> AllParagraphs(this IEnumerable<Document.Page> documents) {
            return documents.SelectMany(p => p.Paragraphs);
        }

        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Document instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Document instances.</returns>
        public static IEnumerable<Sentence> AllSentences(this IEnumerable<Document> documents) {
            return documents.SelectMany(d => d.Paragraphs.AllSentences());
        }

        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Sentence> AllSentences(this IEnumerable<Paragraph> paragraphs) {
            return paragraphs.SelectMany(p => p.Sentences);
        }

        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Document.Page instances.
        /// </summary>
        /// <param name="documentPages">A sequence of Document.Page instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Document.Page instances.</returns>
        public static IEnumerable<Sentence> AllSentences(this IEnumerable<Document.Page> documentPages) {
            return documentPages.SelectMany(p => p.Sentences);
        }

        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Phrase> AllPhrases(this IEnumerable<Paragraph> paragraphs) {
            Validator.ThrowIfNull(paragraphs, "paragraphs");
            return paragraphs.SelectMany(p => p.Phrases);
        }

        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Word> AllWords(this IEnumerable<Paragraph> paragraphs) {
            Validator.ThrowIfNull(paragraphs, "paragraphs");
            return paragraphs.SelectMany(p => p.Words);

        }
        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Sentence instances.</returns>
        public static IEnumerable<Phrase> AllPhrases(this IEnumerable<Sentence> sentences) {
            Validator.ThrowIfNull(sentences, "sentences");
            return sentences.SelectMany(s => s.Phrases);
        }
        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static IEnumerable<Word> AllWords(this IEnumerable<Sentence> sentences) {
            Validator.ThrowIfNull(sentences, "sentences");
            return sentences.SelectMany(s => s.Words);
        }

        #endregion

        #region Parallel Implementations

        /// <summary>
        /// Gets the linear aggregation of all Paragraph instances contained within the sequence of Document instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Document instances.</param>
        /// <returns>The linear aggregation of all Paragraph instances contained within the sequence of Document instances.</returns>
        public static ParallelQuery<Paragraph> AllParagraphs(this ParallelQuery<Document> documents) {
            return documents.SelectMany(d => d.Paragraphs);
        }

        public static ParallelQuery<Paragraph> AllParagraphs(this ParallelQuery<Document.Page> documents) {
            return documents.SelectMany(p => p.Paragraphs);
        }

        public static ParallelQuery<Sentence> AllSentences(this ParallelQuery<Document> documents) {
            return documents.SelectMany(d => d.Paragraphs.AllSentences());
        }

        public static ParallelQuery<Sentence> AllSentences(this ParallelQuery<Paragraph> paragraphs) {
            return paragraphs.SelectMany(p => p.Sentences);
        }

        public static ParallelQuery<Sentence> AllSentences(this ParallelQuery<Document.Page> documentPages) {
            return documentPages.SelectMany(p => p.Sentences);
        }

        /// <summary>
        /// Gets the parallel aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The parallel aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static ParallelQuery<Phrase> AllPhrases(this ParallelQuery<Paragraph> paragraphs) {
            Validator.ThrowIfNull(paragraphs, "paragraphs");
            return paragraphs.SelectMany(p => p.Phrases);

        }
        /// <summary>
        /// Gets the parallel aggregation of all Word instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Paragraph instances.</param>
        /// <returns>The parallel aggregation of all Word instances contained within the sequence of Paragraph instances.</returns>
        public static ParallelQuery<Word> AllWords(this ParallelQuery<Paragraph> paragraphs) {
            Validator.ThrowIfNull(paragraphs, "paragraphs");
            return paragraphs.SelectMany(p => p.Words);

        }
        /// <summary>
        /// Gets the parallel aggregation of all Phrase instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The parallel aggregation of all Phrase instances contained within the sequence of Sentence instances.</returns>
        public static ParallelQuery<Phrase> AllPhrases(this ParallelQuery<Sentence> sentences) {
            Validator.ThrowIfNull(sentences, "sentences");
            return sentences.SelectMany(s => s.Phrases);

        }
        /// <summary>
        /// Gets the parallel aggregation of all Word instances contained within the sequence of Sentence instances.
        /// </summary>
        /// <param name="sentences">A sequence of Sentence instances.</param>
        /// <returns>The parallel aggregation of all Word instances contained within the sequence of Sentence instances.</returns>
        public static ParallelQuery<Word> AllWords(this ParallelQuery<Sentence> sentences) {
            Validator.ThrowIfNull(sentences, "sentences");
            return sentences.SelectMany(s => s.Words);
        }

        #endregion


    }
}
