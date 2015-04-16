namespace LASI.Core
{
    /// <summary>
    /// <para>
    /// Defines the role requirements for Action Subjects, generally the subjects of Verbs or VerbPhrases.
    /// </para>
    /// <para>
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IVerbalSubject
    /// interface provides for generalization and abstraction over word and Phrase types.
    /// </para>
    /// </summary>
    public interface IVerbalSubject
    {
        /// <summary>Gets or sets the IVerbal construct the IVerbalSubject is the subject of.</summary>
        IVerbal SubjectOf
        {
            get; set;
        }
    }
}