using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a possessive pronoun word such as "his" or "hers"
    /// A possessive pronoun associates establishes an owned - owner relationships linking to entities.
    /// </summary>
    public class PossessivePronoun : Pronoun
    {
        /// <summary>
        /// Initialiazes a new instance of the PossessivePronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the PossessivePronoun.</param>
        public PossessivePronoun(string text)
            : base(text) {
        }
        /// <summary>
        /// The Entity the PossessivePronoun possesses.
        /// </summary>
        public virtual IEntity PossessedEntity {
            get;
            set;
        }
    }
}
