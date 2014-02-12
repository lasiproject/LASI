using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Provides the base class and functionality for classes which represent Generic Nouns.
    /// </summary>
    /// <seealso cref="CommonSingularNoun"/>
    /// <seealso cref="CommonPluralNoun"/>
    public abstract class CommonNoun : Noun
    {
        /// <summary>
        /// Initializes a new instances of the GenericNoun class.
        /// </summary>
        /// <param name="text">The key text content of the GenericNoun</param>
        protected CommonNoun(string text)
            : base(text) {
            EntityKind = LASI.Core.EntityKind.Thing;
        }
    }
}
