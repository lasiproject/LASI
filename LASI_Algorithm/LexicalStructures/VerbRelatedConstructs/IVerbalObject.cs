
namespace LASI.Algorithm.SyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Action Objects, generally the objects of Verbs or VerbPhrases.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IActionObject interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IVerbalObject
    {
        ITransitiveVerbal DirectObjectOf {
            get;
            set;
        }
        ITransitiveVerbal IndirectObjectOf {
            get;
            set;
        }
    }
}
