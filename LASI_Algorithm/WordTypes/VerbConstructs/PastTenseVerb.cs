using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class PastTenseVerb : Verb
    {
        public PastTenseVerb(string text)
            : base(text, VerbTense.Past) {
        }
    }
}
