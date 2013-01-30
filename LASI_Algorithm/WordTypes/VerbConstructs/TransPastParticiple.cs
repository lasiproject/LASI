using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    /// <summary>
    /// Represents a transitive past participle verb.   
    /// </summary>
    public class TransPastPrtcpl : TransitiveVerb
    {
        public TransPastPrtcpl(string text)
            : base(text) {
        }
    }
}
