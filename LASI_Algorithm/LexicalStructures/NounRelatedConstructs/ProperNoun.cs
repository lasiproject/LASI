using LASI.Algorithm.Lookup;
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
        /// Initializes a new instances of the ProperNoun class.
        /// </summary>
        /// <param name="text">The key text content of the ProperNoun</param>
        protected ProperNoun(string text)
            : base(text) {
            EntityKind = EntityKind.ProperUnknown;
        }

        internal bool IsPersonalName {
            get {
                return IsFirstName || IsLastName;
            }
        }
        internal bool IsLastName { get { return LexicalLookup.IsLastName(this); } }
        internal bool IsFirstName { get { return LexicalLookup.IsFirstName(this); } }
    }
}
