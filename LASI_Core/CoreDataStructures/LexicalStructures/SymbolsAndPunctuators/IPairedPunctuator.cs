using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Defines the structure of Punctuation Words which are used as bookending delimiters, e.g. '(...)' and '[...], 
    /// and are thus paired with another instance of the a Punctuation Word of the same type.
    /// </summary>
    /// <typeparam name="T">The Type of a Punctution Word. This type must be a Punction Word which itself implements the
    /// IPairedPunctuator&lt;T&gt; interface.</typeparam>
    public interface IPairedPunctuator<T> where T : IPairedPunctuator<T>
    {
        /// <summary>
        /// Binds one IPairedPunctuator&lt;T&gt; to another IPairedPunctuator&lt;T&gt;, establishing a reflexive link between the two.
        /// </summary>
        /// <param name="complement">A matching IPairedPunctuator&lt;T&gt; with which to bind.</param>
        void PairWith(T complement);
        /// <summary>
        /// Gets the IPairedPunctuator&lt;T&gt; 
        /// which, together with the current instance, bookends some lexical content.
        /// </summary>
        T PairedInstance { get; }

    }

}
