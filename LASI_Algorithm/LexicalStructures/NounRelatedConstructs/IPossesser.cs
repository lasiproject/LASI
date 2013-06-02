using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.SyntacticInterfaces
{
    /// <summary>
    /// Defines the role requirements for constructs; generally Nouns, Nounphrases or Pronouns; which are semantically capable of "possessing" second Entities.
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IPossesser interface provides for generalization and abstraction over word and Phrase types.
    /// </summary>
    public interface IPossesser
    {
        IEnumerable<IEntity> Possessed {
            get;
        }
        void AddPossession(IEntity possession);
    }
}
