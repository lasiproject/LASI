using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    public interface IEntity : IActionObject, IActionSubject, IPronounBindable, IDescribable, IPossesser, IPossessable, IEquatable<IEntity>, ILexical
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
