using System;
using System.Collections.Generic;

namespace LASI.Core
{
    /// <summary>
    /// Represents an adjective which can describe IDescribable such as a Noun or NounPhrase.
    /// </summary>
    public class Adjective : Word, IAdverbialModifiable, IDescriptor
    {
        public IEnumerable<IAdverbial> AttributedBy => AdverbialModifiers;
        public IEntity AttributedTo => Describes;

        /// <summary>
        /// Initializes a new instance of the Adjective class.
        /// </summary>
        /// <param name="text">The text content of the Adjective.</param>
        public Adjective(string text) : base(text) { }

        /// <summary>
        /// Gets or sets the Describable construct the Adjective describes
        /// </summary>
        public virtual IEntity Describes { get; set; }
        /// <summary>
        /// Binds a modifier to the Adjective, modifying it.
        /// </summary>
        /// <param name="modifier">The IModifier instance (probably an Adverb or AdverbPhrase) to Bind to the Adjective.</param>
        public virtual void ModifyWith(IAdverbial modifier) {
            modifiers.Add(modifier);
            modifier.ModifiedBy = this;
        }

        private ISet<IAdverbial> modifiers = new HashSet<IAdverbial>();

        /// <summary>
        /// Gets the collection of Adverbial constructs which modify the AdjectivePhrase
        /// </summary>
        public virtual IEnumerable<IAdverbial> AdverbialModifiers { get { return modifiers; } }

    }


}
