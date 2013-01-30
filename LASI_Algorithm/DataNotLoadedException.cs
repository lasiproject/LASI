using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.DataRepresentation
{
    [Serializable]
    public class DataNotLoadedException : Exception
    {

        public DataNotLoadedException(string message)
            : base(message) {

        }
        public DataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
