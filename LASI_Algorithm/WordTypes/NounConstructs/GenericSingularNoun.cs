using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a generic, non-proper, singular noun word.
    /// </summary>
    public class GenericSingularNoun : Noun,IQuantifiable
    {
        /// <summary>
        /// Initializes a new instance of the GenericSingularNounClass.
        /// </summary>
        /// <param name="text">the literal text content of the noun.</param>
        public GenericSingularNoun(string text)
            : base(text) {
        }

        public Quantifier Quantifier {
            get;
            set;
        }
    }
}
