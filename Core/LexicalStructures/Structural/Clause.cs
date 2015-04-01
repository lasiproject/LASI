using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.LexicalStructures.Structural;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Represents a Clause Level construct.
    /// This class is currently experimental and is not a tier in the Document objects created by the tagged file parsers
    /// Initializes a new instance of the Clause class, by composing the given linear sequence of componentPhrases.
    /// </summary>
    public class Clause : ILexical, ICompositeLexical<Phrase>, ILinkedUnitLexical<Clause>
    {
        /// <summary>
        /// Initializes a new instances of the Clause class.
        /// </summary>
        /// <param name="phrases">The linear sequence of Phrases which compose to form the Clause.</param>
        public Clause(IEnumerable<Phrase> phrases)
        {
            Phrases = phrases;
        }
        /// <summary>
        /// Initializes a new instances of the Clause class.
        /// </summary>
        /// <param name="first">The phrase which begins the Clause.</param>
        /// <param name="rest">The linear sequence of Phrases which form the remainder of the Clause.</param>
        public Clause(Phrase first, params Phrase[] rest) : this(rest.Prepend(first)) { }
        /// <summary>
        /// Establishes the nested links between the Clause, its parent Sentence and Phrases which comprise it.
        /// </summary>
        /// <param name="sentence">The Sentence containing the Clause.</param>
        public void EstablishTextualLinks(Sentence sentence)
        {
            Sentence = sentence;
            foreach (var phrase in Phrases)
            {
                phrase.EstablishTextualLinks(this);
            }

            var sistren = Sentence.Clauses.ToList();
            for (var i = 0; sistren.Count > 1 && i < sistren.Count - 1; ++i)
            {
                if (i > 0)
                {
                    sistren[i].Previous = sistren[i - 1];
                }
                sistren[i].Next = sistren[i + 1];

            }
        }
        /// <summary>
        /// Returns a string representation of the Clause.
        /// </summary>
        /// <returns>A string representation of the Clause.</returns>
        public override string ToString() => $"{base.ToString()} \"{Text}\"";
        /// <summary>
        /// Gets or set the Document instance to which the Clause belongs.
        /// </summary>
        public Document Document => Sentence?.Document;
        /// <summary>
        /// Gets or set the Paragraph instance to which the Clause belongs.
        /// </summary>
        public Paragraph ParentParagraph => Sentence?.Paragraph;
        /// <summary>
        /// Gets the punctuation, if any, which ends the clause.
        /// </summary>
        public Punctuator EndingPunctuation { get; }
        /// <summary>
        /// Gets the collection of Phrases which the Clause contains.
        /// </summary>
        public IEnumerable<Phrase> Phrases { get; }
        /// <summary>
        /// Gets the concatenated text content of all of the Phrases which compose the Clause.
        /// </summary>
        /// <summary>
        /// Gets the collection of Words which the Clause contains.
        /// </summary>
        public IEnumerable<Word> Words => Phrases.SelectMany(r => r.Words);
        /// <summary>
        /// Gets the concatenated text content of all of the Phrases which compose the Clause.
        /// </summary>
        public string Text => string.Join(" ", Phrases.Select(p => p.Text));
        /// <summary>
        /// Gets or sets the numeric Weight of the Phrase within the context of its document.
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the numeric Weight of the Phrase over the context of all extant documents.
        /// </summary>
        public double MetaWeight { get; set; }

        public IEnumerable<Phrase> Components => Phrases;

        /// <summary>
        /// Gets the Sentence which contains The Clause.
        /// </summary>
        public Sentence Sentence { get; private set; }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the Clause.
        /// </summary>
        public IPrepositional PrepositionOnLeft { get; set; }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Right of the Clause.
        /// </summary>
        public IPrepositional PrepositionOnRight { get; set; }

        public Clause Previous { get; private set; }
        public Clause Next { get; private set; }
    }

}