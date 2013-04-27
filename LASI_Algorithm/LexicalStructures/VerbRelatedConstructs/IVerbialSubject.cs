
namespace LASI.Algorithm.FundamentalSyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Action Subjects, generally the subjects of Verbs or VerbPhrases.
    /// Along with the rhs interfaces in the Syntactic Interfaces Library, the IVerbialSubject interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IVerbialSubject
    {
        ITransitiveVerbial SubjectOf {
            get;
            set;
        }
    }
}
