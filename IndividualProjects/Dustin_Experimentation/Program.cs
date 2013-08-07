using LASI;
using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Lookup;
using LASI.ContentSystem;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustin_Experimentation
{ //this is a comment 
    class Program
    {
        static void Main(string[] args) {
            foreach (var t in LexicalLookup.GetUnstartedLoadingTasks()) {
                t.Wait();
            }

        }
    }
}
