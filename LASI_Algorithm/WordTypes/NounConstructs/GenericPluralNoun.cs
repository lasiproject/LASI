using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class GenericPluralNoun : GenericNoun,IQuantifiable
    {
        public GenericPluralNoun(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets a Qunatifier which specifies the number of units of the GenericPluralNoun which are referred to in this occurance.
        /// E.g. "[five] miscreants"
        /// </summary>
        public virtual Quantifier Quantifier {
            get;
            set;
        }
    }
}
