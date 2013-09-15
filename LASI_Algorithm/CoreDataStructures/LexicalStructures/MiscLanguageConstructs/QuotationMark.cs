using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public abstract class DelimitingPunctuator<T> : Punctuation where T : DelimitingPunctuator<T>
    {
        protected DelimitingPunctuator(char punctuationChar) : base(punctuationChar) { }
        public abstract T PairedDelimiter { get; set; }
    }
    public class QuotationMark : DelimitingPunctuator<QuotationMark>
    {
        public QuotationMark()
            : base('"') {
        }



        public override QuotationMark PairedDelimiter {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
