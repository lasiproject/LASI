using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;
using LASI.Utilities.Validation;

namespace LASI.Core {
    /// <summary>
    /// <para>A data structure containing all of the paragraph, sentence, clause, phrase, and word objects which comprise a single document.</para>
    /// <para>Provides overlapping direct and indirect access to all of its children,</para>
    /// <para>
    /// For example, given some Document myDocument, myDocument.Paragraphs.Sentences.Phrases.Words will get all the words in the document in linear order
    /// comparatively: myDocument.Words; yields the same collection.
    /// </para>
    /// </summary>
    /// <seealso cref="Page" />
    /// <seealso cref="Paragraph" />
    /// <seealso cref="Sentence" />
    /// <seealso cref="IReifiedTextual" />
    public class Document : IReifiedTextual {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Document class comprised from the provided paragraphs and having the provided title.
        /// </summary>
        /// <param name="title">The title of the document.</param>
        /// <param name="paragraphs">The collection of paragraphs which contain all text in the document.</param>
        public Document(string title, IEnumerable<Paragraph> paragraphs) {
            Name = title;

            this.paragraphs = paragraphs
                .Where(p => p.ParagraphKind == ParagraphKind.Default)
                .ToList();

            this.listOrBulletParagraphs = paragraphs
                .Where(p => p.ParagraphKind == ParagraphKind.Heading || p.ParagraphKind == ParagraphKind.Enumeration)
                .ToList();

            Reifier.Reifiy(this);
        }

        /// <summary>
        /// Initializes a new instance of the Document class comprised from the provided paragraphs.
        /// </summary>
        /// <param name="paragraphs">The collection of paragraphs which contain all text in the document.</param>
        public Document(IEnumerable<Paragraph> paragraphs) : this(DefaultTitle, paragraphs) { }

        /// <summary>
        /// Initializes a new instance of the Document class comprised from the provided paragraphs.
        /// </summary>
        /// <param name="first">The first <see cref="Paragraph"/> of the Document.</param>
        /// <param name="rest">The rest of the paragraphs of the document.</param>
        public Document(Paragraph first, params Paragraph[] rest) : this(DefaultTitle, rest.Prepend(first)) { }

        /// <summary>
        /// Initializes a new instance of the Document class comprised from the provided paragraphs and having the provided title.
        /// </summary>
        /// <param name="title">The title of the document.</param>
        /// <param name="first">The first <see cref="Paragraph"/> of the Document.</param>
        /// <param name="rest">The rest of the paragraphs of the document.</param>
        public Document(string title, Paragraph first, params Paragraph[] rest) : this(title, rest.Prepend(first)) { }

