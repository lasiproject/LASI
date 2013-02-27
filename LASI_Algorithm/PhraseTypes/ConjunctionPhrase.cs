using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #region Methods

        public override void DetermineHeadWord() {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Word, Phrase, or Clause on the right hand side of the ConjunctionPhrase.
        /// </summary>
        public ILexical OnRight {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the Word, Phrase, or Clause on the left hand side of the ConjunctionPhrase.
        /// </summary>
        public ILexical OnLeft {
            get;
            set;
        }
     

        #endregion
    }
}
