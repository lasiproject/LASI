using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    public class Preposition : Word, IPrepositional
    {
        /// <summary>
        /// Initializes a new instance of the Preposition class.
        /// </summary>
        /// <param name="text">The literal text content of the Preposition.</param>
        public Preposition(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the right-hand-side of the Preposition.
        /// </summary>
        public virtual IPrepositionLinkable RightLinked {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the left-hand-side of the Preposition.
        /// </summary>
        public virtual IPrepositionLinkable LeftLinked {
            get;
            set;
        }
        /// <summary>
        /// Links the Preposition to an IPrepositionLinkable construct to its left.
        /// </summary>
        /// <param name="toLink">A compatable construct which is to the left of The Prepositon.</param>
        public virtual void LinkToLeft(IPrepositionLinkable toLink) {
            LeftLinked = toLink;
        }
        /// <summary>
        /// Links the Preposition to an IPrepositionLinkable construct to its right.
        /// </summary>
        /// <param name="toLink">A compatable construct which is to the left of The Prepositon.</param>
        public virtual void LinkToRight(IPrepositionLinkable toLink) {
            throw new NotImplementedException();
        }
    }
}
