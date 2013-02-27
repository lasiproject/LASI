using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Algorithm
{
    public interface IEntity : IActionObject, IActionSubject, IPronounBindable, IDescribable, IPossesser, IPossessable, IEquatable<IEntity>, ILexical
    {
        EntityKind EntityKind {
            get;
        }
    }
}
