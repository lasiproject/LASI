
using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        /// <param name="text">The literal text content of the w.</param>
        public Adverb(string text)
            : base(text) {
        }
        /// <summary>
        /// Gets or sets the w or entity which the Adverb modiffies
        /// </summary>
        public virtual IVerbial Modiffied {
            get;
            set;
        }

    }
}
