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

            //var nounTest = new NounThesaurus(@"..\..\..\..\WordNetThesaurusData\data.noun");
            //nounTest.Load();
            //string key = "object";
            //nounTest.SearchFor(key);

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
            //PHASE 1 - Standard word weight based on part of speech (standardization)
            //PHASE 2 - Word weight based on part of speech and neighbors' (+2) part of speech
            //PHASE 3 - Standard phrase weight based on phrase part of speech (standardization)
            //PHASE 4 - Phrase weight based on part of speech and neibhors' (full sentence) part of speech
            //PHASE 5 - FREQUENCIES
            // .1 - Frequency of Word/Phrase in document
            // .2 - Frequency of Word/Phrase in document compared to other documents in set
            //PHASE 6 - SYNONYMS
            // .1 - Frequency of Word (/Phrase?) in document
            // .2 - Frequency of Word (/Phrase?) in document compared to other documents in set



            int primary, secondary, tertiary, quaternary, quinary, senary;
            int based = 20;
            primary = (secondary = (tertiary = (quaternary = (quinary = (senary = 0) + based) + based) + based) + based) + based;

           
            //PHASE 1 - Standard word weight based on part of speech (standardization)
            //COMPLETE - easy peasy.

            //Console.WriteLine("Standard Word weight based on POS:");
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





            //PHASE 2 - Word weight based on part of speech and neighbors' (+2) part of speech
            // WORKS, BUT
            // NEED FORMULAS FOR MODIFIER VARIABLES - WHAT SHOULD THESE BE?

            foreach (Sentence s in document.Sentences)
            {
                //Console.WriteLine(s);

                foreach (Word w in s.Words)
                {

                    Word next = w ?? w.NextWord;
                    Word nextNext = next ?? next.NextWord;

                    Word prev = w ?? w.PreviousWord;
                    Word prevPrev = prev ?? prev.PreviousWord;


                    new Switch(w)
                       .Case<Noun>(n =>
                       {
                            new Switch(next)
                                   .Case<Noun>(nn =>
                                   {
                                       w.Weight = primary;

                                   })
                                   .Case<Verb>(vn =>
                                   {
                                       w.Weight = secondary;
                                   })
                                   .Case<Adjective>(adjn =>
                                   {
                                       w.Weight = tertiary;
                                   })
                                   .Case<Adverb>(advn =>
                                   {
                                       w.Weight = quaternary;
                                   })
                                   .Case<Pronoun>(pnn =>
                                   {
                                       w.Weight = quinary;
                                   })
                                   .Default(def =>
                                   {
                                       w.Weight = senary;
                                   });

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
                    //nouns
                    

                   

                }


            }




            //PHASE 3 - Standard phrase weight based on phrase part of speech (standardization)




            //PHASE 4 - Phrase weight based on part of speech and neibhors' (full sentence) part of speech



            //PHASE 5 - FREQUENCIES
            // .1 - Frequency of Word in document
            Console.WriteLine(" ");
            Console.WriteLine("Word Frequencies in Document:");

            foreach (Word w in document.Words)
            {



                foreach (Word w1 in document.Words)
                {

                    if (w.Text == w1.Text)  //why doesn't 'w1.Equals(current)' work?
                    {
                        w1.FrequencyCurrent += 1;
                    }
                }



            }

            foreach (Word w in document.Words)
            {
                Console.WriteLine(w);
                Console.WriteLine(w.FrequencyCurrent); //integrate with existing
                float percentID = (w.FrequencyCurrent / (float)document.Words.Count());
                Console.WriteLine("Frequency % in document:" + percentID);
            } //NEED FORMULA TO MODIFY WEIGHT



            // .2 - Frequency of Word in document compared to other documents in set
            // NEED FORUMULA FOR THIS

            //PHASE 6 - SYNONYMS
            // .1 - Frequency of Word in document

            //Console.WriteLine(" ");
            //Console.WriteLine("Word Synonyms in Document:");
            //IEnumerable<Noun> nouns = document.Words.GetNouns();

            //IEnumerable<string> synonyms;

            //foreach (var n in nouns)
            //{
            //    var nounTest = new NounThesaurus(@"..\..\..\..\WordNetThesaurusData\data.noun");

            //    nounTest.Load();
            //    synonyms = nounTest.SearchFor(n.Text);




            //    foreach (Word w in document.Words)
            //    {
            //        foreach (var s in synonyms)
            //        {
            //            if (w.Text == s)
            //            {
            //                w.Synonyms += 1;
            //            }
            //        }





                    //NEED REAL FORMULA FOR WEIGHT MODIFICATION
            //        w.Weight += w.Synonyms;




            //        Console.WriteLine(w);
            //        Console.WriteLine(w.Synonyms);
            //        Console.WriteLine(w.Weight);

            //    }
            //}



            //var adjTest = new AdjectiveThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adj");
            //adjTest.Load();
            //string key = "able";
            //adjTest.SearchFor(key);

            //var advTest = new AdverbThesaurus(@"..\..\..\..\WordNetThesaurusData\data.adv");
            //advTest.Load();
            //string key = "vastly";
            //advTest.SearchFor(key);

            // .2 - Frequency of Word in document compared to other documents in set
          
         

            




















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
    }
}
