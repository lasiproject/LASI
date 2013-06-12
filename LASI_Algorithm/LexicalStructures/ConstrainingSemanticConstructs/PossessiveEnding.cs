using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a possessive ending such as 's which indicates that the noun it follows has a possessive relationship with respect to the following Entity
    /// </summary>
    public class PossessiveEnding : Word, IWeakPossessor
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the possessive ending class.
        /// </summary>
        /// <param name="text">The literla  text content of the possessive ending.</param>
        public PossessiveEnding(string text)
            : base(text) {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Adds a possession to the collection of items this instance possesses.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IEntity possession) {

            if (PossessesFor != null) {
                PossessesFor.AddPossession(possession);
            } else if (!_possessed.Contains(possession)) {
                _possessed.Add(possession);
            }
        }


        #region Properties

        /// <summary>
        /// Gets or sets the possessing the Entity the Posssessive ending is attached to.
        /// </summary>
        public IEntity PossessesFor {
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

        /// <summary>
        /// Gets the describables the possessive ending has ownership of.
        /// </summary>
        public IEnumerable<IEntity> Possessed {
            get {
                return _possessed;
            }
        }

        #endregion



        #endregion

        #region Fields

        private ICollection<IEntity> _possessed = new List<IEntity>();
        private IEntity _possessedFor;

        #endregion





        IEnumerable<IEntity> IPossesser.Possessed {
            get {
                throw new NotImplementedException();
            }
        }


    }
}
