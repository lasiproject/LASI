using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a preposition such as "below", "atop", "into", "through", "by", "via", or "for".
    /// Example: The duplicitous blue bird, via its trecherous machinations, betrayed the ardent, hard-working dog.
    /// </summary>
    public class Preposition : Word, IPrepositional
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Preposition class.
        /// </summary>
        /// <param name="text">The literal text content of the Preposition.</param>
        public Preposition(string text)
            : base(text) {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the PrepositionLinkable construct on the right-hand-side of the Preposition.
        /// </summary>
        public virtual IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the PrepositionLinkable construct on the left-hand-side of the Preposition.
        /// </summary>
        public virtual IPrepositionLinkable OnLeftSide {
            get;
            set;
        }

        #endregion
        /// <summary>
        /// Binds an ILexical construct as the object of the Preposition. 
        /// Lexical constructs include Word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the Preposition.</param>
        public void BindObjectOfPreposition(ILexical prepositionalObject) {
            PrepositionalObject = prepositionalObject;
        }
        #region Methods

        #endregion

        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        public ILexical PrepositionalObject {
            get;
            protected set;
        }

    }
}
