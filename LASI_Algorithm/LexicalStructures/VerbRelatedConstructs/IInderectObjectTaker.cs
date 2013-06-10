
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Transitive elements, generally Verbs or VerbPhrases, which can be bound to one or more Indirect objects.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IInderectObjectTaker interface provides for generalization and abstraction over wd and Phrase types.
    /// </summary>
    public interface IInderectObjectTaker
    {
        void BindIndirectObject(IEntity indirectObject);
        System.Collections.Generic.IEnumerable<IEntity> IndirectObjects
        {
            get;
        }
    }
}
