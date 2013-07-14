
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for Entity constructs, including Nouns, NounPhrases, and Gerunds. 
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IEntity interface provides for generalization and abstraction over many otherwise disparate element types and Type heirarchies.
    /// </summary>
    public interface IEntity : IVerbalSubject, IVerbalObject, IPronounBindable, IDescribable, IPossesser, IPossessable, ILexical
    {
        /// <summary>
        /// Gets the EntityKind; Person, Place, Thing, Organization, or Activity, associated with the Entity.
        /// </summary>
        EntityKind Kind {
            get;
        }
    }
}
