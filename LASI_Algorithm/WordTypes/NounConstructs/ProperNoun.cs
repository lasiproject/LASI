using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public abstract class ProperNoun : Noun
    {
        /// <summary>
        /// Initializes a new instances of the ProperNoun class.
        /// </summary>
        /// <param name="text">The literal text content of the ProperNoun</param>
        protected ProperNoun(string text)
            : base(text) {
        }
        
    }
}
