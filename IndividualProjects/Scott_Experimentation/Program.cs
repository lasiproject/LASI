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

namespace Scott_Experimentation
{
    class Program
    {
        static void Main(string[] args) {
            /*
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

            var tagger5 = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\LasiTests\TestSentences5.txt");
            var tagged5 = tagger5.ProcessFile();
            var paragraphs5 = new TaggedFileParser(tagged5).LoadParagraphs();
            var document5 = new Document(paragraphs5); 

            //string TestString = "The Boy rides his big bold bike up a large steep hill. The tiny brown dog watched. I saw her book on your table. What's mine is yours, my friend. Johnny, Steve and I went to the old corner store to purchase some potently strong perfume.";
            //var DocTest = LASI.Utilities.TaggerUtil.UntaggedToDoc(TestString);

            //Noun Phrase Binding
            InterPhraseWordBinding ip1 = new InterPhraseWordBinding();
            foreach (var phrs in document5.Phrases.GetNounPhrases()) {
                ip1.IntraNounPhrase(phrs);
            }

            //Verb Phrase Binding
            foreach (var vbphrs in document5.Phrases.GetVerbPhrases()) {
                ip1.IntraVerbPhrase(vbphrs);
            }
            */


            // Test Case Demonstrations
            string TestString = "Virginia is a state located on the east coast in the USA.  It is one of the original thirteen colonies.  It has both mountains and beaches.  It is known as the Land of the Presidents and the state motto is Virginia is for Lovers.";
            var DocTest = LASI.Utilities.TaggerUtil.UntaggedToDoc(TestString);

            var wrd1 = DocTest.Words.GetNouns().FirstOrDefault();
            var phrs1 = DocTest.Phrases.GetNounPhrases().LastOrDefault();

            Output.WriteLine("Word: {0}: ", wrd1);
            Output.WriteLine("Word Weight Before Associations: {0}", wrd1.Weight);

            wrd1.BindNounPhrase = phrs1;
            wrd1.Weight++;

            Output.WriteLine("Word Weight After Phrase Association: {0}", wrd1.Weight);

            var nounWrd = DocTest.Words.GetNouns().LastOrDefault();
            wrd1.SubTaxonomicNoun = nounWrd;
            wrd1.Weight++;

            Output.WriteLine("Word Weight After Word Association: {0}", wrd1.Weight);

            Output.WriteLine("\nPhrase: {0}", phrs1);
            Output.WriteLine("Phrase Weight Before Associations: {0}", phrs1.Weight);

            phrs1.BindNoun = nounWrd;
            phrs1.Weight++;

            Output.WriteLine("Phrase Weight After Word Association: {0}", phrs1.Weight);

            var nounPhrs = DocTest.Phrases.GetNounPhrases().FirstOrDefault();
            phrs1.BindNounPhrase = nounPhrs;
            phrs1.Weight++;

            Output.WriteLine("Phrase Weight After Phrase Association: {0}", phrs1.Weight);


           

            Input.WaitForAnyKey();
        }
    }
}
