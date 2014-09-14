using LASI.Core;
using LASI.Core.Binding;
using LASI.Core.DocumentStructures;
using LASI.Core.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Interop;
using LASI.Output;

namespace LASI.Experimentation.CommandLine
{
    class Program
    {
        static void Main(string[] args) {
            var fragment = new ContentSystem.RawTextFragment(
                @"Virginia was the first state in the United states. 
Virginia is an at-will state. 
This means that companies can fire employees for no reason as long as doing so does not violate federal labour laws. 
Virginia takes many of it's political views from the religious right wing. 
It has a very prominent conservative community.", "Test");
            var percent = 0d;
            var notfier = new ResourceNotifier();
            notfier.ResourceLoaded += (s, e) => {
                percent = Math.Min(100, percent + e.PercentWorkRepresented);
                WriteLine("Update : {0} Percent : {1} MS : {2}", e.Message, percent += e.PercentWorkRepresented, e.ElapsedMiliseconds);
            };
            var orchestrator = new AnalysisOrchestrator(fragment);
            orchestrator.ProgressChanged += (s, e) => {
                percent = Math.Min(100, percent + e.PercentWorkRepresented);
                WriteLine("Update : {0} Percent : {1}", e.Message, percent);
            };

            var document = orchestrator.ProcessAsync().Result.First();

            var dd = document.GetEntities().FirstOrDefault();
            dd.Match()
                .Yield<string>()
                .With((IReferencer r) => r.Referencers != null ? r.RefersTo.Text : r.Text)
                .With((IEntity e) => e.Text)
                .Result();
            WriteLine(document);
            Phrase.VerboseOutput = true;
            foreach (var phrase in document.Phrases) { WriteLine(phrase); }


            Input.WaitForKey(ConsoleKey.Escape);

        }
    }
}

