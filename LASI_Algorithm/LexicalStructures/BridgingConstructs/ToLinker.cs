
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents the wd "TO", a dynamic prepositional construct which can link words, componentPhrases and clauses together.
    /// </summary>
    public class ToLinker : Word, IPrepositional
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ToLinker class.
        /// </summary> 
        public ToLinker()
            : base("to") {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the second-hand-side of the ToLinker.
        /// </summary>
        public IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the first-hand-side of the ToLinker.
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
        /// Lexical constructs include wd, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the ToLinker.</param>
        public void BindObjectOfPreposition(ILexical prepositionalObject) {
            PrepositionalObject = prepositionalObject;
        }

        /// <summary>
        /// Gets or sets the contextually extrapolated role of the ToLinker.
        /// </summary>
        /// <see cref="PrepositionalRole"/>
        public PrepositionalRole PrepositionalRole {
            get;
            set;
        }
    }
}
