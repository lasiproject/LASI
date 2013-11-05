
namespace LASI.Core
{
    /// <summary>
    /// Defines the role reqirements for Action elements, generally Verbs or VerbPhrases, which can be bound to a one or more subjects.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the ISubjectTaker interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface ISubjectTaker
    {

        /// <summary>
        /// Binds the given IEntity as a subject of the ISubjectTaker instance.
        /// </summary>
        /// <param name="subject">The IEntity to attach to the ISubjectTaker as a subject.</param>
        void BindSubject(IEntity subject);

        /// <summary>
        /// Gets the collection of IEntity constructs which are bound as subjects of the ISubjetTaker.
        /// </summary>
        System.Collections.Generic.IEnumerable<IEntity> Subjects {
            get;
        }
        /// <summary>
        /// Gets an IAggregateEntity implementation composed from all of the ISubjectTaker's subjects.
        /// </summary>
        IAggregateEntity AggregateSubject { get; }

    }
}