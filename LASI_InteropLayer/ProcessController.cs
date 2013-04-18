using LASI.Algorithm;
using LASI.Algorithm.Analysis;
using LASI.Algorithm.Thesauri;
using LASI.FileSystem;
using LASI.GuiInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace LASI.InteropLayer
{


    public class ProcessController
    {
        ProcessingState[] statuses = new[] { ProcessingState.ConvertingFiles, ProcessingState.ParsingTaggedData, ProcessingState.AggregatingPhrases, 
ProcessingState.ComputingMetrics, ProcessingState.CrossReferencing, ProcessingState.Completing };

        public async Task<IEnumerable<Document>> LoadAndAnalyseAllDocuments(ProgressBar progressBar, Label progressLabel) {
            progressBar.ToolTip = ProcessingState.LoadingThesauri.ToString();
            progressLabel.Content = ProcessingState.LoadingThesauri.ToString();
            await Thesaurus.LoadAllAsync();
            progressBar.Value = 15;
            progressBar.ToolTip = ProcessingState.ConvertingFiles.ToString();
            progressLabel.Content = ProcessingState.ConvertingFiles.ToString();
            await FileManager.ConvertAsNeededAsync();
            progressBar.Value += 5;
            progressBar.ToolTip = ProcessingState.TaggingData.ToString();
            progressLabel.Content = ProcessingState.TaggingData.ToString();
            await FileManager.TagTextFilesAsync();
            progressBar.Value += 15;
            progressBar.ToolTip = ProcessingState.ParsingTaggedData.ToString();
            progressLabel.Content = ProcessingState.ParsingTaggedData.ToString();
            var docs = await InstantiateDocuments();
            progressBar.Value += 25;
            progressBar.ToolTip = ProcessingState.BuildingAssociations.ToString();
            progressLabel.Content = ProcessingState.BuildingAssociations.ToString();
            var k = await Task.WhenAll(from file in FileManager.TaggedFiles
                                       select Task.Run(async () => await new TaggedFileParser(file).LoadDocumentAsync()).ContinueWith(async ds => {
                                           var d = await ds;
                                           Binder.Bind(d);
                                           Weighter.Weight(d);
                                           return d;
                                       }));
            return await Task.WhenAll(k);
            //, TaskContinuationOptions.LongRunning);//    doc=> (from t in docs
            //                   select Task.Run(async () => await Task.Run(() => Binder.Bind(t)).ContinueWith(async f => await Task.Run(() => Weighter.Weight(t)))));
            //progressBar.Value += 40;

            //return docs;

        }

        private static async Task WeightAllDocs(IEnumerable<Document> docs) {
            foreach (var doc in docs) {
                await Task.Run(() => Weighter.Weight(doc));
                await Task.Yield();
            }

        }

        private static async Task BindLexicals(IEnumerable<Document> docs) {
            foreach (var doc in docs) {
                await Task.Run(() => Binder.Bind(doc));
                await Task.Yield();
            }
        }
        private async Task<IEnumerable<Document>> InstantiateDocuments() {
            var result = new ConcurrentBag<Document>();
            foreach (var doc in FileManager.TaggedFiles) {
                result.Add(await new TaggedFileParser(doc).LoadDocumentAsync());
                await Task.Yield();
            }
            return result;
        }
    }

    internal class AsyncWorkItem
    {

        public Task WorkToDo {
            get;
            set;
        }
        public AsyncWorkItem() {

        }
        public async Task BeginTask() {
            await WorkToDo;
        }

        public ProcessingState CompletionMessage {
            get;
            set;
        }
    }

}
