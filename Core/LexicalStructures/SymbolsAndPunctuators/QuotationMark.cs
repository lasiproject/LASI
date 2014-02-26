using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Represents a quotation mark. Implementing the IPairedPunctuator interface, it provides for a pairing to the its complementary quotation mark type quote.
    /// </summary>
    public abstract class QuotationMark<TQuote> : Punctuator, IPairedPunctuator<TQuote>
    where TQuote : QuotationMark<TQuote>, IPairedPunctuator<TQuote>
    {
        /// <summary>
        /// Initializes a new instance of the QuotationMark class.
        /// </summary>
        /// <param name="punctuationSymbol">The literal textual of the punctuation word.</param>
        protected QuotationMark(char punctuationSymbol) : base(punctuationSymbol) { }
        /// <summary>
        /// Pairs one QuotationMark with another QuotationMark, establishing a reflexive link between the two.
        /// </summary>
        /// <param name="complement">A matching QuotationMark with which to pair.</param>
        public abstract void PairWith(TQuote complement);
        /// <summary>
        /// Gets the QuotationMark 
        /// which, together with the current instance, bookends some lexical content.
        /// </summary>
        public TQuote PairedWith {
            get;
            protected set;
        }
        /// <summary>
        /// Returns a string representation of the QuotationMark&lt;TQuote&gt;.
        /// </summary>
        /// <returns>A string representation of the QuotationMark&lt;TQuote&gt;.</returns>
        public override string ToString() {
            return base.ToString() + " Paired With " + (PairedWith != null ? (PairedWith.Text + PairedWith.ID) : string.Empty);
        }
    }
}
