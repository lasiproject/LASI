using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// A specialization of the Adjective class, SuperlativeAdjective represents adjectives such as "greenest" and "best".
    /// </summary>
    public class SuperlativeAdjective : Adjective
    {
        /// <summary>
        /// Initializes a new instance of the SuperalitiveAdjective class
        /// </summary>
        /// <param name="text">The key text content of the Adjective.</param>
        public SuperlativeAdjective(string text)
            : base(text) {
        }
    }
}