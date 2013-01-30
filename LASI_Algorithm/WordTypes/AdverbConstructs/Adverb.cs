
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents an adverb which can be bound as a modiffier to either a verb construct or an adjective construct.
    /// </summary>
    public class Adverb : Word, IAdverbial
    {
        /// <summary>
        /// Initializes a new instance of the Adverb class.
        /// </summary>
        /// <param name="text">The literal text content of the word.</param>
        public Adverb(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the word or phrase which the Adverb modiffies
        /// </summary>
        public virtual IModifiable Modiffied {
            get;
            set;
        }
    }
}
