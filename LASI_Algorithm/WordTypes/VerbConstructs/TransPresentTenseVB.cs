using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public class TransPresentTenseVB : TransitiveVerb
    {
        /// <summary>
        /// Initializes a new instance of the TransPresentTenseVerb class.        
        /// </summary>
        /// <param name="text">The literal text content of the TransPresentTenseVerb.</param>
        public TransPresentTenseVB(string text)
            : base(text) {
        }
    }
}
