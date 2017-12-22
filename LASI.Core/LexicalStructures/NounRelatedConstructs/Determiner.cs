using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LASI.Core
{
    using LASI.Utilities;
    using static DeterminerKind;
    /// <summary>
    /// Represents a Determiner word such as "the" or "a"
    /// </summary>
    public class Determiner : Word
    {
        /// <summary>
        /// Initializes a new instance of the Determiner class.
        /// </summary>
        /// <param name="text">The text content of the Determiner.</param>
        public Determiner(string text)
            : base(text) {
            // TODO: Improve this to handle contextual cases. Need to analyse tagger output first.

            DeterminerKind = text.EqualsIgnoreCase("the") ? Definite : Indefinite;
        }
        /// <summary>
        /// Gets or sets the Entity Determined by the Determiner.
        /// </summary>
        public IEntity Determines { get; set; }
        /// <summary>
        /// The DeterminerKind value of the current instance.
        /// </summary>
        public DeterminerKind DeterminerKind { get; }
    }
}
