using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using LASI.Utilities;


namespace LASI.Core
{
    /// <summary>
    /// Represents a isPossessive Pronoun such as his, hers, its, or theirs. isPossessive Pronouns provide contextual ownership semantics. That is, 
    /// they transfer ownership based on where they appear in relationship to the nouns they refer to.
    /// </summary>
    public class PossessivePronoun : Word, IWeakPossessor
    {
        /// <summary>
        /// Initializes entity new instance of the PossessivePronoun class.
        /// </summary>
        /// <param name="text">The text content of the PossessivePronoun.</param>
        public PossessivePronoun(string text) : base(text) { }

        /// <summary>
        /// Adds an IPossessable construct, such as a person place or thing, to the collection of Entity instances the PossessivePronoun "Owns",
        /// and sets its owner to be the PossessivePronoun.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession)
        {
            if (PossessesFor != null)
            {
                PossessesFor.AddPossession(possession);
            }
            possessions = possessions.Add(possession);
        }
        /// <summary>
        /// Returns a string representation of the PossessivePronoun.
        /// </summary>
        /// <returns>A string representation of the PossessivePronoun.</returns>
        public override string ToString() => base.ToString() + $"\nSignifying {PossessesFor.Text} as owner of {Possessions.Format(e => e.Text)}";

        /// <summary>
        /// Gets all of the IEntity constructs which the Entity "owns".
        /// </summary>
        public virtual IEnumerable<IPossessable> Possessions => possessions;
        /// <summary>
        /// Gets or sets the possessor which actually, by proxy, owns the things owned by the PossessivePronoun.
        /// When this property is set, ownership of all possessions associated with the PossessivePronoun is transferred to the target IEntity.
        /// </summary>
        public virtual IPossesser PossessesFor
        {
            get
            {
                return possessesFor;
            }
            set
            {
                possessesFor = value;
                if (value != null)
                {
                    foreach (var possession in possessions)
                    {
                        possessesFor.AddPossession(possession);
                    }
                }
            }
        }

        #region Fields
        private IImmutableSet<IPossessable> possessions = ImmutableHashSet.Create<IPossessable>();
        private IPossesser possessesFor;
        #endregion Fields
    }
}
