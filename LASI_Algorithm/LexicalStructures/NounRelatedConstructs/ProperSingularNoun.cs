using LASI.Algorithm.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Proper Singular Noun.
    /// </summary>
    public class ProperSingularNoun : ProperNoun, ISimpleGendered
    {
        /// <summary>
        /// Initialiazes a new instance of the ProperSingularNoun class.
        /// </summary>
        /// <param name="text">The key text content of the ProperSingularNoun.</param>
        public ProperSingularNoun(string text)
            : base(text) {
            EntityKind = text.All(c => char.IsUpper(c)) ? EntityKind.Organization : EntityKind;
        }
        private Gender? gender;
        /// <summary>
        /// Gets the likely gender of the ProperNoun.
        /// </summary>
        public virtual Gender Gender { get { gender = gender ?? LexicalLookup.DetermineGender(this); return gender.Value; } }
    }

}
