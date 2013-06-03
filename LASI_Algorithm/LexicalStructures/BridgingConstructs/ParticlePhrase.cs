
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


        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        public ILexical PrepositionalObject {
            get;
            protected set;
        }
        /// <summary>
        /// Binds an ILexical construct as the object of the ParticlePhrase. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the ParticlePhrase.</param>
        public void BindObjectOfPreposition(ILexical prepositionalObject) {
            PrepositionalObject = prepositionalObject;
        }

        /// <summary>
        /// Gets or sets the contextually extrapolated role of the ParticlePhrase.
        /// </summary>
        /// <see cref="PrepositionalRole"/>
        public PrepositionalRole PrepositionalRole {
            get;
            set;
        }
    }
}
