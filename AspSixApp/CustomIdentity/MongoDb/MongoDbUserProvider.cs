using System;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using Microsoft.Framework.ConfigurationModel;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using System.Linq;

namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDBUserProvider : IUserProvider<ApplicationUser>
    {

        public MongoDBUserProvider(MongoDBService dbService)
        {
            users = new Lazy<MongoCollection<ApplicationUser>>(() => dbService.GetCollection<ApplicationUser>());
        }

        public IEnumerator<ApplicationUser> GetEnumerator() => Users.AsQueryable().GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();


        public IdentityResult Add(ApplicationUser user)
        {
            return WithLock(() =>
            {
                if (Users.Count(Query.EQ("Email", user.NormalizedEmail)) != 0)
                {
                    return IdentityResult.Failed(IdentityErrorDescriber.Default.DuplicateEmail(user.Email));
                }
                var outcome = Users.Insert(user);
                if (outcome?.ErrorMessage == null)
                {
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = outcome.ErrorMessage });
                }
            });
        }

        public IdentityResult Update(ApplicationUser user)
        {
            var outcome = Users.Update(Query.EQ("_id", user.Id), Update<ApplicationUser>.Replace(user));
            if (outcome?.ErrorMessage == null)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $@"Unable to locate the user to update.
                                    Failed to locate user with the Id of {user.Id}"
                }, new IdentityError
                {
                    Description = outcome.ErrorMessage
                });
            }
        }
        public IdentityResult Delete(ApplicationUser user)
        {
            var outcome = Users.Remove(Query.EQ("_id", user.Id));
            if (outcome?.ErrorMessage == null)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = outcome.ErrorMessage
                });
            }
        }
        public ApplicationUser Get(Func<ApplicationUser, bool> match) => WithLock(() => Users.AsQueryable().FirstOrDefault(match));
        /// <summary>
        /// The <see cref="MongoCollection{T}"/> type is thread safe, making explicit synchronization necessary 
        /// only when it is accessed multiple times.
        /// </summary>
        private MongoCollection<ApplicationUser> Users => users.Value;

        private Lazy<MongoCollection<ApplicationUser>> users;

        private object lockon = new object();
        /// <summary>
        /// Invokes the given function after wrapping it in an instance lock.
        /// The <see cref="MongoCollection{T}"/> type is thread safe, making explicit synchronization necessary 
        /// only when it is accessed multiple times.
        /// </summary>
        private T WithLock<T>(Func<T> f)
        {
            lock (lockon)
            {
                return f();
            }
        }
    }
}