using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.FundamentalSyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Thesauri;

namespace LASI.Algorithm.Analysis
{
    static public class Weighter
    {

        public static async Task WeightAsync(Document doc) {
            await Task.Run(() => Weight(doc));
        }
        /// <summary>
        /// Weighting algorithm assigns Weight to each word and phrase in a Document
        /// </summary>
        /// <param name="doc">The Document whose elements are to be weighted</param>
        static public void Weight(Document doc) {


            AssignBaseWordWeights(doc, typeof(Determiner), typeof(IConjunctive), typeof(IPrepositional));

            modifyNounWeightsBySynonyms(doc);

            modifyVerbWeightsBySynonyms(doc);

            WeightPhrasesByAVGWordWeight(doc);

            WeightPhrasesByLiteralFrequency(doc);


        }

        private static void modifyVerbWeightsBySynonyms(Document doc) {

            var verbsynonymgroups = from verb in doc.Words.GetVerbs().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    let synstrings = Thesaurus.Lookup(verb)
                                    from t in doc.Words.GetVerbs().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    where synstrings != null
                                    where synstrings.Contains(t.Text)
                                    group t by verb;





            //verb synonyms increase Weight of individual verbs 
            //foreach (var grp in verbsynonymgroups) {
            //    grp.Key.Weight += 0.7m * grp.Count();
            //}

            verbsynonymgroups.ForAll(grp => {
                grp.Key.Weight += 0.7m * grp.Count();
            });
        }

        private static void WeightPhrasesByAVGWordWeight(Document doc) {

            var phraseWeightPairs = from phrase in doc.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    let weight = phrase.Words.Average(w => w.Weight)
                                    select new {
                                        p = phrase,
                                        weight
                                    };


            //assign weights to phrases 
            //foreach (var pair in phraseWeightPairs) {
            //    pair.phrase.Weight = pair.weight;
            //}

            phraseWeightPairs.ForAll(pWPair => {
                pWPair.p.Weight = pWPair.weight;
            });
        }

        private static void WeightPhrasesByLiteralFrequency(Document doc) {
            var phraseByCount = from phrase in doc.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                group phrase by new {
                                    Type = phrase.GetType(),
                                    phrase.Text
                                };


            //increment Weight of identical phrases
            //sequential enumeration of parallel query
            //foreach (var grp in phraseByCount) {
            //    foreach (var phrase in grp) {
            //        phrase.Weight += grp.Count();
            //    }
            //}

            //increment Weight of identical phrases
            //parallell enumeration of parallel query
            //Basic idea here is. 
            //Instead of the syntax: foreach (var obj in collection) { do shit with obj...}
            //We use the syntaxt: parallellCollection.ForAll( obj => { do shit with obj...} );
            //Note phraseByCount is a parallelCollection

            phraseByCount.ForAll(grp => {
                foreach (var p in grp) { //inner loop is just normal.
                    p.Weight += grp.Count();
                }
            });
        }
        /// <summary>
        /// Increase noun weights in a document by abstracting over synonyms
        /// </summary>
        /// <param name="doc">the Document whose noun weights may be modiffied</param>
        private static void modifyNounWeightsBySynonyms(Document doc) {
            var nounSynonymGroups = from noun in doc.Words.GetNouns().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    let synstrings = Thesaurus.Lookup(noun)
                                    from t in doc.Words.GetNouns().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    where synstrings != null
                                    where synstrings.Contains(t.Text)
                                    group t by noun;

            ////noun synonyms increase Weight of individual nouns
            //foreach (var grp in nounSynonymGroups) {
            //    grp.Key.Weight += 0.7m * grp.Count();
            //    var pn = grp.Key;
            //    pn.Weight *= pn is ProperNoun ? 3 : 1;
            //}

            nounSynonymGroups.ForAll(grp => {
                grp.Key.Weight += 0.7m * grp.Count();
                var pn = grp.Key;
                pn.Weight *= pn is ProperNoun ? 3 : 1;

            });
        }
        /// <summary>basic word count by part of speech ignoring determiners and conjunctions</summary>
        /// <param name="doc">the Document whose words to weight</param>
        /// <param name="excluded">zero or more types to exlcude from weighting</param>
        private static void AssignBaseWordWeights(Document doc, params Type[] excluded) {
            var wordsByCount = from word in doc.Words.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                               where ((from type in excluded
                                       where word.Type == type
                                       select type).Count() == 0)
                               group word by new {
                                   word.Type,
                                   word.Text
                               };

            foreach (var grp in wordsByCount) {
                foreach (var w in grp) {
                    w.Weight = grp.Count();
                }
            }
        }

        //static double InverserDocumentFrequency(IEnumerable<Document> documentGroup) {
        //    var numDocs = ;
        //    foreach (var doc in documentGroup) {
        //        numwords += doc.Words.Count(w => w.Text == "searchText");
        //    }
        //}

    }
}
