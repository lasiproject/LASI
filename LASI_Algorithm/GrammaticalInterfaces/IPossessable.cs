using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.DataRepresentation
{
    public interface IPossessable
    {
        IEntity Possesser {
            get;
            set;
        }
    }
}
