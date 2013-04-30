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
using LASI.Algorithm.Analysis;
using LASI.Algorithm.DocumentConstructs;

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

            var converter = new DocxToTextConverter(new LASI.FileSystem.FileTypes.DocXFile(@"C:\Users\Scott\Desktop\HesterTestDocs\411writtensummary2.docx"));
            //var converter = new DocxToTextConverter(new LASI.FileSystem.FileTypes.DocXFile(@"C:\Users\Scott\Desktop\HesterTestDocs\CapabilitiesBasedPlanningProcessOverview.docx"));
            var tagger5 = new SharpNLPTagger(TaggingOption.TagAndAggregate, converter.ConvertFile().FullPath);
            var tagged5 = tagger5.ProcessFile();
            var paragraphs5 = new TaggedFileParser(tagged5).LoadParagraphs();
            var document5 = new Document(paragraphs5);

            //string TestString = "The Boy rides his big bold bike up a large steep hill. The tiny brown dog watched. I saw her book on your table. What's mine is yours, my friend. Johnny, Steve and I went to the old corner store to purchase some potently strong perfume.";
            string TestString = "The Boy rides his big bold bike up a large steep hill.  He fell over when he reached the top.  The Boy then got up and rode down the other side.";
            var DocTest = LASI.Utilities.TaggerUtil.UntaggedToDoc(TestString);

            Binder.Bind(document4);
            Weighter.Weight(document4);
           
            //var wrd = document5.Words.FirstOrDefault();

            var sm = from t in document4.Phrases //.Words
                      where document4.Phrases.Count() > 0 //.Words.Count() > 0
                      orderby t.Weight ascending 
                     select t;

            Output.WriteLine("Start Here: ");
            foreach (var w in sm.GetNounPhrases()) //.GetNouns().OfType<ProperNoun>()) //sm.GetNouns().OfType<ProperNoun>())
            {
                    Output.WriteLine("{0} => {1}", w, w.Weight);
            }
             


            Input.WaitForAnyKey();
        }
    }
}
