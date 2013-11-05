
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace LASI.Core
{
    /// <summary>
    /// Represents the word "TO", a dynamic prepositional construct which can link words, componentPhrases and clauses together.
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
        /// Gets or sets the IprepositionLinkable construct on the right-hand-side of the ToLinker.
        /// </summary>
        public ILexical ToTheRightOf {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the left-hand-side of the ToLinker.
        /// </summary>
        public ILexical ToTheLeftOf {
            get;
            set;
        }

        #endregion



        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        public ILexical BoundObject {
            get;
            protected set;
        }
        /// <summary>
        /// Binds an ILexical construct as the object of the ToLinker. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the ToLinker.</param>
        public void BindObject(ILexical prepositionalObject) {
            BoundObject = prepositionalObject;
        }

        /// <summary>
        /// Gets or sets the contextually extrapolated role of the ToLinker.
        /// </summary>
        /// <see cref="PrepositionRole"/>
        public PrepositionRole Role {
            get;
            set;
        }
    }
}
