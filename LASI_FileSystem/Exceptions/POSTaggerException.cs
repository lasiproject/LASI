using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// Thrown when attempting to parse an unknown word Tag
    /// </summary>
    [Serializable]
    class UnknownPOSException : POSTagException
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
    /// Thrown when attempting to parse an unknown phrase Tag
    /// </summary>
    [Serializable]
    class UnknownPhraseTypeException : POSTagException
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
    /// Thrown when attempting to parse an untagged word
    /// </summary>
    [Serializable]
    class UntaggedElementException : POSTagException
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
    /// <see cref="TagsetMap"/>
    /// <seealso cref="SharpNLPTagsetMap"/>
    /// </summary>
    [Serializable]
    class UnmappedWordConstructorException : POSTagException
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
    class EmptyTagException : POSTagException
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
    /// Thrown when attempting to parse an improperly delimited phrase
    /// </summary>
    [Serializable]
    class UndelimitedPhraseException : POSTagException
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
    /// If one encounters an exception not suited for one of its derrived types, a new exception class should be derrived from it
    /// </summary>
    [Serializable]
    abstract class POSTagException : Exception
    {

        public POSTagException(string message)
            : base(message) {
        }
        public POSTagException(string message, Exception inner)
            : base(message, inner) {
        }
        public POSTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {

        }


    }
}
