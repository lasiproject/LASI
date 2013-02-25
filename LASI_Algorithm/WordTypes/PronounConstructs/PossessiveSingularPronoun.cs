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
    public class PossessiveSingularPronoun : PossessivePronoun
    {
        /// <summary>
        /// Initialiazes a new instance of the PossessiveSingularPronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the PossessiveSingularPronoun.</param>
        public PossessiveSingularPronoun(string text)
            : base(text) {
        }
        /// <summary>
        /// The Entity the PossessiveSingularPronoun possesses.
        /// </summary>
        public virtual IEntity PossessedEntity {
            get;
            set;
        }
        public override void AddPossession(IEntity possession) {
            if (BoundEntity != null) {
                BoundEntity.AddPossession(possession);
            }
        }
    }
}
