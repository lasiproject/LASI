using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace LASI.Core
{
    /// <summary>
    /// Represents a conjunction phrase which links two Clauses, Words or Phrases together.
    /// </summary>
    public class ConjunctionPhrase : Phrase, IConjunctive
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ConjunctionPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the ConjunctionPhrase.</param>
        public ConjunctionPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        /// <summary>
        /// Initializes a new instance of the ConjunctionPhrase class.
        /// </summary>
        /// <param name="first">The first Word of the ConjunctionPhrase.</param>
        /// <param name="rest">The rest of the Words comprise the ConjunctionPhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public ConjunctionPhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Word, Phrase, or Clause on the Right hand side of the ConjunctionPhrase.
        /// </summary>
        public ILexical JoinedRight {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the Word, Phrase, or Clause on the Left hand side of the ConjunctionPhrase.
        /// </summary>
        public ILexical JoinedLeft {
            get;
            set;
        }

        #endregion



    }
}
