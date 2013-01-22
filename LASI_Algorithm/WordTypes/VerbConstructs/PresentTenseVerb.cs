using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class PresentTenseVerb : Verb
    {
        public PresentTenseVerb(string text)
            : base(text, VerbTense.SingularPresent) {
        }
    }
}
