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
namespace Aluan_Experimentation
{
    public class Program
    {
        static void Main(string[] args) {


            TaggerUtil.TaggerOption = TaggingOption.TagAndAggregate;
            var str = LASI.FileSystem.TaggerUtil.TagString("The wind hit a pale green mitten and a fluffy dog to me.");
            Console.WriteLine(str);
            var doc = LASI.FileSystem.TaggerUtil.TaggedToDoc(str);

            PhraseWiseObjectBinder binder = new PhraseWiseObjectBinder(doc.Phrases.ToList()[1] as VerbPhrase, doc.Phrases.Skip(2));
            foreach (var phrase in doc.Phrases)
                print(phrase.ToString(true));
            StdIO.WaitForKey();


        }

        //private static void ParseAndCreate() {

        //    TaggerUtil.TaggerOption = TaggingOption.NameFind;
        //    var str = TaggerUtil.TagString(new[]{
        //        @"Add one plus one."
        //    });

        //    print(str);
        //    TaggerUtil.TaggerOption = TaggingOption.TagAndAggregate;
        //    str = TaggerUtil.TagString(str);

        //    print(str);

        //    var document = TaggerUtil.TaggedToDoc(str);
        //    foreach (var S in CountByTypeAndText(document).Result) {
        //        print(S);
        //    }

        //    var actions = document.GetActions();
        //    actions.WithSubject(e => e.Text == null);
        //    foreach (var A in actions) {
        //        print(A);
        //    }
        //    StdIO.WaitForKey(ConsoleKey.Escape);
        //}



        //private static async Task<Document> MakeDocumentFromTaggedFile(string filePath) {

        //    return await Task.Run(async () => await new TaggedFileParser(new TaggedFile(filePath)).LoadDocumentAsync());


        //}

        //private static async Task<IEnumerable<string>> CountByTypeAndText(Document document) {
        //    return await Task.Run(() => {
        //        var phrasePOSCounts = from R in document.Phrases
        //                              group R by new {
        //                                  Type = R.GetType(),
        //                                  R.Text
        //                              } into G
        //                              orderby G.Count()
        //                              select G;
        //        return from g in phrasePOSCounts
        //               select String.Format("{0} : \"{1}\"; with count: {2}:", g.Key.Type.Name, g.Key.Text, g.Count());
        //    });
        //}

        static void ThesaurusCMDLineTest() {
            var verbLookUp = new LASI.Algorithm.Thesauri.VerbThesaurus(@"..\..\..\..\WordNetThesaurusData\data.verb");
            verbLookUp.Load();

            Console.Write("Enter a Verb:    ");
            var input = Console.ReadLine();
            while (input != "~") {

                try {
                    foreach (var v in verbLookUp[input]) {
                        Console.Write(v + ", ");
                    }
                } catch (KeyNotFoundException) {
                    Console.WriteLine(String.Format("No synonyms recognized for \"{0}\" : as verb", input));
                }
                Console.WriteLine();
                Console.Write("Enter a Verb:    ");
                input = Console.ReadLine();
            }
        }


        static Action<object> print = (o) => Console.WriteLine(o);
        static Action<object> printFile = (o) => {
            using (var writer = new StreamWriter(@"..\..\..\..\Desktop\taggingModeTesting.txt", true)) {
                writer.WriteLine(o);
            }
        };
    }
}