        private const string DefaultTitle = "Untitled";

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
        public IEnumerable<Page> Paginate(int lineLength, int linesPerPage, Func<string, double> measureText) {
            Validate.NotLessThan(lineLength, 1, nameof(lineLength), "The supplied line length cannot be less than 0");
            Validate.NotLessThan(linesPerPage, 1, nameof(linesPerPage), "The supplied number of lines per page cannot be less than 0");
            var measuredParagraphs =
               from paragraph in Paragraphs
               let lines = (int)Math.Floor(measureText(paragraph.Text) / lineLength)
               let actualLines = Math.Abs(lines + Math.Round(measureText(paragraph.Text), 1, MidpointRounding.AwayFromZero) % lineLength) > double.Epsilon ? 1 : 0
               select new {
                   Paragraph = paragraph,
                   LinesUsed = actualLines
               };
            var pagesCreated = 0;
            var paragraphsToSkip = 0;
            while (paragraphsToSkip < measuredParagraphs.Count()) {
                var totalLines = 0;
                var pragraphs = measuredParagraphs
                    .Skip(paragraphsToSkip)
                    .TakeWhile((paragraph, index) => {
                        var forceOutput = totalLines == 0 && paragraph.LinesUsed > linesPerPage;
                        totalLines += paragraph.LinesUsed;
                        return totalLines <= linesPerPage || forceOutput;
                    })
                    .Select(measured => measured.Paragraph);
                yield return new Page(pragraphs, this, pagesCreated);
                paragraphsToSkip += pragraphs.Count() + 1;
                ++pagesCreated;
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
        public override string ToString() => $"{GetType()}: {Name}\nParagraphs:\n{Paragraphs.Format()}";

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
        public IEnumerable<ILexical> Lexicals => lexicals ?? (lexicals = LexicalConstituentEnumerator.ToList());
        private IEnumerable<ILexical> LexicalConstituentEnumerator =>
            ListConsituent(words).Concat(ListConsituent(phrases)).Concat(ListConsituent(clauses.ToList()));

        private IEnumerable<ILexical> ListConsituent(IReadOnlyList<ILexical> constituentList) {
            for (var i = 0; i < constituentList.Count; ++i) {
                yield return constituentList[i];
            }
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// The Sentences of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Sentence> Sentences => sentences;

        /// <summary>
        /// The Paragraphs of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Paragraph> Paragraphs => paragraphs;

        /// <summary>
        /// The Clauses of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Clause> Clauses => sentences.Clauses();

        /// <summary>
        /// The Phrases of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Phrase> Phrases => phrases;

        /// <summary>
        /// The Words of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Word> Words => words;

        /// <summary>
        /// The Name of the Document.
        /// </summary>
        /// <remarks></remarks>
        public string Name { get; }

        /// <summary>
        /// The text content of the Document.
        /// </summary>
        public string Text => paragraphs.Format(120, p => p.Text + Environment.NewLine);

        #endregion Properties

        #region Fields

        private List<Word> words;

        private List<Phrase> phrases;

        private List<Clause> clauses;

        private List<ILexical> lexicals;

        private List<Sentence> sentences;

        readonly List<Paragraph> paragraphs;

        readonly List<Paragraph> listOrBulletParagraphs;

        #endregion Fields

        #region Classes 

        #region Page

        /// <summary>
        /// Represents a page of a document. Pages are somewhat arbitrary segments of a <see cref="Document"/> that contain some contiguous subset of its content.
        /// </summary>
        public sealed class Page : IReifiedTextual {
            /// <summary>
            /// Initializes a new instance of the Page class.
            /// </summary>
            /// <param name="paragraphs">The Paragraphs which comprise the Page.</param>
            /// <param name="document">The Document to which the Page belongs.</param>
            /// <param name="pageNumber">The page number of the Page within its document.</param>
            internal Page(IEnumerable<Paragraph> paragraphs, Document document, int pageNumber) {
                Document = document;
                Sentences = paragraphs.Sentences();
                PageNumber = pageNumber;
            }
            /// <summary>
            /// The Paragraphs which comprise the Page.
            /// </summary>
            public IEnumerable<Paragraph> Paragraphs => from sentence in Sentences.DistinctBy(s => s.Paragraph)
                                                        let rank = Document.paragraphs.IndexOf(sentence.Paragraph)
                                                        orderby rank
                                                        select sentence.Paragraph;

            /// <summary>
            /// Returns a string representation of the <see cref="Page"/>.
            /// </summary>
            /// <returns></returns>
            public override string ToString() => Paragraphs.Format(120, p => p.Text + Environment.NewLine);

            /// <summary>
            /// The Document to which the Page belongs.
            /// </summary>
            public Document Document { get; }
            /// <summary>
            /// The text content of the Page.
            /// </summary>
            public string Text => ToString();
            /// <summary>
            /// The Sentences spanned by the Page.
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
            /// The page number of the Page, that is its index relative to other pages across the document it belongs to.
            /// </summary>
            public int PageNumber { get; }
        }

        #endregion Page

        #region Reifier

        /// <summary>
        /// Handles the setup and management of the interdependent links between elements within the Document.
        /// </summary>
        private static class Reifier {
            public static void Reifiy(Document document) {
                AssignMembers(document);
                foreach (var paragraph in document.paragraphs) {
                    paragraph.EstablishTextualLinks(document);
                }
                LinksAdjacentElements(document);
            }

            private static void AssignMembers(Document document) {
                document.sentences = document.paragraphs.Sentences().Where(sentence => sentence.Words.OfVerb().Any()).ToList();

                document.clauses = document.sentences.Clauses().ToList();

                document.phrases = document.sentences.Phrases().ToList();

                document.words = document.sentences.SelectMany(sentence => sentence.Words.Append(sentence.Ending)).ToList();
            }

            /// <summary>
            /// Establishes the linear linkages between all adjacent words and phrases in the Document.
            /// </summary>
            private static void LinksAdjacentElements(Document document) {
                LinksAdjacentWords(document);
                LinksAdjacentPhrases(document);
            }

            private static void LinksAdjacentWords(Document document) {
                var words = document.words;
                if (words.Count > 1) {
                    var indexOfLast = 0;
                    for (var i = 1; i < words.Count; ++i) {
                        words[i].PreviousWord = words[i - 1];
                        words[i - 1].NextWord = words[i];
                        indexOfLast = i;
                    }
                    if (indexOfLast > 0) {
                        var lastWord = words[indexOfLast];
                        lastWord.PreviousWord = words[indexOfLast - 1];
                    }
                }
            }

            private static void LinksAdjacentPhrases(Document document) {
                var phrases = document.phrases;
                if (phrases.Count > 1) {
                    for (var i = 1; i < phrases.Count; ++i) {
                        phrases[i].PreviousPhrase = phrases[i - 1];
                        phrases[i - 1].NextPhrase = phrases[i];
                    }
                }
            }
        }

        #endregion Reifier

        #endregion Classes 
    }
}
