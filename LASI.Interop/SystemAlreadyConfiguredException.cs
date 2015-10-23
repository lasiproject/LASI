using System;
using System.Runtime.Serialization;

namespace LASI.Interop
{
    [Serializable]
    public sealed class SystemAlreadyConfiguredException : Exception
    {
        public SystemAlreadyConfiguredException() : base(AlreadyConfiguredErrorMessage)
        {
        }

        public SystemAlreadyConfiguredException(string message) : base(message)
        {
        }

        public SystemAlreadyConfiguredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private SystemAlreadyConfiguredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        private const string AlreadyConfiguredErrorMessage = "Configuration has already been Initialized. Initialize may only be called once.";

    }
}