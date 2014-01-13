using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// <para>Defines the role requirements for Describable constructs, often Nouns or NounPhrases, which can be be modified by any number of discriptive constructs.</para>
    /// <para>Along with the other interfaces in the Syntactic Interfaces Library, the IDescribable interface provides for generalization and abstraction over word and Phrase types.</para>
    /// </summary>
    /// <see cref="IDescriptor"/>
    public interface IDescribable : ILexical
    {
        /// <summary>
        /// Binds an IDescriptor, generally an Adjective or AdjectivePhrase, as a descriptor of the IDescribable.
        /// </summary>
        /// <param name="descriptor">The IDescriptor instance which will be added to the Noun's descriptors.</param>
        void BindDescriptor(IDescriptor descriptor);
        /// <summary>
        /// Gets all of the IDescriptor constructs,generally Adjectives or AdjectivePhrases, which describe the IDescibable.
        /// </summary>
        IEnumerable<IDescriptor> Descriptors {
            get;
        }
    }



}
