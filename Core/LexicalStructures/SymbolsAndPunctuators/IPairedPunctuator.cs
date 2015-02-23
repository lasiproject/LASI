using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the structure of Punctuator elements which are used as bookending delimiters, e.g. '(...)' and '[...], </para>
    /// <para> and are thus paired with another instance of the a Punctuator Word of the same type. </para>
    /// </summary>
    /// <typeparam name="TPunctuator">The Type of a Punctuation Word. This type must itself implement the
    /// IPairedPunctuator&lt;TPunctuator&gt; interface.</typeparam>
    /// <remarks>Because The Type Parameter, TPunctuator is invariant and must itself implement IPairedPunctuator&lt;TPunctuator&gt; this interface defines a reflexive, recursive Type.
    /// </remarks>
    /// <seealso cref="DoubleQuote">An useful, and illustrative implementation.</see>
    /// <seealso cref="SingleQuote">An useful, and illustrative implementation.</seealso>
    public interface IPairedPunctuator<TPunctuator> where TPunctuator : IPairedPunctuator<TPunctuator>
    {
        /// <summary>
        /// Binds one IPairedPunctuator&lt;TPunctuator&gt; to another IPairedPunctuator&lt;TPunctuator&gt;, establishing a reflexive link between the two.
        /// </summary>
        /// <param name="complement">A matching IPairedPunctuator&lt;TPunctuator&gt; with which to bind.</param>
        void PairWith(TPunctuator complement);
        /// <summary>
        /// Gets the IPairedPunctuator&lt;TPunctuator&gt; 
        /// which, together with the current instance, bookends some lexical content.
        /// </summary>
        TPunctuator PairedWith { get; }

    }

}
