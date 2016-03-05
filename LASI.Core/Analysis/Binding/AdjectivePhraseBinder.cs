using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

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
        public static void Bind(Sentence sentence)
        {
            GetPossibilities(sentence).InvokeAll();
        }

        private static IEnumerable<Action> GetPossibilities(Sentence sentence) =>
            from adjectival in sentence.Phrases
                .OfAdjectivePhrase()
                .Concat(sentence.Clauses
                .SelectMany(c => c.Phrases.OfAdjectivePhrase().Reverse()
                .Take(1)))
            where adjectival.IsCoClausalWith(adjectival.Next)
            select new Action(() =>
            {
                adjectival.Next.Match().Case((NounPhrase n) => n.BindDescriptor(adjectival));
            });
    }
}
