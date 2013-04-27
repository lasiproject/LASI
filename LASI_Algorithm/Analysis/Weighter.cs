using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis
{
    static public class Weighter
    {

        public static async Task WeightAsync(Document doc) {
            await Task.Run(() => Weight(doc));
        }
        /// <summary>
        /// Weighting algorithm assigns Weight to each word and phrase in a document
        /// </summary>
        /// <param name="doc"></param>
        static public void Weight(Document doc) {
            var wordsByCount = from w in doc.Words
                               where !(w is IPrepositional)
                               where !(w is Determiner)
                               where !(w is IConjunctive)
                               group w by new {
                                   w.Type,
                                   w.Text
                               };

            var phraseByCount = from p in doc.Phrases
                                group p by new {
                                    Type = p.GetType(),
                                    p.Text
                                };

            var nounSynonymGroups = from w in doc.Words.GetNouns().AsParallel().WithDegreeOfParallelism(4)
                                    let synstrings = Thesauri.Thesaurus.NounProvider[w]
                                    from t in doc.Words.GetNouns()
                                    where synstrings != null
                                    where synstrings.Contains(t.Text)
                                    group t by w;

            var verbsynonymgroups = from w in doc.Words.GetVerbs().AsParallel().WithDegreeOfParallelism(4)
                                    let synstrings = Thesauri.Thesaurus.VerbProvider[w]
                                    from t in doc.Words.GetVerbs()
                                    where synstrings != null
                                    where synstrings.Contains(t.Text)
                                    group t by w;

            var phraseWeightPairs = from p in doc.Phrases
                                    let weight = p.Words.Average(w => w.Weight)
                                    select new {
                                        p,
                                        weight
                                    };


            //basic word count by part of speech ignoring determiners and conjunctions
            foreach (var grp in wordsByCount) {
                foreach (var w in grp) {
                    w.Weight = grp.Count();
                }
            }

            //noun synonyms increase Weight of individual nouns
            foreach (var grp in nounSynonymGroups) {
                grp.Key.Weight += 0.7m * grp.Count();
                var pn = grp.Key;
                pn.Weight *= pn is ProperNoun ? 3 : 1;

            }

            //verb synonyms increase Weight of individual verbs 
            foreach (var grp in verbsynonymgroups) {
                grp.Key.Weight += 0.7m * grp.Count();
            }

            //assign weights to phrases 
            foreach (var pair in phraseWeightPairs) {
                pair.p.Weight = pair.weight;
            }

            //increment Weight of identical phrases
            foreach (var grp in phraseByCount) {
                foreach (var p in grp) {
                    p.Weight += grp.Count();
                }
            }


        }
    }
}
