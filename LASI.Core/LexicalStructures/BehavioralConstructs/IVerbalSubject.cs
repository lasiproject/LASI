namespace LASI.Core
{
    /// <summary>
    /// <para>
    /// Defines the role requirements for Action Subjects, generally the subjects of <see cref="Verb"/>s or <see cref="VerbPhrase"/>s.
    /// </para>
    /// <para>
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IVerbalSubject
    /// interface provides for generalization and abstraction over word and Phrase types.
    /// </para>
    /// </summary>
    public interface IVerbalSubject
    {
        /// <summary>
        /// Binds the <see cref="IVerbalSubject"/> as a subject of the <see cref="IVerbal"/>.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> to which to bind.</param>
        void BindAsSubjectOf(IVerbal verbal);

        /// <summary>Gets the <see cref="IVerbal"/> construct the <see cref="IVerbalSubject"/> is the subject of.</summary>
        IVerbal SubjectOf { get; }
    }
}