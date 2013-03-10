using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a IsPossessive Pronoun such as his, hers, its, or theirs. IsPossessive Pronouns provide contextual ownership semantics.
    /// </summary>
    public class PossessivePronoun : Pronoun, IPossesser
    {

        /// <summary>
        /// Initialiazes a new instance of the PossessivePronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the PossessivePronoun.</param>
        public PossessivePronoun(string text)
            : base(text) {
        }

        public override void AddPossession(IEntity possession) {
            if (BoundEntity != null) {
                BoundEntity.AddPossession(possession);
            }
        }
    }
}
