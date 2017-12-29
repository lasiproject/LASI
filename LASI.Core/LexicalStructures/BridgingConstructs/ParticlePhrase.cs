using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Represents a phrase with the syntactic role of a particle.
    /// </summary>
    /// <seealso cref="Particle"/>
    /// <seealso cref="IPrepositional"/>
    public class ParticlePhrase : Phrase, IPrepositional
    {
        /// <summary>
        /// Initializes a new instance of the ParticlePhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the ParticlePhrase.</param>
        public ParticlePhrase(IEnumerable<Word> words) : base(words) { }

        /// <summary>
        /// Initializes a new instance of the ParticlePhrase class.
        /// </summary>
        /// <param name="first">The first Word of the ParticlePhrase.</param>
        /// <param name="rest">The rest of the Words comprise the ParticlePhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public ParticlePhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

        /// <summary>
        /// Binds an ILexical construct as the object of the ParticlePhrase. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the ParticlePhrase.</param>
        public void BindObject(ILexical prepositionalObject)
        {
            BoundObject = prepositionalObject;
        }
        /// <summary>
        /// Gets or sets the ILexical construct on the right-hand-side of the Preposition.
        /// </summary>
        public ILexical ToTheRightOf
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ILexical construct on the left-hand-side of the Preposition.
        /// </summary>
        public ILexical ToTheLeftOf
        {
            get;
            set;
        }


        /// <summary>
        /// The object of the IPrepositional construct.
        /// </summary>
        public ILexical BoundObject
        {
            get;
            protected set;
        }


        /// <summary>
        /// Gets or sets the contextually extrapolated role of the ParticlePhrase.
        /// </summary>
        /// <seealso cref="Core.PrepositionRole"/>
        public PrepositionRole PrepositionRole
        {
            get;
            set;
        }
    }
}
