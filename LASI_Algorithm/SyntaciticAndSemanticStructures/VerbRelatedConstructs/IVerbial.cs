using LASI.Algorithm.ClauseTypes;
using LASI.Algorithm.FundamentalSyntacticInterfaces;
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Intransitive elements, generally Verbs or VerbPhrases.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IVerbial interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IVerbial : ILexical, ISubjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        void AttachObjectViaPreposition(IPrepositional prep);


        ILexical ObjectViaPreposition {
            get;
        }


        ILexical GivenExposition {
            get;
        }
    }
}
