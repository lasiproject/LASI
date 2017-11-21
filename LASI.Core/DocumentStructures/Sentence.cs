using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    /// <summary>Represents a single sentence.</summary>
    public sealed class Sentence : IReifiedTextual
    {
        #region Constructors

        /// <summary>Initializes a new instance of the Sentence class.</summary>
        /// <param name="phrases">The sequence of Phrase elements which comprise the Sentence.</param>
        /// <param name="ending">
        /// The SentenceEnding which demarcates the Sentence. If not provided, a period will be
        /// assumed, and an instance of SentenceEnding created to represent it.
        /// </param>
        public Sentence(IEnumerable<Phrase> phrases, SentenceEnding ending) : this(new [] { new Clause(phrases) }, ending) {}

        /// <summary>Initializes a new instance of the Sentence class.</summary>
        /// <param name="clauses">The sequence of Clause elements which comprise the Sentence.</param>
        /// <param name="ending">
        /// The SentenceEnding which terminates the Sentence. If not provided, a period will be
        /// assumed, and an instance of SentenceEnding created to represent it.
        /// </param>
        public Sentence(IEnumerable<Clause> clauses, SentenceEnding ending)
        {
            Clauses = clauses;
            Ending = ending ?? SentenceEnding.Period;
        }

        /// <summary>
        /// Returns the Phrase elements in the Sentence, following and not including the given Phrase.
        /// </summary>
        /// <param name="phrase">The Phrase from which to start.</param>
        /// <returns>
        /// The Phrase elements in the Sentence, following and not including the given Phrase.
        /// </returns>
        public IEnumerable<Phrase> GetPhrasesAfter(Phrase phrase) => Phrases.SkipWhile(r => r != phrase).Skip(1);

        #endregion Constructors

        #region Methods

        /// <summary>Returns a string representation of the Sentence.</summary>
        /// <returns>A string representation of the Sentence.</returns>
        public override string ToString() => $"{base.ToString()} \"{Text}\"";

        /// <summary>
        /// Establishes the linkages between the Sentence, its parent Paragraph, and its child Clauses.
        /// </summary>
        /// <param name="parent">The Paragraph to which the Sentence belongs.</param>
        public void EstablishTextualLinks(Paragraph parent)
        {
            EndsParagraph = this == parent.Sentences.LastOrDefault();
            BeginsParagraph = this == parent.Sentences.FirstOrDefault();
            Paragraph = parent;

            foreach (var clause in Clauses)
            {
                clause.EstablishTextualLinks(this);
            }
        }

        #endregion Methods

        #region Properties

        /// <summary>Gets the sequence of Clauses which comprise the sentence.</summary>
        public IEnumerable<Clause> Clauses { get; }

        /// <summary>Gets the Document to which the Sentence Belongs.</summary>
        public Document Document => Paragraph?.Document;

        /// <summary>Gets the ending punctuation character of the sentence.</summary>
        public SentenceEnding Ending { get; }

        /// <summary>Gets all of the Entities in the Sentence.</summary>
        public IEnumerable<IEntity> Entities => Lexicals.OfEntity();

        /// <summary>Gets all of the Words, Phrases, and Clauses in the Sentence.</summary>
        public IEnumerable<ILexical> Lexicals
        {
            get
            {
                foreach (var clause in Clauses)
                {
                    yield return clause;
                    foreach (var phrase in clause.Phrases)
                    {
                        yield return phrase;
                        foreach (var word in phrase.Words)
                        {
                            yield return word;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Sentence is an inverted sentence. That is a
        /// sentence in which the Verbal precedes its subject. ex:
        /// <para>
        /// inverted -&gt; "Vastly overblown were the rumors." </para><para>
        /// standard -&gt; "The rumors were vastly overblown."
        /// </para>
        /// </summary>
        public bool IsInverted { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the Sentence is exclamatory. Generally this indicates that it ends with an exclamation point.
        /// </summary>
        /// <seealso cref="SentenceEnding.ExclamationPoint"/>
        public bool IsExclaramatory => false;

        /// <summary>
        /// Gets a value indicating whether or not the Sentence is interrogative. Generally this indicates that it ends with a question mark.
        /// </summary>
        /// <seealso cref="SentenceEnding.QuestionMark"/>
        public bool IsInterrogative => false;

        /// <summary>Gets the Paragraph to which the Sentence belongs.</summary>
        public Paragraph Paragraph { get; private set; }

        /// <summary>Gets the sequence of Phrases which comprise the sentence.</summary>
        public IEnumerable<Phrase> Phrases => from clause in Clauses
        from phrase in clause.Phrases
        select phrase;

        /// <summary>
        /// Gets the concatenated text content of all of the Words which compose the Sentence.
        /// </summary>
        public string Text => string.Join(" ", Phrases.Select(e => e.Text)).Trim() + Ending.Text;

        /// <summary>Gets all of the Verbals in the Sentence.</summary>
        public IEnumerable<IVerbal> Verbals => Lexicals.OfVerbal();

        /// <summary>Gets the sequence of Words which comprise the sentence.</summary>
        public IEnumerable<Word> Words => Clauses.SelectMany(clause => clause.Phrases.SelectMany(phrase => phrase.Words));

        /// <summary>
        /// Gets a value indicating whether the Sentence is the first Sentence in its Paragraph.
        /// </summary>
        public bool BeginsParagraph { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the Sentence is the Last Sentence in its Paragraph.
        /// </summary>
        public bool EndsParagraph { get; private set; }

        #endregion Properties

        #region Types
        public static class Factory
        {
            public interface ISentenceBuilder
            {
                Sentence WithEnding(SentenceEnding ending);
            }

            public static ISentenceBuilder Sentence(params Phrase[] phrases) => new Builder(phrases);
            public static ISentenceBuilder Sentence(params Clause[] clauses) => new Builder(clauses);

            class Builder : ISentenceBuilder
            {
                public Builder(params Phrase[] phrases) : this(new [] { new Clause(phrases) }) {}
                public Builder(params Clause[] clauses) => this.clauses = clauses;
                public Sentence WithEnding(SentenceEnding ending) => new Sentence(clauses, ending);
                readonly Clause[] clauses;
            }
        }
        #endregion Types
    }
}