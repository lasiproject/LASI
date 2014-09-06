using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Represents a Possessive Relative Pronoun such as "whose". isPossessive Relative Pronouns provide contextual ownership semantics.
    /// </summary>
    public class RelativePossessivePronoun : PossessivePronoun
    {
        /// <summary>
        /// Initializes a new instance of the RelativePossessivePronoun class.
        /// </summary>
        /// <param name="text">The text content of the RelativePossessivePronoun.</param>
        public RelativePossessivePronoun(string text) : base(text) { }


    }
}
