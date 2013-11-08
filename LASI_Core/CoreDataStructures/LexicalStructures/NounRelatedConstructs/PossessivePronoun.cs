using System;
using System.Collections.Generic;
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
        /// Initialiazes entity new instance of the PossessivePronoun class.
        /// </summary>
        /// <param name="text">The key text content of the PossessivePronoun.</param>
        public PossessivePronoun(string text)
            : base(text) {
        }

        /// <summary>
        /// Adds an IPossessable construct, such as a person place or thing, to the collection of IEntity instances the PossessivePronoun "Owns",
        /// and sets its owner to be the PossessivePronoun.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public virtual void AddPossession(IPossessable possession) {
            if (PossessesFor != null) {
                PossessesFor.AddPossession(possession);
            }
            _possessed.Add(possession);
        }
        /// <summary>
        /// Returns a string represntation of the PossessivePronoun.
        /// </summary>
        /// <returns>A string represntation of the PossessivePronoun.</returns>
        public override string ToString() {
            return base.ToString() + (VerboseOutput ? string.Format("\nSignifying {0} as owner of {1}", PossessesFor.Text, Possessed.Format(e => e.Text)) : string.Empty);
        }

        /// <summary>
        /// Gets all of the IEntity constructs which the Entity "owns".
        /// </summary>
        public virtual IEnumerable<IPossessable> Possessed {
            get {
                return _possessed;
            }
        }
        /// <summary>
        /// Gets or sets the IEntity which actually, by proxy, owns the things owned by the PossessivePronoun.
        /// When this property is set, ownership of all possessions associated with the PossessivePronoun is tranferred to the target IEntity.
        /// </summary>
        public virtual IPossesser PossessesFor {
            get {
                return _possessesFor;
            }
            set {
                _possessesFor = value;
                if (value != null)
                    foreach (var possession in _possessed)
                        _possessesFor.AddPossession(possession);
            }
        }

        #region Fields
        private HashSet<IPossessable> _possessed = new HashSet<IPossessable>();
        private IPossesser _possessesFor;
        #endregion Fields
    }
}
