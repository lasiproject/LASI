using System;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using Microsoft.Framework.ConfigurationModel;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using System.Linq;

namespace AspSixApp.CustomIdentity.MongoDb
{
    public class MongoDbUserProvider : IUserProvider<ApplicationUser>
    {

        public MongoDbUserProvider(MongoDbService dbService)
        {
            users = new Lazy<MongoCollection<ApplicationUser>>(() => dbService.GetCollection<ApplicationUser>());
        }

        public void Insert(ApplicationUser account)
        {
            Users.Insert(account);
        }

        public IEnumerator<ApplicationUser> GetEnumerator() => Users.AsQueryable().GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        private const string MongoIdFieldName = "_id";
        private static IMongoQuery EQ_id_Query(string id) => Query.EQ(MongoIdFieldName, id);

        public IdentityResult Add(ApplicationUser user)
        {
            return WithLock(() =>
            {
                if (Users.Count(Query.EQ("email", user.NormalizedEmail)) != 0)
                {
                    return IdentityResult.Failed(IdentityErrorDescriber.Default.DuplicateEmail(user.Email));
                }
                var result = Users.Insert(user);
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

        public IdentityResult Update(ApplicationUser user)
        {
            return WithLock(() =>
            {
                var result = Users.Update(EQ_id_Query(user.Id), Update<ApplicationUser>.Replace(user));
                if (result?.ErrorMessage == null) { return IdentityResult.Success; }
                else
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Description = $@"Unable to locate the user to update.
                                         Failed to locate user with the Id of {user.Id}"
                    }, new IdentityError
                    {
                        Description = result.ErrorMessage
                    });
                }
            });
        }
        public IdentityResult Delete(ApplicationUser user)
        {
            return WithLock(() =>
            {
                var writeConcernResult = Users.Remove(EQ_id_Query(user.Id));
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
        public ApplicationUser Get(Func<ApplicationUser, bool> match) => WithLock(() => Users.AsQueryable().FirstOrDefault(match));
        public ApplicationUser this[string id] => WithLock(() => Users.FindOneById(id));


        private MongoCollection<ApplicationUser> Users => users.Value;

        private Lazy<MongoCollection<ApplicationUser>> users;

        private object lockon = new object();
        private T WithLock<T>(Func<T> f)
        {
            lock (lockon)
            {
                return f();
            }
        }


    }
}