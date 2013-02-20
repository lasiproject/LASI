using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.StateMachines
{
    /// <summary>
    /// An input stream of Phrases. This provides forward only access to the Phrases.
    /// </summary>
    internal class InputPhraseStream
    {
        public InputPhraseStream(IEnumerable<Phrase> phrases) {
            phraseStack = new Stack<Phrase>(phrases);
        }
        public InputPhraseStream(Clause clause) {
            phraseStack = new Stack<Phrase>(clause.Phrases);

        }

        public InputPhraseStream(Sentence sentence) {
            phraseStack = new Stack<Phrase>(sentence.Phrases);
        }

        /// <summary>
        /// Gets the next Phrase from the input stream dynamically such that, at runtime, the type of the returned Phrase will be its actually, most derrived, type.
        /// </summary>
        /// <remarks>Gets the next phrase from the input stream as a dynamically typed object. Be extremely careful with this.
        /// In particular, do not reassign the result or attempt to access any of its Properties or Methods.
        /// Instead, make use of the return value by feeding it directly to an overloaded function which will access it as a normal object.
        /// </remarks>
        public dynamic Next {
            get {
                return phraseStack.Pop();
            }
        }
        Stack<Phrase> phraseStack;
    }

}
