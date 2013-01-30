using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.DataRepresentation;
using LASI.FileSystem;
using SharpNLPTaggingModule;

namespace Aluan_Experimentation
{
    class Program
    {
        static void Main(string[] args) {
            var localParser = new TaggedFileParser(@"C:\Users\Aluan\Desktop\LASI\LASI_v1\TestDocs\Draft_Environmental_Assessment.tagged");
            var paras = localParser.GetParagraphs();
            foreach (var P in paras)
                Console.WriteLine(P);

        }
    }
}
