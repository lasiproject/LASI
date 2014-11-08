using LASI.Utilities;
using LASI.Core.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Core
{
    /// <summary>
    /// Attempts to collect semantically grouped entities within a source into aggregate objects.
    /// </summary>
    public class PhraseWiseEntityGroupBinder
    {
        private IList<IAggregateEntity> entityGroups = new List<IAggregateEntity>();

        /// <summary>
        /// Gets the collection of IEntityGroup constructs which were formed from all of the all binding Binder's activities over the course of its lifetime.
        /// </summary>
        public IEnumerable<IAggregateEntity> EntityGroups {
            get {
                return entityGroups;
            }
        }
        /// <summary>
        /// Aggregates and binds the Phrase level IEntity constructs within the Sentence into instances aggregate objects which implement IEntityGroup.
        /// </summary>
        /// <param name="sentence">The Sentence to bind within.</param>
        public void Bind(Sentence sentence) {
            var betwixt = FindAllBetwixt(sentence);
            var aggregateEntities = new List<NounPhrase>();
            foreach (var b in betwixt) {
                if (b.TillNextNP.OfConjunctionPhrase().Count() + b.TillNextNP.OfType<SymbolPhrase>().Count() != b.TillNextNP.Count() && b.TillNextNP.Count() < 3) {
                    aggregateEntities.Add(b.Np);
                    if (aggregateEntities.Count > 2) {
                        entityGroups.Add(new AggregateEntity(aggregateEntities));
                    }
                } else {
                    aggregateEntities = new List<NounPhrase>();
                }


            }
        }

        private static List<NpWithBetween> FindAllBetwixt(Sentence sentence) {
            var betwixtAll = new List<NpWithBetween>();
            var nPS = sentence.Phrases.OfNounPhrase();
            while (nPS.Any()) {
                var n1 = nPS.First();
                var nss = sentence.GetPhrasesAfter(n1).OfNounPhrase();
                if (nss.Any()) {
                    var betwixt = nPS.First().Between(nss.First());
                    betwixtAll.Add(new NpWithBetween(n1, betwixt));

                }
                nPS = sentence.GetPhrasesAfter(n1).OfNounPhrase();


            }
            return betwixtAll;
        }
        internal struct NpWithBetween
        {
            internal NpWithBetween(NounPhrase nounPhrase, IEnumerable<Phrase> tillNextNounPhrase)
                : this() {
                Np = nounPhrase;
                TillNextNP = tillNextNounPhrase;
            }

            public IEnumerable<Phrase> TillNextNP { get; private set; }

            public NounPhrase Np { get; private set; }
        }
    }
}
