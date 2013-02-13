using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public interface IModifiable
    {
        void ModifyWith(IAdverbial adv);
        IEnumerable<IAdverbial> Modifiers {
            get;
        }
    }
}
