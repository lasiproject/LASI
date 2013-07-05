

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role reqirements for Transitive elements, generally Verbs or VerbPhrases,
    /// which can be bound to one or more Direct objects and to one or more Indirect objects .
    /// Along with the other interfaces in the Syntactic Interfaces Library,
    /// the IVerbal interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IVerbal : ILexical, ISubjectTaker, IDirectObjectTaker, IInderectObjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        void AttachObjectViaPreposition(IPrepositional prepositional);
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

        bool HasSubject();
        bool HasSubject(System.Func<IEntity, bool> predicate);
        bool HasDirectObject();
        bool HasDirectObject(System.Func<IEntity, bool> predicate);
        bool HasIndirectObject();
        bool HasIndirectObject(System.Func<IEntity, bool> predicate);
        bool HasObject();
        bool HasObject(System.Func<IEntity, bool> predicate);
    }
}
