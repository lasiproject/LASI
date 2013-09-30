using LASI.Algorithm.CoreDataStructures.LexicalStructures.MiscLanguageConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a single single quote within a passage of text. 
    /// </summary>
    public class SingleQuote : QuotationMark<SingleQuote>
    {
        /// <summary>
        /// Initializes a new instance of the DoubleQuote class.
        /// </summary>
        public SingleQuote() : base('\'') { }
        /// <summary>
        /// Pairs one SingleQuote with another SingleQuote, establishing a reflexive link between the two.
        /// </summary>
        /// <param name="complement">A matching SingleQuote with which to pair.</param>
        public override void PairWith(SingleQuote complement)
        {
            this.PairedInstance = complement;
            complement.PairedInstance = this;
        }

    }

}
