using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LASI.WebApp.Models;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Immutable;
using System.IO;
using LASI.Utilities;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace LASI.WebApp
{
    internal class MongoDbAccountProvider : IAccountProvider
    {
        public MongoDbAccountProvider() {
            CreateMongoServer();
        }
        public void Insert(IAccountModel account) {
            accounts.Insert(account);
        }

        internal MongoDbAccountProvider(HttpApplication applicationContext) {
            this.applicationContext = applicationContext;
        }

        private void CreateMongoServer() {
            var mongoServerProcess = Process.Start(new ProcessStartInfo
            {
                FileName = mongodExecutableLocation,
                Arguments = string.Join(" ", "--dbpath", applicationContext.Server.MapPath(mongoDbPath))
            });
            applicationContext.Disposed += delegate { mongoServerProcess.Dispose(); };
        }

        public IEnumerator<IAccountModel> GetEnumerator() {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return accounts.AsQueryable().GetEnumerator();
        }

        #region Fields
        private MongoCollection<AccountModel> accounts = mongoServer.Value.GetDatabase("accounts").GetCollection<AccountModel>("users");

        private static readonly string mongodExecutableLocation = ConfigurationManager.AppSettings["MongodExecutableLocation"];
        private readonly HttpApplication applicationContext;
        private static Lazy<MongoServer> mongoServer = new Lazy<MongoServer>(valueFactory: () => new MongoClient(connectionString).GetServer(), isThreadSafe: true);
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
        private static readonly string mongoDbPath = ConfigurationManager.AppSettings["MongoDbPath"];

        #endregion Fields
    }
}