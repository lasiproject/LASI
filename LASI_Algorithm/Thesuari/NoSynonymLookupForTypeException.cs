using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesuari
{
    [Serializable]
    public class NoSynonymLookupForTypeException : ArgumentException
    {
        public NoSynonymLookupForTypeException(ILexical unsupported)
            : base(string.Format("Thesaurus Operations are Not Supported for Words of type {0}\n{1}", unsupported.Type, unsupported)) {
        }
        public NoSynonymLookupForTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
