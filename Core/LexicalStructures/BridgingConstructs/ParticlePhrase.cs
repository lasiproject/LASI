
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Represents a phrase with the syntactic role of a particle.
    /// </summary>
    public class ParticlePhrase : Phrase, IPrepositional
    {
        /// <summary>
        /// Initializes a new instance of the ParticlePhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the ParticlePhrase.</param>
        public ParticlePhrase(IEnumerable<Word> words)
            : base(words) {
        }

        /// <summary>
        /// Binds an ILexical construct as the object of the ParticlePhrase. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the ParticlePhrase.</param>
        public void BindObject(ILexical prepositionalObject) {
            BoundObject = prepositionalObject;
        }
        /// <summary>
        /// Gets or sets the ILexical construct on the right-hand-side of the Preposition.
        /// </summary>
        public ILexical ToTheRightOf {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ILexical construct on the left-hand-side of the Preposition.
        /// </summary>
        public ILexical ToTheLeftOf {
            get;
            set;
        }


        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        public ILexical BoundObject {
            get;
            protected set;
        }


        /// <summary>
        /// Gets or sets the contextually extrapolated role of the ParticlePhrase.
        /// </summary>
        /// <see cref="PrepositionRole"/>
        public PrepositionRole Role {
            get;
            set;
        }
    }
}
