using System;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Pronoun elements, generally Pronouns or Pronoun Phrases, which can be bound to indirectly refer to an Entity.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IPronoun interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IPronoun : IEntity
    {

        /// <summary>
        /// Gets the Entity which the IPronoun references.
        /// </summary>
        IEntityGroup BoundEntity {
            get;
        }
        /// <summary>
        /// Binds the IPronoun to refer to the given Entity.
        /// </summary>
        /// <param name="target">The entity to which to bind.</param>
        void BindToEntity(IEntity target);
        /// <summary>
        /// Indicates wether or not the IPronoun is bound to an Entity.
        /// </summary>
        bool IsBound {
            get;
        }
    }
}
