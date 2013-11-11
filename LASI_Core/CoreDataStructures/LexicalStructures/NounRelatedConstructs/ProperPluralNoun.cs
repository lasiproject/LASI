using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Core.Heuristics;

namespace LASI.Core
{

    /// <summary>
    /// Represents a Proper Plural Noun.
    /// </summary>
    public class ProperPluralNoun : ProperNoun, IQuantifiable
    {
        /// <summary>
        /// Initializes a new instance of the ProperPluralNoun class.
        /// </summary>
        /// <param name="text">The key text content of the ProperPluralNoun.</param>
        public ProperPluralNoun(string text)
            : base(text) {
            EntityKind = this.IsLastName() ? EntityKind.Person : EntityKind;
        }

        /// <summary>
        /// Gets or sets the Qunatifier which specifies the number of units of the ProperNoun which are referred to in this occurance.
        /// e.g. "[18] Pinkos"
        /// </summary>
        public override IQuantifier QuantifiedBy {
            get;
            set;
        }
    }
}
