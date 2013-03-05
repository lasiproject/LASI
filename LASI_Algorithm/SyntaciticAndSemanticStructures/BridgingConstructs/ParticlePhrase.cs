using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    public class ParticlePhrase : Phrase, IPrepositional
    {
        /// <summary>
        /// Initializes a new instance of the ParticlePhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the ParticlePhrase.</param>
        public ParticlePhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }

        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the right-hand-side of the Preposition.
        /// </summary>
        public IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the left-hand-side of the Preposition.
        /// </summary>
        public IPrepositionLinkable OnLeftSide {
            get;
            set;
        }


        public override XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
