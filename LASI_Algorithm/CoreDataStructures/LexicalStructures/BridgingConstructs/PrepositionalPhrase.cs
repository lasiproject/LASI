
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a prepositional phrase, an object which has Prepositional properties at the phrase level.
    /// <see cref="IPrepositional"/>
    /// <seealso cref="Preposition"/>
    /// </summary>
    public class PrepositionalPhrase : Phrase, IPrepositional
    {
        /// <summary>
        /// Initializes a new instance of the PrepositionalPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the PrepositionalPhrase.</param>
        public PrepositionalPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
            Role = PrepositionRole.Undetermined;
        }
        /// <summary>
        /// Gets or sets the ILexical construct on the right-hand-side of the Preposition.
        /// </summary>
        public virtual ILexical ToTheRightOf {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ILexical construct on the left-hand-side of the Preposition.
        /// </summary>
        public virtual ILexical ToTheLeftOf {
            get;
            set;
        }

        /// <summary>
        /// Returns a string representation of the PrepositionalPhrase.
        /// </summary>
        /// <returns>A string representation of the PrepositionalPhrase.</returns>
        public override string ToString() {
            if (Phrase.VerboseOutput) {
                var result = base.ToString();
                if (ToTheLeftOf != null)
                    result += "\n\tleft linked: " + ToTheLeftOf.ToString();
                if (ToTheRightOf != null)
                    result += "\n\tright linked: " + ToTheRightOf.ToString();
                if (BoundObject != null)
                    result += "\n\tObject: " + BoundObject.ToString();
                return result;
            }
            return base.ToString();
        }



        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        public ILexical BoundObject {
            get;
            protected set;
        }
        /// <summary>
        /// Binds an ILexical construct as the object of the PrepositionalPhrase. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the PrepositionalPhrase.</param>
        public void BindObject(ILexical prepositionalObject) {
            BoundObject = prepositionalObject;
        }

        /// <summary>
        /// Gets or sets the contextually extrapolated role of the PrepositionalPhrase.
        /// </summary>
        /// <see cref="PrepositionRole"/>
        public PrepositionRole Role {
            get;
            set;
        }
    }
}
