using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a conjunction verb which links two clauses together.
    /// </summary>
    public class Conjunction : Word, IConjunctive
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Conjunction class.
        /// </summary>
        /// <param name="text">the literal text content of the verb.</param>
        public Conjunction(string text)
            : base(text) {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the clause on the right hand side of conjunction.
        /// </summary>
        public virtual ILexical OnRight {
            get;
            set;
        } /// <summary>
        /// Gets or sets the clause on the left hand side of conjunction.
        /// </summary>
        public virtual ILexical OnLeft {
            get;
            set;
        }

        #endregion

    }
}
