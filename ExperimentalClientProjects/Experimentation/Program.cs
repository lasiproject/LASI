using LASI.Core;
using LASI.Core.Binding;

using LASI.Core.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Interop;
using LASI.Utilities;
using LASI.Interop.ResourceManagement;
using LASI.Content;
using System.IO;

namespace LASI.Experimentation.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var fragment = new RawTextFragment(rawText, "Test");
            var percent = 0d;
            var notfier = new ResourceNotifier();
            var setsProcessed = 0;
            notfier.ResourceLoading += (s, e) =>
            {
                Logger.Log($"Sets Processed {++setsProcessed}");
            };
            notfier.ResourceLoaded += (s, e) =>
            {
                percent = Math.Min(100, percent + e.PercentWorkRepresented);
                Logger.Log($"Update : {e.Message} Percent : {percent += e.PercentWorkRepresented} MS : {e.ElapsedMiliseconds}");
            };
            var orchestrator =
                new AnalysisOrchestrator(new RawTextFragment(
                    File.ReadAllLines(@".\..\..\testDocs\testDoc1.txt"), "testDoc1")
                );
            orchestrator.ProgressChanged += (s, e) =>
            {
                percent = Math.Min(100, percent + e.PercentWorkRepresented);
                Logger.Log($"Update : {e.Message} Percent : {percent}");
            };

            var document = orchestrator.ProcessAsync().Result.First();

            var x = document.Entities.FirstOrDefault();
            x.Match()
                .Case((IReferencer r) => r.Referencers != null ? r.RefersTo.Text : r.Text)
                .Result(x.Text);
            Logger.Log(document);
            foreach (var phrase in document.Phrases)
            {
                Logger.Log(phrase);
            }
            Input.WaitForKey(ConsoleKey.Escape);
        }
        private const string rawText =
            @"Virginia was the first state in the United states. 
            Virginia is an at-will state. 
            This means that companies can fire employees for no reason as long as doing so does not violate federal labour laws. 
            Virginia takes many of it's political views from the religious right wing. 
            It has a very prominent conservative community.";
    }
}

