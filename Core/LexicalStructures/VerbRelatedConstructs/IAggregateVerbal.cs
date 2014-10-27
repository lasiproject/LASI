using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    public interface IAggregateVerbal : IVerbal, IAggregateLexical<IVerbal>
    {
    }
}
