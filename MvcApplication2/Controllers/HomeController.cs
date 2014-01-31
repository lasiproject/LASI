using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LASI.Utilities;
using LASI.ContentSystem;

namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Upload() {
            for (var i = 0; i < Request.Files.Count; ++i) {
                var file = Request.Files[i];// file in Request.Files where file != null && file.ContentLength > 0 select file;
                var path = System.IO.Path.Combine(Server.MapPath("~/App_Data/SourceFiles/"), file.FileName);
                file.SaveAs(path);
            }
            return RedirectToAction("Example", "Home");
        }
        public ActionResult Example() {
            ViewBag.ReturnUrl = "Example";
            var extensionMap = new Dictionary<string, Func<string, InputFile>> {
                { "txt" , p => new TextFile(p) },
                { "doc" , p => new DocFile(p) },
                { "docx" , p => new DocXFile(p) },
                { "pdf" , p=> new PdfFile(p) }, 
            };
            var files = from file in System.IO.Directory.EnumerateFiles(Server.MapPath("~/App_Data/SourceFiles/")) select extensionMap[file.Split('.').Last()](file);


            var doc = System.Threading.Tasks.Task.Run(() => new LASI.Interop.AnalysisController(files).ProcessAsync().Result.First());
            ViewData.Add("doc", doc.Result);
            doc.Result.Phrases.Select(p => {
                var items = new MenuItemCollection { };
                var item = new MenuItem { Text = "Phrase", Value = "p" };
                items.Add(item);
                return new { E = p, Menu = new Menu {/**/} };
            });
            ViewBag.Title = "Example";
            return View();
        }
    }
}