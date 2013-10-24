using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.ContentSystem
{
    /// <summary>
    /// The Exception that is thrown when attempting to parse an unknown Word Tag
    /// </summary>
    [Serializable]
    public sealed class UnknownWordTagException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownPOSException class with its message string set to message.
        /// </summary>
        /// <param name="posTagString">A description of the error. The content of message is intended to be understood by humans.</param>
        public UnknownWordTagException(string posTagString)
            : base(string.Format("The Word Level Tag \"{0}\" is not defined by the TagSet", posTagString)) {
        }
        /// <summary>
        /// Initializes a new instance of the UnknownPOSException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UnknownWordTagException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the UnknownPOSException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UnknownWordTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }

    }
    /// <summary>
    /// The Exception that is thrown when attempting to parse an unknown Phrase Tag
    /// </summary>
    [Serializable]
    public sealed class UnknownPhraseTagException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownPhraseTypeException class with its message string set to message.
        /// </summary>
        /// <param name="posTagString">A description of the error. The content of message is intended to be understood by humans.</param>
        public UnknownPhraseTagException(string posTagString)
            : base(string.Format("The phrase tag {0}\nis not defined by the TagSet", posTagString)) {
        }
        /// <summary>
        /// Initializes a new instance of the UnknownPhraseTypeException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UnknownPhraseTagException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the UnknownPhraseTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UnknownPhraseTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }

    }
    /// <summary>
    /// The Exception that is thrown when attempting to parse an empty Word Tag
    /// </summary>
    [Serializable]
    public class EmptyWordTagException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with its message string set to message.
        /// </summary>
        /// <param name="wordText">A description of the error. The content of message is intended to be understood by humans.</param>
        public EmptyWordTagException(string wordText)
            : base(string.Format("The tag for word: {0} is empty", wordText)) {
        }
        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public EmptyWordTagException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private EmptyWordTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception that is thrown when attempting to parse empty Phrase Tag
    /// </summary>
    [Serializable]
    public sealed class EmptyPhraseTagException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the EmptyPhraseTagException class with its message string set to message.
        /// </summary>
        /// <param name="phraseText">A description of the error. The content of message is intended to be understood by humans.</param>
        public EmptyPhraseTagException(string phraseText)
            : base(string.Format("The tag for phrase: {0} is empty", phraseText)) {
        }
        /// <summary>
        /// Initializes a new instance of the EmptyPhraseTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public EmptyPhraseTagException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the EmptyPhraseTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private EmptyPhraseTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }

    }
    /// <summary>
    /// The Exception that is thrown when attempting to parse an untagged word
    /// </summary>
    [Serializable]
    public sealed class UntaggedWordException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with its message string set to message.
        /// </summary>
        /// <param name="wordText">A description of the error. The content of message is intended to be understood by humans.</param>
        public UntaggedWordException(string wordText)
            : base(string.Format("The word level token: {0} has no tag", wordText)) {
        }
        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UntaggedWordException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UntaggedWordException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception that is thrown when attempting to parse an untagged phrase
    /// </summary>
    [Serializable]
    public sealed class UntaggedPhraseException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with its message string set to message.
        /// </summary>
        /// <param name="phraseText">A description of the error. The content of message is intended to be understood by humans.</param>
        public UntaggedPhraseException(string phraseText)
            : base(string.Format("The word level token: {0} has no tag", phraseText)) {
        }
        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UntaggedPhraseException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UntaggedPhraseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception that is thrown when attempting to access the indexing tag for a LASI.Algorithm.Word Type (or constructor returning it) which is not known to the Tagset.
    /// <see cref="LASI.ContentSystem.TaggerEncapsulation.WordTagsetMap"/>
    /// <seealso cref="LASI.ContentSystem.TaggerEncapsulation.SharpNLPWordTagsetMap"/>
    /// </summary>
    [Serializable]
    public sealed class UnmappedWordTypeException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the UnmappedWordConstructorException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        public UnmappedWordTypeException(string message)
            : base(message) {
        }
        /// <summary>
        /// Initializes a new instance of the UnmappedWordConstructorException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UnmappedWordTypeException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the UnmappedWordConstructorException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UnmappedWordTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception that is thrown when attempting to access the indexing tag for a LASI.Algorithm.Phrase Type (or constructor returning it) which is not known to the Tagset.
    /// </summary>
    [Serializable]
    public sealed class UnmappedPhraseTypeException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the UnmappedPhraseTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        public UnmappedPhraseTypeException(string message)
            : base(message) {
        }
        /// <summary>
        /// Initializes a new instance of the UnmappedPhraseTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UnmappedPhraseTypeException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the UnmappedPhraseTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UnmappedPhraseTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {

        }
    }
    /// <summary>
    /// The Exception that is thrown when attempting to parse an improperly delimited Phrase
    /// </summary>
    [Serializable]
    public sealed class UndelimitedPhraseException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the UndelimitedPhraseException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        public UndelimitedPhraseException(string message)
            : base(message) {
        }
        /// <summary>
        /// Initializes a new instance of the UndelimitedPhraseException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UndelimitedPhraseException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the UndelimitedPhraseException class with its message string set to message.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UndelimitedPhraseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception that is thrown when attempting to parse an unknown clause tag.
    /// </summary>
    [Serializable]
    public sealed class UnknownClauseTypeException : POSTagException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownClauseTypeException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        public UnknownClauseTypeException(string message)
            : base(message) {
        }
        /// <summary>
        /// Initializes a new instance of the UnknownClauseTypeException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UnknownClauseTypeException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the UnknownClauseTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UnknownClauseTypeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// Base of the tag parsing exception heirarchy.
    /// Cannot be instantiated and thus cannot be explicitely thrown
    /// If one encounters an exception not suited for one of its derrived types, a new exception class should be derrived from this class.
    /// </summary>
    [Serializable]
    public abstract class POSTagException : NotSupportedException
    {
        /// <summary>
        /// Initializes a new instance of the POSTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        protected POSTagException(string message)
            : base(message) {
        }
        /// <summary>
        /// Initializes a new instance of the POSTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood by humans.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        protected POSTagException(string message, Exception inner)
            : base(message, inner) {
        }
        /// <summary>
        /// Initializes a new instance of the POSTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected POSTagException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {

        }
    }
}
