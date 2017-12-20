using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core
{
    /// <summary>
    /// Represents a prepositional construct at the phrase level.
    /// <seealso cref="IPrepositional"/>
    /// <seealso cref="Preposition"/>
    /// </summary>
    public class PrepositionalPhrase : Phrase, IPrepositional
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PrepositionalPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the PrepositionalPhrase.</param>
        public PrepositionalPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) => Role = PrepositionRole.Undetermined;
        /// <summary>
        /// Initializes a new instance of the PrepositionalPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the PrepositionalPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the PrepositionalPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplify test code.</remarks>
        public PrepositionalPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }
        #endregion

        #region Methods

        /// <summary>
        /// Returns a string representation of the PrepositionalPhrase.
        /// </summary>
        /// <returns>A string representation of the PrepositionalPhrase.</returns>
        public override string ToString() {
            var result = base.ToString();
            if (ToTheLeftOf != null)
            {
                result += "\n\tleft linked: " + ToTheLeftOf.Text;
            }

            if (ToTheRightOf != null)
            {
                result += "\n\tright linked: " + ToTheRightOf.Text;
            }

            if (BoundObject != null)
            {
                result += "\n\tObject: " + BoundObject;
            }

            return result;
        }




        /// <summary>
        /// Binds an ILexical construct as the object of the PrepositionalPhrase. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the PrepositionalPhrase.</param>
        public void BindObject(ILexical prepositionalObject) {
            BoundObject = prepositionalObject;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ILexical construct on the right-hand-side of the Preposition.
        /// </summary>
        public virtual ILexical ToTheRightOf {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ILexical construct on the left-hand-side of the Preposition.
        /// </summary>
        public virtual ILexical ToTheLeftOf {
            get;
            set;
        }  /// <summary>
           /// Gets the object of the IPrepositional construct.
           /// </summary>
        public ILexical BoundObject { get; private set; }
        /// <summary>
        /// Gets or sets the contextually extrapolated role of the PrepositionalPhrase.
        /// </summary>
        /// <seealso cref="PrepositionRole"/>
        public PrepositionRole Role { get; set; }
        #endregion

    }
}
