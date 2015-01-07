using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    using Validator = Utilities.Validation.Validator;

    /// <summary>
    /// Defines extension methods for sequences of various document level constructs.
    /// </summary>
    /// <see cref="Document"/>
    /// <seealso cref="Sentence"/>
    /// <seealso cref="Paragraph"/>
    public static partial class LexicalEnumerable
    {
        #region Sequential Implementations

        /// <summary>
        /// Gets the linear aggregation of all Paragraph instances contained within the sequence of Document instances.
        /// </summary>
        /// <param name="documents">A sequence of Document instances.</param>
        /// <returns>The linear aggregation of all Paragraph instances contained within the sequence of Document instances.</returns>
        public static IEnumerable<Paragraph> Paragraphs(this IEnumerable<Document> documents) => documents.SelectMany(d => d.Paragraphs);

        /// <summary>
        /// Gets the linear aggregation of all Paragraph instances contained within the sequence of Document.Page instances.
        /// </summary>
        /// <param name="pages">A sequence of Document.Page instances.</param>
        /// <returns>The linear aggregation of all Paragraph instances contained within the sequence of Document.Page instances.</returns>
        public static IEnumerable<Paragraph> Paragraphs(this IEnumerable<Document.Page> pages) => pages.SelectMany(p => p.Paragraphs);
        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Document instances.
        /// </summary>
        /// <param name="documents">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Document instances.</returns>
        public static IEnumerable<Sentence> Sentences(this IEnumerable<Document> documents) => documents.SelectMany(d => d.Paragraphs.Sentences());

        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="paragraphs">A sequence of Sentence instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Sentence> Sentences(this IEnumerable<Paragraph> paragraphs) => paragraphs.SelectMany(p => p.Sentences);

        /// <summary>
        /// Gets the linear aggregation of all Sentence instances contained within the sequence of Document.Page instances.
        /// </summary>
        /// <param name="documentPages">A sequence of Document.Page instances.</param>
        /// <returns>The linear aggregation of all Sentence instances contained within the sequence of Document.Page instances.</returns>
        public static IEnumerable<Sentence> Sentences(this IEnumerable<Document.Page> documentPages) => documentPages.SelectMany(p => p.Sentences);
        /// <summary>
        /// Gets the linear aggregation of all Clause instances contained within the sequence of textual sources.
        /// </summary>
        /// <param name="sources">A sequence of textual sources.</param>
        /// <returns>The linear aggregation of all Clause instances contained within the sequence of textual sources.</returns>
        public static IEnumerable<Clause> Clauses(this IEnumerable<IReifiedTextual> sources) {
            Validator.ThrowIfNull(sources, "paragraphs");
            return sources.SelectMany(p => p.Clauses);
        }
        /// <summary>
        /// Gets the linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.
        /// </summary>
        /// <param name="sources">A sequence of Paragraph instances.</param>
        /// <returns>The linear aggregation of all Phrase instances contained within the sequence of Paragraph instances.</returns>
        public static IEnumerable<Phrase> Phrases(this IEnumerable<IReifiedTextual> sources) {
            Validator.ThrowIfNull(sources, "paragraphs");
            return sources.SelectMany(p => p.Phrases);
        }

        /// <summary>
        /// Gets the linear aggregation of all Word instances contained within the sequence of textual sources.
        /// </summary>
        /// <param name="sources">A sequence of textual sources.</param>
        /// <returns>The linear aggregation of all Word instances contained within the sequence of textual sources.</returns>
        public static IEnumerable<Word> Words(this IEnumerable<IReifiedTextual> sources) {
            Validator.ThrowIfNull(sources, "paragraphs");
            return sources.SelectMany(p => p.Words);
        }

        #endregion

        #region Parallel Implementations

        /// <summary>
        /// Gets the linear aggregation of all Paragraph instances contained within the sequence of Document instances.
        /// </summary>
        /// <param name="documents">A sequence of Document instances.</param>
        /// <returns>The linear aggregation of all Paragraph instances contained within the sequence of Document instances.</returns>
        public static ParallelQuery<Paragraph> Paragraphs(this ParallelQuery<Document> documents) => documents.SelectMany(d => d.Paragraphs);

        public static ParallelQuery<Paragraph> Paragraphs(this ParallelQuery<Document.Page> documents) => documents.SelectMany(p => p.Paragraphs);

        public static ParallelQuery<Sentence> Sentences(this ParallelQuery<Document> documents) => documents.SelectMany(d => d.Paragraphs.Sentences());

        public static ParallelQuery<Sentence> Sentences(this ParallelQuery<Paragraph> paragraphs) => paragraphs.SelectMany(p => p.Sentences);

        public static ParallelQuery<Sentence> Sentences(this ParallelQuery<Document.Page> documentPages) => documentPages.SelectMany(p => p.Sentences);

        /// <summary>
        /// Gets the parallel aggregation of all Clause instances contained within the sequence of textual sources.
        /// </summary>
        /// <param name="sources">A sequence of textual sources.</param>
        /// <returns>The parallel aggregation of all Clause instances contained within the sequence of textual sources.</returns>
        public static ParallelQuery<Clause> Clauses(this ParallelQuery<IReifiedTextual> sources) {
            Validator.ThrowIfNull(sources, "sentences");
            return sources.SelectMany(s => s.Clauses);

        }
        /// <summary>
        /// Gets the parallel aggregation of all Phrase instances contained within the sequence of textual sources.
        /// </summary>
        /// <param name="sources">A sequence of textual sources.</param>
        /// <returns>The parallel aggregation of all Phrase instances contained within the sequence of textual sources.</returns>
        public static ParallelQuery<Phrase> Phrases(this ParallelQuery<IReifiedTextual> sources) {
            Validator.ThrowIfNull(sources, "paragraphs");
            return sources.SelectMany(p => p.Phrases);

        }
        /// <summary>
        /// Gets the parallel aggregation of all Word instances contained within the sequence of textual sources.
        /// </summary>
        /// <param name="sources">A sequence of textual sources.</param>
        /// <returns>The parallel aggregation of all Word instances contained within the sequence of textual sources.</returns>
        public static ParallelQuery<Word> Words(this ParallelQuery<IReifiedTextual> sources) {
            Validator.ThrowIfNull(sources, "paragraphs");
            return sources.SelectMany(p => p.Words);

        }

        #endregion


    }
}
