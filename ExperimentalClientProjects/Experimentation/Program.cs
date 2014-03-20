using LASI.ContentSystem;
using LASI.Core;
using LASI.Core.Binding;
using LASI.Core.DocumentStructures;
using LASI.Core.Heuristics;
using LASI.Core.Heuristics.Morphemization;
using LASI.Core.PatternMatching;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Experimentation.CommandLine
{
	class Program
	{
		static void Main (string[] args)
		{
			var document = Tagger.DocumentFromRaw (new[] { "Hello there you fool." });

			var dd = document.GetEntities ().FirstOrDefault ();
			dd.Match ()
                .Yield<string> ()
                .With ((IEntity e) => e.Text)
                .Result ();
			Output.WriteLine (document);

 

 
			 
			Input.WaitForKey (ConsoleKey.NumPad0);
 
		}
	}
}

