using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public interface IEntityReferencer : IEntity
    {
        IEntity BoundEntity {
            get;
            set;
        }
    }
}
