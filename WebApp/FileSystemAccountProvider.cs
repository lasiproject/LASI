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
    internal class FileSystemAccountProvider : IAccountProvider
    {
        public FileSystemAccountProvider(HttpApplication app) {
            var userDirectory = app.Server.MapPath(USER_DIR);
            var accounts =
                from path in Directory.EnumerateFiles(userDirectory, "*.json", SearchOption.AllDirectories) select JsonConvert.DeserializeObject<AccountModel>(File.ReadAllText(path));
            app.Disposed += (s, e) => accounts.ToList().ForEach(account => {
                using (var writer = new StreamWriter(Path.Combine(userDirectory, account.Email + ".json"), append: false)) {
                    writer.Write(JsonConvert.SerializeObject(account));
                }
            });
        }

        public void Insert(AccountModel account) {
            accounts = accounts.Add(account);
        }

        public IEnumerator<IAccountModel> GetEnumerator() => accounts.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IImmutableSet<AccountModel> accounts;

        private const string USER_DIR = "~/App_Data/Users/";
    }
}