
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Entity constructs, including Nouns, NounPhrases, and Gerunds. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IEntity interface provides for generalization and abstraction over many otherwise disparate element types and Type heirarchies. </para>
    /// </summary>
    public interface IEntity : IVerbalSubject, IVerbalObject, IReferenceable, IDescribable, IPossesser, IPossessable, ILexical
    {
        /// <summary>
        /// Gets the EntityKind; Person, Place, Thing, Organization, or Activity, associated with the Entity.
        /// </summary>
        EntityKind EntityKind { get; }
    }
}
