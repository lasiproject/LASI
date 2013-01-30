using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public class GenericPluralNoun : Noun, IQuantifiable
    {
        public GenericPluralNoun(string text)
            : base(text) {
        }



        public Quantifier Quantifier {
            get;
            set;
        }
    }
}
