using LASI.Core.DocumentStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Binding
{
    /// <summary>
    /// A Binder which binds the AdjectivePhrases within a sentence to applicable phrase level entities.
    /// </summary>
    public static class AdjectivePhraseBinder
    {
        /// <summary>
        /// Binds the AdjectivePhrases within a sentence to applicable phrase level entities.
        /// </summary>
        /// <param name="sentence">The Sentence to bind within.</param>
        public static void Bind(Sentence sentence) {
            foreach (var bindingAction in GetPossibilities(sentence)) {
                bindingAction();
            }
        }

        private static IEnumerable<Action> GetPossibilities(Sentence sentence) {
            return from adjectival in sentence.Phrases.OfAdjectivePhrase().Concat(sentence.Clauses.SelectMany(c => c.Phrases.OfAdjectivePhrase().Reverse().Take(1)))
                   where adjectival.Clause == adjectival.NextPhrase.Clause
                   let described = adjectival.NextPhrase as NounPhrase
                   where described != null
                   select new Action(() => described.BindDescriptor(adjectival));
        }
    }
}
