using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Core.ComparativeHeuristics
{
    /// <summary>
    /// The Exception to be thrown if and when an attempt is made to lookup a word of a syntactic category which has no corresponding thesaurus.
    /// </summary>
    [Serializable]
    public sealed class NoSynonymLookupForTypeException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the NoSynonymLookupForTypeException class.
        /// </summary>
        /// <param name="unsupported">The ILexical Type which was looked up</param>
        public NoSynonymLookupForTypeException(ILexical unsupported)
            : base(string.Format("Thesaurus Operations are Not Supported for Words of type {0}\n{1}", unsupported.Type, unsupported)) {
        }
        private NoSynonymLookupForTypeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception to be thrown if and when an attempt is made to lookup a word before the thesaurus corresponding its type has been loaded.
    /// </summary>
    [Serializable]
    public abstract class WordDataNotLoadedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the WordDataNotLoadedException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        protected WordDataNotLoadedException(string message)
            : base(message) {
        }
        /// <summary>
        /// Initializes a new instance of the WordDataNotLoadedException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        protected WordDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the WordDataNotLoadedException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected WordDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception to be thrown if and when an attempt is made to lookup a Noun before the NounThesaurus has been loaded.
    /// </summary>
    [Serializable]
    public class NounDataNotLoadedException : WordDataNotLoadedException
    {
        /// <summary>
        /// Initializes a new instance of the NounDataNotLoadedException class with its message string set to message.
        /// </summary> 
        public NounDataNotLoadedException()
            : base("An attempt was made to access Noun data before loading could complete") {
        }
        /// <summary>
        /// Initializes a new instance of the NounDataNotLoadedException class.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public NounDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the NounDataNotLoadedException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected NounDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception to be thrown if and when an attempt is made to lookup a Verb before the VerbThesaurus has been loaded.
    /// </summary>
    [Serializable]
    public class VerbDataNotLoadedException : WordDataNotLoadedException
    {
        /// <summary>
        /// Initializes a new instance of the VerbDataNotLoadedException class.
        /// </summary>
        public VerbDataNotLoadedException()
            : base("An attempt was made to access Verb data before loading could complete") {
        }
        /// <summary>
        /// Initializes a new instance of the VerbDataNotLoadedException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public VerbDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the VerbDataNotLoadedException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected VerbDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception to be thrown if and when an attempt is made to lookup an Adjective before the AdjectiveThesaurus has been loaded.
    /// </summary>
    [Serializable]
    public class AdjectiveDataNotLoadedException : WordDataNotLoadedException
    {
        /// <summary>
        /// Initializes a new instance of the AdjectiveDataNotLoadedException class.
        /// </summary> 
        public AdjectiveDataNotLoadedException()
            : base("An attempt was made to access Adjective data before loading could complete") {
        }
        /// <summary>
        /// Initializes a new instance of the AdjectiveDataNotLoadedException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public AdjectiveDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the AdjectiveDataNotLoadedException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected AdjectiveDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception to be thrown if and when an attempt is made to lookup an Adverb before the AdverbThesaurus has been loaded.
    /// </summary>
    [Serializable]
    public class AdverbDataNotLoadedException : WordDataNotLoadedException
    {
        /// <summary>
        /// Initializes a new instance of the AdverbDataNotLoadedException class.
        /// </summary> 
        public AdverbDataNotLoadedException()
            : base("An attempt was made to access Adverb data before loading could complete") {
        }
        /// <summary>
        /// Initializes a new instance of the AdverbDataNotLoadedException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param> 
        public AdverbDataNotLoadedException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the AdverbDataNotLoadedException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected AdverbDataNotLoadedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
}
