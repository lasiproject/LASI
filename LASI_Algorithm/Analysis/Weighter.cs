using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.Utilities;
using LASI.Utilities.TypedSwitch;

namespace LASI.Algorithm.Weighting
{
    static public class Weighter
    {
        public static Task<string>[] GetWeightingTasksForDocument(Document doc) {
            return new[]{ 
               Task .Run  (   () => 
               {
                       WeightWordsBySyntacticSequence(doc);
                   return string.Format("{0}: Calculating Harmonic Distance", doc.FileName);
               }),       
               Task .Run  (   () => 
               {
                       WeightWordsByLiteralFrequency (doc);
                   return string.Format("{0}: Aggregating Literals", doc.FileName);
               }),
               Task .Run    (   () => 
               {
                       ModifyNounWeightsBySynonyms(doc);//.ContinueWith(t =>
                   return  string.Format("{0}: Generalizing Nouns",doc.FileName );
               }), 
               Task .Run    (   () => 
               {
                       ModifyVerbWeightsBySynonyms (doc);//.ContinueWith(t =>
                   return  string.Format("{0}: Generalizing Verbs",doc.FileName );
               }), 
               Task .Run   (   () => 
               {
                       WeightPhrasesByLiteralFrequency (doc);//.ContinueWith(t =>   
                   return string.Format("{0}: Aggregating Complex Literals", doc.FileName);
               }),
                Task .Run   (   () => 
               {
                        HackSubjectPropernounImportance (doc);//.ContinueWith(t => 
                        return string.Format("{0}: Focusing Patterns", doc.FileName);
               }),  
               Task .Run   (  () =>
               {
                    WeightPhrasesByAVGWordWeight(doc);//.ContinueWith(t =>   
                    return string.Format("{0}: Averaging Metrics",doc.FileName);
               }), 
               Task .Run     (   () => 
               {
                        WeightSimilarNounPhrases (doc);//.ContinueWith(t =>   
                        return string.Format("{0}: Generalizing Entities!",doc.FileName);
               }),      
               Task .Run  (   () => 
               {
                    NormalizeWeights (doc);//.ContinueWith(t =>  
                    return  string.Format("{0}: Normalizing Metrics", doc.FileName);
               }),
            };

        }

        /// <summary>
        /// Asynchronously assigns a Weight to each word and start in a Document.
        /// </summary>
        /// <param name="doc">The Document whose elements are to be weighted</param>
        public static async Task WeightAsync(Document doc) {
            await Task.Run(() => Weight(doc));
        }
        /// <summary>
        /// Assigns a Weight to each word and start in a Document.
        /// </summary>
        /// <param name="doc">The Document whose elements are to be weighted</param>
        static public void Weight(Document doc) {
            WeightWordsByLiteralFrequency(doc);

            WeightWordsBySyntacticSequence(doc);

            HackSubjectPropernounImportance(doc);

            WeightPhrasesByAVGWordWeight(doc);

            ModifyNounWeightsBySynonyms(doc);

            ModifyVerbWeightsBySynonyms(doc);

            WeightPhrasesByLiteralFrequency(doc);

            WeightSimilarNounPhrases(doc);

            NormalizeWeights(doc);
        }
        private static async Task NormalizeWeightsAsync(Document doc) {
            await Task.Run(() => NormalizeWeights(doc));
        }
        private static void NormalizeWeights(Document doc) {
            double TotPhraseWeight = 0.0;
            double MaxWeight = 0.0;
            int NonZeroWghts = 0;
            foreach (var w in doc.Phrases) {
                TotPhraseWeight += w.Weight;

                if (w.Weight > 0)
                    NonZeroWghts++;

                if (w.Weight > MaxWeight)
                    MaxWeight = w.Weight;
            }
            if (NonZeroWghts != 0) {//Caused a devide by zero exception if document was empty.
                var AvgWght = TotPhraseWeight / NonZeroWghts;
                var ratio = 100 / MaxWeight;

                foreach (var p in doc.Phrases) {
                    p.Weight = Math.Round(p.Weight * ratio, 3);
                }
            }
        }
        private static async Task ModifyVerbWeightsBySynonymsAsync(Document doc) {
            await Task.Run(() => ModifyVerbWeightsBySynonyms(doc));
        }
        private static void ModifyVerbWeightsBySynonyms(Document doc) {
            var verbsSynonymGroups = from outerVerb in doc.Words.GetVerbs().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     from innerVerb in doc.Words.GetVerbs().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     where outerVerb.IsSynonymFor(innerVerb)
                                     group innerVerb by outerVerb;
            verbsSynonymGroups.ForAll(grp => {
                grp.Key.Weight += 0.7 * grp.Count();
            });
        }

        private static async Task ModifyNounWeightsBySynonymsAsync(Document doc) {
            await Task.Run(() => ModifyNounWeightsBySynonyms(doc));
        }

