using LASI.Utilities;
using System.Collections.Generic;
using System.Collections.Immutable;


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
            : base(text)
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Adds a possession to the collection of items this instance possesses.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        public void AddPossession(IPossessable possession)
        {
            PossessesFor = PossessesFor ?? PreviousWord as IEntity;
            PossessesFor?.AddPossession(possession);
            possessions = possessions.Add(possession);
        }
        /// <summary>
        /// Returns a string representation of the PossessiveEnding.
        /// </summary>
        /// <returns>A string representation of the PossessiveEnding.</returns>
        public override string ToString() => base.ToString() + (VerboseOutput ? $"\n\t\t\tSignifying {PossessesFor} as owner of {Possessions.Format(e => e.Text)}" : string.Empty);

        #region Properties

        /// <summary>
        /// Gets or sets the possessing the Entity the PosssessiveEnding is attached to.
        /// When this property is set, ownership of all possessions associated with the PossessiveEnding is transferred to the target Entity.
        /// </summary>
        public IPossesser PossessesFor
        {
            get => possessesFor;
            set
            {
                possessesFor = value;
                if (possessesFor != null)
                {
                    foreach (var possession in possessions)
                    {
                        possessesFor.AddPossession(possession);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the possessables which the PossessiveEnding indicates are owned.
        /// </summary>
        public IEnumerable<IPossessable> Possessions => possessions;

        #endregion



        #endregion

        #region Fields

        private IImmutableSet<IPossessable> possessions = ImmutableHashSet<IPossessable>.Empty;
        private IPossesser possessesFor;

        #endregion

    }
}
