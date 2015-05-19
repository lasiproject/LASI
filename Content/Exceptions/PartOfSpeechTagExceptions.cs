namespace LASI.Content.Tagging
{
    using Exception = System.Exception;
    using SerializableAttribute = System.SerializableAttribute;
    using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
    using StreamingContext = System.Runtime.Serialization.StreamingContext;

    /// <summary>
    /// The Exception that is thrown when attempting to parse a Word Tag with no associated text.
    /// Likely indicates a Tagger error.
    /// </summary>
    [Serializable]
    public class EmptyOrWhiteSpaceStringTaggedAsWordException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the BlankWordException class.
        /// </summary>
        /// <param name="tagGivenToBlankWord">
        /// The Word Tag that was associated with a blank or empty piece of text.
        /// </param>
        public EmptyOrWhiteSpaceStringTaggedAsWordException(string tagGivenToBlankWord) : base($"An piece of whitespace was annotated with a Word Tag. Tag: {tagGivenToBlankWord}")
        {
        }

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
        public EmptyOrWhiteSpaceStringTaggedAsWordException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected EmptyOrWhiteSpaceStringTaggedAsWordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// The Exception that is thrown when attempting to parse empty Phrase Tag
    /// </summary>
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
        public EmptyPhraseTagException(string phraseText) : base($"The tag for phrase: {phraseText} is empty")
        {
        }

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
        public EmptyPhraseTagException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EmptyPhraseTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private EmptyPhraseTagException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// The Exception that is thrown when attempting to parse an empty Word Tag
    /// </summary>
    [Serializable]
    public sealed class EmptyWordTagException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with its message string set to message.
        /// </summary>
        /// <param name="wordText">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        public EmptyWordTagException(string wordText) : base($"The tag for word: {wordText} is empty")
        {
        }

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
        public EmptyWordTagException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EmptyTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private EmptyWordTagException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Base of the tag parsing exception hierarchy. Cannot be instantiated and thus cannot be
    /// explicitly thrown If one encounters an exception not suited for one of its derived types, a
    /// new exception class should be derived from this class.
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
        protected PartOfSpeechTagException(string message) : base(message)
        {
        }

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
        protected PartOfSpeechTagException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the POSTagException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected PartOfSpeechTagException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
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
        public UndelimitedPhraseException(string message) : base(message)
        {
        }

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
        public UndelimitedPhraseException(string message, Exception inner) : base(message, inner)
        {
        }

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
        private UndelimitedPhraseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// The Exception that is thrown when attempting to parse an unknown clause tag.
    /// </summary>
    [Serializable]
    public sealed class UnknownClauseTypeException : UnknownPartOfSpeechException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownClauseTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        public UnknownClauseTypeException(string message) : base(message)
        {
        }

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
        public UnknownClauseTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UnknownClauseTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private UnknownClauseTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// The Exception that is thrown when attempting to parse an unknown Phrase Tag
    /// </summary>
    [Serializable]
    public sealed class UnknownPhraseTagException : UnknownPartOfSpeechException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownPhraseTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="posTagString">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        public UnknownPhraseTagException(string posTagString) : base($"The phrase tag {posTagString}\nis not defined by the TagSet")
        {
        }

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
        public UnknownPhraseTagException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UnknownPhraseTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private UnknownPhraseTagException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// The Exception that is thrown when attempting to parse an unknown Word Tag
    /// </summary>
    [Serializable]
    public sealed class UnknownWordTagException : UnknownPartOfSpeechException
    {
        /// <summary>
        /// Initializes a new instance of the UnknownPOSException class with its message string set
        /// to message.
        /// </summary>
        /// <param name="posTagString">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        public UnknownWordTagException(string posTagString) : base($"The Word Level Tag \"{posTagString}\" is not defined by the TagSet")
        {
        }

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
        public UnknownWordTagException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UnknownPOSException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private UnknownWordTagException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    /// <summary>
    /// The Exception that is thrown when attempting to parse an unknown part of speech.
    /// </summary>
    [Serializable]
    public class UnknownPartOfSpeechException : PartOfSpeechTagException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownPartOfSpeechException"/> class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        public UnknownPartOfSpeechException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownPartOfSpeechException"/> class with its message
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
        public UnknownPartOfSpeechException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownPartOfSpeechException"/> class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected UnknownPartOfSpeechException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
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
        public UntaggedPhraseException(string phraseText) : base($"The word level token: {phraseText} has no tag")
        {
        }

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
        public UntaggedPhraseException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private UntaggedPhraseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
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
        public UntaggedWordException(string wordText) : base($"The word level token: {wordText} has no tag")
        {
        }

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
        public UntaggedWordException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UntaggedElementException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private UntaggedWordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}