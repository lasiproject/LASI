using System;
using System.Collections.Generic;
using LASI.Utilities;

namespace LASI.Core
{

    /// <summary>
    /// Represents an adjective phrase which describes an Entity such as a Noun or NounPhrase construct.
    /// </summary>
    public class AdjectivePhrase : Phrase, IDescriptor, IAdverbialModifiable
    {
        /// <summary>
        /// Gets the collection of Adverbial constructs which modify the AdjectivePhrase
        /// </summary>
        public IEnumerable<IAdverbial> AttributedBy => AdverbialModifiers;
        /// <summary>
        /// Gets the Entity the AdjectivePhrase modifies.
        /// </summary>
        public IEntity AttributedTo => Describes;
        /// <summary>
        /// Initializes a new instance of the AdjectivePhrase class.
        /// </summary>
        /// <param name="words">The words which compose to form the AdjectivePhrase.</param>
        public AdjectivePhrase(IEnumerable<Word> words) : base(words) { }
        /// <summary>
        /// Initializes a new instance of the AdjectivePhrase class.
        /// </summary>
        /// <param name="first">The first Word of the AdjectivePhrase.</param>
        /// <param name="rest">The rest of the Words comprise the AdjectivePhrase.</param>
        /// <remarks>This constructor overload reduces the syntactic overhead associated with the manual construction of Phrases. 
        /// Thus, its purpose is to simplifiy test code.</remarks>
        public AdjectivePhrase(Word first, params Word[] rest) : this(rest.Prepend(first)) { }
        /// <summary>
        /// Attaches an Adverbial construct, such as an Adverb or AdverbPhrase, as a modifier of the AdjectivePhrase.
        /// </summary>
        /// <param name="modifier">The Adverbial construct by which to modify the AdjectivePhrase.</param>
        public virtual void ModifyWith(IAdverbial modifier) {
            modifiers.Add(modifier);
            modifier.Modifies = this;
        }


        /// <summary>
        /// Gets the collection of Adverbial constructs which modify the AdjectivePhrase.
        /// </summary>
        public virtual IEnumerable<IAdverbial> AdverbialModifiers { get { return modifiers; } }
        /// <summary>
        /// Gets the Entity which the AdjectivePhrase describes.
        /// </summary>
        public virtual IEntity Describes { get; set; }

        private ISet<IAdverbial> modifiers = new HashSet<IAdverbial>();
    }
}
