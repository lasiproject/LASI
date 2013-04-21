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
    public class ProperSingularNoun : ProperNoun
    {
        /// <summary>
        /// Initialiazes a new instance of the ProperSingularNoun class.
        /// </summary>
        /// <param name="text">The literal text content of the ProperSingularNoun.</param>
        public ProperSingularNoun(string text)
            : base(text) {
            EntityKind = text.All(c => char.IsUpper(c)) ? Algorithm.EntityKind.Organization : EntityKind;
        }


    }

}
