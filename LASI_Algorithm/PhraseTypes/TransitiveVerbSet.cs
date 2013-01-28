using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class TransitiveVerbSet : VerbList
    {

        public TransitiveVerbSet(IEnumerable<TransitiveVerb> elements)
            : base(elements) {
            tVElements = elements;
        }



        private IEnumerable<TransitiveVerb> tVElements;
    }
}
