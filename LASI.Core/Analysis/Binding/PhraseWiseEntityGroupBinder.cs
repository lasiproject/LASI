using System.Collections.Generic;
using System.Linq;

namespace LASI.Core {
    /// <summary>
    /// Attempts to collect semantically grouped entities within a source into aggregate objects.
    /// </summary>
    public class PhraseWiseEntityGroupBinder {
        /// <summary>
        /// Aggregates and binds the Phrase level IEntity constructs within the Sentence into
        /// instances aggregate objects which implement IEntityGroup.
        /// </summary>
        /// <param name="sentence">
        /// The Sentence to bind within.
        /// </param>
        public void Bind(Sentence sentence)
        {
            var betwixt = FindAllBetwixt(sentence);
            var aggregatedNounPhrases = new List<NounPhrase>();
            foreach (var b in betwixt)
            {
                var symbolicPhraseOrConjunctiveCount = b.TillNextNounPhrase.Count(isSymbolPhraseOrConjuctive);
                if (symbolicPhraseOrConjunctiveCount != b.TillNextNounPhrase.Count && b.TillNextNounPhrase.Count < 3)
                {
                    aggregatedNounPhrases.Add(b.NounPhrase);
                    if (aggregatedNounPhrases.Count > 2)
                    {
                        entityGroups.Add(new AggregateEntity(aggregatedNounPhrases));
                    }
                }
                else
                {
                    aggregatedNounPhrases = new List<NounPhrase>();
                }
            }

            bool isSymbolPhraseOrConjuctive(Phrase phrase) => phrase is IConjunctive || phrase is SymbolPhrase;
        }

        private static List<NpWithBetween> FindAllBetwixt(Sentence sentence)
        {
            var betwixtAll = new List<NpWithBetween>();
            var nPS = sentence.Phrases.OfNounPhrase();
            while (nPS.Any())
            {
                var first = nPS.First();
                var nss = sentence.GetPhrasesAfter(first).OfNounPhrase();
                if (nss.Any())
                {
                    var betwixt = nPS.First().Between(nss.First()).ToList();
                    betwixtAll.Add(new NpWithBetween(first, betwixt));
                }
                nPS = sentence.GetPhrasesAfter (first).OfNounPhrase ();
            }
            return betwixtAll;
        }

        /// <summary>
        /// Gets the collection of IEntityGroup constructs which were formed from all of the all
        /// binding Binder's activities over the course of its lifetime.
        /// </summary>
        public IEnumerable<IAggregateEntity> EntityGroups => entityGroups;

        private readonly IList<IAggregateEntity> entityGroups = new List<IAggregateEntity> ();

        private readonly struct NpWithBetween
        {
            internal NpWithBetween(NounPhrase nounPhrase, IReadOnlyList<Phrase> tillNextNounPhrase)
                : this()
            {
                NounPhrase = nounPhrase;
                TillNextNounPhrase = tillNextNounPhrase;
            }

            public NounPhrase NounPhrase { get; }

            public IReadOnlyList<Phrase> TillNextNounPhrase { get; }
        }
    }
}
