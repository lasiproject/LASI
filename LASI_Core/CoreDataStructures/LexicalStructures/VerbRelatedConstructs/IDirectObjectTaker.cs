
namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role reqirements for Transitive elements, generally Verbs or VerbPhrases, which can be bound to one or more Direct objects. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IDirectObjectTaker interface provides for generalization and abstraction over word and Phrase types. </para>
    /// </summary>
    public interface IDirectObjectTaker
    {
        /// <summary>
        /// Binds an IEntity construct as a direct object of the IDirectObjectTaker.
        /// </summary>
        /// <param name="directObject">The IEntity to bind as a direct object.</param>
        void BindDirectObject(IEntity directObject);
        /// <summary>
        /// Gets the sequence of IEntity constructs which are direct objects of the IDirectObjectTaker.
        /// </summary>
        System.Collections.Generic.IEnumerable<IEntity> DirectObjects {
            get;
        }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the IDirectObjectTaker's directobjects.
        /// </summary>
        IAggregateEntity AggregateDirectObject { get; }

    }

}
