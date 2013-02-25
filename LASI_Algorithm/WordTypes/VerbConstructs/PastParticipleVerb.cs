using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class PastParticipleVerb : Verb
    {
        public PastParticipleVerb(string text)
            : base(text, VerbTense.PastParticiple) {
        }
    }
}
