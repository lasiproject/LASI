
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace LASI.Core
{
    /// <summary>
    /// <para> Represents a particle construct at the word level. </para>
    /// <para> For example, the the word (about) in the sentence: "He read a book about abstract erotica." </para>
    /// <para> Note that the distinction between particle and prepositions can sometimes be tricky and is heavily dependent on nuances of grammatical usage. </para>
    /// </summary>
    public class Particle : Word, IPrepositional
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Particle class.
        /// </summary>
        /// <param name="text">The text content of the particle.</param>
        public Particle(string text)
            : base(text) { }

        #endregion

        #region Methods

        /// <summary>
        /// Binds an ILexical construct as the object of the ToLinker. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the Particle.</param>
        public void BindObject(ILexical prepositionalObject) {
            BoundObject = prepositionalObject;
        }

        #endregion

        #region Properties

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
        /// Gets or sets the contextually extrapolated Prepositional Role of the Particle.
        /// </summary>
        /// <see cref="PrepositionRole"/>
        public PrepositionRole Role {
            get;
            set;
        }
        #endregion
    }
}
