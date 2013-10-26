using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    public interface IDeterminable
    {
        Determiner Determiner {
            get;
        }
        void BindDeterminer(Determiner determiner);
    }
}
