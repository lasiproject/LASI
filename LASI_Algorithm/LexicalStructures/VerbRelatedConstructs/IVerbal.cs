namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Intransitive elements, generally Verbs or VerbPhrases.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IVerbal interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IVerbal : ILexical, ISubjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        void AttachObjectViaPreposition(IPrepositional prep);


        ILexical ObjectViaPreposition {
            get;
        }


        ILexical GivenExposition {
            get;
        }
        IPrepositional PrepositionLinkingTarget {
            get;
        }
    }
}
