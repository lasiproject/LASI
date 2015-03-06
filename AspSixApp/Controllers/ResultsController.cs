using AspSixApp.Models;
using LASI.Core;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspSixApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AspSixApp.Models.DocumentStructures;
    using LASI.Interop;
    using LASI.Utilities;
    using Directory = System.IO.Directory;
    using Path = System.IO.Path;
    using SerializerSettings = Newtonsoft.Json.JsonSerializerSettings;
    using CamelCasePropertyNamesContractResolver = Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver;
    using JobStatusMap = System.Collections.Concurrent.ConcurrentDictionary<int, Models.Results.JobStatus>;
    using ProcessedDocumentSet = System.Collections.Immutable.IImmutableSet<Document>;
    using FileExtensionMap = LASI.Content.ExtensionWrapperMap;
    using System.Security.Principal;
    using AspSixApp.CustomIdentity.MongoDb;

    public class ResultsController : Controller
    {
        public ResultsController(MongoDbInputDocumentStore documentStore)
        {
            Phrase.VerboseOutput = true; this.documentStore = documentStore;
        }
        //static int timesExecuted = 0;
        public async Task<ActionResult> Index()
        {
            TrackedJobs.Clear();
            CurrentOperation = string.Empty;
            PercentComplete = 0;
            return await Results();
        }

        public ActionResult Progress()
        {
            return View();
        }

        public async Task<ViewResult> Results()
        {
            var documents = await LoadResults();
            var charts = from document in documents
                         let topResults = NaiveResultSelector.GetTopResultsByEntity(document).Take(ChartItemLimit)
                         let rowData = from result in topResults
                                       select new object[] { result.First, result.Second }
                         select new { Rows = Newtonsoft.Json.Linq.JArray.FromObject(rowData), Title = document.Title };

            ViewBag.Charts = charts.ToDictionary(chart => chart.Title, chart => chart.Rows);
            ViewBag.Title = "Results";
            return View(new DocumentSetModel(documents));
        }

        public async Task<ActionResult> Upload()
        {
            //if (!Directory.Exists(UserDocumentsDir))
            //{
            //    Directory.CreateDirectory(UserDocumentsDir);
            //}
            //for (var i = 0; i < Items.Files.Count; ++i)
            //{
            //    var file = Request.Files[i];// file in Request.Files where file != null && file.ContentLength > 0 select file;
            //    foreach (var remnant in from remnant in new DirectoryInfo(serverPath).EnumerateFileSystemInfos()
            //                            where remnant.Name.Contains(file.FileName.SplitRemoveEmpty('\\').Last())
            //                            select remnant)
            //    {
            //        var dir = remnant as DirectoryInfo;
            //        if (dir != null)
            //        {
            //            dir.Delete(true);
            //        } else
            //        {
            //            remnant.Delete();
            //        }
            //    }
            //    var path = Path.Combine(serverPath,
            //        file.FileName.SplitRemoveEmpty('\\').Last());
            //    file.SaveAs(path);
            //}
            return await Results();
        }

        private async Task<IEnumerable<Document>> LoadResults()
        {
            var extensionMap = new FileExtensionMap(path => null);
            //var files = Directory.EnumerateFiles(UserDocumentsDirectory)
            //    .Select(file =>
            //    {
            //        try
            //        {
            //            return extensionMap['.' + file.SplitRemoveEmpty('.').Last()](file);
            //        }
            //        catch (ArgumentException)
            //        {
            //            return null;
            //        }
            //    })
            //    .Where(file => file != null && !processedDocuments.Any(d => d.Title == file.NameSansExt));

            var userId = Context.User.Identity.GetUserId();

            var files = documentStore.GetAllUserInputDocuments(userId);
            var analyzer = new AnalysisOrchestrator(files);
            analyzer.ProgressChanged += (s, e) =>
            {
                PercentComplete += e.PercentWorkRepresented;
                CurrentOperation = e.Message;
            };
            var documents = await analyzer.ProcessAsync();
            PercentComplete = 100;
            CurrentOperation = "Analysis Complete.";
            processedDocuments = processedDocuments.Union(documents);
            return processedDocuments;
        }

        // These properties should be removed and replaced with a better solution to sharing
        // progress This was a hack to initially test the functionality
        public static string CurrentOperation { get; private set; }

        public static double PercentComplete { get; internal set; }

        public static JobStatusMap TrackedJobs { get; } = new JobStatusMap();
        private string ServerPath => AppDomain.CurrentDomain.BaseDirectory/*.GetData("DataDirectory").ToString()*/;
        private string UserDocumentsDirectory => Path.Combine(ServerPath, "App_Data", "SourceFiles");
        private const int ChartItemLimit = 5;

        private static ProcessedDocumentSet processedDocuments = System.Collections.Immutable.ImmutableHashSet.Create(
                    ComparerFactory.Create<Document>((dx, dy) => dx.Title == dy.Title, d => d.Title.GetHashCode()));

        private static SerializerSettings serializerSettings = new SerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        private readonly MongoDbInputDocumentStore documentStore;
    }
}
