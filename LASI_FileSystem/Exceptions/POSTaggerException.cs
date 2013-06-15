using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// Thrown when attempting to parse an unknown adverb Tag
    /// </summary>
    [Serializable]
    public class UnknownPOSException : POSTagException
    {
        public UnknownPOSException(string message)
            : base(message) {
        }
        public UnknownPOSException(string message, Exception inner)
            : base(message, inner) {
        }
        public UnknownPOSException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }

    }
    /// <summary>
    /// Thrown when attempting to parse an unknown Phrase Tag
    /// </summary>
    [Serializable]
    public class UnknownPhraseTypeException : POSTagException
    {
        public UnknownPhraseTypeException(string message)
            : base(message) {
        }
        public UnknownPhraseTypeException(string message, Exception inner)
            : base(message, inner) {
        }
        public UnknownPhraseTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }

    }

    /// <summary>
    /// Thrown when attempting to parse empty Phrase Tag
    /// </summary>
    [Serializable]
    public class EmptyPhraseTagException : POSTagException
    {
        public EmptyPhraseTagException(string message)
            : base(message) {
        }
        public EmptyPhraseTagException(string message, Exception inner)
            : base(message, inner) {
        }
        public EmptyPhraseTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }

    }

    /// <summary>
    /// Thrown when attempting to parse an untagged adverb
    /// </summary>
    [Serializable]
    public class UntaggedElementException : POSTagException
    {
        public UntaggedElementException(string message)
            : base(message) {
        }
        public UntaggedElementException(string message, Exception inner)
            : base(message, inner) {
        }
        public UntaggedElementException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// Thrown when attempting to access the key for an unmapped constructor value in the Tagset
    /// <see cref="WordTagsetMap"/>
    /// <seealso cref="SharpNLPWordTagsetMap"/>
    /// </summary>
    [Serializable]
    public class UnmappedWordConstructorException : POSTagException
    {
        public UnmappedWordConstructorException(string message)
            : base(message) {
        }
        public UnmappedWordConstructorException(string message, Exception inner)
            : base(message, inner) {
        }
        public UnmappedWordConstructorException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// Thrown when attempting to parse an empty Tag
    /// </summary>
    [Serializable]
    public class EmptyTagException : POSTagException
    {
        public EmptyTagException(string message)
            : base(message) {
        }
        public EmptyTagException(string message, Exception inner)
            : base(message, inner) {
        }
        public EmptyTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// Throw when attempting to parse an unknown clause tag.
    /// </summary>
    [Serializable]
    public class UnknownClauseTypeException : POSTagException
    {
        public UnknownClauseTypeException(string message)
            : base(message) {
        }
        public UnknownClauseTypeException(string message, Exception inner)
            : base(message, inner) {
        }
        public UnknownClauseTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// Thrown when attempting to parse an improperly delimited Phrase
    /// </summary>
    [Serializable]
    public class UndelimitedPhraseException : POSTagException
    {

        public UndelimitedPhraseException(string message)
            : base(message) {
        }
        public UndelimitedPhraseException(string message, Exception inner)
            : base(message, inner) {
        }
        public UndelimitedPhraseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// Base of the tag parsing exception heirarchy.
    /// Cannot be instantiated and thus cannot be explicitely thrown
    /// If one encounters an exception not suited for one of its derrived types, a new exception class should be derrived from this class.
    /// </summary>
    [Serializable]
    public abstract class POSTagException : Exception
    {

        protected POSTagException(string message)
            : base(message) {
        }
        protected POSTagException(string message, Exception inner)
            : base(message, inner) {
        }
        protected POSTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {

        }


    }
    [Serializable]
    public class UnmappedPhraseConstructorException : POSTagException
    {
        public UnmappedPhraseConstructorException(string message)
            : base(message) {
        }
        public UnmappedPhraseConstructorException(string message, Exception inner)
            : base(message, inner) {
        }
        public UnmappedPhraseConstructorException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {

        }


    }
}
