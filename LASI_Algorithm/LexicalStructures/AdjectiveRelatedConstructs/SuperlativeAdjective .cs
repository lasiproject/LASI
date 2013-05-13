using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// entity specialization of the Adjective class, SuperlativeAdjective represents adjectives such as "greenest" and "best".
    /// </summary>
    public class SuperlativeAdjective : Adjective
    {
        /// <summary>
        /// Initializes entity new instance of the SuperalitiveAdjective class
        /// </summary>
        /// <param name="text">The literal text content of the verb.</param>
        public SuperlativeAdjective(string text)
            : base(text) {
        }
    }
}