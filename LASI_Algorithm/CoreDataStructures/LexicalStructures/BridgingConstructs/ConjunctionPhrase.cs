using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace LASI.Algorithm
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
