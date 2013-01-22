using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ThesaurusParsingTest
{
    [Serializable]
    class ThesuarusNotYerLoadedException : Exception
    {
        public ThesuarusNotYerLoadedException(string message)
            : base(message) {
        }
        public ThesuarusNotYerLoadedException(string message, Exception innerException)
            : base(message, innerException) {
        }
        public ThesuarusNotYerLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
