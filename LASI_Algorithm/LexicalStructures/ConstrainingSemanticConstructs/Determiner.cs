using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LASI.Algorithm
{
    /// <summary>
    /// Represents leftNPDeterminer words such as "the" and "a"
    /// </summary>
    public class Determiner : Word
    {
        /// <summary>
        /// Initializes a new instance of the Determiner class.
        /// </summary>
        /// <param name="text">the key text content of the word.</param>
        public Determiner(string text)
            : base(text) {
            if (string.Compare(text, "the", true) == 0)
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
        public DeterminerKind DeterminerKind {
            get;
            protected set;
        }

    }
}
