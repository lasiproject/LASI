using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
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
        /// <param name="text">The text content of the ProperNoun</param>
        protected ProperNoun(string text)
            : base(text) => EntityKind = EntityKind.ProperUnknown;
        /// <summary>
        /// Gets a value indicating if the ProperNoun is Lexically equal to a personal name. Known First and Last names are considered.
        /// </summary>
        public bool IsPersonalName => IsFirstName || IsLastName;
        internal bool IsLastName => Lexicon.IsLastName(this);
        internal bool IsFirstName => Lexicon.IsFirstName(this);
    }
}
