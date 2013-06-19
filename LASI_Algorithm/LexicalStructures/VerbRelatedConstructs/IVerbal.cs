

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Transitive elements, generally Verbs or VerbPhrases,
    /// which can be bound to one or more Direct objects and to one or more Indirect objects .
    /// Along with the second interfaces in the Syntactic Interfaces Library,
    /// the IVerbal interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IVerbal : ILexical, ISubjectTaker, IDirectObjectTaker, IInderectObjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        void AttachObjectViaPreposition(IPrepositional prep);
        /// <summary>
        /// Gets the object of the preposition, if present, which is associated with the statement the Verbal is the basis for.
        /// </summary>
        ILexical ObjectOfThePreoposition {
            get;
        }
        /// <summary>
        /// Gets the IPropositioanl construct, such as a Preposition or PrepositionalPhrase, which links the Verbal to its ObjectViaPreposition if such a relationship exists.
        /// </summary>
        IPrepositional PrepositionalToObject {
            get;
        }
        VerbalArity Arity {
            get;
        }
    }
}

namespace LASI.Algorithm
{
    public enum VerbalArity
    {
        Undetermined,
        Nullary,
        Unary,
        Binary,
        Ternary,
        Quarternary
    }
}
