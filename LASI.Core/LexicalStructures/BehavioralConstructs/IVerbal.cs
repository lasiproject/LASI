﻿namespace LASI.Core
{
    /// <summary>
    /// Defines the role requirements for Transitive elements, generally Verbs or VerbPhrases, which can be bound to one or more Direct
    /// objects and to one or more Indirect objects . Along with the other interfaces in the Syntactic Interfaces Library, the IVerbal
    /// interface provides for cross-axial generalization over lexical types.
    /// </summary>
    public interface IVerbal :
        ILexical,
        ISubjectTaker,
        IObjectTaker,
        IDirectObjectTaker,
        IInderectObjectTaker,
        IAdverbialModifiable,
        IModalityModifiable,
        IAttributable<IVerbal, IAdverbial>
    {
        /// <summary>
        /// Binds the IVerbal to the corresponding object of the preposition.
        /// </summary>
        /// <param name="prepositional">The IPrepositional construct whose object is the corresponding object of the preposition.</param>
        void AttachObjectViaPreposition(IPrepositional prepositional);

        /// <summary>
        /// The object of the preposition, if present, which is associated with the statement the Verbal is the basis for.
        /// </summary>
        ILexical ObjectOfThePreposition { get; }

        /// <summary>
        /// Gets the IPropositioanl construct, such as a Preposition or PrepositionalPhrase, which links the Verbal to its
        /// ObjectViaPreposition if such a relationship exists.
        /// </summary>
        IPrepositional PrepositionalToObject { get; }

        /// <summary>
        /// Gets a value indicating whether or not the IVerbal has classifying semantics. E.g. "A (is) a B"
        /// </summary>
        bool IsClassifier { get; }

        /// <summary>
        /// Gets a value indicating whether or not the IVerbal has possessive semantics. E.g. "A (has) a B"
        /// </summary>
        bool IsPossessive { get; }
    }
}