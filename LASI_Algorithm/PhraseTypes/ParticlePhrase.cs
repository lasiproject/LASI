using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class ParticlePhrase : Phrase, IPrepositional
    {

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
        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }

        public override System.Xml.Linq.XElement Serialize() {
            throw new NotImplementedException();
        }
    }
}
