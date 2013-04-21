
namespace LASI.Algorithm.FundamentalSyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Action Objects, generally the objects of Verbs or VerbPhrases.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IActionObject interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IVerbialObject
    {
        ITransitiveVerbial DirectObjectOf {
            get;
            set;
        }
        ITransitiveVerbial IndirectObjectOf {
            get;
            set;
        }
    }
}
