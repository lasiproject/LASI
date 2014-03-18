using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// <para>  Defines the role requirements for constructs; generally Nouns, NounPhrases or Pronouns; which are semantically capable of "possessing" other Entities. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IPossesser interface provides for generalization and abstraction over word and Phrase types. </para>
    /// </summary>
    public interface IPossesser : ILexical
    {
        /// <summary>
        /// Gets all of the IEntity constructs which the IPossesser "owns".
        /// </summary>
        IEnumerable<IPossessable> Possessed {
            get;
        }
        /// <summary>
        /// Adds an IPossessible construct, such as a person place or thing, to the collection of IEntity instances the IPossesser "Owns",
        /// and sets its owner to be the IPossesser.
        /// If the item is already possessed by the current instance, this method has no effect.
        /// </summary>
        /// <param name="possession">The possession to add.</param>
        void AddPossession(IPossessable possession);
    }
}
