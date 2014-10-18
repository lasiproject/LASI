using System;

namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for Transitive elements, generally Verbs or VerbPhrases,
    /// which can be bound to one or more Direct objects and to one or more Indirect objects .
    /// Along with the other interfaces in the Syntactic Interfaces Library,
    /// the IVerbal interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IVerbal : ILexical, ISubjectTaker, IDirectObjectTaker, IInderectObjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        #region Methods

        /// <summary>
        /// Binds the IVerbal to the corresponding object of the preposition.
        /// </summary>
        /// <param name="prepositional">The IPrepositional construct whose object is the corresponding object of the preposition.</param>
        void AttachObjectViaPreposition(IPrepositional prepositional);
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one subject.
        /// </summary>
        /// <returns>True if the IVerbal has at least one subject; otherwise, false.</returns>
        bool HasSubject();
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one subject matching the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate to test each subject.</param>
        /// <returns>True if the IVerbal has at least one subject matching the provided predicate; otherwise, false.</returns>
        bool HasSubject(System.Func<IEntity, bool> predicate);
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one direct object.
        /// </summary>
        /// <returns>True if the IVerbal has at least one subject; otherwise, false.</returns>
        bool HasDirectObject();
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one direct object matching the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate to test each subject.</param>
        /// <returns>True if the IVerbal has at least one direct object matching the provided predicate; otherwise, false.</returns>
        bool HasDirectObject(Func<IEntity, bool> predicate);
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one indirect object.
        /// </summary>
        /// <returns>True if the IVerbal has at least one indirect object; otherwise, false.</returns>
        bool HasIndirectObject();
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one indirect object matching the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate to test each indirect object.</param>
        /// <returns>True if the IVerbal has at least one indirect object matching the provided predicate; otherwise, false.</returns>
        bool HasIndirectObject(System.Func<IEntity, bool> predicate);
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one direct or indirect object.
        /// </summary>
        /// <returns>True if the IVerbal has at least one object; otherwise, false.</returns>
        bool HasObject();
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one direct or indirect object matching the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate to test each object.</param>
        /// <returns>True if the IVerbal has at least one direct or indirect object  matching the provided predicate; otherwise, false.</returns>
        bool HasObject(System.Func<IEntity, bool> predicate);
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one subject, direct object, or indirect object.
        /// </summary>
        /// <returns>True if the IVerbal has at least one subject, direct object, or indirect object; otherwise, false.</returns>
        bool HasSubjectOrObject();
        /// <summary>
        /// Gets a value indicating if the IVerbal has at least one subject, direct object, or indirect object matching the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate to test each associated subject, direct object, or indirect object..</param>
        /// <returns>True if the IVerbal has at least one subject, direct object, or indirect object  matching the provided predicate; otherwise, false.</returns>
        bool HasSubjectOrObject(System.Func<IEntity, bool> predicate);

        #endregion

        #region Properties
        /// <summary>
        /// Gets the object of the preposition, if present, which is associated with the statement the Verbal is the basis for.
        /// </summary>
        ILexical ObjectOfThePreposition {
            get;
        }
        /// <summary>
        /// Gets the IPropositioanl construct, such as a Preposition or PrepositionalPhrase, which links the Verbal to its ObjectViaPreposition if such a relationship exists.
        /// </summary>
        IPrepositional PrepositionalToObject {
            get;
        }
        /// <summary>
        /// Gets a value indicating whether or not the IVerbal has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        bool IsClassifier { get; }
        /// <summary>
        /// Gets a value indicating whether or not the IVerbal has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        bool IsPossessive { get; }

        #endregion
    }
}
