using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Utilities;
using SharpNLPTaggingModule;
using System.IO;
using LASI.Algorithm.Binding; 
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Weighting;

namespace Scott_Experimentation
{
    class Program
    {
        static void Main(string[] args) {

            var tagger1 = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\LasiTests\TestSentences.txt");
            var tagged1 = tagger1.ProcessFile();
            var paragraphs1 = new TaggedFileParser(tagged1).LoadParagraphs();
            var document1 = new Document(paragraphs1);

            var tagger2 = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\LasiTests\TestSentences2.txt");
            var tagged2 = tagger2.ProcessFile();
            var paragraphs2 = new TaggedFileParser(tagged2).LoadParagraphs();
            var document2 = new Document(paragraphs2);

            var tagger3 = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\LasiTests\TestSentences3.txt");
            var tagged3 = tagger3.ProcessFile();
            var paragraphs3 = new TaggedFileParser(tagged3).LoadParagraphs();
            var document3 = new Document(paragraphs3);

            var tagger4 = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\LasiTests\TestSentences4.txt");
            var tagged4 = tagger4.ProcessFile();
            var paragraphs4 = new TaggedFileParser(tagged4).LoadParagraphs();
            var document4 = new Document(paragraphs4);

            //var converter = new DocxToTextConverter(new LASI.FileSystem.FileTypes.DocXFile(@"wd:\Users\Scott\Desktop\HesterTestDocs\411writtensummary2.docx"));
            //var converter = new DocxToTextConverter(new LASI.FileSystem.FileTypes.DocXFile(@"wd:\Users\Scott\Desktop\HesterTestDocs\CapabilitiesBasedPlanningProcessOverview.docx"));
            var converter = new DocxToTextConverter(new LASI.FileSystem.FileTypes.DocXFile(@"C:\Users\Scott\Desktop\HesterTestDocs\BTSDraftNeedsandStrategy.docx"));
            var tagger5 = new SharpNLPTagger(TaggingOption.TagAndAggregate, converter.ConvertFile().FullPath);
            var tagged5 = tagger5.ProcessFile();
            var paragraphs5 = new TaggedFileParser(tagged5).LoadParagraphs();
            var document5 = new Document(paragraphs5);

            //string TestString = "The Boy rides his big bold bike up a large steep hill. The tiny brown dog watched. I saw her book on your table. What's mine is yours, my friend. Johnny, Steve and I went to the old corner store to purchase some potently strong perfume.";
            string TestString = "The Boy rides his big bold bike up a large steep hill.  He fell over when he reached the top.  The Boy then got up and rode down the other side.";
            var DocTest = LASI.Utilities.TaggerUtil.UntaggedToDoc(TestString);




            Binder.Bind(document5);
            Weighter.Weight(document5);

            var sm = from t in document5.Phrases //.GetNounPhrases() //.Words
                      where document5.Phrases.Count() > 0 //.GetNounPhrases().Count() > 0 //.Words.Count() > 0
                      orderby t.Weight descending 
                     select t;

            //foreach (var i in sm.GetNounPhrases())
            for(var x = 0; x < 30; x++)
            {
                //Output.WriteLine("{0}, {1}", i, i.Weight);
                Output.WriteLine("{0}, {1}", sm.ElementAt(x), sm.ElementAt(x).Weight);
            }
            

            /*
            var NP = sm.GetNounPhrases();
            List<Phrase> UniqueNounPhrases = new List<Phrase>();
            //inserts first noun parent into list
            UniqueNounPhrases.Add(NP.ElementAt(0));
            //Output.WriteLine("{0}, {1}", NP.Count(), UniqueNounPhrases.Count());
            bool match = false;
            for(int x = 0; x < NP.Count(); x++)
            {
                for (int y = 0; y < UniqueNounPhrases.Count(); y++)
                {
                    //compare whole text
                    if (NP.ElementAt(x).Text.ToUpper() == UniqueNounPhrases.ElementAt(y).Text.ToUpper())
                    {
                        match = true;
                    }

                    //compare last noun in noun parent
                    if((NP.ElementAt(x).Words.GetNouns().LastOrDefault() != null) &&
                        (UniqueNounPhrases.ElementAt(y).Words.GetNouns().LastOrDefault() != null) &&
                        (NP.ElementAt(x).Words.GetNouns().LastOrDefault().Text.ToUpper() == UniqueNounPhrases.ElementAt(y).Words.GetNouns().LastOrDefault().Text.ToUpper())
                    )
                    {
                        match = true;
                    }

                    //Removes certain words and chars
                    if (NP.ElementAt(x).Text == "\"" || 
                        NP.ElementAt(x).Text.ToLower() == "the" ||
                        NP.ElementAt(x).Text.ToLower() == "a" ||
                        NP.ElementAt(x).Text.ToLower() == "an" ||
                        NP.ElementAt(x).Text.ToLower() == "or" ||
                        NP.ElementAt(x).Text.ToLower() == "were" ||
                        NP.ElementAt(x).Text.ToLower() == "is" ||
                        NP.ElementAt(x).Text.ToLower() == "what" ||
                        NP.ElementAt(x).Text.ToLower() == "that" ||
                        NP.ElementAt(x).Text.ToLower() == "which" ||
                        NP.ElementAt(x).Text.ToLower() == "these" ||
                        NP.ElementAt(x).Text.ToLower() == "there" ||
                        NP.ElementAt(x).Text.ToLower() == "those"
                     )
                    {
                        match = true;
                    }
                    
                }
                if (match == false)
                {
                    //stick in list
                    if(NP.ElementAt(x) is NounPhrase)
                        UniqueNounPhrases.Add(NP.ElementAt(x)); //sm.GetNounPhrases().ElementAt(x));
                }
                match = false;
            }

            Output.WriteLine("\n\nAnother Output: ");
            foreach (var nounText in UniqueNounPhrases)
            {
                Output.WriteLine("{0} => {1}", nounText, nounText.Weight);
            }
           */

            Input.WaitForAnyKey();
        }
    }
}
