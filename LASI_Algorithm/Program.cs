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
        static void Main(string[] args)
        {

            KeyCodeProvider.Load();
            //foreach (var pair in KeyCodeProvider.WordCodeMap)
            //{
            //    Console.WriteLine("Key: {0}, Val: {1}", pair.Key, pair.Value);
            //}
            SynonymLookup.Load();
            //NewMethod();

            foreach (var word in KeyCodeProvider.WordCodeMap.Skip(new Random().Next(0, 10000)).Take(4))
            {
                Console.WriteLine("\"{0}\"\n\n", word.Key);
                var matches = SynonymLookup.GetMatches(word.Value);
                foreach (var m in matches.SynIDCodes)
                {
                    var wordText = KeyCodeProvider.GetWord(m);
                    if (wordText != null)
                        Console.WriteLine(wordText + ",");

                }
                Console.WriteLine();
                //Console.WriteLine("\n Ants: ");
                ////foreach (var m in matches.AntIDCodes)
                ////    Console.Write(KeyCodeProvider.GetWord(m) + ",");

            }


            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey())
            {
            }


        }

        private static void NewMethod()
        {
            foreach (var associationSet in SynonymLookup.Assocuations.Take(10))
            {
                //Console.WriteLine((from s in associationSet.ToString().Split('_')
                //                   select s).Aggregate("", (str, wd) => str + "\n" + wd));
                Console.WriteLine(associationSet);
                Console.WriteLine((from code in associationSet
                                   select KeyCodeProvider.GetWord(code)).Aggregate("", (str, c) => {
                                       return str + "  " + c;
                                   }));

            }
        }

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


    }


}



