using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.SyntacticInterfaces
{
    /// <summary>
    /// Defines the role requirements for constructs; generally Nouns, Nounphrases or Pronouns; which are "possessable" by second Entities.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IPossessable interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IPossessable
    {
        /// <summary>
        /// Gets or sets the Entity which has been inferred as the "owner" of the IPossessable.
        /// </summary>
        IEntity Possesser {
            get;
            set;
        }
    }
}
