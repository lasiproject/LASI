using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

namespace AspSixApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult About()
        {
            ViewBag.Message = "Your application description page.";

            return PartialView("_About");
        }

        public PartialViewResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return PartialView("Acount");
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}