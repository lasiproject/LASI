using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.DataRepresentation
{
    public interface IEntity : IActionObject, IActionSubject, IReferenciable, IDescribable, IPossesser, IPossessable, IEquatable<IEntity>
    {

    }
}
