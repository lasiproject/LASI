using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the role requirements for Entity type constructs, including Nouns, NounPhrases, and Gerunds. 
    /// Along with the other interfaces in the Syntactic Interfaces Library, the IEntity interface provides for generalization and abstraction over many otherwise disparate element types and type heirarchies.
    /// </summary>
    public interface IEntity : IVerbialObject, IVerbialSubject, IPronounBindable, IDescribable, IPossesser, IPossessable, IEquatable<IEntity>, ILexical
    {
        /// <summary>
        /// Gets or sets the Entity Kind; Person, Place, Thing, Organization, or Activity, associated with the Entity.
        /// </summary>
        EntityKind EntityKind {
            get;
        }
        /// <summary>
        /// Gets or sets the Theme Member kind; subject or object, associated with the Entity.
        /// </summary>
        EntityThemeMemberKind ThemeMemberKind {
            get;
            set;
        }
    }
}
