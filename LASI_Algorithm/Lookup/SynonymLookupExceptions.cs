using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm.Lookup
{
    /// <summary>
    /// The Exception to be thrown if and when an attempt is made to lookup a word of a syntactic category which has no corresponding thesaurus.
    /// </summary>
    [Serializable]
    public class NoSynonymLookupForTypeException : ArgumentException
    {
        public NoSynonymLookupForTypeException(ILexical unsupported)
            : base(string.Format("Thesaurus Operations are Not Supported for Words of type {0}\n{1}", unsupported.Type, unsupported)) {
        }
        public NoSynonymLookupForTypeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    [Serializable]
    public abstract class WordDataNotLoadedException : Exception
    {
        protected WordDataNotLoadedException(string message)
            : base(message) {
        }
        protected WordDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        protected WordDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    [Serializable]
    public class VerbDataNotLoadedException : WordDataNotLoadedException
    {
        public VerbDataNotLoadedException(string message)
            : base(message) {
        }
        public VerbDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        public VerbDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    [Serializable]
    public class NounDataNotLoadedException : WordDataNotLoadedException
    {
        public NounDataNotLoadedException(string message)
            : base(message) {
        }
        public NounDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        public NounDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    [Serializable]
    public class AdjectiveDataNotLoadedException : WordDataNotLoadedException
    {
        public AdjectiveDataNotLoadedException(string message)
            : base(message) {
        }
        public AdjectiveDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        public AdjectiveDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    [Serializable]
    public class AdverbDataNotLoadedException : WordDataNotLoadedException
    {
        public AdverbDataNotLoadedException(string message)
            : base(message) {
        }
        public AdverbDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        public AdverbDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
