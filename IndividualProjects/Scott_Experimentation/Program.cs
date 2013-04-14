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
            var tagger1 = new SharpNLPTagger(TaggingOption.TagAndAggregate, @"C:\Users\Scott\Desktop\LasiTests\TestSentences.txt");
            var tagged1 = tagger1.ProcessFile();
            var paragraphs1 = new TaggedFileParser(tagged1).LoadParagraphs();
            var document1 = new Document(paragraphs1);

            string sep = "\n***************************************************************\n";
            /* 
             try{
                 Word w1 = document.WordAt(5000);
                 Console.WriteLine("Word at 5: {0}", w1.Text);
             }catch (ArgumentOutOfRangeException ex){
                 Console.WriteLine("Exception: {0}", ex.Message);
             }

             try{
                 var strng1 = document.WordTextAt(500);
                 Console.WriteLine("Word Text at 5: {0}{1}", strng1, sep);
             }catch (ArgumentOutOfRangeException ex){
                 Console.WriteLine("Exception: {0}", ex.Message);
             }


             try{
                 Sentence sent1 = document.SentenceAt(18947348);
                 Console.WriteLine("Sentence at 1: {0}", sent1.Text);
             } catch (ArgumentOutOfRangeException ex){
                 Console.WriteLine(ex.Message);
             }
            
             try{
                 string strng2 = document.SentenceTextAt(1000);
                 Console.WriteLine("Sentence text at 1: {0}{1}", strng2, sep);
             }catch(ArgumentOutOfRangeException ex){
                 Console.WriteLine(ex.Message);
             }

             Console.WriteLine(sep);
            
            
             for (var x = 0; x <= document.Words.Count(); x++)
             {
                 try{
                     Console.WriteLine("{0}: {1} => {2}", x, document.WordTextAt(x), document.WordAt(x).GetType());
                 }catch (ArgumentOutOfRangeException ex){
                     Console.WriteLine("ERROR Loc: {0}, [{1}]", x, ex.Message);
                 }
             }
             

            Console.WriteLine(sep);

            string TestString = "This is a string of text to test.";
            var DocTest = LASI.Utilities.TaggerUtil.UntaggedToDoc(TestString);

            SubjectBinder sb1 = new SubjectBinder();

            foreach (Sentence s in document.Sentences) {
                sb1.Bind(s);
            }

            var ctrl = 0;
            foreach (var phrs in document.Phrases) {
                Console.WriteLine("{0}, {1}", ctrl, phrs.ToString());
                ctrl++;
            }

            */

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

            string TestString = "The Boy rides his big bold bike up a large steep hill. The tiny brown dog watched. I saw her book on your table. What's mine is yours, my friend. Johnny, Steve and I went to the old corner store to purchase some potently strong perfume.";
            var DocTest = LASI.Utilities.TaggerUtil.UntaggedToDoc(TestString);


            InterPhraseWordBinding ip1 = new InterPhraseWordBinding();
            foreach (var phrs in document2.Phrases.GetNounPhrases()) {
                ip1.IntraNounPhrase(phrs);
            }


            Output.WriteLine("Verb Phrases:\n");
            foreach (var vbphrs in document1.Phrases.GetVerbPhrases()) {
                ip1.IntraVerbPhrase(vbphrs);
            }


            Input.WaitForAnyKey();
        }
    }
}
