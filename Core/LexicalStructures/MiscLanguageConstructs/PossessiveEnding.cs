using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using LASI.Utilities;


namespace LASI.Core
{
    /// <summary>
    /// Represents a possessive ending such as ('s) which indicates that the Entity preceding it has a possessive relationship with respect to the Entity which follows it. 
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
        public void AddPossession(IPossessable possession) {
            PossessesFor = PossessesFor ?? PreviousWord as IEntity;
            if (PossessesFor != null) {
                PossessesFor.AddPossession(possession);
            }
            possessions = possessions.Add(possession);
        }
        /// <summary>
        /// Returns a string represntation of the PossessiveEnding.
        /// </summary>
        /// <returns>A string represntation of the PossessiveEnding.</returns>
        public override string ToString() {
            return base.ToString() + (VerboseOutput ? string.Format("\n\t\t\tSignifying {0} as owner of {1}", PossessesFor, Possessions.Format(e => e.Text)) : string.Empty);
        }

        #region Properties

        /// <summary>
        /// Gets or sets the possessing the Entity the PosssessiveEnding is attached to.
        /// When this property is set, ownership of all possessions associated with the PossessiveEnding is tranferred to the target IEntity.
        /// </summary>
        public IPossesser PossessesFor {
            get {
                return possessesFor;
            }
            set {
                possessesFor = value;
                if (value != null)
                    foreach (var possession in possessions)
                        possessesFor.AddPossession(possession);
            }
        }

        /// <summary>
        /// Gets the possessables which the PossessiveEnding indicates are owned.
        /// </summary>
        public IEnumerable<IPossessable> Possessions { get { return possessions; } }

        #endregion



        #endregion

        #region Fields

        private IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        private IPossesser possessesFor;

        #endregion

    }
}