        /// <summary>
        /// Increase noun weights in a document by abstracting over synonyms
        /// </summary>
        /// <param name="doc">the Document whose noun weights may be modiffied</param>
        private static void ModifyNounWeightsBySynonyms(Document doc) {
            //Currently, include only those nouns which exist in relationships with some IVerbal or IPronoun.
            var nounsToConsider =
                doc.Words.GetNouns().InDirectObjectRole()
                .Concat(doc.Words.GetNouns().InIndirectObjectRole())
                .Concat(doc.Words.GetNouns().InSubjectRole())
                .Concat(doc.Words.GetPronouns().Referencing(lex => lex is Noun)
                .Select(pro => pro.BoundEntity as Noun));

            var nounSynonymGroups = from outerNoun in nounsToConsider.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    where outerNoun.SubjectOf != null || outerNoun.DirectObjectOf != null || outerNoun.IndirectObjectOf != null
                                    from innerNoun in nounsToConsider
                                    where outerNoun.IsSynonymFor(innerNoun)
                                    group innerNoun by outerNoun;

            nounSynonymGroups.ForAll(grp => {
                grp.Key.Weight += 0.7 * grp.Count();
            });
        }
        private static async Task WeightPhrasesByAVGWordWeightAsync(Document doc) {
            await Task.Run(() => WeightPhrasesByAVGWordWeight(doc));
        }
        private static void WeightPhrasesByAVGWordWeight(Document doc) {
            var phraseWeightPairs =
                from phrase in doc.Phrases.Where(p => !(p is InfinitivePhrase)).AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                let weight = phrase.Words.Average(w => w.Weight)
                select new {
                    phr = phrase,
                    weight
                };
            phraseWeightPairs.ForAll(pWPair => {
                pWPair.phr.Weight += pWPair.weight;
            });
        }
        private static async Task WeightPhrasesByLiteralFrequencyAsync(Document doc) {
            await Task.Run(() => WeightPhrasesByLiteralFrequency(doc));
        }
        private static void WeightPhrasesByLiteralFrequency(Document doc) {
            WeightByLiteralFrequency(doc.Phrases);

        }

        private static void WeightByLiteralFrequency(IEnumerable<ILexical> syntacticElements) {
            var g = (from phrase in syntacticElements.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                     group phrase by new {
                         phrase.Type,
                         phrase.Text
                     });
            g.ForAll(grouped => {
                foreach (var p in grouped)
                    p.Weight += grouped.Count();
            });
        }
        private static async Task WeightWordsByLiteralFrequencyAsync(Document doc) {
            await Task.Run(() => WeightWordsByLiteralFrequency(doc));
        }
        /// <summary>basic word count by part of speech ignoring determiners and conjunctions</summary>
        /// <param name="doc">the Document whose words to weight</param>
        /// <param name="excluded">zero or more types to exlcude from weighting</param>
        private static void WeightWordsByLiteralFrequency(Document doc) {
            WeightByLiteralFrequency(doc.Words
                .Except(doc.Words.GetDeterminers())
                .Except(doc.Words.GetPronouns())
                .Except(doc.Words.GetAdverbs())
                .Except(doc.Words.GetAdjectives()));
        }
        private static async Task WeightSimilarNounPhrasesAsync(Document doc) {
            await Task.Run(() => WeightSimilarNounPhrases(doc));
        }
        /// <summary>
        /// For each noun parent in a document that is similar to another noun parent, increase the weight of that noun
        /// </summary>
        /// <param name="doc">Document containing the componentPhrases to weight</param>
        private static void WeightSimilarNounPhrases(Document doc) {


            var nps = doc.Phrases.GetNounPhrases();
            var similarNounPhraseLookup = (from NP in nps.InSubjectRole().Concat(nps.InDirectObjectRole()).Concat(nps.InIndirectObjectRole()).AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                           select NP)
                                           .ToLookup(key => key,
                                            new NounPhraseComparer());
            foreach (var outerNP in nps.InSubjectRole().Concat(nps.InDirectObjectRole()).Concat(nps.InIndirectObjectRole()).AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)) {
                var similarPhrases = from potentialM in
                                         (from innerNP in similarNounPhraseLookup[outerNP]
                                          select new {
                                              NP = outerNP,
                                              innerNP,
                                              similarityRatio = Thesaurus.GetSimilarityRatio(outerNP, innerNP)
                                          })
                                     where potentialM.similarityRatio >= 0.6
                                     select potentialM;
                //Need to fix this. Its causing stack overflow

                foreach (var match in similarPhrases) {

                    ////match.NP.Weight += match.innerNP.Weight * match.similarityRatio;
                    //match.innerNP.Weight += match.NP.Weight * match.similarityRatio;
                    //match.innerNP.Weight = Math.Round(match.innerNP.Weight, 5);

                }
            }

        }
        //static double InverserDocumentFrequency(IEnumerable<Document> documentGroup, bool useSynonyms = false) {
        //    var numDocs = documentGroup.Count();
        //    var wordsWithFreqPairs = from doc in documentGroup  from word in doc.Words group word by word.Text 
        //}
        private struct NounPhraseComparer : IEqualityComparer<NounPhrase>
        {
            public bool Equals(NounPhrase x, NounPhrase y) {
                return x == null || y == null ? false : x.IsSimilarTo(y);
            }

