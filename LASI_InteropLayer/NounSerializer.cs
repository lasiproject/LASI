using LASI.GuiInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.InteropLayer
{
    public class NounSerializer : IWordSerializer
    {

        public void Serialize() {
            if (WordData == null) {
                throw new WordSerializationException("No data was provided for serialization");
            }
        }

        public Task<string> SerializeAsync() {
            throw new NotImplementedException();
        }

        public IEnumerable<Algorithm.Word> WordData {
            protected get;
            set;
        }

        public IEnumerable<string> WordSerializations {
            get {
                throw new NotImplementedException();
            }
        }
    }
}
