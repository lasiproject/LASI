using LASI.Algorithm;
using LASI.Algorithm.Binding;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.Utilities;
using LASI.Utilities.TypedSwitch;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Aluan_Experimentation
{
    public class Program
    {
        static string sourceText = @"To work together is a bad idea.";
        static string testPath = @"C:\Users\Aluan\Desktop\411writtensummary2.txt";

        static void Main(string[] args)
        {




            var pronoun = new PersonalPronoun("him");

















            var synonymsForCat = Thesaurus.LookupNoun("feline", WordNetNounCategory.Animal);

            foreach (var syn in synonymsForCat)
                Output.WriteLine(syn);



            TaggerUtil.TaggerOption = TaggingOption.TagAndAggregate;
            var taggedText = TaggerUtil.TagString(sourceText);
            Output.WriteLine(taggedText);

            var doc = TaggerUtil.TaggedToDoc(taggedText);
            Phrase.VerboseOutput = false;
            Word.VerboseOutput = false;
            foreach (var s in doc.Sentences) {
                try {
                    new SubjectBinder().Bind(s);
                    new ObjectBinder().Bind(s);
                }
                catch (VerblessPhrasalSequenceException) {
                }
            }
            foreach (var phrase in doc.Phrases) {
                Output.WriteLine(phrase);
            }

            Input.WaitForKey();
        }

        private static void GroupingByBehaviorAndKindExample(Document doc)
        {
            var walkers =
                from action in doc.Words.GetVerbs()
                where Thesaurus.Lookup(action).Contains("walk")
                from actionPerformer in action.BoundSubjects
                select actionPerformer;

            var kindsOfCats = from entity in doc.Words.GetNouns()
                              let synonyms = Thesaurus.Lookup(entity)
                              where synonyms.Contains("cat")
                              group entity by entity.DescribedBy;


        }

        private static void TestNounConjugator()
        {
            NounConjugator conjugator = new NounConjugator(ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "noun.exc");
            Output.WriteLine(conjugator);
            var conjugations = conjugator.GetConjugations("cat");
            var bases = conjugator.TryExtractRoot("women");
            conjugations.ForEach(c => Output.WriteLine(c));
            bases.ForEach(c => Output.WriteLine(c));
        }





        private static void TestWordAndPhraseBindings()
        {
            var doc = TaggerUtil.LoadTextFile(new LASI.FileSystem.FileTypes.TextFile(testPath));
            PerformIntraPhraseBinding(doc);
            PerformSVOBinding(doc);
            new PronounBinder().Bind(doc);
            foreach (var p in doc.Phrases.GetPronounPhrases())
                Output.WriteLine(p);

            PerformAttributeNounPhraseBinding(doc);






            PrintDocument(doc);
        }

        private static void PrintDocument(Document doc)
        {
            foreach (var r in doc.Phrases) {
                Output.WriteLine(r);
                foreach (var w in r.Words)
                    Output.WriteLine(w);
            }
        }
        private static void PerformAttributeNounPhraseBinding(Document doc)
        {
            foreach (var s in doc.Sentences) {
                var attributiveBinder = new AttributiveNounPhraseBinder(s);
            }
        }
        private static void PerformSVOBinding(Document doc)
        {
            foreach (var s in doc.Sentences) {
                var subjectBinder = new SubjectBinder();
                var objectBinder = new ObjectBinder();
                try {
                    subjectBinder.Bind(s);
                }
                catch (NullReferenceException) {
                }
                try {
                    objectBinder.Bind(s);
                }
                catch (VerblessPhrasalSequenceException) {
                }
            }



        }

        private static void PerformIntraPhraseBinding(Document doc)
        {
            foreach (var r in doc.Phrases) {
                var wordBinder = new InterPhraseWordBinding();
                new Switch(r)
                .Case<NounPhrase>(np =>
                {
                    wordBinder.IntraNounPhrase(np);
                })
                .Case<VerbPhrase>(vp =>
                {
                    wordBinder.IntraVerbPhrase(vp);
                })
                .Default(a =>
                {
                });
            }
        }




        static void WeightAll(Document doc)
        {




            //ASSIGN BASE WEIGHTS TO WORDS
            foreach (var N in doc.Words.GetNouns()) {
                N.Weight = 100;
            }
            foreach (var V in doc.Words.GetToLinkers()) {
                V.Weight = 90;
            }
            foreach (var A in doc.Words.GetAdjectives()) {
                A.Weight = 85;
            }
            foreach (var A in doc.Words.GetAdverbs()) {
                A.Weight = 77;
            }



            //ASSUMING RELEVANT GROUP OF NOUNS HAS TEXT "DOG"


            var wordsByTypeAndText = from n in doc.Words
                                     group n by new
                                     {
                                         n.Text,
                                         n.Type
                                     };






            foreach (var wGroup in wordsByTypeAndText) {
                foreach (var w in wGroup) {
                    w.Weight = wGroup.Count();
                }
            }




            foreach (var NP in doc.Phrases.GetNounPhrases()) {
                NP.Weight = 1;
            }

            IEnumerable<IGrouping<string, NounPhrase>> nounPhraseGroups
                = from NP in doc.Phrases.GetNounPhrases()
                  group NP by NP.Text;

            foreach (IGrouping<string, NounPhrase> group in nounPhraseGroups) {
                foreach (NounPhrase NP in group) {
                    NP.Weight += group.Count();
                }
            }




            foreach (var sentence in doc.Sentences) {
                new SubjectBinder().Bind(sentence);
                new ObjectBinder().Bind(sentence);

            }
            var nounBinder = new LASI.Algorithm.Binding.InterPhraseWordBinding();
            foreach (var phrase in doc.Phrases.GetNounPhrases())
                nounBinder.IntraNounPhrase(phrase);
            foreach (var sentence in doc.Sentences) {
                foreach (var phrase in sentence.Phrases)
                    Output.WriteLine(phrase);
                Output.WriteLine("\n");
            }

        }



    }
}