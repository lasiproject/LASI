using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    [Serializable]
    public class ConflictingClauseRoleException : Exception
    {
        public ConflictingClauseRoleException(string message)
            : base(message) {

        }
        public ConflictingClauseRoleException(string message, Exception inner)
            : base(message, inner) {

        }
        public ConflictingClauseRoleException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {

        }
    }
}
