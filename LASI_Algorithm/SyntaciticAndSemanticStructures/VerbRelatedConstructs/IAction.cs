namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Intransitive elements, generally Verbs or VerbPhrases.
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IAction interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IAction : ILexical, ISubjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        void AttachObjectViaPreposition(IPrepositional prep);

        
        ILexical ObjectViaPreposition {
            get;
        }
        Clause GivenExposition {
            get;
            set;
        }
    }
}
