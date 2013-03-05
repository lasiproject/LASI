using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a possessive ending such as 'd which indicates that the noun it follows has a possessive relationship with respect to the following Entity
    /// </summary>
    public class PossessiveEnding : Word, IPossesser
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
            if (!_possessed.Contains(possession)) {
                possession.Possesser = AssociatedEntity;
                _possessed.Add(possession);
            }
        }

        #region Properties

        /// <summary>
        /// Gets or sets the possessing the entity the Posssessive ending is attached to.
        /// </summary>
        public IEntity AssociatedEntity {
            get;
            set;
        }

        /// <summary>
        /// Gets the entities the possessive ending has ownership of.
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
        #endregion


        public override System.Xml.Linq.XElement Serialize() {
            throw new NotImplementedException();
        }


    }
}
