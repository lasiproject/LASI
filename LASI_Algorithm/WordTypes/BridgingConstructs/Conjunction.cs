using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    /// <summary>
    /// Represents a conjunction word which links two clauses together.
    /// </summary>
    public class Conjunction : Word, IConjunctive
    {
        /// <summary>
        /// Initializes a new instance of the Conjunction class.
        /// </summary>
        /// <param name="text">the literal text content of the word.</param>
        public Conjunction(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the clause on the left hand side of conjunction.
        /// </summary>
        public virtual Clause OnLeft {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the clause on the right hand side of conjunction.
        /// </summary>
        public virtual Clause OnRight {
            get;
            set;
        }

    }
}
