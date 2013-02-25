using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
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
