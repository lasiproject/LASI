using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a Present Particple Verb Phrase, a phrase with the syntactic role of a verb and a Noun.
    /// </summary>
    public class PresentPraticipleVPhrase : VerbPhrase, IActionSubject, IActionObject
    {

        /// <summary>
        /// Initializes a new instance of the PresentPraticipleVPhrase class.
        /// </summary>
        /// <param name="composedWords">The words which compose to form the PresentPraticipleVPhrase.</param>
        public PresentPraticipleVPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }
        /// <summary>
        /// Gets the IAction instance, generally a Verb or VerbPhrase, which the Present Participle Verb Phrase is the direct object of.     
        /// </summary>
        public ITransitiveAction DirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the IAction instance, generally a Verb or VerbPhrase, which the Present Participle Verb Phrase is the indirect object of.     
        /// </summary>
        public ITransitiveAction IndirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the IAction instance, generally a Verb or VerbPhrase, which the Present Participle Verb Phrase is the subject of.     
        /// </summary>
        public IAction SubjectOf {
            get;
            set;
        }
    }
}
