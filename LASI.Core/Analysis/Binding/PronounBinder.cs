using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core.Binding
{
    /// <summary>
    /// Attempts to bind pronouns to the entities they refer to.
    /// </summary>
    public static class PronounBinder
    {
        /// <summary>
        /// Attempts to perform pronoun binding within the given Sentence.
        /// </summary>
        /// <param name="sentence">The Sentence over which to perform pronoun binding.</param>
        public static void Bind(Sentence sentence)
        {
            BindPosessivePronouns(sentence.Phrases);
        }


        /// <summary>
        /// Binds possessive pronouns located in the sequence of phrases sentence. 
        /// Example Sentence that this applies to:
        /// "LASI binds its pronouns."
        /// Pronoun "its" binds to the proper noun "LASI"
        /// </summary>
        /// <param name="phrases">The sequence of phrases to bind within.</param>
        private static void BindPosessivePronouns(IEnumerable<Phrase> phrases)
        {
            foreach (var vp in phrases.OfVerbPhrase().WithObject(o => o is IWeakPossessor))
            {
                var pronouns = vp.DirectObjects.Concat(vp.IndirectObjects).OfType<IWeakPossessor>();
                foreach (var pro in pronouns)
                {
                    pro.PossessesFor = new AggregateEntity(vp.Subjects).ToOption();
                }
            }
        }
    }

}
