using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LASI.Utilities;
using LASI.ContentSystem;
using LASI.Interop;
using System.Threading.Tasks;
using LASI.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LASI.WebApp.Models;

using System.Collections.Concurrent;

namespace LASI.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(AccountModel account = null, string returnUrl = "") {
            ViewBag.ReturnUrl = returnUrl;
            trackedJobs.Clear();
            currentOperation = string.Empty;
            percentComplete = 0;
            if (Directory.Exists(USER_UPLOADED_DOCUMENTS_DIR)) {
                foreach (var fileSystemInfo in new DirectoryInfo(USER_UPLOADED_DOCUMENTS_DIR).EnumerateFileSystemInfos()) {
                    fileSystemInfo.Delete();
                }
            }
            return View(new AccountModel());
        }

        [HttpPost]
        public async Task<ActionResult> Upload() {
            var serverPath = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            if (!Directory.Exists(serverPath)) {
                Directory.CreateDirectory(serverPath);
            }
            for (var i = 0; i < Request.Files.Count; ++i) {
                var file = Request.Files[i];// file in Request.Files where file != null && file.ContentLength > 0 select file;
                foreach (var remnant in from remnant in new DirectoryInfo(serverPath).EnumerateFileSystemInfos()
                                        where remnant.Name.Contains(file.FileName.SplitRemoveEmpty('\\').Last())
                                        select remnant) {
                    var dir = remnant as DirectoryInfo;
                    if (dir != null) {
                        dir.Delete(true);
                    } else {
                        remnant.Delete();
                    }
                }
                var path = Path.Combine(serverPath,
                    file.FileName.SplitRemoveEmpty('\\').Last());
                file.SaveAs(path);
            }
            return await Results();
        }

        private const short CHART_ITEM_MAX = 5;

        public ActionResult Progress() {
            return View();
        }


        internal async Task<ViewResult> Results() {
            var documents = await LoadResults();
            Phrase.VerboseOutput = true;
            var chartData = JArray.FromObject(from document in documents
                                              let topResults = NaiveResultSelector.GetTopResultsByEntity(document).Take(CHART_ITEM_MAX)
                                              from result in topResults
                                              let row = new[] { (string)result.Key, (string)result.Value }
                                              let rank = row[0]
                                              orderby rank descending
                                              group row by document);
            //.ToDictionary(g => g.Key, g => JsonConvert.SerializeObject(g.ToArray())
            //.Take(CHART_ITEM_MAX)).ToArray();
            ViewData["charts"] = chartData.ToArray();
            ViewData["documents"] = documents.Select(document => new DocumentModel(document));
            ViewBag.Title = "Results";
            return View(new DocumentSetModel(documents));
        }

        private async Task<IEnumerable<Document>> LoadResults() {
            var serverPath = Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR);
            var extensionMap = new ExtensionWrapperMap(UnsupportedFormatHandling.YieldNull);
            var files = Directory.EnumerateFiles(serverPath)
                .Select(file => {
                    try {
                        return extensionMap[file.SplitRemoveEmpty('.').Last()](file);
                    }
                    catch (ArgumentException) { return null; }
                })
                .Where(file => file != null && !processedDocuments.Any(d => d.Name == file.NameSansExt));
            var analyzer = new AnalysisOrchestrator(files);
            analyzer.ProgressChanged += (s, e) => {
                percentComplete += e.PercentWorkRepresented;
                currentOperation = e.Message;
            };
            var documents = await Task.Run(async () => await analyzer.ProcessAsync());
            percentComplete = 100;
            currentOperation = "Analysis Complete.";
            processedDocuments.UnionWith(documents);
            return processedDocuments;
        }

        private static ISet<Document> processedDocuments = new HashSet<Document>(
            CustomComparer.Create<Document>((dx, dy) => dx.Name == dy.Name,
                d => d.Name.GetHashCode())
            );

        private static ConcurrentDictionary<string, JobStatus> trackedJobs = new ConcurrentDictionary<string, JobStatus>(comparer: StringComparer.OrdinalIgnoreCase);
        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };
        //static int timesExecuted = 0;
        [HttpGet]
        public string GetJobStatus(string jobId = "") {
            if (jobId == "") {
                return JsonConvert.SerializeObject(
                    trackedJobs.Select(job => new
                {
                    job.Value.Message,
                    job.Value.Percent,
                    Id = job.Key
                }).ToArray(), serializerSettings);
            }
            percentComplete %= 100;

            if (percentComplete > 99) { percentComplete = 0; }
            bool extant = trackedJobs.ContainsKey(jobId);
            int id;
            var update = new JobStatus(int.TryParse(jobId, out id) ? id : -1, currentOperation, percentComplete);
            trackedJobs[jobId] = update;

            return JsonConvert.SerializeObject(update, serializerSettings);
        }

        private const string USER_UPLOADED_DOCUMENTS_DIR = "~/App_Data/SourceFiles/";

        // These fields should be removed and replaced with a better solution to sharing progress
        // This was a hack to initially test the functionality
        private static double percentComplete;
        private static string currentOperation;

    }
}