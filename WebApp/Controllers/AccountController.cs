using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LASI.WebApp.Models;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private const string USER_DIR = "~/App_Data/Users/";

        //public MongoDBEntities() : base("name=connectionString") { }

        //static MongoServer server = new MongoClient(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString).GetServer();
        //MongoDatabase MyDB = server.GetDatabase("test");

        public ActionResult Login(AccountModel account) {
            return RedirectToAction("Index", "Home", View(account));
        }
        public ActionResult Authenticate(LoginModel credentials) {
            bool authenticated = ValidateCredentials(credentials);
            return authenticated ? Success() : Failure();
        }

        private ActionResult Failure() {
            throw new NotImplementedException();
        }

        private ActionResult Success() {
            throw new NotImplementedException();
        }

        private bool ValidateCredentials(LoginModel credentials) {
            throw new NotImplementedException();
        }

        public ActionResult CreateAccount() {
            return View(new AccountModel());
        }

        public ActionResult CreateNew(AccountModel account) {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var userDataFile = Server.MapPath(USER_DIR + account.Email + ".json");
            using (var writer = new JsonTextWriter(new System.IO.StreamWriter(userDataFile, append: true))
            {
                Formatting = Formatting.Indented
            }) {
                JsonSerializer.Create(settings).Serialize(writer, account);
            }
            return Login(account);
        }
        public ActionResult Settings(AccountModel account) {
            var profiles = from af in System.IO.Directory.EnumerateFiles(Server.MapPath(USER_DIR), "*.json")
                           let rawJson = System.IO.File.OpenText(af).ReadToEnd()
                           let accountData = JsonConvert.DeserializeObject<AccountModel>(rawJson)
                           where accountData.Email == account.Email
                           select account;
            return View(profiles.Single());
        }
    }
}