using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public interface IPossesser
    {
        ICollection<IEntity> Possessed {
            get;

        }
    }
}
