using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using SharpNLPTaggingModule;
using System.IO;
using System.Xml;
using LASI.FileSystem.FileTypes;
using LASI.Algorithm.Binding;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Algorithm.Weighting;
using LASI.Algorithm.Thesauri;
using System.Text.RegularExpressions;
using LASI.Utilities.TypedSwitch;


namespace Erik_Experimentation
{
    class Program
    {

        static void Main(string[] args) {

            //var nounTest2 = new NounThesaurus(@"..\..\..\..\WordNetThesaurusData\data.noun");
            //nounTest2.Load();
            //string key = "man";
            //HashSet<string> synonyms2;
            //synonyms2 = nounTest2.SearchFor(key);
            //foreach (var s in synonyms2)
            //{
            //    Console.WriteLine(s);
            //}

            //var adjTest = new AdjectiveThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adj");
            //adjTest.Load();
            //string key = "able";
            //adjTest.SearchFor(key);

            //var advTest = new AdverbThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adv");
            //advTest.Load();
            //string key = "vastly";
            //advTest.SearchFor(key);


            //Keeps the console window open until the escape key is pressed
            //Console.WriteLine("Press escape to exit");
            //for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey())
            //{
            //    Console.WriteLine("Press escape to exit");
            //}

            var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\CynosureEPR\Desktop\weight2.txt");
            var tagged = tagger.ProcessFile();
            var paragraphs = new TaggedFileParser(tagged).LoadParagraphs();
            var document = new Document(paragraphs);

            //foreach (var i in document.Phrases)
            //{
            //    Console.WriteLine(i);
            //}

            //foreach (var i in document.Words)
            //{
            //    Console.WriteLine(i);
            //}





            //SIX PHASES
            //PHASE 1 - Standard word Weight based on part of speech (standardization) - COMPLETE
            //PHASE 2 - Word Weight based on part of speech and neighbors' (+2) part of speech
            //PHASE 3 - Standard phrase Weight based on phrase part of speech (standardization) - COMPLETE
            //PHASE 4 - Phrase Weight based on part of speech and neibhors' (full sentence) part of speech
            //PHASE 5 - FREQUENCIES
            // .1 - Frequency of Word/Phrase in document
            // .2 - Frequency of Word/Phrase in document compared to other documents in set -EXCLUDED FOR 1-DOCUMENT DEMO
            //PHASE 6 - SYNONYMS
            //ALLUAN READ:            // .1 - Frequency of Word (/Phrase?) in document - COMPLETE MINUS VERBS (couldn't search the verb thesaurus in any way)
            // .2 - Frequency of Word (/Phrase?) in document compared to other documents in set -EXCLUDED FOR 1-DOCUMENT DEMO



            int primary, secondary, tertiary, quaternary, quinary, senary;
            int based = 20;
            primary = (secondary = (tertiary = (quaternary = (quinary = (senary = 0) + based) + based) + based) + based) + based;


            //PHASE 1 - Standard word Weight based on part of speech (standardization)
            //COMPLETE - easy peasy.

            //Console.WriteLine("Standard Word Weight based on POS:");
            //foreach (Sentence s in document.Sentences)
            //{
            //    //Console.WriteLine(s);

            //    foreach (Word w in s.Words)
            //    {
            //        Console.WriteLine(w);

            //        new Switch(w)
            //            .Case<Noun>(n =>
            //            {
            //                w.Weight = primary;

            //            })
            //            .Case<Verb>(v =>
            //            {
            //                w.Weight = secondary;
            //            })
            //            .Case<Adjective>(adj =>
            //            {
            //                w.Weight = tertiary;
            //            })
            //            .Case<Adverb>(adv =>
            //            {
            //                w.Weight = quaternary;
            //            })
            //            .Case<Pronoun>(pn =>
            //            {
            //                w.Weight = quinary;
            //            })
            //            .Default(def =>
            //            {
            //                w.Weight = senary;
            //            });

            //        Console.WriteLine(w.Weight);

            //    }


            //}





            //PHASE 2 - Word Weight based on part of speech and neighbors' (+2) part of speech
            // WORKS, BUT
            // NEED FORMULAS FOR MODIFIER VARIABLES - WHAT SHOULD THESE BE?
            decimal modOne, modTwo, modThree, modFour;
            modOne = modTwo = 0;
            foreach (Sentence s in document.Sentences) {
                //Console.WriteLine(s);

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
                           new Switch(next)
                                  .Case<Noun>(nn => {
                                      modOne = 0; //verb actor

                                      //second (Verb -> Noun)
                                      modTwo = VerbNoun(nextNext);
                                  })
                                  .Case<PastParticipleVerb>(vn => {
                                      modOne = 0; //verb-verb descriptor

                                      //second (Verb -> PastParticipleVerb)
                                      modTwo = VerbPastParticipleVerb(nextNext);
                                  })
                                  .Case<Adjective>(advn => {
                                      modOne = 0;  //verb state

                                      //second (Verb -> Adjective)
                                      modTwo = VerbAdjective(nextNext);
                                  })
                                  .Case<Adverb>(advn => {
                                      modOne = 0;  //perfect adverb

                                      //second (Verb -> Adverb)
                                      modTwo = VerbAdverb(nextNext);
                                  })
                                  .Case<Pronoun>(pnn => {
                                      modOne = 0; //verb actor

                                      //second (Verb -> Pronoun)
                                      modTwo = VerbPronoun(nextNext);
                                  })
                                  .Case<ToLinker>(lnkn => {
                                      modOne = 0; //verb directional

                                      //second (Verb -> ToLinker)
                                      modTwo = VerbToLinker(nextNext);
                                  })
                                  .Case<Preposition>(pren => {
                                      modOne = 0; //verb-verb positional

                                      //second (Verb -> Preposition)
                                      modTwo = VerbPreposition(nextNext);
                                  })
                                  .Case<Determiner>(dn => {
                                      modOne = 0; //determiner

                                      //second (Verb -> Determiner)
                                      modTwo = VerbDeterminer(nextNext);
                                  })
                                  .Default(def => {
                                      modOne = 0;

                                      //second (Verb -> UNCAUGHT)
                                      modTwo = VerbUncaught(nextNext);
                                  });
                       })
                       .Case<Adjective>(adj => {
                           new Switch(next)
                                  .Case<Noun>(nn => {
                                      modOne = 0; //noun descriptor

                                      //second (Adjective -> Noun)
                                      modTwo = AdjectiveNoun(nextNext);
                                  })
                                  .Case<Adjective>(advn => {
                                      modOne = 0;  //double descriptor

                                      //second (Adjective -> Adjective)
                                      modTwo = AdjectiveAdjective(nextNext);
                                  })
                                  .Case<Adverb>(advn => {
                                      modOne = 0;  //coloured brilliantly

                                      //second (Adjective -> Adverb)
                                      modTwo = AdjectiveAdverb(nextNext);
                                  })
                                  .Case<Pronoun>(pnn => {
                                      modOne = 0; //noun descriptor

                                      //second (Adjective -> Noun)
                                      modTwo = AdjectivePronoun(nextNext);
                                  })
                                  .Case<ToLinker>(lnkn => {
                                      modOne = 0; //adjective directional

                                      //second (Adjective -> ToLinker)
                                      modTwo = AdjectiveToLinker(nextNext);
                                  })
                                  .Case<Preposition>(pren => {
                                      modOne = 0; //adjective positional

                                      //second (Adjective -> Prepositional)
                                      modTwo = AdjectivePreposition(nextNext);
                                  })
                                  .Case<Determiner>(dn => {
                                      modOne = 0; //determiner

                                      //second (Adjective -> Determiner)
                                      modTwo = AdjectiveDeterminer(nextNext);
                                  })
                                  .Default(def => {
                                      modOne = 0;

                                      //second (Adjective -> UNCAUGHT)
                                      modTwo = AdjectiveUncaught(nextNext);
                                  });
                       })
                       .Case<Adverb>(adv => {
                           new Switch(next)
                                  .Case<Noun>(nn => {
                                      modOne = 0; //adverbial noun

                                      //second (Adverb -> Noun)
                                      modTwo = AdverbNoun(nextNext);
                                  })
                                  .Case<Adjective>(advn => {
                                      modOne = 0;  //normal adv-adj

                                      //second (Adverb -> Adjective)
                                      modTwo = AdverbAdjective(nextNext);
                                  })
                                  .Case<Adverb>(advn => {
                                      modOne = 0;  //bi-adverbial

                                      //second (Adverb -> Adverb)
                                      modTwo = AdverbAdverb(nextNext);
                                  })
                                  .Case<Pronoun>(pnn => {
                                      modOne = 0; //adverbial pronoun

                                      //second (Adverb -> Noun)
                                      modTwo = AdverbPronoun(nextNext);
                                  })
                                  .Case<ToLinker>(lnkn => {
                                      modOne = 0; //adverb directional

                                      //second (Adverb -> ToLinker)
                                      modTwo = AdverbToLinker(nextNext);
                                  })
                                  .Case<Preposition>(pren => {
                                      modOne = 0; //adverb positional

                                      //second (Adverb -> Preposition)
                                      modTwo = AdverbPreposition(nextNext);
                                  })
                                  .Case<Determiner>(dn => {
                                      modOne = 0; //determiner

                                      //second (Adverb -> Determiner)
                                      modTwo = AdverbDeterminer(nextNext);
                                  })
                                  .Default(def => {
                                      modOne = 0;

                                      //second (Adverb -> UNCAUGHT)
                                      modTwo = AdverbUncaught(nextNext);
                                  });
                       })
                       .Case<Pronoun>(pn => {
                           new Switch(next)
                                  .Case<Noun>(nn => {
                                      modOne = 0; //compound noun/pronoun / possessed by pronoun

                                      //second (Pronoun -> Noun)
                                      modTwo = PronounNoun(nextNext);
                                  })
                                  .Case<Adjective>(advn => {
                                      modOne = 0;  //possessed/descriptor 

                                      //second (Pronoun -> Adjective)
                                      modTwo = PronounAdjective(nextNext);
                                  })
                                  .Case<Adverb>(advn => {
                                      modOne = 0;  //pronoun amplifier

                                      //second (Pronoun -> Adverb)
                                      modTwo = PronounAdverb(nextNext);
                                  })
                                  .Case<Pronoun>(pnn => {
                                      modOne = 0; //compound pronoun

                                      //second (Pronoun -> Noun)
                                      modTwo = PronounPronoun(nextNext);
                                  })
                                  .Case<ToLinker>(lnkn => {
                                      modOne = 0; //pronoun directional

                                      //second (Pronoun -> ToLinker)
                                      modTwo = PronounToLinker(nextNext);
                                  })
                                  .Case<Preposition>(pren => {
                                      modOne = 0; //pronoun positional

                                      //second (Pronoun -> Preposition)
                                      modTwo = PronounPreposition(nextNext);
                                  })
                                  .Case<Determiner>(dn => {
                                      modOne = 0; //determiner

                                      //second (Pronoun -> Determiner)
                                      modTwo = PronounDeterminer(nextNext);
                                  })
                                  .Default(def => {
                                      modOne = 0;

                                      //second (Pronoun -> UNCAUGHT)
                                      modTwo = PronounUncaught(nextNext);
                                  });
                       })

