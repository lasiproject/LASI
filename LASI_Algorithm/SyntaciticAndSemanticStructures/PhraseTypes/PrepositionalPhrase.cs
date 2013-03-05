using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    public class PrepositionalPhrase : Phrase, IPrepositional
    {
        /// <summary>
        /// Initializes a new instance of the PrepositionalPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the PrepositionalPhrase.</param>
        public PrepositionalPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the right-hand-side of the Preposition.
        /// </summary>
        public virtual IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the left-hand-side of the Preposition.
        /// </summary>
        public virtual IPrepositionLinkable OnLeftSide {
            get;
            set;
        }




        public override XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
