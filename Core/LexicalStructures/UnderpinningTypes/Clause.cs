using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace LASI.Core
{
    /// <summary>
    /// Represents a Clause Level construct.
    /// </summary>
    public class Clause : ILexical
    {

        /// <summary>
        /// This class is currently experimental and is not a tier in the Document objects created by the tagged file parsers
        /// Initializes a new instance of the Clause class, by composing the given linear sequence of componentPhrases.
        /// </summary>
        /// <param name="phrases">The linear sequence of Phrases which compose to form the Clause.</param>
        public Clause(IEnumerable<Phrase> phrases) {
            Phrases = phrases;
        }
        /// <summary>
        /// Establishes the nested links between the Clause, its parent Sentence and Phrases which comprise it.
        /// </summary>
        /// <param name="sentence">The Sentence containing the Clause.</param>
        public void EstablishParent(LASI.Core.DocumentStructures.Sentence sentence) {
            Sentence = sentence;
            foreach (var r in Phrases)
                r.EstablishParent(this);
        }
        /// <summary>
        /// Returns a string representation of the Clause.
        /// </summary>
        /// <returns>A string representation of the Clause.</returns>
        public override string ToString() {
            return base.ToString() + " \"" + Text + "\"";
        }
        /// <summary>
        /// Gets or set the Document instance to which the Clause belongs.
        /// </summary>
        public Document Document { get { return Sentence != null ? Sentence.Document : null; } }
        /// <summary>
        /// Gets or set the Paragraph instance to which the Clause belongs.
        /// </summary>
        public Paragraph ParentParagraph { get { return Sentence != null ? Sentence.Paragraph : null; } }
        /// <summary>
        /// Gets the punctuation, if any, which ends the clause.
        /// </summary>
        public Punctuator EndingPunctuation { get; protected set; }
        /// <summary>
        /// Gets the collection of Phrases which the Clause contains.
        /// </summary>
        public IEnumerable<Phrase> Phrases { get; protected set; }
        /// <summary>
        /// Gets the concatenated text content of all of the Phrases which compose the Clause.
        /// </summary>
        /// <summary>
        /// Gets the collection of Words which the Clause contains.
        /// </summary>
        public IEnumerable<Word> Words { get { return Phrases.SelectMany(r => r.Words); } }
        /// <summary>
        /// Gets the concatenated text content of all of the Phrases which compose the Clause.
        /// </summary>
        public string Text {
            get {
                return string.Join(" ", Phrases.Select(p => p.Text));
            }
        }

        /// <summary>
        /// Gets or sets the numeric Weight of the Phrase within the context of its document.
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the numeric Weight of the Phrase over the context of all extant documents.
        /// </summary>
        public double MetaWeight { get; set; }
        /// <summary>
        /// Gets the Sentence which contains The Clause.
        /// </summary>
        public LASI.Core.DocumentStructures.Sentence Sentence { get; private set; }
        /// <summary>
        /// Gets the unique ID number of the Clause
        /// </summary>
        public int ID { get; private set; }
        /// <summary>
        /// Gets the System.Type of the Clause.
        /// </summary>
        public Type Type { get { return GetType(); } }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Left of the Clause.
        /// </summary>
        public IPrepositional PrepositionOnLeft { get; set; }
        /// <summary>
        /// Gets or sets the IPrepositional instance lexically to the Right of the Clause.
        /// </summary>
        public IPrepositional PrepositionOnRight { get; set; }
    }

}