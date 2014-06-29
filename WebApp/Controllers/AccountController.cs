﻿using System;
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
        //public MongoDBEntities() : base("name=connectionString") { }

        //static MongoServer server = new MongoClient(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString).GetServer();
        //MongoDatabase MyDB = server.GetDatabase("test");

        public ActionResult Login() {
            return View();
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

        [HttpPost]
        public ActionResult CreateNew(AccountModel model) {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var userDataFile = Server.MapPath("~/App_Data/Users/" + model.Email + ".json");
            using (var writer = new JsonTextWriter(new System.IO.StreamWriter(userDataFile, append: true))
            {
                Formatting = Formatting.Indented
            }) {
                JsonSerializer.Create(settings).Serialize(writer, model);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Settings() {
            return View();
        }
    }
}