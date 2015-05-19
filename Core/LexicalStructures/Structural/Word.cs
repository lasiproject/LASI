using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LASI.Utilities.Validation;

namespace LASI.Core
{
    /// <summary>
    /// Provides the base class, properties, and behaviors for all word level grammatical constructs.
    /// </summary>
    public abstract class Word : ILexical, LexicalStructures.Structural.ILinkedUnitLexical<Word>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the word class which represents the properties
        /// and behaviors of a word-level grammatical element.
        /// </summary>
        /// <param name="text">The text content of the word.</param>
        protected Word(string text)
        {
            Validate.DoesNotExistIn(text, ' ', "The text of a word may not contain white space");
#if ENSURENOEMPTYWORDS
            Validate.NotEmpty(text, nameof(text), "A word cannot be created from empty text");
#endif
            Text = text;
            Weight = 1;
            MetaWeight = 1;
        }


        #endregion

        #region Methods

        /// <summary>
        /// Establishes the linkage between the word and its parent Phrase.
        /// </summary>
        /// <param name="parent">The Phrase to which the word belongs.</param>
        internal void EstablishTextualLinks(Phrase parent) => Phrase = parent;
        /// <summary>
        /// Returns a string representation of the word.
        /// </summary>
        /// <returns>A string containing its underlying Noun and its text content.</returns>
        public override string ToString() => $"{GetType().Name } \"{Text}\"";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the text content of the word instance.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the document instance to which the word belongs.
        /// </summary>
        public Document Document => Phrase?.Document;
        /// <summary>
        /// Gets, lexically speaking, the next word in the Document to which the instance belongs.
        /// </summary>
        public Word NextWord { get; internal set; }
        /// <summary>
        /// Gets, lexically speaking, the previous word in the Document to which the instance belongs.
        /// </summary>
        public Word PreviousWord { get; internal set; }
        /// <summary>
        /// Gets, lexically speaking, the next word in the Document to which the instance belongs.
        /// </summary>
        public Word Next => NextWord;
        /// <summary>
        /// Gets, lexically speaking, the previous word in the Document to which the instance belongs.
        /// </summary>
        public Word Previous => PreviousWord;
        /// <summary>
        /// Gets or the Phrase the word belongs to.
        /// </summary>
        public Phrase Phrase { get; private set; }
        /// <summary>
        /// Gets the Sentence the word belongs to.
        /// </summary>
        public Sentence Sentence => Phrase?.Sentence;
        /// <summary>
        /// Gets or sets the Prepositional construct which is lexically to the Left of the word.
        /// </summary>
        public IPrepositional PrepositionOnLeft { get; set; }
        /// <summary>
        /// Gets or sets the Prepositional construct which is lexically to the Right of the word.
        /// </summary>
        public IPrepositional PrepositionOnRight { get; set; }
        /// <summary>
        /// Gets or sets the numeric Weight of the word within the context of its parent document.
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Gets or sets the numeric Weight of the word over the context of all extant documents.
        /// </summary>
        public double MetaWeight { get; set; }

        #endregion

        #region Static Members

        /// <summary>
        /// Controls the level detail of the information provided by the ToString method of all instances of the Word class.
        /// </summary>
        public static bool VerboseOutput { get; set; }

        #endregion
    }
}
