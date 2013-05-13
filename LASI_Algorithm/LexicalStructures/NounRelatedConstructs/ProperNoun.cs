using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Provides the base class and functionality for classes which represent Proper Nouns.
    /// </summary>
    /// <seealso cref="ProperSingularNoun"/>
    /// <seealso cref="ProperPluralNoun"/>
    public abstract class ProperNoun : Noun
    {
        /// <summary>
        /// Initializes entity new instances of the ProperNoun class.
        /// </summary>
        /// <param name="text">The literal text content of the ProperNoun</param>
        protected ProperNoun(string text)
            : base(text) {
            EntityKind = Algorithm.EntityKind.ProperUnknown;
        }

    }
}
