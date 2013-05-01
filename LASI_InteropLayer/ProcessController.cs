using LASI.Algorithm;
using LASI.Algorithm.Analysis;
using LASI.Algorithm.DocumentConstructs;
using LASI.Algorithm.Thesauri;
using LASI.FileSystem;
using LASI.Utilities;
using LASI.GuiInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace LASI.InteropLayer
{


    public class ProcessController
    {
        public StringBuilder InfoProvider {
            get;
            private set;
        }

        public async Task<IEnumerable<Document>> LoadAndAnalyseAllDocuments(ProgressBar progressBar, Label progressLabel) {
            var sw = Stopwatch.StartNew();
            Output.SetToStringWriter(InfoProvider);

            await LoadThesauri(progressBar, progressLabel);

            progressBar.ToolTip = ProcessingState.ConvertingFiles.ToString();
            progressLabel.Content = ProcessingState.ConvertingFiles.ToString();
            await FileManager.ConvertAsNeededAsync();
            progressBar.Value += 5;
            progressBar.ToolTip = ProcessingState.IdentifyingSyntacticRoles.ToString();
            progressLabel.Content = ProcessingState.IdentifyingSyntacticRoles.ToString();
            await FileManager.TagTextFilesAsync();
            progressBar.Value += 15;
            progressBar.ToolTip = ProcessingState.TransformingTextualRepresentations.ToString();
            progressLabel.Content = ProcessingState.TransformingTextualRepresentations.ToString();
            var docs = await InstantiateDocuments();
            progressBar.Value += 25;

            progressBar.ToolTip = ProcessingState.BindingTextualStructures.ToString();
            progressLabel.Content = ProcessingState.BindingTextualStructures.ToString();
            await BindLexicals(docs);
            progressBar.Value += 20;
            progressBar.ToolTip = ProcessingState.ComputingMetrics.ToString();
            progressLabel.Content = ProcessingState.ComputingMetrics.ToString();
            await WeightAllDocs(docs);
            progressBar.Value += 20;
            progressBar.ToolTip = sw.ElapsedMilliseconds / 1000f;
            return docs;
        }

        private static async Task LoadThesauri(ProgressBar progressBar, Label progressLabel) {
            progressBar.ToolTip = ProcessingState.LoadingThesauri.ToString();
            progressLabel.Content = ProcessingState.LoadingThesauri.ToString();
            await Thesaurus.LoadAllAsync();
            progressBar.Value = 15;
        }

        private static async Task WeightAllDocs(IEnumerable<LASI.Algorithm.DocumentConstructs.Document> docs) {
            foreach (var doc in docs) {
                await Task.Run(() => Weighter.Weight(doc));
                await Task.Yield();
            }

        }

        private static async Task BindLexicals(IEnumerable<LASI.Algorithm.DocumentConstructs.Document> docs) {
            foreach (var doc in docs) {
                await Task.Run(() => Binder.Bind(doc));
                await Task.Yield();
            }
        }
        private async Task<IEnumerable<Document>> InstantiateDocuments() {
            var result = new ConcurrentBag<LASI.Algorithm.DocumentConstructs.Document>();
            foreach (var doc in FileManager.TaggedFiles) {
                result.Add(await new TaggedFileParser(doc).LoadDocumentAsync());
                await Task.Yield();
            }
            return result;
        }
    }



}


#region deprecated
//internal class AsyncWorkItem
//{

//    public Task WorkToDo {
//        get;
//        set;
//    }
//    public AsyncWorkItem() {

//    }
//    public async Task BeginTask() {
//        await WorkToDo;
//    }

//    public ProcessingState CompletionMessage {
//        get;
//        set;
//    }
//    ProcessingState[] statuses = new[] { 
//            ProcessingState.ConvertingFiles,
//            ProcessingState.TransformingTextualRepresentations,
//            ProcessingState.AggregatingPhrases, 
//ProcessingState.ComputingMetrics, 
//ProcessingState.CrossReferencing, 
//ProcessingState.Completing };

//    public ProcessingState[] Statuses {
//        get {
//            return statuses;
//        }
//        protected set {
//            statuses = value;
//        }
//    }
//}

#endregion