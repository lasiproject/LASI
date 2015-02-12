using LASI.Utilities.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// <para>A data structure containing all of the paragraph, sentence, clause, phrase, and word objects which comprise a single document.</para>
    /// <para>Provides overlapping direct and indirect access to all of its children,</para>
    /// <para>
    /// For example, given some Document myDocument, myDocument.Paragraphs.Sentences.Phrases.Words will get all the words in the document in linear order
    /// comparatively: myDocument.Words; yields the same collection.
    /// </para>
    /// </summary>
    /// <see cref="Page" />
    /// <see cref="Paragraph" />
    /// <see cref="Sentence" />
    /// <see cref="IReifiedTextual" />
    public class Document : IReifiedTextual
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Document class comprised from the provided paragraphs and having the provided name.
        /// </summary>
        /// <param name="paragraphs">The collection of paragraphs which contain all text in the document.</param>
        /// <param name="title">The name for the document.</param>
        public Document(IEnumerable<Paragraph> paragraphs, string title)
        {
            Title = title;
            var nonStandardAndStandardParagraphs = paragraphs.Bisect(p => p.ParagraphKind == ParagraphKind.Enumeration || p.ParagraphKind == ParagraphKind.Heading);
            this.enumerationOrHeadingsParagraphs = nonStandardAndStandardParagraphs.Item1.ToList();
            this.paragraphs = nonStandardAndStandardParagraphs.Item2.ToList();
            new DocumentReifier(this).Reifiy();
        }

        /// <summary>
        /// Initializes a new instance of the Document class comprised from the provided paragraphs.
        /// </summary>
        /// <param name="paragraphs">The collection of paragraphs which contain all text in the document.</param>
        public Document(IEnumerable<Paragraph> paragraphs) : this(paragraphs, "Unititled") { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns the contents of the Document aggregated into a sequences of Page objects based on the line length and lines per page
        /// supplied. The supplied text measurement function is applied to determine the amount of space any piece text takes up relative to
        /// a line.
        /// </summary>
        /// <param name="lineLength">The number of characters defining the length of a line of text.</param>
        /// <param name="linesPerPage">The number of lines of text a page can contain.</param>
        /// <param name="measureText">A function used to measure the length of text.</param>
        /// <returns>
        /// The contents of the Document aggregated into a sequences of Page objects based on the line length and lines per page supplied.
        /// </returns>
        public IEnumerable<Page> Paginate(int lineLength, int linesPerPage, Func<string, double> measureText)
        {
            Validate.NotLessThan(lineLength, 1, nameof(lineLength), "The supplied line length cannot be less than 0");
            Validate.NotLessThan(linesPerPage, 1, nameof(linesPerPage), "The supplied number of lines per page cannot be less than 0");
            var measuredParagraphs =
               from paragraph in Paragraphs
               let lines = (int)Math.Floor(measureText(paragraph.Text) / lineLength)
               let actualLines = lines + Math.Round(measureText(paragraph.Text), 1, MidpointRounding.AwayFromZero) % lineLength != 0 ? 1 : 0
               select new
               {
                   Paragraph = paragraph,
                   LinesUsed = actualLines
               };

            var skip = 0;
            while (skip < measuredParagraphs.Count())
            {
                var totalLines = 0;
                var pragraphs = measuredParagraphs
                    .Skip(skip)
                    .TakeWhile((p, index) =>
                    {
                        bool forceOutput = totalLines == 0 && p.LinesUsed > linesPerPage;
                        totalLines += p.LinesUsed;
                        return totalLines <= linesPerPage || forceOutput;
                    })
                    .Select(measured => measured.Paragraph);
                yield return new Page(pragraphs, this);
                skip += pragraphs.Count() + 1;
            }
        }

        /// <summary>
        /// Returns the contents of the Document aggregated into a sequences of Page objects based on the specified line length and lines
        /// per page.
        /// </summary>
        /// <param name="lineLength">The number of characters defining the length of a line of text.</param>
        /// <param name="linesPerPage">The number of lines of text a page can contain.</param>
        /// <returns>
        /// The contents of the Document aggregated into a sequences of Page objects based on the line length and lines per page supplied.
        /// </returns>
        public IEnumerable<Page> Paginate(int lineLength, int linesPerPage) => Paginate(lineLength, linesPerPage, t => t.Length);

        /// <summary>
        /// Returns a string representation of the current document. The result contains the entire textual contents of the Document, thus
        /// resulting in the instance's full materialization and reification.
        /// </summary>
        /// <returns>
        /// A string representation of the current document. The result contains the entire textual contents of the Document, thus resulting
        /// in the instance's full materialization and reification.
        /// </returns>
        public override string ToString() => $"{GetType()}: {Title}\nParagraphs:\n{Paragraphs.Format()}";

        /// <summary>
        /// Returns all of the verbals identified within the document.
        /// </summary>
        /// <returns>all of the verbals identified within the document.</returns>
        public IEnumerable<IVerbal> Verbals => Lexicals.OfVerbal();

        /// <summary>
        /// Returns all of the entities identified in the document.
        /// </summary>
        /// <returns>All of the entities identified in the document.</returns>
        public IEnumerable<IEntity> Entities => Lexicals.OfEntity();

        /// <summary>
        /// Returns all of lexical constructs in the document, including all words, phrases, and clauses.
        /// </summary>
        /// <returns>All of lexical constructs in the document, including all words, phrases, and clauses.</returns>
        public IEnumerable<ILexical> Lexicals => lexicals ?? (lexicals = EnumerateAllLexicals().ToList());

        private IEnumerable<ILexical> EnumerateAllLexicals()
        {
            foreach (var lexical in words)
                yield return lexical;
            foreach (var lexical in phrases)
                yield return lexical;
            foreach (var lexical in Clauses)
                yield return lexical;
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets the Sentences of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Sentence> Sentences => sentences;

        /// <summary>
        /// Gets the Paragraphs of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Paragraph> Paragraphs => paragraphs.Where(p => p.ParagraphKind == ParagraphKind.Default);

        /// <summary>
        /// Gets the Clauses of the Document ub linear, left to right order.
        /// </summary>
        public IEnumerable<Clause> Clauses => sentences.Clauses();

        /// <summary>
        /// Gets the Phrases of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Phrase> Phrases => phrases;

        /// <summary>
        /// Gets the Words of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Word> Words => words;

        /// <summary>
        /// Gets The Title of the Document.
        /// </summary>
        /// <remarks></remarks>
        public string Title { get; }

        /// <summary>
        /// Gets the text content of the Document.
        /// </summary>
        public string Text => paragraphs.Format(120, p => p.Text + Environment.NewLine);

        #endregion Properties

        #region Fields

        private List<Word> words;
        private List<Phrase> phrases;

        private List<ILexical> lexicals;

        private List<Sentence> sentences;

        private List<Paragraph> paragraphs;
        private List<Paragraph> enumerationOrHeadingsParagraphs;

        #endregion Fields

        #region Document.Page

        /// <summary>
        /// Represents a page of a document. Pages are somewhat arbitrary segments of a Document, that contain some subset of its content.
        /// </summary>
        public sealed class Page : IReifiedTextual
        {
            /// <summary>
            /// Gets the Paragraphs which comprise the Page.
            /// </summary>
            public IEnumerable<Paragraph> Paragraphs =>
                from paragraph in Sentences.Select(s => s.Paragraph).Distinct()
                let rank = Document.paragraphs.ToList().IndexOf(paragraph)
                orderby rank
                select paragraph;


            /// <summary>
            /// Gets the Document to which the Page belongs.
            /// </summary>
            public Document Document { get; }
            /// <summary>
            /// Gets the text content of the Page.
            /// </summary>
            public string Text => ToString();
            /// <summary>
            /// Gets the Sentences spanned by the Page.
            /// </summary>
            public IEnumerable<Sentence> Sentences { get; }
            /// <summary>
            /// Gets all clauses spanned by the page.
            /// </summary>
            public IEnumerable<Clause> Clauses => Sentences.Clauses();
            /// <summary>
            /// Gets all phrases spanned by the page.
            /// </summary>
            public IEnumerable<Phrase> Phrases => Sentences.Phrases();
            /// <summary>
            /// Gets all words spanned by the page.
            /// </summary>
            public IEnumerable<Word> Words => Sentences.Words();
            /// <summary>
            /// Gets all lexicals spanned by the page.
            /// </summary>
            public IEnumerable<ILexical> Lexicals => Sentences.SelectMany(s => s.Lexicals);
            /// <summary>
            /// Gets all entities spanned by the page.
            /// </summary>
            public IEnumerable<IEntity> Entities => Sentences.SelectMany(s => s.Entities);
            /// <summary>
            /// Gets all verbals spanned by the page.
            /// </summary>
            public IEnumerable<IVerbal> Verbals => Sentences.SelectMany(s => s.Verbals);
            /// <summary>
            /// Initializes a new instance of the Page class.
            /// </summary>
            /// <param name="paragraphs">The Paragraphs which comprise the Page.</param>
            /// <param name="document">The Document to which the Page belongs.</param>
            internal Page(IEnumerable<Paragraph> paragraphs, Document document)
            {
                Document = document;
                Sentences = paragraphs.Sentences();
            }
        }

        #endregion Document.Page

        #region Private Classes

        /// <summary>
        /// Handles the setup and management of the interdependent links between elements within the Document.
        /// </summary>
        private class DocumentReifier
        {
            public DocumentReifier(Document source) { this.document = source; }

            public void Reifiy()
            {
                AssignMembers();
                foreach (var paragraph in document.paragraphs)
                {
                    paragraph.EstablishParent(document);
                }
                LinksAdjacentElements();
            }

            private void AssignMembers()
            {
                document.sentences = document.paragraphs
                    .Sentences()
                    .Where(sentence => sentence.Words.OfVerb().Any())
                    .ToList();
                document.phrases = document.sentences.Phrases()
                    .ToList();
                document.words = document.sentences
                    .SelectMany(s => s.Words.Append(s.Ending))
                    .ToList();
            }

            /// <summary>
            /// Establishes the linear linkages between all adjacent words and phrases in the Document.
            /// </summary>
            private void LinksAdjacentElements()
            {
                LinksAdjacentWords();
                LinksAdjacentPhrases();
            }

            private void LinksAdjacentWords()
            {
                var words = document.words;
                if (words.Count > 1)
                {
                    var indexOfLast = 0;
                    for (var i = 1; i < words.Count; ++i)
                    {
                        words[i].PreviousWord = words[i - 1];
                        words[i - 1].NextWord = words[i];
                        indexOfLast = i;
                    }
                    if (indexOfLast > 0)
                    {
                        var lastWord = words[indexOfLast];
                        lastWord.PreviousWord = words[indexOfLast - 1];
                    }
                }
            }

            private void LinksAdjacentPhrases()
            {
                var phrases = document.phrases;
                if (phrases.Count > 1)
                {
                    for (var i = 1; i < phrases.Count; ++i)
                    {
                        phrases[i].PreviousPhrase = phrases[i - 1];
                        phrases[i - 1].NextPhrase = phrases[i];
                    }
                }
            }
            private readonly Document document;
        }

        #endregion Private Classes
    }
}