using System;
namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    [Serializable]
    public class MatchFailureException : Exception
    {
        public MatchFailureException(Delegate failedCase, string message) { }
        public MatchFailureException(string message) : base(message) { }
        public MatchFailureException(string message, Exception inner) : base(message, inner) { }
        protected MatchFailureException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
