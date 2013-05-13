using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents entity IsPossessive WH Pronoun such as "whose". IsPossessive WH Pronouns provide contextual ownership semantics.
    /// </summary>
    public class WhPossessivePronoun : PossessivePronoun
    {
        /// <summary>
        /// Initialiazes entity new instance of the WhPossessivePronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the WhPossessivePronoun.</param>
        public WhPossessivePronoun(string text)
            : base(text) {
        }


    }
}
