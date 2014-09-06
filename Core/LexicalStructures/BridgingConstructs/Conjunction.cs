using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Represents a conjunction word which links two clauses together.
    /// </summary>
    public class Conjunction : Word, IConjunctive
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Conjunction class.
        /// </summary>
        /// <param name="text">The text content of the Conjunction.</param>
        public Conjunction(string text)
            : base(text) {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Word, Phrase, or Clause on the Right hand side of Conjunction.
        /// </summary>
        public virtual ILexical JoinedRight {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Word, Phrase, or Clause on the Left hand side of Conjunction.
        /// </summary>
        public virtual ILexical JoinedLeft {
            get;
            set;
        }

        #endregion

    }
}
