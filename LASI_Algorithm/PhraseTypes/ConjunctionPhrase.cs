using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    /// <summary>
    /// Represents a conjunction phrase which links two clauses together.
    /// </summary>
    public class ConjunctionPhrase : Phrase, IConjunctive
    {
        /// <summary>
        /// Initializes a new instance of the ConjunctionPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the ConjunctionPhrase.</param>
        public ConjunctionPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        /// <summary>
        /// Gets or sets the clause on the left hand side of the ConjunctionPhrase.
        /// </summary>
        public virtual Clause OnLeft {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the clause on the right hand side of the ConjunctionPhrase.
        /// </summary>
        public virtual Clause OnRight {
            get;
            set;
        }

        public override Word HeadWord {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
