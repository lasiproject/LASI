using System;
using System.Collections.Generic;

namespace LASI.Algorithm
{
    public class PrepositionalPhrase : Phrase, IPrepositional
    {
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

      

        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }

        public override System.Xml.Linq.XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
