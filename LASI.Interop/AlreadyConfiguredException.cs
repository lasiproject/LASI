using System;
using System.Runtime.Serialization;

namespace LASI.Interop
{
    [Serializable]
    public sealed class AlreadyConfiguredException : Exception
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

        private AlreadyConfiguredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        private const string AlreadyConfiguredErrorMessage = "LASI Configuration has already been Initialized. Initialize may only be called once.";
    }
}