using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using LASI.Algorithm;
namespace LASI.GuiInterop
{
    interface IWordSerializer
    {
        void Serialize();

        Task<string> SerializeAsync();
        IEnumerable<Word> WordData {
            set;
        }
        IEnumerable<string> WordSerializations {
            get;
        }

    }
}
