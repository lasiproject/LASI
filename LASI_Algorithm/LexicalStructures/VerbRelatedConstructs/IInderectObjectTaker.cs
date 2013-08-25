
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Transitive elements, generally Verbs or VerbPhrases, which can be bound to one or more Indirect objects.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IInderectObjectTaker interface provides for generalization and abstraction over word and Phrase types.
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
        System.Collections.Generic.IEnumerable<IEntity> IndirectObjects {
            get;
        }
        IAggregateEntity AggregateIndirectObject { get; }
    }
}
