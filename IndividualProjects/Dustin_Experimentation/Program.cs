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
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;

namespace Dustin_Experimentation
{ //this is a comment 
    class Program
    {
        static void Main(string[] args) {
            foreach (var t in Thesaurus.GetThesaurusTasks())
            {
                t.Wait();
            }
            Thesaurus
        }
    }
}
