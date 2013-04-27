using LASI.Algorithm.SyntaciticAndSemanticStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.TypedSwitch;
using LASI.Utilities;
using LASI.Algorithm.DocumentConstructs;

namespace LASI.Algorithm.Analysis.Binding
{
    public class PhraseWiseEntityGroupBinder
    {
        List<IEntityGroup> entityGroups = new List<IEntityGroup>();

        public List<IEntityGroup> EntityGroups {
            get {
                return entityGroups;
            }
            private set {
                entityGroups = value;
            }
        }

        public void Bind(Sentence sentence) {
            var betwixt = FindAllBetwixt(sentence);
            var aggregateEntities = new List<NounPhrase>();
            foreach (var b in betwixt) {
                if (b.TillNextNP.GetConjunctionPhrases().Count() + b.TillNextNP.OfType<PunctuatorPhrase>().Count() != b.TillNextNP.Count() && b.TillNextNP.Count() < 3) {
                    aggregateEntities.Add(b.NP);
                    if (aggregateEntities.Count > 2) {
                        EntityGroups.Add(new NPAggregateSubjectObject(aggregateEntities));
                    }
                }
                else {
                    aggregateEntities = new List<NounPhrase>();
                }


            }
        }

        private List<NpWithBetween> FindAllBetwixt(Sentence sentence) {
            var betwixtAll = new List<NpWithBetween>();
            var nPS = sentence.Phrases.GetNounPhrases();
            while (nPS.Count() > 0) {
                var n1 = nPS.First();
                var nss = sentence.GetPhrasesAfter(n1).GetNounPhrases();
                if (nss.Count() > 0) {
                    var betwixt = nPS.First().Between(nss.First());
                    betwixtAll.Add(new NpWithBetween(n1, betwixt));

                }
                nPS = sentence.GetPhrasesAfter(n1).GetNounPhrases();


            }
            return betwixtAll;
        }
        internal struct NpWithBetween
        {
            internal NpWithBetween(NounPhrase nounPhrase, IEnumerable<Phrase> tillNextNounPhrase)
                : this() {
                NP = nounPhrase;
                TillNextNP = tillNextNounPhrase;
            }

            public IEnumerable<Phrase> TillNextNP {
                get;
                private set;
            }

            public NounPhrase NP {
                get;
                private set;
            }
        }
    }
}
