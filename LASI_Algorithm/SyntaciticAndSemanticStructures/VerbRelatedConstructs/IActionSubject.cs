
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Action Subjects, generally the subjects of Verbs or VerbPhrases.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IActionSubject interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IActionSubject
    {
        ITransitiveAction SubjectOf {
            get;
            set;
        }
    }
}
