using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LASI.Core
{
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
            if (string.Compare(text, "the", true, System.Globalization.CultureInfo.InvariantCulture) == 0)
                DeterminerKind = DeterminerKind.Definite;
            else
                DeterminerKind = DeterminerKind.Indefinite;
        }
        /// <summary>
        /// Gets or sets the Entity Determined by the Determiner.
        /// </summary>
        public virtual IEntity Determines {
            get;
            set;
        }
        /// <summary>
        /// Gets the DeterminerKind value of the current instance.
        /// </summary>
        public DeterminerKind DeterminerKind {
            get;
            protected set;
        }

    }
}
