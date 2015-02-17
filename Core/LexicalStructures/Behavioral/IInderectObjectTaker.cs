
using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Transitive elements, generally Verbs or VerbPhrases, which can be bound to one or more Indirect objects. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IInderectObjectTaker interface provides for generalization and abstraction over word and Phrase types. </para>
    /// </summary>
    public interface IInderectObjectTaker
    {
        /// <summary>
        /// Binds an IEntity construct as an indirect object of the IInderectObjectTaker.
        /// </summary>
        /// <param name="indirectObject">The IEntity to bind as an indirect object.</param>
        void BindIndirectObject(IEntity indirectObject);
        /// <summary>
        /// Gets the sequence of IEntity constructs which are indirect objects of the IInderectObjectTaker.
        /// </summary> 
        IEnumerable<IEntity> IndirectObjects
        {
            get;
        }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the IInderectObjectTaker's IndirectObjects.
        /// </summary>
        IAggregateEntity AggregateIndirectObject
        {
            get;
        }
    }
}
