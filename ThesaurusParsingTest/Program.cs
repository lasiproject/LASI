using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThesaurusParsingTest
{
    class Program
    {
        static void Main(string[] args) {

            KeyCodeProvider.Load();
            //foreach (var pair in KeyCodeProvider.WordCodeMap)
            //{
            //    Console.WriteLine("Key: {0}, Val: {1}", pair.Key, pair.Value);
            //}

            LoadAndUseAync();


            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
            }


        }

        private static async void LoadAndUseAync() {
            var verbLookup = new LASI.Algorithm.VerbThesaurus();
            await Task.Run(() => verbLookup.LoadAsync());

            foreach (var word in KeyCodeProvider.WordCodeMap.Skip(new Random().Next(0, 10000)).Take(10)) {
                Console.Write("{0}    ->    ", word.Key);
                var matches = verbLookup.GetMatches(word.Value);
                foreach (var m in matches.SynIDCodes) {
                    var wordText = KeyCodeProvider.GetWord(m);
                    if (wordText != null)
                        Console.Write(wordText + ",");

                }
                Console.WriteLine();
            }
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
            }
        }
        #region Deprecated Code


        //private static void NewMethod()
        //{
        //    foreach (var associationSet in LASI.Algorithm.SynonymLookup.Assocuations.Take(10))
        //    {
        //        //Console.WriteLine((from s in associationSet.ToString().Split('_')
        //        //                   select s).Aggregate("", (str, wd) => str + "\n" + wd));
        //        Console.WriteLine(associationSet);
        //        Console.WriteLine((from code in associationSet.Value
        //                           select KeyCodeProvider.GetWord(code)).Aggregate("", (str, c) => {
        //                               return str + "  " + c;
        //                           }));

        //    }
        //}

        //private static void LoadDataFile(string dataFilePath)
        //{
        //    using (var reader = new StreamReader(new FileStream(dataFilePath, FileMode.Open, FileAccess.Read, FileShare.None, 8092, FileOptions.SequentialScan)))
        //    {
        //        var lines = new List<string>(2000);
        //        while (!reader.EndOfStream)
        //        {
        //            lines.Add(reader.ReadLine());
        //        }
        //        BuildLookup(lines);
        //    }
        //}





        //internal class ThesaurusIndex
        //{
        //    ThesaurusIndex(string dataFilePath)
        //    {
        //        using (var reader = new StreamReader(new FileStream(dataFilePath, FileMode.Open, FileAccess.Read, FileShare.None, 8092, FileOptions.SequentialScan)))
        //        {
        //            var lines = new List<string>(2000);
        //            while (!reader.EndOfStream)
        //            {
        //                lines.Add(reader.ReadLine());
        //            }
        //            BuildLookup(lines);
        //        }
        //    }
        //}

        #endregion
    }


}



