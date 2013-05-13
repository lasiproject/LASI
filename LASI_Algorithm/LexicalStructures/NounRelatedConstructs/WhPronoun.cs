using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents entity WH Pronoun such as "that", "Which, "What" or "who".
    /// </summary>
    public class WhPronoun : Pronoun
    {
        /// <summary>
        /// Initialiazes entity new instance of the WhPronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the WhPronoun.</param>
        public WhPronoun(string text)
            : base(text) {
        }
    }
}