            public int GetHashCode(NounPhrase obj) {
                return obj != null ? 1 : 0;
            }
        }
        public static async Task HackSubjectPropernounImportanceAsync(Document doc) {
            await Task.Run(() => HackSubjectPropernounImportance(doc));
        }
        public static void HackSubjectPropernounImportance(Document doc) {

            foreach (var n in doc.Phrases.GetNounPhrases().InSubjectRole()) {
                if ((n as NounPhrase).Words.Any(i => i is ProperNoun))
                    n.Weight *= 2;
            }
            foreach (var n in doc.Phrases.GetNounPhrases()) {
                if ((n as NounPhrase).Words.Any(i => i is ProperNoun)) {
                    n.Weight *= 2;
                }

            }

        }
        /// <summary>
        /// SIX PHASES 
        ///PHASE 2 - word Weight based on part of speech and neighbors' (+2) part of speech
        ///PHASE 3 - Normal parent Weight based on parent part of speech (standardization) - COMPLETE
        ///PHASE 4 - Phrase Weight based on part of speech and neibhors' (full sentence) part of speech
        ///PHASE 5 - FREQUENCIES
        /// .1 - Frequency of word/Phrase in document
        /// .2 - Frequency of word/Phrase in document compared to second documents in set -EXCLUDED FOR 1-DOCUMENT DEMO
        ///PHASE 6 - SYNONYMS
        ///ALLUAN READ:            // .1 - Frequency of word (/Phrase?) in document - COMPLETE MINUS VERBS (couldn't NounText the adverb thesaurus in any way)
        /// .2 - Frequency of word (/Phrase?) in document compared to second documents in set -EXCLUDED FOR 1-DOCUMENT DEMO
        /// </summary>
        /// <param name="doc"></param>
        private static void WeightWordsBySyntacticSequence(Document doc) {

            int primary, secondary, tertiary, quaternary, quinary, senary;
            int based = 20;
            primary = (secondary = (tertiary = (quaternary = (quinary = (senary = 0) + based) + based) + based) + based) + based;
            //PHASE 1 - Normal word Weight based on part of speech (standardization)
            //COMPLETE - easy peasy.

            //Output.WriteLine("Normal word Weight based on POS:");
            //doc.Sentences.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax).ForAll(s => {
            //////Output.WriteLine(subject);
            foreach (var s in doc.Sentences) {
                foreach (Word w in s.Words) {
                    //Output.WriteLine(w);

                    new Switch(w)
                        .Case<Noun>(n => {
                            w.Weight = primary;
                        })
                        .Case<Verb>(v => {
                            w.Weight = secondary;
                        })
                        .Case<Adjective>(adj => {
                            w.Weight = tertiary;
                        })
                        .Case<Adverb>(adv => {
                            w.Weight = quaternary;
                        })
                        .Case<Pronoun>(pn => {
                            w.Weight = quinary;
                        })
                        .Default(def => {
                            w.Weight = senary;
                        });

                    //Output.WriteLine(w.Weight);

                }


            };





            //PHASE 2 - word Weight based on part of speech and neighbors' (+2) part of speech
            // WORKS, BUT
            // NEED FORMULAS FOR MODIFIER VARIABLES - WHAT SHOULD THESE BE?
            double modOne, modTwo;
            modOne = modTwo = 0;
            foreach (Sentence s in doc.Sentences.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)) {
                //////Output.WriteLine(subject);

                foreach (Word w in s.Words) {

                    Word next = w ?? w.NextWord;
                    Word nextNext = next ?? next.NextWord;

                    //cut?
                    Word prev = w ?? w.PreviousWord;
                    Word prevPrev = prev ?? prev.PreviousWord;


                    new Switch(w)
                       .Case<Noun>(n => {

                           Noun(next, nextNext, out modOne, out modTwo);

                       })
                       .Case<Verb>(v => {
                           Verb(next, nextNext, out modOne, out modTwo);
                       })
                       .Case<Adjective>(adj => {
                           Adjective(next, nextNext, out modOne, out modTwo);
                       })
                       .Case<Adverb>(adv => {
                           Adverb(next, nextNext, out modOne, out modTwo);
                       })
                       .Case<Pronoun>(pn => {
                           Pronoun(next, nextNext, out modOne, out modTwo);
                       })

                       .Case<Preposition>(pn => {
                           Preposition(next, nextNext, out modOne, out modTwo);
                       })
                       .Case<Determiner>(d => {
                           Determiner(next, nextNext, out modOne, out modTwo);
                       })
                       .Default(def => {
                           modOne = 0.1d;

                           //second (UNCAUGHT -> UNCAUGHT)
                           modTwo = UncaughtUncaught(nextNext);
                       });


                    w.Weight += (w.Weight * (modOne * modTwo)) / 3;

                }


            }





