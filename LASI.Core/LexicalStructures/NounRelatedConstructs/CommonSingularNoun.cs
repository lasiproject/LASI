using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Represents a generic, non-proper, singular noun.
    /// </summary>
    public class CommonSingularNoun : CommonNoun
    {
        /// <summary>
        /// Initializes a new instance of the GenericSingularNounClass.
        /// </summary>
        /// <param name="text">The text content of the noun.</param>
        public CommonSingularNoun(string text)
            : base(text) => EntityKind = EntityKind.Thing;
    }
}
