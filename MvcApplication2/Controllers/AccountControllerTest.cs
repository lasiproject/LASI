using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LASI.WebService.Models;
using Newtonsoft.Json;

namespace LASI.WebService.Controllers
{
    public class AccountControllerTest : Controller
    {
        //
        [HttpPost]
        public ActionResult Login(dynamic credentials) {
            //Request.Form["username"]
            return View();
        }

        public ActionResult CreateAccount() {
            var user = new { Email = "myname@email.com", PassWord = "pwd" };
            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var str = JsonConvert.SerializeObject(user, settings);
            return View();
        }

    }
}