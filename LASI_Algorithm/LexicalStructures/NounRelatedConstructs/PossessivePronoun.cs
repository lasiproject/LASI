using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm
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
        public virtual void AddPossession(IEntity possession) {
            if (PossessesFor != null) {
                PossessesFor.AddPossession(possession);
            }
            else if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
        }



        /// <summary>
        /// Gets all of the IEntity constructs which the Entity "owns".
        /// </summary>
        public virtual IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }
        /// <summary>
        /// Gets or sets the IEntity which actually, by proxy, owns the things owned by the PossessivePronoun.
        /// </summary>
        public virtual IEntity PossessesFor {
            get {
                return _possessedFor;
            }
            set {
                _possessedFor = value;
                foreach (var possession in _possessed)
                    _possessedFor.AddPossession(possession);
                _possessed.Clear();
            }
        }

        #region Fields

        private ICollection<IEntity> _possessed = new List<IEntity>();
        private IEntity _possessedFor;
        #endregion
    }
}
