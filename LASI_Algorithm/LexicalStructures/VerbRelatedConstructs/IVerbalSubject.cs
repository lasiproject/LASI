
namespace LASI.Algorithm.SyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Action Subjects, generally the subjects of Verbs or VerbPhrases.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IVerbalSubject interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IVerbalSubject
    {
        ITransitiveVerbal SubjectOf {
            get;
            set;
        }
    }
}
