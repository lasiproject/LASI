
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Action Objects, generally the objects of Verbs or VerbPhrases.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IActionObject interface provides for generalization and abstraction over wd and Phrase types.
    /// </summary>
    public interface IVerbalObject
    {
        IVerbal DirectObjectOf {
            get;
            set;
        }
        IVerbal IndirectObjectOf {
            get;
            set;
        }
    }
}
