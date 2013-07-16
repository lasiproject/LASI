using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// The Exception to be thrown if and when an attempt is to bind a Subordinate clause as a modifer of some element when it has already been bound as a different kind of modifer.
    /// </summary>
    [Serializable]
    public class ConflictingClauseRoleException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ConflictingClauseRoleException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        public ConflictingClauseRoleException(string message)
            : base(message) {

        }
        /// <summary>
        /// Initializes a new instance of the ConflictingClauseRoleException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected ConflictingClauseRoleException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {

        }
    }
}
