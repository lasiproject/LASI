using System;
using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
using StreamingContext = System.Runtime.Serialization.StreamingContext;

namespace LASI.Content.Tagging
{
    /// <summary>
    /// The Exception that is thrown when attempting to access the tag corresponding to a
    /// <see cref="Core.Word" /> whose type is not mapped by the Tagset.
    /// </summary>
    /// <seealso cref="WordTagsetMap" />
    /// <seealso cref="SharpNLPWordTagsetMap" />
    [Serializable]
    public sealed class UnmappedWordTypeException : UnmappedLexicalTypeException
    {
        /// <summary>
        /// Initializes a new instance of the UnmappedWordTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        public UnmappedWordTypeException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the UnmappedWordTypeException class with the given type information.
        /// </summary>
        /// <param name="wordType">The type of the unmapped word.</param>
        /// <param name="tagsetType">The type of the tagset in which the word is unmapped.</param>
        public UnmappedWordTypeException(Type wordType, Type tagsetType) : base(wordType, tagsetType) { }

        /// <summary>
        /// Initializes a new instance of the UnmappedWordTypeException class with its message
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
        public UnmappedWordTypeException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the UnmappedLexicalTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        private UnmappedWordTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}