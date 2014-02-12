﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LASI.Utilities;
using LASI.ContentSystem;
using LASI.Interop;
using LASI;
using System.Threading.Tasks;
using LASI.Core;
using Newtonsoft.Json;

namespace LASI.WebService.Controllers
{
    public class HomeController : Controller
    {
        private const string USER_UPLOADED_DOCUMENTS_DIR = "~/App_Data/SourceFiles/";

        public ActionResult Index(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Upload() {
            if (!Directory.Exists(USER_UPLOADED_DOCUMENTS_DIR)) { Directory.CreateDirectory(USER_UPLOADED_DOCUMENTS_DIR); }
            for (var i = 0; i < Request.Files.Count; ++i) {

                var file = Request.Files[i];// file in Request.Files where file != null && file.ContentLength > 0 select file;
                foreach (var remnant in from remnant in new DirectoryInfo(Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR)).EnumerateFileSystemInfos()
                                        where remnant.Name.Contains(file.FileName.SplitRemoveEmpty('\\').Last())
                                        select remnant) {
                    var dir = remnant as DirectoryInfo;
                    if (dir != null) {
                        dir.Delete(true);
                    } else {
                        remnant.Delete();
                    }
                }
                var path = Path.Combine(Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR), file.FileName);

                file.SaveAs(path);
            }
            return RedirectToAction("Example", "Home");
        }
        public async Task<ActionResult> Example() {
            ViewBag.ReturnUrl = "Example";
            var extensionMap = new Dictionary<string, Func<string, InputFile>> {
                { "txt" , p => new TxtFile(p) },
                { "doc" , p => new DocFile(p) },
                { "docx" , p => new DocXFile(p) },
                { "pdf" , p => new PdfFile(p) },
                { "zip", p => null}
            };
            var files = from file in Directory.EnumerateFiles(Server.MapPath(USER_UPLOADED_DOCUMENTS_DIR)) select extensionMap[file.SplitRemoveEmpty('.').Last()](file) into f where f != null select f;


            var loadingTask = await Task.Run(async () => await new AnalysisController(files).ProcessAsync());

            ViewData.Add("docs", from task in loadingTask select task);
            loadingTask.ToList().ForEach(doc => {
                doc.Phrases.Select(p => {
                    var items = new MenuItemCollection { };
                    var item = new MenuItem { Text = "Phrase", Value = "p" };
                    items.Add(item);
                    return new { E = p, Menu = new Menu {/**/} };
                });
            });
            ViewBag.Title = "Example";
            return View();
        }

    }
}