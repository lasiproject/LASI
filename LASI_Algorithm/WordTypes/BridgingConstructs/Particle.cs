using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class Particle : Word, IPrepositional
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Particle class.
        /// </summary>
        /// <param name="text">The literal text content of the particle.</param>
        public Particle(string text)
            : base(text) {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the right-hand-side of the Preposition.
        /// </summary>
        public IPrepositionLinkable RightLinked {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the left-hand-side of the Preposition.
        /// </summary>
        public IPrepositionLinkable LeftLinked {
            get;
            set;
        }

        #endregion
    }
}
