using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Binding
{
    [Serializable]
    public class UnknownEntityCompatibleWordTypeException : Exception
    {
        public UnknownEntityCompatibleWordTypeException(Word word)
            : base(String.Format("{0} has Type : {1} which is incompatible with its usage", word.Text, word.GetType())) {

        }
        public UnknownEntityCompatibleWordTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {

        }
    }
}
