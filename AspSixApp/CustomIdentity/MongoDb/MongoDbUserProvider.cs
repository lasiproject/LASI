using System;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Framework.ConfigurationModel;
using MongoDB.Driver.Linq;
using System.Collections;
using Microsoft.AspNet.Mvc;
using MongoDB.Driver.Builders;

namespace AspSixApp.CustomIdentity.MongoDb
{
    public class MongoDbUserProvider : UserProvider<ApplicationUser>
    {
        public MongoDbUserProvider(IConfiguration config, AppDomain appDomain)
        {
            var mongodExePath = config["MongodExecutableLocation"];
            var mongoDbPath = config["MongoDbPath"];
            var mongoFilesDirectory = appDomain.BaseDirectory + mongoDbPath;
            var connectionString = config["MongoConnection"];
            System.Diagnostics.Process.Start(
               fileName: mongodExePath,
               arguments: $"--dbpath {mongoFilesDirectory}"
           );

            mongoDatabase = new Lazy<MongoDatabase>(valueFactory: () => new MongoClient(new MongoUrl(connectionString)).GetServer().GetDatabase("accounts"));

        }
        public void Insert(ApplicationUser account)
        {
            Accounts.Insert(account);
        }

        public override IEnumerator<ApplicationUser> GetEnumerator() => Accounts.AsQueryable().GetEnumerator();

        private const string MongoIdFieldName = "_id";
        private static IMongoQuery EQ_id_Query(string id) => Query.EQ(MongoIdFieldName, id);

        public override IdentityResult Add(ApplicationUser user)
        {
            return WithLock(() =>
            {
                if (Accounts.Count(Query.EQ("email", user.NormalizedEmail)) != 0)
                {
                    return IdentityResult.Failed(IdentityErrorDescriber.Default.DuplicateEmail(user.Email));
                }
                var result = Accounts.Insert(user);
                if (result?.ErrorMessage == null)
                {
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage });
                }
            });
        }

        public override IdentityResult Update(ApplicationUser user)
        {
            return WithLock(() =>
            {
                var result = Accounts.Update(EQ_id_Query(user.Id), Update<ApplicationUser>.Replace(user));
                if (result?.ErrorMessage == null) { return IdentityResult.Success; }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage });
                }
            });
        }
        public override IdentityResult Delete(ApplicationUser user)
        {
            return WithLock(() =>
            {
                var writeConcernResult = Accounts.Remove(EQ_id_Query(user.Id));
                if (writeConcernResult?.ErrorMessage == null)
                {
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Description = writeConcernResult.ErrorMessage
                    });
                }
            });
        }
        public override ApplicationUser this[string id] => WithLock(() => Accounts.FindOneById(id));

        private T WithLock<T>(Func<T> f)
        {
            lock (lockon)
            {
                return f();
            }
        }

        private MongoCollection<ApplicationUser> Accounts => mongoDatabase.Value.GetCollection<ApplicationUser>("users");

        private Lazy<MongoDatabase> mongoDatabase;
        private object lockon = new object();

    }
}