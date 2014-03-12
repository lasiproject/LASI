using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LASI.WebApp.Controllers
{
    public class AccountController : Controller
    {
        //public MongoDBEntities() : base("name=connectionString") { }

        static MongoServer server = new MongoClient(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString).GetServer();
        MongoDatabase MyDB = server.GetDatabase("test");

        public ActionResult Login() {
            return View();
        }


        public ActionResult CreateAccount() {
            return View(new UserModel());
        }

        [HttpPost]
        public ActionResult CreateNew(UserModel model) {
            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var userDataFile = Server.MapPath("~/App_Data/Users/" + model.Email + ".json");
            using (var writer = new JsonTextWriter(new System.IO.StreamWriter(userDataFile, append: true)) { Formatting = Formatting.Indented }) {
                JsonSerializer.Create(settings).Serialize(writer, model);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Settings() {
            return View();
        }
    }
}