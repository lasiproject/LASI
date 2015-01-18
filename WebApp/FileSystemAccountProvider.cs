using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.WebApp.Models;
using System.Collections;
using System.Collections.Immutable;
using System.IO;
using Newtonsoft.Json;

namespace LASI.WebApp
{
    internal class FileSystemAccountProvider : IAccountProvider
    {
        public FileSystemAccountProvider(HttpApplication app) {
            var userDirectory = app.Server.MapPath(USER_DIR);
            var accounts = from path in Directory.EnumerateFiles(userDirectory, "*.json", SearchOption.AllDirectories)
                           select JsonConvert.DeserializeObject<AccountModel>(File.ReadAllText(path));
            AppDomain.CurrentDomain.DomainUnload += delegate {
                foreach (var account in accounts) {
                    using (var writer = new StreamWriter(Path.Combine(userDirectory, account.Email + ".json"), append: false)) {
                        writer.Write(JsonConvert.SerializeObject(account));
                    }
                }
            };
        }

        public void Insert(IAccountModel account) => accounts = accounts.Add(account);

        public IEnumerator<IAccountModel> GetEnumerator() => accounts.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IImmutableSet<IAccountModel> accounts;

        private const string USER_DIR = "~/App_Data/Users/";
    }
}