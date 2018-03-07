using System;
using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
using StreamingContext = System.Runtime.Serialization.StreamingContext;

namespace LASI.Content.Exceptions
{

    /// <summary>
    /// The Exception that is thrown when attempting to access the tag corresponding to a
    /// <see cref="Core.Phrase" /> whose type is not mapped by the Tagset.
    /// </summary>
    [Serializable]
    public sealed class UnmappedPhraseTypeException : UnmappedLexicalTypeException
    {
        /// <summary>
        /// Initializes a new instance of the UnmappedPhraseTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        public UnmappedPhraseTypeException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the UnmappedPhraseTypeException class with the given type information.
        /// </summary>
        /// <param name="phraseType">The type of the unmapped word.</param>
        /// <param name="tagsetType">The type of the tagset in which the word is unmapped.</param>
        public UnmappedPhraseTypeException(Type phraseType, Type tagsetType) : base(phraseType, tagsetType) { }

        /// <summary>
        /// Initializes a new instance of the UnmappedPhraseTypeException class with its message
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
        public UnmappedPhraseTypeException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the UnmappedPhraseTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private UnmappedPhraseTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}