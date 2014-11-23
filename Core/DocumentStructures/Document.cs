using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// <para> A data structure containing all of the paragraph, sentence, clause, phrase, and word objects which comprise a single document.</para>
    /// <para> Provides overlapping direct and indirect access to all of its children, </para>
    /// <para> e.g. such as myDoc.Paragraphs.Sentences.Phrases.Words will get all the words in the document in linear order 
    /// comparatively: myDoc.Words; yields the same collection. </para>
    /// </summary>
    public sealed class Document : IReifiedTextual
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Document class comprised from the provided paragraphs and having the provided name.
        /// </summary>
        /// <param name="content">The collection of paragraphs which contain all text in the document.</param>
        /// <param name="name">The name for the document.</param>
        public Document(IEnumerable<Paragraph> content, string name) : this(content) {
            Name = name;
        }
        /// <summary>
        /// Initializes a new instance of the Document class comprised from the provided paragraphs.
        /// </summary>
        /// <param name="content">The collection of paragraphs which contain all text in the document.</param>
        public Document(IEnumerable<Paragraph> content) {
            paragraphs = ImmutableList.CreateRange(content);
            enumerationOrHeadingsParagraphs = paragraphs
                .Where(paragraph => paragraph.ParagraphKind == ParagraphKind.Enumeration || paragraph.ParagraphKind == ParagraphKind.Heading)
                .ToImmutableList();
            AssignMembers();
            paragraphs.ForEach(paragraph => paragraph.EstablishParent(this));
            LinksAdjacentElements();
        }

        private void AssignMembers() {
            sentences = paragraphs
                .Sentences()
                .Where(sentence => sentence.Words.OfVerb().Any())
                .ToImmutableList();
            phrases = sentences.Phrases().ToImmutableList();
            words = sentences
                .SelectMany(sentence => sentence.Words.Append(sentence.EndingPunctuator))
                .ToImmutableList();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Establishes the linear linkages between all adjacent words and phrases in the Document.
        /// </summary>
        private void LinksAdjacentElements() {
            LinksAdjacentWords();
            LinksAdjacentPhrases();
        }
        private void LinksAdjacentWords() {
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
        private void LinksAdjacentPhrases() {
            if (phrases.Count > 1) {
                for (var i = 1; i < phrases.Count; ++i) {
                    phrases[i].PreviousPhrase = phrases[i - 1];
                    phrases[i - 1].NextPhrase = phrases[i];
                }
            }
        }


        /// <summary>
        /// Returns all of the verbals identified within the document.
        /// </summary>
        /// <returns>all of the verbals identified within the document.</returns>
        public IEnumerable<IVerbal> Verbals => Lexicals.OfVerbal();

        /// <summary>
        /// Returns all of the entities identified in the document.
        /// </summary>
        /// <returns> All of the entities identified in the document.</returns>
        public IEnumerable<IEntity> Entities => Lexicals.OfEntity();
        /// <summary>
        /// Returns all of lexical constructs in the document, including all words, phrases, and clauses.
        /// </summary>
        /// <returns>All of lexical constructs in the document, including all words, phrases, and clauses.</returns>
        public IEnumerable<ILexical> Lexicals {
            get {
                foreach (var lexical in words)
                    yield return lexical;
                foreach (var lexical in phrases)
                    yield return lexical;
                foreach (var lexical in Clauses)
                    yield return lexical;
            }
        }

        /// <summary>
        /// Returns the contents of the Document aggregated into a sequences of Page objects based on the line length and lines per page supplied.
        /// The supplied text measurement function is applied to determine the amount of space any piece text takes up relative to a line.
        /// </summary>
        /// <param name="lineLength">The number of characters defining the length of a line of text.</param>
        /// <param name="linesPerPage">The number of lines of text a page can contain.</param>
        /// <param name="measureText">A function used to measure the length of text.</param>
        /// <returns>The contents of the Document aggregated into a sequences of Page objects based on the line length and lines per page supplied.</returns>
        public IEnumerable<Page> Paginate(int lineLength, int linesPerPage, Func<string, double> measureText) {
            if (lineLength < 1) { throw new ArgumentOutOfRangeException("lineLength", "The supplied line length cannot be less than 0"); }
            if (linesPerPage < 1) { throw new ArgumentOutOfRangeException("linesPerPage", "The supplied number of lines per page cannot be less than 0"); }
            var measuredParagraphs =
                from paragraph in Paragraphs
                let lines = (int)Math.Floor(measureText(paragraph.Text) / lineLength)
                let actualLines = lines + Math.Round(measureText(paragraph.Text), 1, MidpointRounding.AwayFromZero) % lineLength != 0 ? 1 : 0
                select new { Paragraph = paragraph, LinesUsed = actualLines };
            // note that this variable is modified within and only within the function passed to TakeWhile
            var skip = 0;
            while (skip < measuredParagraphs.Count()) {
                var totalLines = 0;
                var pragraphs = measuredParagraphs
                    .Skip(skip)
                    .TakeWhile((p, index) => {
                        bool forceOutput = totalLines == 0 && p.LinesUsed > linesPerPage;
                        totalLines += p.LinesUsed;

                        return (totalLines <= linesPerPage || forceOutput);
                    })
                    .Select(measured => measured.Paragraph);
                var page = new Page(pragraphs, this);
                yield return page;
                skip += pragraphs.Count() + 1;
            }
        }
        /// <summary>
        /// Returns the contents of the Document aggregated into a sequences of Page objects based on the specified line length and lines per page.
        /// </summary>
        /// <param name="lineLength">The number of characters defining the length of a line of text.</param>
        /// <param name="linesPerPage">The number of lines of text a page can contain.</param>
        /// <returns>The contents of the Document aggregated into a sequences of Page objects based on the line length and lines per page supplied.</returns>
        public IEnumerable<Page> Paginate(int lineLength, int linesPerPage) => Paginate(lineLength, linesPerPage, t => t.Length);
        /// <summary>
        /// Returns a string representation of the current document. The result contains the entire textual contents of the Document, thus resulting in the instance's full materialization and reification.
        /// </summary>
        /// <returns>A string representation of the current document. The result contains the entire textual contents of the Document, thus resulting in the instance's full materialization and reification.</returns>
        public override string ToString() => GetType() + ":  " + Name + "\nParagraphs: \n" + Paragraphs.Format();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Sentences of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Sentence> Sentences => sentences;


        /// <summary>
        /// Gets the Paragraphs of the Document in linear, left to right order.
        /// </summary>
        public IEnumerable<Paragraph> Paragraphs => paragraphs.Where(paragraph => paragraph.ParagraphKind == ParagraphKind.Default);


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
        /// The name of the file the Document was parsed from.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the text content of the Document.
        /// </summary>
        public string Text => paragraphs.Format(120, p => p.Text + Environment.NewLine);

        #endregion

        #region Fields
        private ImmutableList<Word> words;
        private ImmutableList<Phrase> phrases;
        private ImmutableList<Sentence> sentences;
        private ImmutableList<Paragraph> paragraphs;
        private ImmutableList<Paragraph> enumerationOrHeadingsParagraphs;
        #endregion


        /// <summary>
        /// Represents a page of a document. Pages are somewhat arbitrary segments of a Document, that contain some subset of its content.
        /// </summary>
        public sealed class Page : IReifiedTextual
        {
            /// <summary>
            /// Initializes a new instance of the Page class.
            /// </summary>
            /// <param name="sentences">The Sentences which comprise the Page.</param>
            /// <param name="document">The Document to which the Page belongs.</param>
            internal Page(IEnumerable<Sentence> sentences, Document document) {
                Document = document;
                Sentences = sentences;
            }
            internal Page(IEnumerable<Paragraph> paragraphs, Document document)
                : this(paragraphs.SelectMany(paragraph => paragraph.Sentences), document) {
            }
            /// <summary>
            /// Gets the Paragraphs which comprise the Page.
            /// </summary>
            public IEnumerable<Paragraph> Paragraphs {
                get {
                    return Sentences
                        .Select(sentence => sentence.Paragraph)
                        .Distinct()
                        .OrderBy(Document.paragraphs.IndexOf);
                }
            }
            /// <summary>
            /// Gets the Sentences which belong to the Page.
            /// </summary>
            public IEnumerable<Sentence> Sentences { get; }
            /// <summary>
            /// Gets the Document to which the Page belongs.
            /// </summary>
            public Document Document { get; }

            public IEnumerable<Clause> Clauses => Sentences.Clauses();

            public IEnumerable<Phrase> Phrases => Sentences.Phrases();

            public IEnumerable<Word> Words => Sentences.Words();

            public IEnumerable<ILexical> Lexicals => Sentences.SelectMany(sentence => sentence.Lexicals);

            public IEnumerable<IEntity> Entities => Sentences.SelectMany(sentence => sentence.Entities);

            public IEnumerable<IVerbal> Verbals => Sentences.SelectMany(sentence => sentence.Verbals);

            /// <summary>
            /// Gets the text content of the Page.
            /// </summary>
            public string Text => ToString();
        }
    }
}