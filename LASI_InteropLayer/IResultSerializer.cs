using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace LASI.GuiInterop
{
    class IResultSerializer
    {
        IEnumerable<ISerializationSurrogate> WordSerializations {
            get;
            set;
        }

    }
}
