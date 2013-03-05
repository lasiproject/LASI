
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Action Objects, generally the objects of Verbs or VerbPhrases.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IActionObject interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IActionObject
    {
        ITransitiveAction DirectObjectOf {
            get;
            set;
        }
        ITransitiveAction IndirectObjectOf {
            get;
            set;
        }
    }
}
