using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.InteropLayer
{
    [Serializable]
    class WordSerializationException : Exception
    {
        public WordSerializationException(string message)
            : base(message) {
        }
        public WordSerializationException(string message, Exception inner)
            : base(message, inner) {
        }
        public WordSerializationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
