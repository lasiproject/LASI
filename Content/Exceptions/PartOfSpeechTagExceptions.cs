using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Content.Tagging
{
    using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
    using StreamingContext = System.Runtime.Serialization.StreamingContext;

    /// <summary>The Exception that is thrown when attempting to parse an unknown Word Tag</summary>
    [Serializable]
    public sealed class UnknownWordTagException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownPOSException class with its message string set
        /// to message.
        /// </summary>
        /// <param name="posTagString">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        internal UnknownWordTagException(string posTagString)
            : base($"The Word Level Tag \"{posTagString}\" is not defined by the TagSet")
        { }

        /// <summary>
        /// Initializes a new instance of the UnknownPOSException class with its message string set
        /// to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        internal UnknownWordTagException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes a new instance of the UnknownPOSException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected UnknownWordTagException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>The Exception that is thrown when attempting to parse an unknown Phrase Tag</summary>
    [Serializable]
    public sealed class UnknownPhraseTagException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownPhraseTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="posTagString">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        public UnknownPhraseTagException(string posTagString)
            : base($"The phrase tag {posTagString}\nis not defined by the TagSet")
        { }

        /// <summary>
        /// Initializes a new instance of the UnknownPhraseTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        internal UnknownPhraseTagException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes a new instance of the UnknownPhraseTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected UnknownPhraseTagException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>
    /// The Exception that is thrown when attempting to parse a Word Tag with no associated text.
    /// Likely indicates a Tagger error.
    /// </summary>
    [Serializable]
    public class EmptyOrWhiteSpaceStringTaggedAsWordException : PartOfSpeechTagException
    {
        /// <summary>Initializes a new instance of the BlankWordException class.</summary>
        /// <param name="tagGivenToBlankWord">
        /// The Word Tag that was associated with a blank or empty piece of text.
        /// </param>
        public EmptyOrWhiteSpaceStringTaggedAsWordException(string tagGivenToBlankWord) : base($"An piece of whitespace was annotated with a Word Tag. Tag: {tagGivenToBlankWord}") { }

        /// <summary>
        /// Initializes a new instance of the EmptyOrWhiteSpaceStringTaggedAsWordException class
        /// with its message string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        public EmptyOrWhiteSpaceStringTaggedAsWordException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected EmptyOrWhiteSpaceStringTaggedAsWordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>The Exception that is thrown when attempting to parse an empty Word Tag</summary>
    [Serializable]
    public sealed class EmptyWordTagException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with its message string set to message.
        /// </summary>
        /// <param name="wordText">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        internal EmptyWordTagException(string wordText)
            : base($"The tag for word: {wordText} is empty")
        { }

        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        internal EmptyWordTagException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected EmptyWordTagException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>The Exception that is thrown when attempting to parse empty Phrase Tag</summary>
    [Serializable]
    public sealed class EmptyPhraseTagException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the EmptyPhraseTagException class with its message string
        /// set to message.
        /// </summary>
        /// <param name="phraseText">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        internal EmptyPhraseTagException(string phraseText) : base($"The tag for phrase: {phraseText} is empty")
        { }

        /// <summary>
        /// Initializes a new instance of the EmptyPhraseTagException class with its message string
        /// set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        internal EmptyPhraseTagException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the EmptyPhraseTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected EmptyPhraseTagException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// The Exception that is thrown when attempting to parse an untagged chunk of text as word.
    /// </summary>
    [Serializable]
    public sealed class UntaggedWordException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with its message string
        /// set to message.
        /// </summary>
        /// <param name="wordText">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        internal UntaggedWordException(string wordText) : base($"The word level token: {wordText} has no tag") { }

        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with its message string
        /// set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        internal UntaggedWordException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected UntaggedWordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// The Exception that is thrown when attempting to parse an untagged chunk of text as phrase.
    /// </summary>
    [Serializable]
    public sealed class UntaggedPhraseException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with its message string
        /// set to message.
        /// </summary>
        /// <param name="phraseText">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        internal UntaggedPhraseException(string phraseText) : base($"The word level token: {phraseText} has no tag") { }

        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with its message string
        /// set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        internal UntaggedPhraseException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected UntaggedPhraseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }



    /// <summary>
    /// The Exception that is thrown when attempting to parse an improperly delimited Phrase
    /// </summary>
    [Serializable]
    public sealed class UndelimitedPhraseException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the UndelimitedPhraseException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        internal UndelimitedPhraseException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the UndelimitedPhraseException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        internal UndelimitedPhraseException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the UndelimitedPhraseException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected UndelimitedPhraseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>The Exception that is thrown when attempting to parse an unknown clause tag.</summary>
    [Serializable]
    public sealed class UnknownClauseTypeException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownClauseTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        internal UnknownClauseTypeException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the UnknownClauseTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        internal UnknownClauseTypeException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the UnknownClauseTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected UnknownClauseTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Base of the tag parsing exception heirarchy. Cannot be instantiated and thus cannot be
    /// explicitely thrown If one encounters an exception not suited for one of its derrived types,
    /// a new exception class should be derrived from this class.
    /// </summary>
    [Serializable]
    public abstract class PartOfSpeechTagException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the POSTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        protected PartOfSpeechTagException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the POSTagException class with its message string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that handles the
        /// inner exception.
        /// </param>
        protected PartOfSpeechTagException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the POSTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected PartOfSpeechTagException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}