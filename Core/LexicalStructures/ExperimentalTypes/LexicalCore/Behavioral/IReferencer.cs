using System;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for IReferencer elements, generally Pronouns or Pronoun Phrases, which can be bound to indirectly refer to an Entity. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IReferencer interface provides for generalization and abstraction over word and Phrase types. </para> 
    /// </summary>
    public interface IReferencer : IEntity
    {
        /// <summary>
        /// Gets an aggregate entity which represents all entities to which the Referencer refers.
        /// </summary>
        IAggregateEntity RefersTo { get; }

        /// <summary>
        /// Binds the Referencer to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        void BindAsReferringTo(IEntity target);
    }
}