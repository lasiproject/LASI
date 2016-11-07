using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Action elements, generally Verbs or VerbPhrases, which can be bound to a one or more subjects. </para>
    /// <para>
    /// Along with the other interfaces in the Syntactic Interfaces Library, the ISubjectTaker interface provides for cross-axial generalization over lexical types. </para>
    /// </summary>
    public interface ISubjectTaker
    {
        /// <summary>
        /// Binds the given IEntity as a subject of the ISubjectTaker.
        /// </summary>
        /// <param name="subject">The IEntity to attach to the ISubjectTaker as a subject.</param>
        void BindSubject(IEntity subject);

        /// <summary>
        /// Gets the collection of IEntity constructs which are bound as subjects of the ISubjetTaker.
        /// </summary>
        IEnumerable<IEntity> Subjects { get; }

        /// <summary>
        /// Gets the Aggregate Entity composed of the ISubjectTaker's subjects.
        /// </summary>
        IAggregateEntity AggregateSubject { get; }
        /// <summary>
        /// Gets or sets the SubjectComplement, a Lexical construct generally an IDescriptor or IEntity construct, which modifies the Subjects of the ISubjectTaker.
        /// </summary>
        // TODO: Refine the type of the Subject Complement property by unifying certain aspects of IEntity and IDescriptor and lifting them into a new Interface.
        ILexical SubjectComplement { get; set; }
    }
}