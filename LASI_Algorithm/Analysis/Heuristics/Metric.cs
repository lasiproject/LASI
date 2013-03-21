using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Heuristics
{
    public struct ResultSet
    {
        public IEnumerable<IEntity> TopEntities {
            get;
            set;
        }
        public IEnumerable<ITransitiveVerbial> TopActions {
            get;
            set;
        }
        public IEnumerable<ILexical> TopMiscelaneous {
            get;
            set;
        }
    }
}
