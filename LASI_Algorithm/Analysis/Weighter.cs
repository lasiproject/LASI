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
        /// <summary>
        /// Asynchronously assigns a Weight to each word and start in a Document.
        /// </summary>
        /// <param name="doc">The Document whose elements are to be weighted</param>
        public static async Task WeightAsync(Document doc)
        {
            await Task.Run(() => Weight(doc));
        }
        /// <summary>
        /// Assigns a Weight to each word and start in a Document.
        /// </summary>
        /// <param name="doc">The Document whose elements are to be weighted</param>
        static public void Weight(Document doc)
        {
            AssignBaseWordWeights(doc);

            WeightWordsBySyntacticSequence(doc);

            HackSubjectPropernounImportance(doc);

            WeightPhrasesByAVGWordWeight(doc);

            ModifyNounWeightsBySynonyms(doc);

            ModifyVerbWeightsBySynonyms(doc);

            WeightPhrasesByLiteralFrequency(doc);

            WeightSimilarNounPhrases(doc);

            NormalizeWeights(doc);
        }

        private static void NormalizeWeights(Document doc)
        {
            decimal TotPhraseWeight = 0.0m;
            decimal MaxWeight = 0.0m;
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

        private static void ModifyVerbWeightsBySynonyms(Document doc)
        {
            var verbsSynonymGroups = from verb in doc.Words.GetVerbs().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     let synstrings = Thesaurus.Lookup(verb)
                                     from t in doc.Words.GetVerbs().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                     where synstrings.Contains(t.Text)
                                     group t by verb;
            verbsSynonymGroups.ForAll(grp =>
            {
                grp.Key.Weight += 0.7m * grp.Count();
            });
        }

        private static void WeightPhrasesByAVGWordWeight(Document doc)
        {
            var phraseWeightPairs = from phrase in doc.Phrases.Where(p => !(p is InfinitivePhrase)).AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    let weight = phrase.Words.Average(w => w.Weight)
                                    select new
                                    {
                                        p = phrase,
                                        weight
                                    };
            phraseWeightPairs.ForAll(pWPair =>
            {
                pWPair.p.Weight = pWPair.weight;
            });
        }

        private static void WeightPhrasesByLiteralFrequency(Document doc)
        {
            var phrasesByCount = from phrase in doc.Phrases.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                 group phrase by new
                                 {
                                     Type = phrase.GetType(),
                                     phrase.Text
                                 };
            phrasesByCount.ForAll(grp =>
            {
                foreach (var p in grp) { //inner loop is just normal.
                    p.Weight += grp.Count();
                }
            });
        }
        /// <summary>
        /// Increase noun weights in a document by abstracting over synonyms
        /// </summary>
        /// <param name="doc">the Document whose noun weights may be modiffied</param>
        private static void ModifyNounWeightsBySynonyms(Document doc)
        {
            var nounSynonymGroups = from noun in doc.Words.GetNouns().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    let synstrings = Thesaurus.Lookup(noun)
                                    from t in doc.Words.GetNouns().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                    where synstrings.Contains(t.Text, StringComparer.OrdinalIgnoreCase)
                                    group t by noun;


            nounSynonymGroups.ForAll(grp =>
            {
                grp.Key.Weight += 0.7m * grp.Count();
                var pn = grp.Key;

            });
        }
        /// <summary>basic word count by part of speech ignoring determiners and conjunctions</summary>
        /// <param name="doc">the Document whose words to weight</param>
        /// <param name="excluded">zero or more types to exlcude from weighting</param>
        private static void AssignBaseWordWeights(Document doc)
        {
            var excluded = new[] { typeof(Determiner), typeof(IConjunctive), typeof(IPrepositional) };
            var wordsByCount = from word in doc.Words.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                               where (!excluded.Contains(word.Type))
                               group word by new
                               {
                                   word.Type,
                                   word.Text
                               };

            wordsByCount.ForAll(
            grp =>
            {
                foreach (var w in grp) {
                    w.Weight = grp.Count();
                }
            });
        }

        /// <summary>
        /// For each noun parent in a document that is similar to another noun parent, increase the weight of that noun
        /// </summary>
        /// <param name="doc">Document containing the componentPhrases to weight</param>
        private static void WeightSimilarNounPhrases(Document doc)
        {

            var similarNounPhraseLookup = (from NP in doc.Phrases.GetNounPhrases().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)
                                           select NP)
                                           .ToLookup(key => key,
                                            new NounPhraseComparer());
            foreach (var outerNP in doc.Phrases.GetNounPhrases().AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)) {
                var similarPhrases = from potentialM in
                                         (from innerNP in similarNounPhraseLookup[outerNP]
                                          select new
                                          {
                                              NP = outerNP,
                                              innerNP,
                                              similarityRatio = Thesaurus.GetSimilarityRatio(outerNP, innerNP)
                                          })
                                     where potentialM.similarityRatio >= 0.6f
                                     select potentialM;
                //Need to fix this. Its causing stack overflow
                foreach (var match in similarPhrases) {
                    //match.NP.Weight += match.innerNP.Weight * (decimal)match.similarityRatio;
                    //match.innerNP.Weight += match.NP.Weight * (decimal)match.similarityRatio;

                }
            }

        }
        //static double InverserDocumentFrequency(IEnumerable<Document> documentGroup, bool useSynonyms = false) {
        //    var numDocs = documentGroup.Count();
        //    var wordsWithFreqPairs = from doc in documentGroup  from word in doc.Words group word by word.Text 
        //}
        private struct NounPhraseComparer : IEqualityComparer<NounPhrase>
        {
            public bool Equals(NounPhrase x, NounPhrase y)
            {
                return x == null || y == null ? false : x.IsSimilarTo(y);
            }

            public int GetHashCode(NounPhrase obj)
            {
                return obj != null ? 1 : 0;
            }
        }

        public static void HackSubjectPropernounImportance(Document doc)
        {

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

        private static void WeightWordsBySyntacticSequence(Document doc)
        {


            //SIX PHASES

            //PHASE 2 - word Weight based on part of speech and neighbors' (+2) part of speech
            //PHASE 3 - Standard parent Weight based on parent part of speech (standardization) - COMPLETE
            //PHASE 4 - Phrase Weight based on part of speech and neibhors' (full sentence) part of speech
            //PHASE 5 - FREQUENCIES
            // .1 - Frequency of word/Phrase in document
            // .2 - Frequency of word/Phrase in document compared to second documents in set -EXCLUDED FOR 1-DOCUMENT DEMO
            //PHASE 6 - SYNONYMS
            //ALLUAN READ:            // .1 - Frequency of word (/Phrase?) in document - COMPLETE MINUS VERBS (couldn't search the verb thesaurus in any way)
            // .2 - Frequency of word (/Phrase?) in document compared to second documents in set -EXCLUDED FOR 1-DOCUMENT DEMO



            int primary, secondary, tertiary, quaternary, quinary, senary;
            int based = 20;
            primary = (secondary = (tertiary = (quaternary = (quinary = (senary = 0) + based) + based) + based) + based) + based;


            //PHASE 1 - Standard word Weight based on part of speech (standardization)
            //COMPLETE - easy peasy.

            //Output.WriteLine("Standard word Weight based on POS:");
            doc.Sentences.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax).ForAll(s =>
            {
                //////Output.WriteLine(s);

                foreach (Word w in s.Words) {
                    //Output.WriteLine(w);

                    new Switch(w)
                        .Case<Noun>(n =>
                        {
                            w.Weight = primary;

                        })
                        .Case<Verb>(v =>
                        {
                            w.Weight = secondary;
                        })
                        .Case<Adjective>(adj =>
                        {
                            w.Weight = tertiary;
                        })
                        .Case<Adverb>(adv =>
                        {
                            w.Weight = quaternary;
                        })
                        .Case<Pronoun>(pn =>
                        {
                            w.Weight = quinary;
                        })
                        .Default(def =>
                        {
                            w.Weight = senary;
                        });

                    //Output.WriteLine(w.Weight);

                }


            });





            //PHASE 2 - word Weight based on part of speech and neighbors' (+2) part of speech
            // WORKS, BUT
            // NEED FORMULAS FOR MODIFIER VARIABLES - WHAT SHOULD THESE BE?
            decimal modOne, modTwo;
            modOne = modTwo = 0;
            foreach (Sentence s in doc.Sentences.AsParallel().WithDegreeOfParallelism(Concurrency.CurrentMax)) {
                //////Output.WriteLine(s);

                foreach (Word w in s.Words) {

                    Word next = w ?? w.NextWord;
                    Word nextNext = next ?? next.NextWord;

                    //cut?
                    Word prev = w ?? w.PreviousWord;
                    Word prevPrev = prev ?? prev.PreviousWord;


                    new Switch(w)
                       .Case<Noun>(n =>
                       {

                           Noun(next, nextNext, out modOne, out modTwo);

                       })
                       .Case<Verb>(v =>
                       {
                           Verb(next, nextNext, out modOne, out modTwo);
                       })
                       .Case<Adjective>(adj =>
                       {
                           Adjective(next, nextNext, out modOne, out modTwo);
                       })
                       .Case<Adverb>(adv =>
                       {
                           Adverb(next, nextNext, out modOne, out modTwo);
                       })
                       .Case<Pronoun>(pn =>
                       {
                           Pronoun(next, nextNext, out modOne, out modTwo);
                       })

                       .Case<Preposition>(pn =>
                       {
                           Preposition(next, nextNext, out modOne, out modTwo);
                       })
                       .Case<Determiner>(d =>
                       {
                           Determiner(next, nextNext, out modOne, out modTwo);
                       })
                       .Default(def =>
                       {
                           modOne = 0.1m;

                           //second (UNCAUGHT -> UNCAUGHT)
                           modTwo = UncaughtUncaught(nextNext);
                       });


                    w.Weight += (w.Weight * (modOne * modTwo)) / 3;

                }


            }





            #region Extra Code



        }


            #endregion

        private static void Determiner(Word next, Word nextNext, out decimal outModOne, out decimal outModTwo)
        {
            decimal modOne = 0;
            decimal modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn =>
                   {
                       modOne = 0.9m; //determiner-noun

                       //second (Determiner -> Noun)
                       modTwo = PronounNoun(nextNext);
                   })
                   .Case<Adjective>(advn =>
                   {
                       modOne = 0.8m;  //deteminer-adjective

                       //second (Determiner -> Adjective)
                       modTwo = PronounAdjective(nextNext);
                   })
                   .Case<Adverb>(advn =>
                   {
                       modOne = 0.7m;  //determiner-adverb

                       //second (Determiner -> Adverb)
                       modTwo = PronounAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn =>
                   {
                       modOne = 0.9m; //determiner-pronoun

                       //second (Determiner -> Noun)
                       modTwo = PronounPronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn =>
                   {
                       modOne = 0.7m; //determiner-tolinker

                       //second (Determiner -> ToLinker)
                       modTwo = PronounToLinker(nextNext);
                   })
                   .Case<Preposition>(pren =>
                   {
                       modOne = 0.3m; //determiner positional

                       //second Determiner -> Preposition)
                       modTwo = PronounPreposition(nextNext);
                   })
                   .Case<Determiner>(dn =>
                   {
                       modOne = 0; //determiner-determiner

                       //second (Determiner -> Determiner)
                       modTwo = PronounDeterminer(nextNext);
                   })
                   .Default(def =>
                   {
                       modOne = 0.1m;

                       //second (Determiner -> UNCAUGHT)
                       modTwo = PronounUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }
        private static void Preposition(Word next, Word nextNext, out decimal outModOne, out decimal outModTwo)
        {
            decimal modOne = 0;
            decimal modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn =>
                   {
                       modOne = 0.8m; // 

                       //second (Preposition -> Noun)
                       modTwo = PrepositionNoun(nextNext);
                   })
                   .Case<Pronoun>(pnn =>
                   {
                       modOne = 0.8m; // 

                       //second (Preposition -> Noun)
                       modTwo = PrepositionPronoun(nextNext);
                   })
                   .Case<Determiner>(dn =>
                   {
                       modOne = 0.7m; //determiner

                       //second (Preposition -> Determiner)
                       modTwo = PrepositionDeterminer(nextNext);
                   })
                   .Default(def =>
                   {
                       modOne = 0.1m;

                       //second (Preposition -> UNCAUGHT)
                       modTwo = PrepositionUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }

        private static void Pronoun(Word next, Word nextNext, out decimal outModOne, out decimal outModTwo)
        {
            decimal modOne = 0;
            decimal modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn =>
                   {
                       modOne = 0.9m; //compound noun/pronoun / possessed by pronoun

                       //second (Pronoun -> Noun)
                       modTwo = PronounNoun(nextNext);
                   })
                   .Case<Adjective>(advn =>
                   {
                       modOne = 0.8m;  //possessed/descriptor 

                       //second (Pronoun -> Adjective)
                       modTwo = PronounAdjective(nextNext);
                   })
                   .Case<Adverb>(advn =>
                   {
                       modOne = 0.7m;  //pronoun amplifier

                       //second (Pronoun -> Adverb)
                       modTwo = PronounAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn =>
                   {
                       modOne = 0.9m; //compound pronoun 
                       //second (Pronoun -> Noun)
                       modTwo = PronounPronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn =>
                   {
                       modOne = 0.6m; //pronoun directional

                       //second (Pronoun -> ToLinker)
                       modTwo = PronounToLinker(nextNext);
                   })
                   .Case<Preposition>(pren =>
                   {
                       modOne = 0.5m; //pronoun positional

                       //second (Pronoun -> Preposition)
                       modTwo = PronounPreposition(nextNext);
                   })
                   .Case<Determiner>(dn =>
                   {
                       modOne = 0.7m; //determiner

                       //second (Pronoun -> Determiner)
                       modTwo = PronounDeterminer(nextNext);
                   })
                   .Default(def =>
                   {
                       modOne = 0.1m;

                       //second (Pronoun -> UNCAUGHT)
                       modTwo = PronounUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }

        private static void Adverb(Word next, Word nextNext, out decimal outModOne, out decimal outModTwo)
        {
            decimal modOne = 0;
            decimal modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn =>
                   {
                       modOne = 0.9m; //adverbial noun

                       //second (Adverb -> Noun)
                       modTwo = AdverbNoun(nextNext);
                   })
                   .Case<Adjective>(advn =>
                   {
                       modOne = 0.8m;  //normal adv-adj

                       //second (Adverb -> Adjective)
                       modTwo = AdverbAdjective(nextNext);
                   })
                   .Case<Adverb>(advn =>
                   {
                       modOne = 0.7m;  //bi-adverbial

                       //second (Adverb -> Adverb)
                       modTwo = AdverbAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn =>
                   {
                       modOne = 0.9m; //adverbial pronoun

                       //second (Adverb -> Noun)
                       modTwo = AdverbPronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn =>
                   {
                       modOne = 0.7m; //adverb directional

                       //second (Adverb -> ToLinker)
                       modTwo = AdverbToLinker(nextNext);
                   })
                   .Case<Preposition>(pren =>
                   {
                       modOne = 0.5m; //adverb positional

                       //second (Adverb -> Preposition)
                       modTwo = AdverbPreposition(nextNext);
                   })
                   .Case<Determiner>(dn =>
                   {
                       modOne = 0.7m; //determiner

                       //second (Adverb -> Determiner)
                       modTwo = AdverbDeterminer(nextNext);
                   })
                   .Default(def =>
                   {
                       modOne = 0.1m;

                       //second (Adverb -> UNCAUGHT)
                       modTwo = AdverbUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }

        private static void Adjective(Word next, Word nextNext, out decimal outModOne, out decimal outModTwo)
        {
            decimal modOne = 0;
            decimal modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn =>
                   {
                       modOne = 0.7m; //noun descriptor

                       //second (Adjective -> Noun)
                       modTwo = AdjectiveNoun(nextNext);
                   })
                   .Case<Adjective>(advn =>
                   {
                       modOne = 0.5m;  //double descriptor

                       //second (Adjective -> Adjective)
                       modTwo = AdjectiveAdjective(nextNext);
                   })
                   .Case<Adverb>(advn =>
                   {
                       modOne = 0.5m;  //coloured brilliantly

                       //second (Adjective -> Adverb)
                       modTwo = AdjectiveAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn =>
                   {
                       modOne = 0.7m; //noun descriptor

                       //second (Adjective -> Noun)
                       modTwo = AdjectivePronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn =>
                   {
                       modOne = 0.4m; //adjective directional

                       //second (Adjective -> ToLinker)
                       modTwo = AdjectiveToLinker(nextNext);
                   })
                   .Case<Preposition>(pren =>
                   {
                       modOne = 0.4m; //adjective positional

                       //second (Adjective -> Prepositional)
                       modTwo = AdjectivePreposition(nextNext);
                   })
                   .Case<Determiner>(dn =>
                   {
                       modOne = 0.4m; //determiner

                       //second (Adjective -> Determiner)
                       modTwo = AdjectiveDeterminer(nextNext);
                   })
                   .Default(def =>
                   {
                       modOne = 0.1m;

                       //second (Adjective -> UNCAUGHT)
                       modTwo = AdjectiveUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }

        private static void Verb(Word next, Word nextNext, out decimal outModOne, out decimal outModTwo)
        {
            decimal modOne = 0;
            decimal modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn =>
                   {
                       modOne = 0.9m; //verb actor

                       //second (Verb -> Noun)
                       modTwo = VerbNoun(nextNext);
                   })
                   .Case<PastParticipleVerb>(vn =>
                   {
                       modOne = 0.7m; //verb-verb descriptor

                       //second (Verb -> PastParticipleVerb)
                       modTwo = VerbPastParticipleVerb(nextNext);
                   })
                   .Case<Adjective>(advn =>
                   {
                       modOne = 0.6m;  //verb state

                       //second (Verb -> Adjective)
                       modTwo = VerbAdjective(nextNext);
                   })
                   .Case<Adverb>(advn =>
                   {
                       modOne = 0.7m;  //perfect adverb

                       //second (Verb -> Adverb)
                       modTwo = VerbAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn =>
                   {
                       modOne = 0.9m; //verb actor

                       //second (Verb -> Pronoun)
                       modTwo = VerbPronoun(nextNext);
                   })
                   .Case<ToLinker>(lnkn =>
                   {
                       modOne = 0.6m; //verb directional

                       //second (Verb -> ToLinker)
                       modTwo = VerbToLinker(nextNext);
                   })
                   .Case<Preposition>(pren =>
                   {
                       modOne = 0.5m; //verb-verb positional

                       //second (Verb -> Preposition)
                       modTwo = VerbPreposition(nextNext);
                   })
                   .Case<Determiner>(dn =>
                   {
                       modOne = 0.4m; //determiner

                       //second (Verb -> Determiner)
                       modTwo = VerbDeterminer(nextNext);
                   })
                   .Default(def =>
                   {
                       modOne = 0.1m;

                       //second (Verb -> UNCAUGHT)
                       modTwo = VerbUncaught(nextNext);
                   });

            outModOne = modOne;
            outModTwo = modTwo;
        }


        private static void Noun(Word next, Word nextNext, out decimal outModOne, out decimal outModTwo)
        {
            decimal modOne = 0; //Renamed parameters and bound created temporary variables to pass into the switch blocks 
            decimal modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn =>
                   {
                       modOne = 0.9m; //compound noun

                       //second (Noun -> Noun)
                       modTwo = NounNoun(nextNext);
                   })
                   .Case<Adjective>(nadj =>
                   {
                       modOne = 0.8m; //possessive

                       //second (Noun -> Adjective)
                       modTwo = NounAdjective(nextNext);
                   })
                   .Case<Verb>(nv =>
                   {
                       modOne = 0.7m; //noun action or descriptor

                       //second (Noun -> Verb)
                       modTwo = NounVerb(nextNext);
                   })
                   .Case<Adverb>(advn =>
                   {
                       modOne = 0.7m;  //noun amplifier

                       //second (Noun -> Adverb)
                       modTwo = NounAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn =>
                   {
                       modOne = 0.9m; //compound noun

                       //second (Noun -> Pronoun)
                       modTwo = NounPronoun(nextNext);


                   })
                   .Case<ToLinker>(lnkn =>
                   {
                       modOne = 0.6m; //noun to link

                       //second (Noun -> ToLinker)
                       modTwo = NounToLinker(nextNext);
                   })
                   .Case<Preposition>(pren =>
                   {
                       modOne = 0.6m; //noun positional

                       //second (Noun -> Preposition)
                       modTwo = NounPreposition(nextNext);
                   })
                   .Case<Determiner>(dn =>
                   {
                       modOne = 0.5m; //determiner

                       //second (Noun -> Determiner)
                       modTwo = NounDeterminer(nextNext);
                   })
                   .Default(def =>
                   {
                       modOne = 0.1m;

                       //second (Noun -> UNCAUGHT)
                       modTwo = NounUncaught(nextNext);
                   });
            outModOne = modOne;//Set parameters = temporaries
            outModTwo = modTwo;//There is a better way to handle this, but this works without any changes
        }

        private static decimal UncaughtUncaught(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.1m; //uncaught-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.1m; //uncaught-uncaught-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.1m;  //uncaught-uncaught-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.1m;  //uncaught-uncaught-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.1m; //uncaught-uncaught-noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.1m; //uncaught-uncaught directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.1m; //uncaught-uncaught positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.1m; //uncaught-uncaught determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m; //uncaught-uncaught-uncaught (epic fail)
                });
            return modTwo;
        }

        private static decimal PrepositionUncaught(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.5m; //preposition-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.4m; //preposition-uncaught-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.3m;  //preposition-uncaught-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.3m;  //preposition-uncaught-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.5m; //preposition-uncaught-noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.2m; //preposition-uncaught directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.2m; //preposition-uncaught positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.2m; //preposition-uncaught determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PrepositionDeterminer(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.7m; //preposition-determiner-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.6m; //preposition-determiner-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.5m;  //preposition-determiner-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.4m;  //preposition-determiner-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.6m; //preposition-determiner-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.3m; //preposition-determiner directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.3m; //preposition-determiner positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.3m; //preposition-determiner determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PrepositionPronoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.5m; //preposition-compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.8m; //preposition-noun-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.3m;  //preposition-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.3m;  //preposition-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.8m; //preposition-compound noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.6m; //preposition-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.2m; //preposition-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.9m; //preposition-noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PrepositionNoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.5m; //preposition-compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.8m; //preposition-noun-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.3m;  //preposition-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.3m;  //preposition-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.3m; //preposition-compound noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.8m; //preposition-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.6m; //preposition-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.2m; //preposition-noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PronounUncaught(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.3m; //pronoun-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.2m; //pronoun-uncaught-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.2m;  //pronoun-uncaught-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.2m;  //pronoun-uncaught-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.3m; //pronoun-uncaught-noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.2m; //pronoun-uncaught directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.2m; //pronoun-uncaught positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.2m; //pronoun-uncaught determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PronounDeterminer(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.9m; //pronoun-determiner-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.7m; //pronoun-determiner-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.6m;  //pronoun-determiner-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.5m;  //pronoun-determiner-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.9m; //pronoun-determiner-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.1m; //pronoun-determiner directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.3m; //pronoun-determiner positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //pronoun-determiner determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PronounPreposition(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.8m; //pronoun-preposition-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.6m; //pronoun-preposition-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.5m;  //pronoun-preposition-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.6m;  //pronoun-preposition-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.8m; //pronoun-preposition-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.4m; //pronoun-preposition directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.4m; //pronoun-preposition positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.3m; //pronoun-preposition determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PronounToLinker(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.9m; //pronoun-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.8m; //pronoun-tolinker-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.7m;  //pronoun-tolinker-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.6m;  //pronoun-tolinker-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.9m; //pronoun-tolinker-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //pronoun-tolinker directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.5m; //pronoun-tolinker positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.7m; //pronoun-tolinker determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PronounPronoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.9m; //triple compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.8m; //compound noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.7m;  //compound noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.6m;  //compound noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.9m; //triple compound (possessive) noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.7m; //compound noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.7m; //compound noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.5m; //compound noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PronounAdverb(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.8m; //pronoun-adverb-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.7m; //pronoun-adverb-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.6m;  //pronoun-adverb-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.5m;  //pronoun-adverb-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = .8m; //pronoun-adverb-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.7m; //pronoun-adverb directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.6m; //pronoun-adverb positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.5m; //pronoun-adverb determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PronounAdjective(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.9m; //pronoun-adjective-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.8m; //pronoun-adjective-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.7m;  //pronoun-adjective-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.6m;  //pronoun-adjective-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.9m; //pronoun-adjective-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.2m; //pronoun-adjective directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.2m; //pronoun-adjective positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.7m; //pronoun-adjective determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal PronounNoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.9m; //triple compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.8m; //compound noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.7m;  //compound noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.7m;  //compound noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.9m; //triple compound (possessive) noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.8m; //compound noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.7m; //compound noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.5m; //compound noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal AdverbUncaught(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.5m; //adverb-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.4m; //adverb-uncaught-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.3m;  //adverb-uncaught-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.3m;  //adverb-uncaught-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.5m; //adverb-uncaught-noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.3m; //adverb-uncaught directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.2m; //adverb-uncaught positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.3m; //adverb-uncaught determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal AdverbDeterminer(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.9m; //adverb-determiner-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.3m; //adverb-determiner-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.8m;  //adverb-determiner-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.7m;  //adverb-determiner-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.9m; //adverb-determiner-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adverb-determiner directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.3m; //adverb-determiner positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adverb-determiner determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal AdverbPreposition(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //adverb-preposition-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //adverb-preposition-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //adverb-preposition-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adverb-preposition-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //adverb-preposition-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adverb-preposition directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //adverb-preposition positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adverb-preposition determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdverbToLinker(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.9m; //adverb-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.8m; //adverb-tolinker-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.7m;  //adverb-tolinker-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.6m;  //adverb-tolinker-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.9m; //adverb-tolinker-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adverb-tolinker directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.3m; //adverb-tolinker positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.4m; //adverb-tolinker determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal AdverbPronoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.5m; //adverb compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.4m; //adverb-noun-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.3m;  //adverb-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adverb-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.5m; //adverb compound noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.6m; //adverb-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.4m; //adverb-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.4m; //adverb-noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal AdverbAdverb(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.3m; //adverb-adverb-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.3m; //adverb-adverb-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.3m;  //adverb-adverb-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.1m;  //tri adverbial
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.3m; //adverb-adverb-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.3m; //adverb-adverb directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.2m; //adverb-adverb positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.3m; //adverb-adverb determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal AdverbAdjective(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.6m; //adverb-adjective-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.4m; //adverb-adjective-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.5m;  //adverb-adjective-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.4m;  //adverb-adjective-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.6m; //adverb-adjective-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.5m; //adverb-adjective directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.4m; //adverb-adjective positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.3m; //adverb-adjective determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal AdverbNoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.5m; //adverb -> compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0.4m; //adverb-noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0.3m;  //adverb-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0.3m;  //adverb-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0.5m; //adverb -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0.3m; //adverb-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0.4m; //adverb-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0.2m; //adverb-noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0.1m;
                });
            return modTwo;
        }

        private static decimal AdjectiveUncaught(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0.4m; //adjective-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //adjective-uncaught-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //adjective-uncaught-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adjective-uncaught-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //adjective -> uncaught -> noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adjective-uncaught directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //adjective-uncaught positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adjective-uncaught determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdjectiveDeterminer(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //adjective-determiner-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //adjective-determiner descriptor 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //adjective-determiner-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adjective-determiner-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //adjective-determiner-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adjective-determiner directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //adjective-determiner positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adjective-determiner determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdjectivePreposition(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //adjective-prepositional-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //adjective-prepositional descriptor 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //adjective-prepositional-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adjective-prepositional-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //adjective-prepositional-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adjective-prepositional directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //adjective-prepositional positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adjective-prepositional determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdjectiveToLinker(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //adjective-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //adjective-tolinker descriptor 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //adjective-tolinker-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adjective-tolinker-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //adjective-tolinker-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adjective-tolinker directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //adjective-tolinker positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adjective-tolinker determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdjectivePronoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //adjective -> compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //adjective-noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //adjective-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adjective-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //adjective -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adjective-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //adjective-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adjective-noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdjectiveAdverb(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //adjective-adverb-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //adjective-adverb-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //adjective-adverb-adjective (triple compound)
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adjective-adverb-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //adjective-adverb-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adjective-adverb-directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //adjective-adverb positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adjective-adverb determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdjectiveAdjective(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //compound adjective -> noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //compound adjective -> verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //compound adjective -> adjective (triple compound)
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //compound adjective -> adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //compound adjective -> noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //compound adjective -> directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //compound adjective -> positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //compound adjective -> determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdjectiveNoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //adjective -> compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //adjective-noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //adjective-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //adjective-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //adjective -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //adjective-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //adjective-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //adjective-noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbUncaught(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-uncaught-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-uncaught-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-uncaught-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> uncaught -> noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-uncaught directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-uncaught positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-uncaught determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbDeterminer(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb-determiner-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-determiner-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-determiner-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-determiner-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> determiner -> noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-determiner directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-determiner positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-determiner determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbPreposition(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb-preposition-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-preposition-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-preposition-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-preposition-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> preposition -> noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-preposition directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-preposition positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-preposition determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbToLinker(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-tolinker-pastverb (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-tolinker-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-tolinker-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> tolinker -> noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-tolinker directional (possible?)
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-tolinker positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-tolinker determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbPronoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb-pronoun-noun (compound)
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-pronoun-pastverb
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-pronoun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-pronoun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> pronoun -> noun  (compound)
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-pronoun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-pronoun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-pronoun determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbAdverb(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb-adverb-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-adverb-pastverb
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-adverb-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-adverb-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> adverb -> noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-aadverb directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-adverb positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-adverb determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbAdjective(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb-adjective-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-adjective-pastverb
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-adjective-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-adjective-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> adjective -> noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-adjective directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-adjective positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-adjective determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbPastParticipleVerb(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb-pastverb -> compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-pastverb-pastverb
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-pastverb-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-pastverb-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> pastverb -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-pastverb directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-pastverb positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-pastverb determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbNoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //verb -> compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //verb-noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //verb-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //verb-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //verb -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //verb-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //verb-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //verb-noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounUncaught(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nun =>
                {
                    modTwo = 0; //noun-uncaught-noun
                })
                .Case<PastParticipleVerb>(nupv =>
                {
                    modTwo = 0; //noun-uncaught-pastverb 
                })
                .Case<Adjective>(nuadj =>
                {
                    modTwo = 0;  //noun-uncaught-adjective
                })
                .Case<Adverb>(nuadv =>
                {
                    modTwo = 0;  //noun-uncaught-adverb
                })
                .Case<Pronoun>(nupn =>
                {
                    modTwo = 0; //noun -> uncaught -> noun 
                })
                .Case<ToLinker>(nulnk =>
                {
                    modTwo = 0; //noun-uncaught directional 
                })
                .Case<Preposition>(nup =>
                {
                    modTwo = 0; //noun-uncaught positional
                })
                .Case<Determiner>(nud =>
                {
                    modTwo = 0; //noun-uncaught determiner
                })
                .Default(nuu =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounDeterminer(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(ndn =>
                {
                    modTwo = 0; //noun-determiner-noun
                })
                .Case<PastParticipleVerb>(ndpv =>
                {
                    modTwo = 0; //noun-determiner-verb descriptor (possible?)
                })
                .Case<Adjective>(ndadj =>
                {
                    modTwo = 0;  //noun-determiner-adjective
                })
                .Case<Adverb>(ndav =>
                {
                    modTwo = 0;  //noun-determiner-adverb
                })
                .Case<Pronoun>(ndpn =>
                {
                    modTwo = 0; //noun-determiner-noun
                })
                .Case<ToLinker>(ndlnk =>
                {
                    modTwo = 0; //noun-determiner directional
                })
                .Case<Preposition>(ndp =>
                {
                    modTwo = 0; //noun-determiner positional
                })
                .Case<Determiner>(ndd =>
                {
                    modTwo = 0; //noun-determiner determiner
                })
                .Default(ndu =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounPreposition(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(npn =>
                {
                    modTwo = 0; //noun-preposition-noun
                })
                .Case<PastParticipleVerb>(nppv =>
                {
                    modTwo = 0; //noun-preposition-verb descriptor (possible?)
                })
                .Case<Adjective>(npadj =>
                {
                    modTwo = 0;  //noun-preposition-adjective
                })
                .Case<Adverb>(npadv =>
                {
                    modTwo = 0;  //noun-preposition-adverb
                })
                .Case<Pronoun>(nppn =>
                {
                    modTwo = 0; //noun-preposition-noun
                })
                .Case<ToLinker>(nplnk =>
                {
                    modTwo = 0; //noun-preposition directional
                })
                .Case<Preposition>(npp =>
                {
                    modTwo = 0; //noun-preposition positional
                })
                .Case<Determiner>(npd =>
                {
                    modTwo = 0; //noun-preposition determiner
                })
                .Default(npu =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounToLinker(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nlnkn =>
                {
                    modTwo = 0; //noun-tolinker-noun
                })
                .Case<PastParticipleVerb>(nlnkpv =>
                {
                    modTwo = 0; //noun-tolinker-verb descriptor (possible?)
                })
                .Case<Adjective>(nlnkadj =>
                {
                    modTwo = 0;  //noun-tolinker-adjective
                })
                .Case<Adverb>(nlnkadv =>
                {
                    modTwo = 0;  //noun-tolinker-adverb
                })
                .Case<Pronoun>(nlnkpn =>
                {
                    modTwo = 0; //noun-tolinker-noun
                })
                .Case<ToLinker>(nlnklnk =>
                {
                    modTwo = 0; //noun-tolinker directional
                })
                .Case<Preposition>(nlnkp =>
                {
                    modTwo = 0; //noun-tolinker positional
                })
                .Case<Determiner>(nlnkd =>
                {
                    modTwo = 0; //noun-tolinker determiner
                })
                .Default(nlinku =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounPronoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(npnn =>
                {
                    modTwo = 0; //triple compound noun
                })
                .Case<PastParticipleVerb>(npnpv =>
                {
                    modTwo = 0; //compound noun-verb descriptor (possible?)
                })
                .Case<Adjective>(npnadj =>
                {
                    modTwo = 0;  //compound noun-adjective
                })
                .Case<Adverb>(npnadv =>
                {
                    modTwo = 0;  //compound noun-adverb
                })
                .Case<Pronoun>(npnpn =>
                {
                    modTwo = 0; //triple compound noun
                })
                .Case<ToLinker>(npnlnk =>
                {
                    modTwo = 0; //compound noun directional
                })
                .Case<Preposition>(npnpre =>
                {
                    modTwo = 0; //compound noun positional
                })
                .Case<Determiner>(npnd =>
                {
                    modTwo = 0; //compound noun determiner
                })
                .Default(npnu =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounAdverb(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nadvn =>
                {
                    modTwo = 0; //noun-adverb-noun
                })
                .Case<PastParticipleVerb>(nadvpv =>
                {
                    modTwo = 0; //noun-adverb-verb descriptor (possible?)
                })
                .Case<Adjective>(nadvadj =>
                {
                    modTwo = 0;  //noun-adverb-adjective
                })
                .Case<Adverb>(nadvadv =>
                {
                    modTwo = 0;  //noun-adverb-adverb
                })
                .Case<Pronoun>(nadvpn =>
                {
                    modTwo = 0; //noun-adverb-noun
                })
                .Case<ToLinker>(nadvlnk =>
                {
                    modTwo = 0; //noun-adverb directional
                })
                .Case<Preposition>(nadvpre =>
                {
                    modTwo = 0; //noun-adverb positional
                })
                .Case<Determiner>(nadvd =>
                {
                    modTwo = 0; //noun-adverb determiner
                })
                .Default(nadvu =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounVerb(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nvn =>
                {
                    modTwo = 0; //noun-verb-noun
                })
                .Case<PastParticipleVerb>(nvpv =>
                {
                    modTwo = 0; //noun-verb-verb descriptor (possible?)
                })
                .Case<Adjective>(nvadj =>
                {
                    modTwo = 0;  //noun-verb-adjective
                })
                .Case<Adverb>(nvadv =>
                {
                    modTwo = 0;  //noun-verb-adverb
                })
                .Case<Pronoun>(nvpn =>
                {
                    modTwo = 0; //noun-verb-noun
                })
                .Case<ToLinker>(nvlnk =>
                {
                    modTwo = 0; //noun-verb directional
                })
                .Case<Preposition>(nvpre =>
                {
                    modTwo = 0; //noun-verb positional
                })
                .Case<Determiner>(nvd =>
                {
                    modTwo = 0; //noun-verb determiner
                })
                .Default(nvu =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounAdjective(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nadjn =>
                {
                    modTwo = 0; //noun-adjective-noun
                })
                .Case<PastParticipleVerb>(nadjpv =>
                {
                    modTwo = 0; //noun-adjective-verb descriptor (possible?)
                })
                .Case<Adjective>(nadjadj =>
                {
                    modTwo = 0;  //noun-adjective-adjective
                })
                .Case<Adverb>(nadjav =>
                {
                    modTwo = 0;  //noun-adjective-adverb
                })
                .Case<Pronoun>(nadjn =>
                {
                    modTwo = 0; //noun-adjective-noun
                })
                .Case<ToLinker>(nadjlnk =>
                {
                    modTwo = 0; //noun-adjective directional
                })
                .Case<Preposition>(nadjpre =>
                {
                    modTwo = 0; //noun-adjective positional
                })
                .Case<Determiner>(nadjd =>
                {
                    modTwo = 0; //noun-adjective determiner
                })
                .Default(nadju =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounNoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nnn =>
                {
                    modTwo = 0; //triple compound noun
                })
                .Case<PastParticipleVerb>(nnpv =>
                {
                    modTwo = 0; //compound noun-verb descriptor (possible?)
                })
                .Case<Adjective>(nnadj =>
                {
                    modTwo = 0;  //compound noun-adjective
                })
                .Case<Adverb>(nnadv =>
                {
                    modTwo = 0;  //compound noun-adverb
                })
                .Case<Pronoun>(nnpn =>
                {
                    modTwo = 0; //triple compound noun
                })
                .Case<ToLinker>(nnlnk =>
                {
                    modTwo = 0; //compound noun directional
                })
                .Case<Preposition>(nnpre =>
                {
                    modTwo = 0; //compound noun positional
                })
                .Case<Determiner>(nnd =>
                {
                    modTwo = 0; //compound noun determiner
                })
                .Default(nnu =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }
        private static decimal DeterminerUncaught(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //determiner-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //determiner-uncaught-pastverb 
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //determiner-uncaught-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //determiner-uncaught-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //determineruncaught-noun 
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //determiner-uncaught directional 
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //determiner-uncaught positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //determiner-uncaught determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal DeterminerDeterminer(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //determiner-determiner-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //determiner-determiner-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //determiner-determiner-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //determiner-determiner-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //determiner-determiner-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //determiner-determiner directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //determiner-determiner positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //determiner-determiner determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal DeterminerPreposition(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //determiner-preposition-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //determiner-preposition-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //determiner-preposition-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //determiner-preposition-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //determiner-preposition-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //determiner-preposition directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //determiner-preposition positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //determiner-preposition determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal DeterminerToLinker(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //determiner-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //determiner-tolinker-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //determiner-tolinker-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //determiner-tolinker-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //determiner-tolinker-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //determiner-tolinker directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //determiner-tolinker positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //determiner-tolinker determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal DeterminerPronoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //determiner compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //determiner-noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //determiner-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //determiner-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //determiner compound noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //determiner-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //determiner-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //determiner-noun-determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal DeterminerAdverb(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //determiner-adverb-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //determiner-adverb-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //determiner-adverb-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //determiner-adverb-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //determiner-adverb-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //determiner-adverb directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //determiner-adverb positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //determiner-adverb determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal DeterminerAdjective(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //determiner-adjective-noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //determiner-adjective-verb descriptor
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //determiner-adjective-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //determiner-adjective-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //determiner-adjective-noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //determiner-adjective directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //determiner-adjective positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //determiner-adjective determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal DeterminerNoun(Word nextNext)
        {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn =>
                {
                    modTwo = 0; //determiner-compound noun
                })
                .Case<PastParticipleVerb>(vn =>
                {
                    modTwo = 0; //determiner-noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn =>
                {
                    modTwo = 0;  //determiner-noun-adjective
                })
                .Case<Adverb>(advn =>
                {
                    modTwo = 0;  //determiner-noun-adverb
                })
                .Case<Pronoun>(pnn =>
                {
                    modTwo = 0; //determiner-compound (possessive) noun
                })
                .Case<ToLinker>(lnkn =>
                {
                    modTwo = 0; //determiner-noun directional
                })
                .Case<Preposition>(pren =>
                {
                    modTwo = 0; //determiner-noun positional
                })
                .Case<Determiner>(dn =>
                {
                    modTwo = 0; //determiner-noun determiner
                })
                .Default(def =>
                {
                    modTwo = 0;
                });
            return modTwo;

        }
    }
}
