using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    public interface IPossesser
    {
        IEnumerable<IEntity> Possessed {
            get;
        }
        void AddPossession(IEntity possession);
    }
}