            #region Extra Code



        }


            #endregion

        private static void Determiner(Word next, Word nextNext, out double outModOne, out double outModTwo) {
            double modOne = 0;
            double modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn => {
                       modOne = 0.9d; //determiner-noun

                       //second (Determiner -> Noun)
                       modTwo = PronounNoun(nextNext);
                   })
                   .Case<Adjective>(advn => {
                       modOne = 0.8d;  //deteminer-adjective

                       //second (Determiner -> Adjective)
                       modTwo = PronounAdjective(nextNext);
                   })
                   .Case<Adverb>(advn => {
                       modOne = 0.7d;  //determiner-adverb

                       //second (Determiner -> Adverb)
                       modTwo = PronounAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn => {
                       modOne = 0.9d; //determiner-pronoun

                       //second (Determiner -> Noun)
                       modTwo = PronounPronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn => {
                       modOne = 0.7d; //determiner-tolinker

                       //second (Determiner -> ToLinker)
                       modTwo = PronounToLinker(nextNext);
                   })
                   .Case<Preposition>(pren => {
                       modOne = 0.3d; //determiner positional

                       //second Determiner -> Preposition)
                       modTwo = PronounPreposition(nextNext);
                   })
                   .Case<Determiner>(dn => {
                       modOne = 0d; //determiner-determiner

                       //second (Determiner -> Determiner)
                       modTwo = PronounDeterminer(nextNext);
                   })
                   .Default(def => {
                       modOne = 0.1d;

                       //second (Determiner -> UNCAUGHT)
                       modTwo = PronounUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }
        private static void Preposition(Word next, Word nextNext, out double outModOne, out double outModTwo) {
            double modOne = 0;
            double modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn => {
                       modOne = 0.8; // 

                       //second (Preposition -> Noun)
                       modTwo = PrepositionNoun(nextNext);
                   })
                   .Case<Pronoun>(pnn => {
                       modOne = 0.8; // 

                       //second (Preposition -> Noun)
                       modTwo = PrepositionPronoun(nextNext);
                   })
                   .Case<Determiner>(dn => {
                       modOne = 0.7; //determiner

                       //second (Preposition -> Determiner)
                       modTwo = PrepositionDeterminer(nextNext);
                   })
                   .Default(def => {
                       modOne = 0.1;

                       //second (Preposition -> UNCAUGHT)
                       modTwo = PrepositionUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }

        private static void Pronoun(Word next, Word nextNext, out double outModOne, out double outModTwo) {
            double modOne = 0;
            double modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn => {
                       modOne = 0.9; //compound noun/pronoun / possessed by pronoun

                       //second (Pronoun -> Noun)
                       modTwo = PronounNoun(nextNext);
                   })
                   .Case<Adjective>(advn => {
                       modOne = 0.8;  //possessed/descriptor 

                       //second (Pronoun -> Adjective)
                       modTwo = PronounAdjective(nextNext);
                   })
                   .Case<Adverb>(advn => {
                       modOne = 0.7;  //pronoun amplifier

                       //second (Pronoun -> Adverb)
                       modTwo = PronounAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn => {
                       modOne = 0.9d; //compound pronoun 
                       //second (Pronoun -> Noun)
                       modTwo = PronounPronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn => {
                       modOne = 0.6d; //pronoun directional

                       //second (Pronoun -> ToLinker)
                       modTwo = PronounToLinker(nextNext);
                   })
                   .Case<Preposition>(pren => {
                       modOne = 0.5d; //pronoun positional

                       //second (Pronoun -> Preposition)
                       modTwo = PronounPreposition(nextNext);
                   })
                   .Case<Determiner>(dn => {
                       modOne = 0.7d; //determiner

                       //second (Pronoun -> Determiner)
                       modTwo = PronounDeterminer(nextNext);
                   })
                   .Default(def => {
                       modOne = 0.1d;

                       //second (Pronoun -> UNCAUGHT)
                       modTwo = PronounUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }

        private static void Adverb(Word next, Word nextNext, out double outModOne, out double outModTwo) {
            double modOne = 0;
            double modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn => {
                       modOne = 0.9d; //adverbial noun

                       //second (Adverb -> Noun)
                       modTwo = AdverbNoun(nextNext);
                   })
                   .Case<Adjective>(advn => {
                       modOne = 0.8d;  //normal adv-adj

                       //second (Adverb -> Adjective)
                       modTwo = AdverbAdjective(nextNext);
                   })
                   .Case<Adverb>(advn => {
                       modOne = 0.7d;  //bi-adverbial

                       //second (Adverb -> Adverb)
                       modTwo = AdverbAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn => {
                       modOne = 0.9d; //adverbial pronoun

                       //second (Adverb -> Noun)
                       modTwo = AdverbPronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn => {
                       modOne = 0.7d; //adverb directional

                       //second (Adverb -> ToLinker)
                       modTwo = AdverbToLinker(nextNext);
                   })
                   .Case<Preposition>(pren => {
                       modOne = 0.5d; //adverb positional

                       //second (Adverb -> Preposition)
                       modTwo = AdverbPreposition(nextNext);
                   })
                   .Case<Determiner>(dn => {
                       modOne = 0.7d; //determiner

                       //second (Adverb -> Determiner)
                       modTwo = AdverbDeterminer(nextNext);
                   })
                   .Default(def => {
                       modOne = 0.1d;

                       //second (Adverb -> UNCAUGHT)
                       modTwo = AdverbUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }

        private static void Adjective(Word next, Word nextNext, out double outModOne, out double outModTwo) {
            double modOne = 0;
            double modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn => {
                       modOne = 0.7d; //noun descriptor

                       //second (Adjective -> Noun)
                       modTwo = AdjectiveNoun(nextNext);
                   })
                   .Case<Adjective>(advn => {
                       modOne = 0.5d;  //double descriptor

                       //second (Adjective -> Adjective)
                       modTwo = AdjectiveAdjective(nextNext);
                   })
                   .Case<Adverb>(advn => {
                       modOne = 0.5d;  //coloured brilliantly

                       //second (Adjective -> Adverb)
                       modTwo = AdjectiveAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn => {
                       modOne = 0.7d; //noun descriptor

                       //second (Adjective -> Noun)
                       modTwo = AdjectivePronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn => {
                       modOne = 0.4d; //adjective directional

                       //second (Adjective -> ToLinker)
                       modTwo = AdjectiveToLinker(nextNext);
                   })
                   .Case<Preposition>(pren => {
                       modOne = 0.4d; //adjective positional

                       //second (Adjective -> Prepositional)
                       modTwo = AdjectivePreposition(nextNext);
                   })
                   .Case<Determiner>(dn => {
                       modOne = 0.4d; //determiner

                       //second (Adjective -> Determiner)
                       modTwo = AdjectiveDeterminer(nextNext);
                   })
                   .Default(def => {
                       modOne = 0.1d;

                       //second (Adjective -> UNCAUGHT)
                       modTwo = AdjectiveUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }

        private static void Verb(Word next, Word nextNext, out double outModOne, out double outModTwo) {
            double modOne = 0;
            double modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn => {
                       modOne = 0.9d; //adverb actor

                       //second (Verb -> Noun)
                       modTwo = VerbNoun(nextNext);
                   })
                   .Case<PastParticipleVerb>(vn => {
                       modOne = 0.7d; //adverb-adverb descriptor

                       //second (Verb -> PastParticipleVerb)
                       modTwo = VerbPastParticipleVerb(nextNext);
                   })
                   .Case<Adjective>(advn => {
                       modOne = 0.6d;  //adverb state

                       //second (Verb -> Adjective)
                       modTwo = VerbAdjective(nextNext);
                   })
                   .Case<Adverb>(advn => {
                       modOne = 0.7d;  //perfect adverb

                       //second (Verb -> Adverb)
                       modTwo = VerbAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn => {
                       modOne = 0.9d; //adverb actor

                       //second (Verb -> Pronoun)
                       modTwo = VerbPronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn => {
                       modOne = 0.6d; //adverb directional

                       //second (Verb -> ToLinker)
                       modTwo = VerbToLinker(nextNext);
                   })
                   .Case<Preposition>(pren => {
                       modOne = 0.5d; //adverb-adverb positional

                       //second (Verb -> Preposition)
                       modTwo = VerbPreposition(nextNext);
                   })
                   .Case<Determiner>(dn => {
                       modOne = 0.4d; //determiner

                       //second (Verb -> Determiner)
                       modTwo = VerbDeterminer(nextNext);
                   })
                   .Default(def => {
                       modOne = 0.1d;

                       //second (Verb -> UNCAUGHT)
                       modTwo = VerbUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }


        private static void Noun(Word next, Word nextNext, out double outModOne, out double outModTwo) {
            double modOne = 0; //Renamed parameters and bound created temporary variables to pass into the switch blocks 
            double modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn => {
                       modOne = 0.9d; //compound noun

                       //second (Noun -> Noun)
                       modTwo = NounNoun(nextNext);
                   })
                   .Case<Adjective>(nadj => {
                       modOne = 0.8d; //possessive

                       //second (Noun -> Adjective)
                       modTwo = NounAdjective(nextNext);
                   })
                   .Case<Verb>(nv => {
                       modOne = 0.7d; //noun action or descriptor

                       //second (Noun -> Verb)
                       modTwo = NounVerb(nextNext);
                   })
                   .Case<Adverb>(advn => {
                       modOne = 0.7d;  //noun amplifier

                       //second (Noun -> Adverb)
                       modTwo = NounAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn => {
                       modOne = 0.9d; //compound noun

                       //second (Noun -> Pronoun)
                       modTwo = NounPronoun(nextNext);


                   })
                   .Case<ToLinker>(lnkn => {
                       modOne = 0.6d; //noun to link

                       //second (Noun -> ToLinker)
                       modTwo = NounToLinker(nextNext);
                   })
                   .Case<Preposition>(pren => {
                       modOne = 0.6d; //noun positional

                       //second (Noun -> Preposition)
                       modTwo = NounPreposition(nextNext);
                   })
                   .Case<Determiner>(dn => {
                       modOne = 0.5d; //determiner

                       //second (Noun -> Determiner)
                       modTwo = NounDeterminer(nextNext);
                   })
                   .Default(def => {
                       modOne = 0.1d;

                       //second (Noun -> UNCAUGHT)
                       modTwo = NounUncaught(nextNext);
                   });
            outModOne = modOne;//Set parameters = temporaries
            outModTwo = modTwo;//There is a better way to handle this, but this works without any changes
        }

        private static double UncaughtUncaught(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.1d; //uncaught-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.1d; //uncaught-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.1d;  //uncaught-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.1d;  //uncaught-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.1; //uncaught-uncaught-noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.1d; //uncaught-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.1d; //uncaught-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.1d; //uncaught-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0.1d; //uncaught-uncaught-uncaught (epic fail)
                });
            return modTwo;
        }

        private static double PrepositionUncaught(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.5d; //preposition-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.4d; //preposition-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.3d;  //preposition-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.3d;  //preposition-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.5d; //preposition-uncaught-noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.2d; //preposition-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.2d; //preposition-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.2d; //preposition-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PrepositionDeterminer(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.7d; //preposition-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.6d; //preposition-determiner-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.5d;  //preposition-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.4d;  //preposition-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.6d; //preposition-determiner-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.3d; //preposition-determiner directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.3d; //preposition-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.3d; //preposition-determiner determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PrepositionPronoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.5d; //preposition-compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.8d; //preposition-noun-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.3d;  //preposition-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.3d;  //preposition-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.8d; //preposition-compound noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.6d; //preposition-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.2d; //preposition-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.9d; //preposition-noun determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PrepositionNoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.5d; //preposition-compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.8d; //preposition-noun-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.3d;  //preposition-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.3d;  //preposition-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.3d; //preposition-compound noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.8d; //preposition-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.6d; //preposition-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.2d; //preposition-noun determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PronounUncaught(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.3d; //pronoun-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.2d; //pronoun-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.2d;  //pronoun-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.2d;  //pronoun-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.3d; //pronoun-uncaught-noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.2d; //pronoun-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.2d; //pronoun-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.2d; //pronoun-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PronounDeterminer(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.9d; //pronoun-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.7d; //pronoun-determiner-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.6d;  //pronoun-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.5d;  //pronoun-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.9d; //pronoun-determiner-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.1d; //pronoun-determiner directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.3d; //pronoun-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //pronoun-determiner determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PronounPreposition(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.8d; //pronoun-preposition-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.6d; //pronoun-preposition-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.5d;  //pronoun-preposition-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.6d;  //pronoun-preposition-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.8d; //pronoun-preposition-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.4d; //pronoun-preposition directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.4d; //pronoun-preposition positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.3d; //pronoun-preposition determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PronounToLinker(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.9d; //pronoun-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.8d; //pronoun-tolinker-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.7d;  //pronoun-tolinker-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.6d;  //pronoun-tolinker-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.9d; //pronoun-tolinker-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //pronoun-tolinker directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.5d; //pronoun-tolinker positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.7d; //pronoun-tolinker determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PronounPronoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.9d; //triple compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.8d; //compound noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.7d;  //compound noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.6d;  //compound noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.9d; //triple compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.7d; //compound noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.7d; //compound noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.5d; //compound noun determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PronounAdverb(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.8d; //pronoun-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.7d; //pronoun-adverb-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.6d;  //pronoun-adverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.5d;  //pronoun-adverb-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = .8d; //pronoun-adverb-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.7d; //pronoun-adverb directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.6d; //pronoun-adverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.5d; //pronoun-adverb determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PronounAdjective(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.9d; //pronoun-adjective-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.8d; //pronoun-adjective-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.7d;  //pronoun-adjective-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.6d;  //pronoun-adjective-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.9d; //pronoun-adjective-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.2d; //pronoun-adjective directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.2d; //pronoun-adjective positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.7d; //pronoun-adjective determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double PronounNoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.9d; //triple compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.8d; //compound noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.7d;  //compound noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.7d;  //compound noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.9d; //triple compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.8d; //compound noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.7d; //compound noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.5d; //compound noun determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double AdverbUncaught(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.5d; //adverb-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.4d; //adverb-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.3d;  //adverb-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.3d;  //adverb-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.5d; //adverb-uncaught-noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.3d; //adverb-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.2d; //adverb-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.3d; //adverb-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double AdverbDeterminer(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.9d; //adverb-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.3d; //adverb-determiner-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.8d;  //adverb-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.7d;  //adverb-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.9d; //adverb-determiner-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-determiner directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.3d; //adverb-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-determiner determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double AdverbPreposition(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-preposition-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-preposition-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-preposition-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-preposition-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb-preposition-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-preposition directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-preposition positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-preposition determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double AdverbToLinker(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.9d; //adverb-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.8d; //adverb-tolinker-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.7d;  //adverb-tolinker-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.6d;  //adverb-tolinker-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.9d; //adverb-tolinker-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-tolinker directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.3d; //adverb-tolinker positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.4d; //adverb-tolinker determiner
                })
                .Default(def => {
                    modTwo = 0.1d;
                });
            return modTwo;
        }

        private static double AdverbPronoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.5d; //adverb compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.4; //adverb-noun-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.3;  //adverb-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.5; //adverb compound noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.6; //adverb-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.4; //adverb-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.4; //adverb-noun determiner
                })
                .Default(def => {
                    modTwo = 0.1;
                });
            return modTwo;
        }

        private static double AdverbAdverb(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.3; //adverb-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.3; //adverb-adverb-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.3;  //adverb-adverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.1;  //tri adverbial
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.3; //adverb-adverb-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.3; //adverb-adverb directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.2; //adverb-adverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.3; //adverb-adverb determiner
                })
                .Default(def => {
                    modTwo = 0.1;
                });
            return modTwo;
        }

        private static double AdverbAdjective(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.6; //adverb-adjective-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.4; //adverb-adjective-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.5;  //adverb-adjective-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.4;  //adverb-adjective-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.6; //adverb-adjective-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.5; //adverb-adjective directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.4; //adverb-adjective positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.3; //adverb-adjective determiner
                })
                .Default(def => {
                    modTwo = 0.1;
                });
            return modTwo;
        }

        private static double AdverbNoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.5d; //adverb -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0.4d; //adverb-noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0.3d;  //adverb-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0.3d;  //adverb-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0.5; //adverb -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0.3; //adverb-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0.4; //adverb-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0.2; //adverb-noun determiner
                })
                .Default(def => {
                    modTwo = 0.1;
                });
            return modTwo;
        }

        private static double AdjectiveUncaught(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0.4; //adjective-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adjective-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adjective-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adjective -> uncaught -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adjective-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adjective-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adjective-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double AdjectiveDeterminer(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-determiner descriptor 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adjective-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adjective-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adjective-determiner-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adjective-determiner directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adjective-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adjective-determiner determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double AdjectivePreposition(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective-prepositional-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-prepositional descriptor 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adjective-prepositional-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adjective-prepositional-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adjective-prepositional-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adjective-prepositional directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adjective-prepositional positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adjective-prepositional determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double AdjectiveToLinker(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-tolinker descriptor 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adjective-tolinker-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adjective-tolinker-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adjective-tolinker-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adjective-tolinker directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adjective-tolinker positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adjective-tolinker determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double AdjectivePronoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adjective-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adjective-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adjective -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adjective-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adjective-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adjective-noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double AdjectiveAdverb(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-adverb-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adjective-adverb-adjective (triple compound)
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adjective-adverb-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adjective-adverb-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adjective-adverb-directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adjective-adverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adjective-adverb determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double AdjectiveAdjective(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //compound adjective -> noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //compound adjective -> adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //compound adjective -> adjective (triple compound)
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //compound adjective -> adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //compound adjective -> noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //compound adjective -> directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //compound adjective -> positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //compound adjective -> determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double AdjectiveNoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adjective-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adjective-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adjective -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adjective-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adjective-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adjective-noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbUncaught(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> uncaught -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbDeterminer(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-determiner-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> determiner -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-determiner directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-determiner determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbPreposition(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-preposition-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-preposition-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-preposition-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-preposition-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> preposition -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-preposition directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-preposition positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-preposition determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbToLinker(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-tolinker-pastverb (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-tolinker-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-tolinker-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> tolinker -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-tolinker directional (possible?)
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-tolinker positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-tolinker determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbPronoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-pronoun-noun (compound)
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-pronoun-pastverb
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-pronoun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-pronoun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> pronoun -> noun  (compound)
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-pronoun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-pronoun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-pronoun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbAdverb(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-adverb-pastverb
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-adverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-adverb-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> adverb -> noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-aadverb directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-adverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-adverb determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbAdjective(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-adjective-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-adjective-pastverb
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-adjective-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-adjective-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> adjective -> noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-adjective directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-adjective positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-adjective determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbPastParticipleVerb(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-pastverb -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-pastverb-pastverb
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-pastverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-pastverb-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> pastverb -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-pastverb directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-pastverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-pastverb determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double VerbNoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //adverb-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //adverb-noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounUncaught(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nun => {
                    modTwo = 0; //noun-uncaught-noun
                })
                .Case<PastParticipleVerb>(nupv => {
                    modTwo = 0; //noun-uncaught-pastverb 
                })
                .Case<Adjective>(nuadj => {
                    modTwo = 0;  //noun-uncaught-adjective
                })
                .Case<Adverb>(nuadv => {
                    modTwo = 0;  //noun-uncaught-adverb
                })
                .Case<Pronoun>(nupn => {
                    modTwo = 0; //noun -> uncaught -> noun 
                })
                .Case<ToLinker>(nulnk => {
                    modTwo = 0; //noun-uncaught directional 
                })
                .Case<Preposition>(nup => {
                    modTwo = 0; //noun-uncaught positional
                })
                .Case<Determiner>(nud => {
                    modTwo = 0; //noun-uncaught determiner
                })
                .Default(nuu => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounDeterminer(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(ndn => {
                    modTwo = 0; //noun-determiner-noun
                })
                .Case<PastParticipleVerb>(ndpv => {
                    modTwo = 0; //noun-determiner-adverb descriptor (possible?)
                })
                .Case<Adjective>(ndadj => {
                    modTwo = 0;  //noun-determiner-adjective
                })
                .Case<Adverb>(ndav => {
                    modTwo = 0;  //noun-determiner-adverb
                })
                .Case<Pronoun>(ndpn => {
                    modTwo = 0; //noun-determiner-noun
                })
                .Case<ToLinker>(ndlnk => {
                    modTwo = 0; //noun-determiner directional
                })
                .Case<Preposition>(ndp => {
                    modTwo = 0; //noun-determiner positional
                })
                .Case<Determiner>(ndd => {
                    modTwo = 0; //noun-determiner determiner
                })
                .Default(ndu => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounPreposition(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(npn => {
                    modTwo = 0; //noun-preposition-noun
                })
                .Case<PastParticipleVerb>(nppv => {
                    modTwo = 0; //noun-preposition-adverb descriptor (possible?)
                })
                .Case<Adjective>(npadj => {
                    modTwo = 0;  //noun-preposition-adjective
                })
                .Case<Adverb>(npadv => {
                    modTwo = 0;  //noun-preposition-adverb
                })
                .Case<Pronoun>(nppn => {
                    modTwo = 0; //noun-preposition-noun
                })
                .Case<ToLinker>(nplnk => {
                    modTwo = 0; //noun-preposition directional
                })
                .Case<Preposition>(npp => {
                    modTwo = 0; //noun-preposition positional
                })
                .Case<Determiner>(npd => {
                    modTwo = 0; //noun-preposition determiner
                })
                .Default(npu => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounToLinker(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nlnkn => {
                    modTwo = 0; //noun-tolinker-noun
                })
                .Case<PastParticipleVerb>(nlnkpv => {
                    modTwo = 0; //noun-tolinker-adverb descriptor (possible?)
                })
                .Case<Adjective>(nlnkadj => {
                    modTwo = 0;  //noun-tolinker-adjective
                })
                .Case<Adverb>(nlnkadv => {
                    modTwo = 0;  //noun-tolinker-adverb
                })
                .Case<Pronoun>(nlnkpn => {
                    modTwo = 0; //noun-tolinker-noun
                })
                .Case<ToLinker>(nlnklnk => {
                    modTwo = 0; //noun-tolinker directional
                })
                .Case<Preposition>(nlnkp => {
                    modTwo = 0; //noun-tolinker positional
                })
                .Case<Determiner>(nlnkd => {
                    modTwo = 0; //noun-tolinker determiner
                })
                .Default(nlinku => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounPronoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(npnn => {
                    modTwo = 0; //triple compound noun
                })
                .Case<PastParticipleVerb>(npnpv => {
                    modTwo = 0; //compound noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(npnadj => {
                    modTwo = 0;  //compound noun-adjective
                })
                .Case<Adverb>(npnadv => {
                    modTwo = 0;  //compound noun-adverb
                })
                .Case<Pronoun>(npnpn => {
                    modTwo = 0; //triple compound noun
                })
                .Case<ToLinker>(npnlnk => {
                    modTwo = 0; //compound noun directional
                })
                .Case<Preposition>(npnpre => {
                    modTwo = 0; //compound noun positional
                })
                .Case<Determiner>(npnd => {
                    modTwo = 0; //compound noun determiner
                })
                .Default(npnu => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounAdverb(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nadvn => {
                    modTwo = 0; //noun-adverb-noun
                })
                .Case<PastParticipleVerb>(nadvpv => {
                    modTwo = 0; //noun-adverb-adverb descriptor (possible?)
                })
                .Case<Adjective>(nadvadj => {
                    modTwo = 0;  //noun-adverb-adjective
                })
                .Case<Adverb>(nadvadv => {
                    modTwo = 0;  //noun-adverb-adverb
                })
                .Case<Pronoun>(nadvpn => {
                    modTwo = 0; //noun-adverb-noun
                })
                .Case<ToLinker>(nadvlnk => {
                    modTwo = 0; //noun-adverb directional
                })
                .Case<Preposition>(nadvpre => {
                    modTwo = 0; //noun-adverb positional
                })
                .Case<Determiner>(nadvd => {
                    modTwo = 0; //noun-adverb determiner
                })
                .Default(nadvu => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounVerb(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nvn => {
                    modTwo = 0; //noun-adverb-noun
                })
                .Case<PastParticipleVerb>(nvpv => {
                    modTwo = 0; //noun-adverb-adverb descriptor (possible?)
                })
                .Case<Adjective>(nvadj => {
                    modTwo = 0;  //noun-adverb-adjective
                })
                .Case<Adverb>(nvadv => {
                    modTwo = 0;  //noun-adverb-adverb
                })
                .Case<Pronoun>(nvpn => {
                    modTwo = 0; //noun-adverb-noun
                })
                .Case<ToLinker>(nvlnk => {
                    modTwo = 0; //noun-adverb directional
                })
                .Case<Preposition>(nvpre => {
                    modTwo = 0; //noun-adverb positional
                })
                .Case<Determiner>(nvd => {
                    modTwo = 0; //noun-adverb determiner
                })
                .Default(nvu => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounAdjective(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nadjn => {
                    modTwo = 0; //noun-adjective-noun
                })
                .Case<PastParticipleVerb>(nadjpv => {
                    modTwo = 0; //noun-adjective-adverb descriptor (possible?)
                })
                .Case<Adjective>(nadjadj => {
                    modTwo = 0;  //noun-adjective-adjective
                })
                .Case<Adverb>(nadjav => {
                    modTwo = 0;  //noun-adjective-adverb
                })
                .Case<Pronoun>(nadjn => {
                    modTwo = 0; //noun-adjective-noun
                })
                .Case<ToLinker>(nadjlnk => {
                    modTwo = 0; //noun-adjective directional
                })
                .Case<Preposition>(nadjpre => {
                    modTwo = 0; //noun-adjective positional
                })
                .Case<Determiner>(nadjd => {
                    modTwo = 0; //noun-adjective determiner
                })
                .Default(nadju => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double NounNoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nnn => {
                    modTwo = 0; //triple compound noun
                })
                .Case<PastParticipleVerb>(nnpv => {
                    modTwo = 0; //compound noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(nnadj => {
                    modTwo = 0;  //compound noun-adjective
                })
                .Case<Adverb>(nnadv => {
                    modTwo = 0;  //compound noun-adverb
                })
                .Case<Pronoun>(nnpn => {
                    modTwo = 0; //triple compound noun
                })
                .Case<ToLinker>(nnlnk => {
                    modTwo = 0; //compound noun directional
                })
                .Case<Preposition>(nnpre => {
                    modTwo = 0; //compound noun positional
                })
                .Case<Determiner>(nnd => {
                    modTwo = 0; //compound noun determiner
                })
                .Default(nnu => {
                    modTwo = 0;
                });
            return modTwo;
        }
        private static double DeterminerUncaught(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //determiner-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //determiner-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //determiner-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //determiner-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //determineruncaught-noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //determiner-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //determiner-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //determiner-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double DeterminerDeterminer(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //determiner-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //determiner-determiner-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //determiner-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //determiner-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //determiner-determiner-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //determiner-determiner directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //determiner-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //determiner-determiner determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double DeterminerPreposition(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //determiner-preposition-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //determiner-preposition-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //determiner-preposition-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //determiner-preposition-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //determiner-preposition-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //determiner-preposition directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //determiner-preposition positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //determiner-preposition determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double DeterminerToLinker(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //determiner-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //determiner-tolinker-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //determiner-tolinker-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //determiner-tolinker-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //determiner-tolinker-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //determiner-tolinker directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //determiner-tolinker positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //determiner-tolinker determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double DeterminerPronoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //determiner compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //determiner-noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //determiner-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //determiner-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //determiner compound noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //determiner-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //determiner-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //determiner-noun-determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double DeterminerAdverb(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //determiner-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //determiner-adverb-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //determiner-adverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //determiner-adverb-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //determiner-adverb-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //determiner-adverb directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //determiner-adverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //determiner-adverb determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double DeterminerAdjective(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //determiner-adjective-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //determiner-adjective-adverb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //determiner-adjective-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //determiner-adjective-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //determiner-adjective-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //determiner-adjective directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //determiner-adjective positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //determiner-adjective determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static double DeterminerNoun(Word nextNext) {
            double modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //determiner-compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //determiner-noun-adverb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //determiner-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //determiner-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //determiner-compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //determiner-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //determiner-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //determiner-noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;

        }
    }
}
