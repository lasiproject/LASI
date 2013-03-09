using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents the word "TO", a dynamic prepositional construct which can link words, phrases and clauses together.
    /// </summary>
    public class ToLinker : Word, IPrepositional
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ToLinker class.
        /// </summary>
        /// <param name="text">The literal text content of the particle.</param>
        public ToLinker()
            : base("to") {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the right-hand-side of the ToLinker.
        /// </summary>
        public IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the left-hand-side of the ToLinker.
        /// </summary>
        public IPrepositionLinkable OnLeftSide {
            get;
            set;
        }

        #endregion

       

        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        public ILexical PrepositionalObject {
            get;
            protected set;
        }
        /// <summary>
        /// Binds an ILexical construct as the object of the ToLinker. 
        /// Lexical constructs include Word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the ToLinker.</param>
        public void BindObjectOfPreposition(ILexical prepositionalObject) {
            PrepositionalObject = prepositionalObject;
        }
    }
}