                       .Case<Preposition>(pn => {
                           new Switch(next)
                                  .Case<Noun>(nn => {
                                      modOne = 0; // 

                                      //second (Preposition -> Noun)
                                      modTwo = PrepositionNoun(nextNext);
                                  })
                                  .Case<Pronoun>(pnn => {
                                      modOne = 0; // 

                                      //second (Preposition -> Noun)
                                      modTwo = PrepositionPronoun(nextNext);
                                  })
                                  .Case<Determiner>(dn => {
                                      modOne = 0; //determiner

                                      //second (Preposition -> Determiner)
                                      modTwo = PrepositionDeterminer(nextNext);
                                  })
                                  .Default(def => {
                                      modOne = 0;

                                      //second (Preposition -> UNCAUGHT)
                                      modTwo = PrepositionUncaught(nextNext);
                                  });
                       })
                       .Default(def => {
                           modOne = 0;

                           //second (UNCAUGHT -> UNCAUGHT)
                           modTwo = UncaughtUncaught(nextNext);
                       });


                    w.Weight += (w.Weight * (modOne * modTwo)) / 3;

                }


            }





            //PHASE 3 - Standard phrase Weight based on phrase part of speech (standardization)
            //COMPLETE - easy peasy.

            //Console.WriteLine("Standard Phrase Weight based on POS:");
            //foreach (Sentence s in document.Sentences)
            //{
            //    //Console.WriteLine(s);

            //    foreach (Phrase p in s.Phrases)
            //    {
            //        Console.WriteLine(p);

            //        new Switch(p)
            //            .Case<NounPhrase>(n =>
            //            {
            //                p.Weight = primary;

            //            })
            //            .Case<VerbPhrase>(v =>
            //            {
            //                p.Weight = secondary;
            //            })
            //            .Case<AdjectivePhrase>(adj =>
            //            {
            //                p.Weight = tertiary;
            //            })
            //            .Case<AdverbPhrase>(adv =>
            //            {
            //                p.Weight = quaternary;
            //            })
            //            .Case<SimpleDeclarativePhrase>(pn =>
            //            {
            //                p.Weight = quinary;
            //            })
            //            .Default(def =>
            //            {
            //                p.Weight = senary;
            //            });

            //        Console.WriteLine(p.Weight);

            //    }


            //}



            //PHASE 4 - Phrase Weight based on part of speech and neibhors' (full sentence) part of speech



            //PHASE 5 - FREQUENCIES
            // .1 - Frequency of Word in document
            //Console.WriteLine(" ");
            //Console.WriteLine("Word Frequencies in Document:");

            //foreach (Word w in document.Words)
            //{



            //    foreach (Word w1 in document.Words)
            //    {

            //        if (w.Text == w1.Text)  //why doesn't 'w1.Equals(current)' work?
            //        {
            //            w1.FrequencyCurrent += 1;
            //        }
            //    }



            //}

            //foreach (Word w in document.Words)
            //{
            //    Console.WriteLine(w);
            //    Console.WriteLine(w.FrequencyCurrent); //integrate with existing
            //    decimal percentID = (w.FrequencyCurrent / (decimal)document.Words.Count());
            //    Console.WriteLine("Frequency % in document:" + percentID);
            //} //NEED FORMULA TO MODIFY WEIGHT



            // .2 - Frequency of Word in document compared to other documents in set - EXCLUDED FOR 1-DOCUMENT DEMO

            //PHASE 6 - SYNONYMS
            // .1 - Frequency of Word in document
            // COMPLETE - minus Aluan's verb lookup!

            //Console.WriteLine(" ");
            //Console.WriteLine("Word Synonyms in Document:");
            //IEnumerable<Noun> nouns = document.Words.GetNouns();
            //IEnumerable<Verb> verbs = document.Words.GetVerbs();
            //IEnumerable<Adjective> adjectives = document.Words.GetAdjectives();
            //IEnumerable<Adverb> adverbs = document.Words.GetAdverbs();

            //HashSet<string> synonyms;


            //foreach (var n in nouns)
            //{
            //    var nounTest = new NounThesaurus(@"..\..\..\..\WordNetThesaurusData\data.noun");

            //    nounTest.Load();
            //    synonyms = nounTest.SearchFor(n.Text);




            //    foreach (var n1 in nouns)
            //    {
            //        foreach (var s in synonyms)
            //        {
            //            if (n1.Text == s)
            //            {
            //                n1.Synonyms += 1;
            //            }
            //        }
            //        Console.WriteLine(n1.Text);
            //        Console.WriteLine(n1.Synonyms);
            //    }
            //}


            //foreach (var a in adjectives)
            //{
            //    var adjTest = new AdjectiveThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adj");

            //    adjTest.Load();
            //    synonyms = adjTest.SearchFor(a.Text);




            //    foreach (var a1 in adjectives)
            //    {
            //        foreach (var s in synonyms)
            //        {
            //            if (a1.Text == s)
            //            {
            //                a1.Synonyms += 1;
            //            }
            //        }
            //        Console.WriteLine(a1.Text);
            //        Console.WriteLine(a1.Synonyms);
            //    }
            //}


            //foreach (var av in adverbs)
            //{
            //    var advTest = new AdverbThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adv");

            //    advTest.Load();
            //    synonyms = advTest.SearchFor(av.Text);




            //    foreach (var av1 in adverbs)
            //    {
            //        foreach (var s in synonyms)
            //        {
            //            if (av1.Text == s)
            //            {
            //                av1.Synonyms += 1;
            //            }
            //        }
            //        Console.WriteLine(av1.Text);
            //        Console.WriteLine(av1.Synonyms);
            //    }
            //}


            //ALUAN: HELP NEEDED for your verb lookup
            //foreach (var v in verbs)
            //{
            //    var verbTest = new VerbThesaurus(@"..\..\..\..\WordNetThesaurusData\data.verb");

            //    verbTest.Load();
            //    IEnumerable<string> synonyms2 = Thesaurus.Lookup(v);  //Can't search through your verbs?




            //    foreach (var v1 in verbs)
            //    {
            //        foreach (var s in synonyms2)
            //        {
            //            if (v1.Text == s)
            //            {
            //                v1.Synonyms += 1;
            //            }
            //        }
            //        Console.WriteLine(v1.Text);
            //        Console.WriteLine(v1.Synonyms);
            //    }
            //}



            //NEED REAL FORMULA FOR WEIGHT MODIFICATION
            //        w.Weight += w.Synonyms;




            //        Console.WriteLine(w);


            //    }
            //}





            // .2 - Frequency of Word in document compared to other documents in set - EXCLUDED FOR 1-DOCUMENT DEMO
























            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }

            //var tagger = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\CynosureEPR\Desktop\test.txt");
            //var tagged = tagger.ProcessFile();
            //var paragraphs = new TaggedFileParser(tagged).LoadParagraphs();
            //var document = new Document(paragraphs);
            //StreamWriter file = new StreamWriter(@"C:\Users\CynosureEPR\Desktop\output.txt");

            //foreach (var i in document.Phrases) {
            //Console.WriteLine(i);
            //}
            //Console.WriteLine("Press escape to exit");
            //for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
            // Console.WriteLine("Press escape to exit");
            //}

        }


        private static void Noun(Word next, Word nextNext, out decimal outModOne, out decimal outModTwo) {
            decimal modOne = 0; //Renamed parameters and bound created temporary variables to pass into the switch blocks 
            decimal modTwo = 0;
            new Switch(next)
                   .Case<Noun>(nn => {
                       modOne = 0; //compound noun

                       //second (Noun -> Noun)
                       modTwo = NounNoun(nextNext);
                   })
                   .Case<Adjective>(nadj => {
                       modOne = 0; //possessive

                       //second (Noun -> Adjective)
                       modTwo = NounAdjective(nextNext);
                   })
                   .Case<Verb>(nv => {
                       modOne = 0; //noun action or descriptor
                       if (next is PastParticipleVerb) {
                           modOne = 0; //noun action-ed
                       }

                       //second (Noun -> Verb)
                       modTwo = NounVerb(nextNext);
                   })
                   .Case<Adverb>(advn => {
                       modOne = 0;  //noun amplifier

                       //second (Noun -> Adverb)
                       modTwo = NounAdverb(nextNext);
                   })
                   .Case<Pronoun>(pnn => {
                       modOne = 0; //compound noun

                       //second (Noun -> Pronoun)
                       modTwo = NounPronoun(nextNext);


                   })
                   .Case<ToLinker>(lnkn => {
                       modOne = 0; //noun to link

                       //second (Noun -> ToLinker)
                       modTwo = NounToLinker(nextNext);
                   })
                   .Case<Preposition>(pren => {
                       modOne = 0; //noun positional

                       //second (Noun -> Preposition)
                       modTwo = NounPreposition(nextNext);
                   })
                   .Case<Determiner>(dn => {
                       modOne = 0; //determiner

                       //second (Noun -> Determiner)
                       modTwo = NounDeterminer(nextNext);
                   })
                   .Default(def => {
                       modOne = 0;

                       //second (Noun -> UNCAUGHT)
                       modTwo = NounUncaught(nextNext);
                   });
            outModOne = modOne;//Set parameters = temporaries
            outModTwo = modTwo;//There is a better way to handle this, but this works without any changes
        }

        private static decimal UncaughtUncaught(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //uncaught-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //uncaught-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //uncaught-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //uncaught-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //uncaught-uncaught-noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //uncaught-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //uncaught-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //uncaught-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0; //uncaught-uncaught-uncaught (epic fail)
                });
            return modTwo;
        }

        private static decimal PrepositionUncaught(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //preposition-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //preposition-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //preposition-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //preposition-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //preposition-uncaught-noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //preposition-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //preposition-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //preposition-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PrepositionDeterminer(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //preposition-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //preposition-determiner-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //preposition-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //preposition-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //preposition-determiner-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //preposition-determiner directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //preposition-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //preposition-determiner determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PrepositionPronoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //preposition-compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //preposition-noun-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //preposition-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //preposition-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //preposition-compound noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //preposition-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //preposition-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //preposition-noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PrepositionNoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //preposition-compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //preposition-noun-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //preposition-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //preposition-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //preposition-compound noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //preposition-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //preposition-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //preposition-noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PronounUncaught(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //pronoun-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //pronoun-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //pronoun-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //pronoun-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //pronoun-uncaught-noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //pronoun-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //pronoun-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //pronoun-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PronounDeterminer(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //pronoun-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //pronoun-determiner-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //pronoun-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //pronoun-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //pronoun-determiner-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //pronoun-determiner directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //pronoun-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //pronoun-determiner determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PronounPreposition(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //pronoun-preposition-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //pronoun-preposition-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //pronoun-preposition-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //pronoun-preposition-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //pronoun-preposition-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //pronoun-preposition directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //pronoun-preposition positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //pronoun-preposition determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PronounToLinker(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //pronoun-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //pronoun-tolinker-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //pronoun-tolinker-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //pronoun-tolinker-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //pronoun-tolinker-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //pronoun-tolinker directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //pronoun-tolinker positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //pronoun-tolinker determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PronounPronoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //triple compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //compound noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //compound noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //compound noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //triple compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //compound noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //compound noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //compound noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PronounAdverb(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //pronoun-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //pronoun-adverb-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //pronoun-adverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //pronoun-adverb-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //pronoun-adverb-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //pronoun-adverb directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //pronoun-adverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //pronoun-adverb determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PronounAdjective(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //pronoun-adjective-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //pronoun-adjective-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //pronoun-adjective-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //pronoun-adjective-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //pronoun-adjective-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //pronoun-adjective directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //pronoun-adjective positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //pronoun-adjective determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal PronounNoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //triple compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //compound noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //compound noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //compound noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //triple compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //compound noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //compound noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //compound noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal AdverbUncaught(Word nextNext) {
            decimal modTwo = 0;
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
                    modTwo = 0; //adverb-uncaught-noun 
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

        private static decimal AdverbDeterminer(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-determiner-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb-determiner-noun
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

        private static decimal AdverbPreposition(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-preposition-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-preposition-verb descriptor
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

        private static decimal AdverbToLinker(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-tolinker-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-tolinker-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-tolinker-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb-tolinker-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-tolinker directional
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

        private static decimal AdverbPronoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-noun-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb compound noun
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

        private static decimal AdverbAdverb(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-adverb-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-adverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //tri adverbial
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb-adverb-noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //adverb-adverb directional
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

        private static decimal AdverbAdjective(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb-adjective-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-adjective-verb descriptor
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //adverb-adjective-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //adverb-adjective-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //adverb-adjective-noun
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

        private static decimal AdverbNoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adverb -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adverb-noun-verb descriptor (possible?)
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

        private static decimal AdjectiveUncaught(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective-uncaught-noun
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

        private static decimal AdjectiveDeterminer(Word nextNext) {
            decimal modTwo = 0;
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

        private static decimal AdjectivePreposition(Word nextNext) {
            decimal modTwo = 0;
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

        private static decimal AdjectiveToLinker(Word nextNext) {
            decimal modTwo = 0;
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

        private static decimal AdjectivePronoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-noun-verb descriptor (possible?)
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

        private static decimal AdjectiveAdverb(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-adverb-verb descriptor
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

        private static decimal AdjectiveAdjective(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //compound adjective -> noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //compound adjective -> verb descriptor
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

        private static decimal AdjectiveNoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //adjective -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //adjective-noun-verb descriptor (possible?)
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

        private static decimal VerbUncaught(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb-uncaught-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-uncaught-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-uncaught-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-uncaught-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> uncaught -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-uncaught directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-uncaught positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-uncaught determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbDeterminer(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb-determiner-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-determiner-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-determiner-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-determiner-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> determiner -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-determiner directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-determiner positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-determiner determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbPreposition(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb-preposition-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-preposition-pastverb 
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-preposition-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-preposition-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> preposition -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-preposition directional 
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-preposition positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-preposition determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbToLinker(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb-tolinker-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-tolinker-pastverb (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-tolinker-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-tolinker-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> tolinker -> noun 
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-tolinker directional (possible?)
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-tolinker positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-tolinker determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbPronoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb-pronoun-noun (compound)
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-pronoun-pastverb
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-pronoun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-pronoun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> pronoun -> noun  (compound)
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-pronoun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-pronoun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-pronoun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbAdverb(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb-adverb-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-adverb-pastverb
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-adverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-adverb-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> adverb -> noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-aadverb directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-adverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-adverb determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbAdjective(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb-adjective-noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-adjective-pastverb
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-adjective-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-adjective-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> adjective -> noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-adjective directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-adjective positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-adjective determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbPastParticipleVerb(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb-pastverb -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-pastverb-pastverb
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-pastverb-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-pastverb-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> pastverb -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-pastverb directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-pastverb positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-pastverb determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal VerbNoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nn => {
                    modTwo = 0; //verb -> compound noun
                })
                .Case<PastParticipleVerb>(vn => {
                    modTwo = 0; //verb-noun-verb descriptor (possible?)
                })
                .Case<Adjective>(advn => {
                    modTwo = 0;  //verb-noun-adjective
                })
                .Case<Adverb>(advn => {
                    modTwo = 0;  //verb-noun-adverb
                })
                .Case<Pronoun>(pnn => {
                    modTwo = 0; //verb -> compound (possessive) noun
                })
                .Case<ToLinker>(lnkn => {
                    modTwo = 0; //verb-noun directional
                })
                .Case<Preposition>(pren => {
                    modTwo = 0; //verb-noun positional
                })
                .Case<Determiner>(dn => {
                    modTwo = 0; //verb-noun determiner
                })
                .Default(def => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounUncaught(Word nextNext) {
            decimal modTwo = 0;
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

        private static decimal NounDeterminer(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(ndn => {
                    modTwo = 0; //noun-determiner-noun
                })
                .Case<PastParticipleVerb>(ndpv => {
                    modTwo = 0; //noun-determiner-verb descriptor (possible?)
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

        private static decimal NounPreposition(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(npn => {
                    modTwo = 0; //noun-preposition-noun
                })
                .Case<PastParticipleVerb>(nppv => {
                    modTwo = 0; //noun-preposition-verb descriptor (possible?)
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

        private static decimal NounToLinker(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nlnkn => {
                    modTwo = 0; //noun-tolinker-noun
                })
                .Case<PastParticipleVerb>(nlnkpv => {
                    modTwo = 0; //noun-tolinker-verb descriptor (possible?)
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

        private static decimal NounPronoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(npnn => {
                    modTwo = 0; //triple compound noun
                })
                .Case<PastParticipleVerb>(npnpv => {
                    modTwo = 0; //compound noun-verb descriptor (possible?)
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

        private static decimal NounAdverb(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nadvn => {
                    modTwo = 0; //noun-adverb-noun
                })
                .Case<PastParticipleVerb>(nadvpv => {
                    modTwo = 0; //noun-adverb-verb descriptor (possible?)
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

        private static decimal NounVerb(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nvn => {
                    modTwo = 0; //noun-verb-noun
                })
                .Case<PastParticipleVerb>(nvpv => {
                    modTwo = 0; //noun-verb-verb descriptor (possible?)
                })
                .Case<Adjective>(nvadj => {
                    modTwo = 0;  //noun-verb-adjective
                })
                .Case<Adverb>(nvadv => {
                    modTwo = 0;  //noun-verb-adverb
                })
                .Case<Pronoun>(nvpn => {
                    modTwo = 0; //noun-verb-noun
                })
                .Case<ToLinker>(nvlnk => {
                    modTwo = 0; //noun-verb directional
                })
                .Case<Preposition>(nvpre => {
                    modTwo = 0; //noun-verb positional
                })
                .Case<Determiner>(nvd => {
                    modTwo = 0; //noun-verb determiner
                })
                .Default(nvu => {
                    modTwo = 0;
                });
            return modTwo;
        }

        private static decimal NounAdjective(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nadjn => {
                    modTwo = 0; //noun-adjective-noun
                })
                .Case<PastParticipleVerb>(nadjpv => {
                    modTwo = 0; //noun-adjective-verb descriptor (possible?)
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

        private static decimal NounNoun(Word nextNext) {
            decimal modTwo = 0;
            new Switch(nextNext)
                .Case<Noun>(nnn => {
                    modTwo = 0; //triple compound noun
                })
                .Case<PastParticipleVerb>(nnpv => {
                    modTwo = 0; //compound noun-verb descriptor (possible?)
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
    }
}
