using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        internal UnmappedWordTypeException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the UnmappedWordTypeException class with the given type information.
        /// </summary>
        /// <param name="wordType">The type of the unmapped word.</param>
        /// <param name="tagsetType">The type of the tagset in which the word is unmapped.</param>
        internal UnmappedWordTypeException(Type wordType, Type tagsetType) : base(wordType, tagsetType) { }

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
        internal UnmappedWordTypeException(string message, Exception inner) : base(message, inner) { }

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
        internal UnmappedPhraseTypeException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the UnmappedPhraseTypeException class with the given type information.
        /// </summary>
        /// <param name="phraseType">The type of the unmapped word.</param>
        /// <param name="tagsetType">The type of the tagset in which the word is unmapped.</param>
        internal UnmappedPhraseTypeException(Type phraseType, Type tagsetType) : base(phraseType, tagsetType) { }

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
        internal UnmappedPhraseTypeException(string message, Exception inner) : base(message, inner) { }

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

    /// <summary>
    /// The Exception that is thrown when attempting to access the tag corresponding to a
    /// <see cref="Core.ILexical" /> whose type is not mapped by the Tagset.
    /// </summary>
    /// <seealso cref="WordTagsetMap" />
    /// <seealso cref="SharpNLPWordTagsetMap" />
    /// <seealso cref="PhraseTagsetMap" />
    /// <seealso cref="SharpNLPPhraseTagsetMap" />
    [Serializable]
    public abstract class UnmappedLexicalTypeException : ArgumentException
    {
        /// <summary>
        /// The <see cref="System.Type"/> of the Tagset which does not provide a mapping for the type.
        /// </summary>
        public Type TagsetType
        {
            get;
        }
        /// <summary>
        /// The <see cref="System.Type"/> not mapped by the Tagset.
        /// </summary>
        public Type UnmappedType
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the UnmappedLexicalTypeException class with its message
        /// string set to message.
        /// </summary>
        /// <param name="message">
        /// A description of the error. The content of message is intended to be understood by humans.
        /// </param>
        protected UnmappedLexicalTypeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UnmappedLexicalTypeException class with the given types.
        /// </summary>
        /// <param name="unmappedType">The type of the unmapped ILexical instance.</param>
        /// <param name="tagsetType">The type of the tagset in which the word is unmapped.</param>
        protected UnmappedLexicalTypeException(Type unmappedType, Type tagsetType)
            : this($"The Lexical type {unmappedType} is not mapped by the TagsetMap type {tagsetType}.")
        {
            UnmappedType = unmappedType;
            TagsetType = tagsetType;
        }

        /// <summary>
        /// Initializes a new instance of the UnmappedLexicalTypeException class with its message
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
        protected UnmappedLexicalTypeException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the UnmappedLexicalTypeException class with the serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being thrown.
        /// </param>
        protected UnmappedLexicalTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}