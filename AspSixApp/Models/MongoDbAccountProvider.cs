using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System.Collections;
using AspSixApp.Models.ApplicationUsers;

namespace AspSixApp
{
    using System.Threading;
    using System.Threading.Tasks;
    using User = Models.ApplicationUsers.MongoUser;
    public class MongoDbAccountProvider : IDisposable
    {
        public void Insert(User account) {
            accounts.Insert(account);
        }
        public void Delete(User user) {
            var toDelete = accounts.FindAllAs<User>().First(u => u.Equals(user));
        }
        internal MongoDbAccountProvider(string webRoot) {
            this.webRoot = webRoot;
        }

        private void CreateMongoServer() {
            mongoServerProcess = Process.Start(new ProcessStartInfo
            {
                FileName = mongodExecutableLocation,
                Arguments = string.Join(" ", "--dbpath", System.IO.Path.Combine(webRoot, mongoDbPath))
            });
        }

        public void Dispose() {
            ((IDisposable)mongoServerProcess).Dispose();
        }



        #region Fields
        private MongoCollection<User> accounts = mongoServer.Value.GetDatabase("accounts").GetCollection<User>("users");

        internal async Task<User> FindSingleAsync(Func<User, bool> p, CancellationToken cancellationToken = default(CancellationToken)) => await accounts.AsQueryable().SingleAsync(x => p(x),cancellationToken);
        private static readonly string mongodExecutableLocation = ConfigurationManager.AppSettings["MongodExecutableLocation"];
        private static Lazy<MongoServer> mongoServer = new Lazy<MongoServer>(valueFactory: () => new MongoClient(connectionString).GetServer(), isThreadSafe: true);
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
        private static readonly string mongoDbPath = ConfigurationManager.AppSettings["MongoDbPath"];
        private readonly string webRoot;
        private Process mongoServerProcess;

        #endregion Fields
    }
}