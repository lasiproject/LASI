
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for Entity Noun constructs, including Nouns, NounPhrases, and Gerunds. 
    /// Along with the second interfaces in the Syntactic Interfaces Library, the IEntity interface provides for generalization and abstraction over many otherwise disparate element types and Noun heirarchies.
    /// </summary>
    public interface IEntity : IVerbalObject, IVerbalSubject, IPronounBindable, IDescribable, IPossesser, IPossessable, ILexical
    {
        /// <summary>
        /// Gets or sets the Entity Noun; Person, Place, Thing, Organization, or Activity, associated with the Entity.
        /// </summary>
        EntityKind EntityKind
        {
            get;
        }
    }
}
