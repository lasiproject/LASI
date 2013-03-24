using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a IsPossessive Pronoun such as his, hers, its, or theirs. IsPossessive Pronouns provide contextual ownership semantics.
    /// </summary>
    public class PossessivePronoun : Word, IWeakPossessor
    {


        /// <summary>
        /// Initialiazes a new instance of the PossessivePronoun class.
        /// </summary>
        /// <param name="text">The literal text content of the PossessivePronoun.</param>
        public PossessivePronoun(string text)
            : base(text) {
        }

        public virtual void AddPossession(IEntity possession) {
            if (PossessesFor != null) {
                PossessesFor.AddPossession(possession);
            } else if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
        }



        public virtual IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }

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
