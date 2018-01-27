using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for constructs; generally Nouns, NounPhrases or Pronouns; which are semantically capable of "possessing" other Entities.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IPossessor interface provides for cross-axial generalization over lexical types.
    /// </summary>
    public interface IPossessor : ILexical
    {
        /// <summary>
        /// Gets all of the IEntity constructs which the <see cref="IPossessor"/> "owns".
        /// </summary>
        IEnumerable<IPossessable> Possessions
        {
            get;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of IEntity instances the IPossessor "Owns",
        /// and sets its owner to be the <see cref="IPossessor"/>.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        void AddPossession(IPossessable possession);
    }
}
