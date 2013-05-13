using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents entity generic, non-proper, singular noun.
    /// </summary>
    public class GenericSingularNoun : GenericNoun
    {
        /// <summary>
        /// Initializes entity new instance of the GenericSingularNounClass.
        /// </summary>
        /// <param name="text">the literal text content of the noun.</param>
        public GenericSingularNoun(string text)
            : base(text) {
        }
       

    }
}
