using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LASI.Utilities;

namespace LASI.Core.Binding
{
    /// <summary>
    /// The Exception which is thrown when an attempt is made to perform VerbPhrase related binding over a sequence of Phrases which contain no VerbPhrases.
    /// </summary>
    [Serializable]
    public class VerblessPhrasalSequenceException : Exception
    {
        public IEnumerable<ILexical> Sequence { get; protected set; }

        /// <summary>
        /// Gets each element in the Lexical sequence which caused the caused the exception. Elements are keyed by index.
        /// </summary>
        public override IDictionary Data => Sequence?.WithIndex().ToDictionary(e => e.Index, e => e.Element);
        /// <summary>
        /// Initializes a new instance of the VerblessPhrasalSequenceException with a default message indicating that the sequence contained no contained no Verb Phrases.
        /// </summary>
        public VerblessPhrasalSequenceException(IEnumerable<Phrase> sequence) : this(sequence, "No verb phrases in sequence") { }

        /// <summary>
        /// Initializes a new instance of the VerblessPhrasalSequenceException with the problematic sequence and a message indicating that the sequence contained no Verb Phrases.
        /// </summary>
        public VerblessPhrasalSequenceException(IEnumerable<Phrase> sequence, string message) : base(message) { Sequence = sequence; }

        [Obsolete("Do not instantiate with this constructor.\nPlease use: new VerblessPhrasalSequenceException(IEnumerable<Phrase>)", true)]
        public VerblessPhrasalSequenceException(string message) : base(message) { }
        [Obsolete("Do not instantiate with this constructor.\nPlease use: new VerblessPhrasalSequenceException(IEnumerable<Phrase>)", true)]
        public VerblessPhrasalSequenceException(string message, Exception innerException) : base(message, innerException) { }

        protected VerblessPhrasalSequenceException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}