using System;
using System.Runtime.Serialization;

namespace LASI.Interop
{
    /// <summary>
    /// Represents errors that occur during application execution.To browse the .NET Framework source code for this type, see the Reference Source. 
    /// </summary>
    [Serializable]
    public class AlreadyConfiguredException : Exception
    {
        public AlreadyConfiguredException() : base(AlreadyConfiguredErrorMessage)
        {
        }

        public AlreadyConfiguredException(string message) : base(message)
        {
        }

        public AlreadyConfiguredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        AlreadyConfiguredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        const string AlreadyConfiguredErrorMessage = "LASI Configuration has already been Initialized. Initialize may only be called once.";
    }
}
