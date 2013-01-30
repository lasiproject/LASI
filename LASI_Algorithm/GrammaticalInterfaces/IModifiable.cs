using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    public interface IModifiable
    {
        void ModifyWith(IAdverbial adv);
        List<IAdverbial> Modifiers {
            get;
        }
    }
}
