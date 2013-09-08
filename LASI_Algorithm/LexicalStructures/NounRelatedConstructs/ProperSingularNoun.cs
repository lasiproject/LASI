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
    public class ProperSingularNoun : ProperNoun, IGendered
    {
        /// <summary>
        /// Initialiazes a new instance of the ProperSingularNoun class.
        /// </summary>
        /// <param name="text">The key text content of the ProperSingularNoun.</param>
        public ProperSingularNoun(string text)
            : base(text) {
            EntityKind = text.All(c => char.IsUpper(c) || c == '.') ? EntityKind.Organization : EntityKind;
        }
        private Gender? gender = null;
        /// <summary>
        /// Gets the Gender value indiciating the likely gender of the ProperNoun.
        /// </summary>
        public virtual Gender Gender {
            get {
                gender = gender ?? (this.IsFemaleFirstName() ? Gender.Female :
                    this.IsMaleFirstName() ? Gender.Male :
                    this.IsLastName() ? Gender.Neutral : Gender.Undetermined);
                return gender.Value;
            }
        }
    }

}
