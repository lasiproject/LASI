using System;
using System.Collections;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Linq;

namespace AspSixApp.CustomIdentity.MongoDb
{
    public class MongoDbRoleProvider : IRoleProvider<UserRole>
    {
        public UserRole Get(Func<UserRole, bool> match) => Roles.AsQueryable().FirstOrDefault(match);
        public MongoDbRoleProvider(MongoDbService dbService)
        {
            roles = new Lazy<MongoCollection<UserRole>>(() => dbService.GetCollection<UserRole>());
        }
        public IdentityResult Add(UserRole role)
        {
            var result = Roles.Insert(role);
            return CreateIdentityResultFromQueryResult(result);
        }

        public IdentityResult Delete(UserRole role)
        {
            var result = Roles.Remove(Query.And(Query.EQ("RoleId", role.RoleId), Query.EQ("UserId", role.UserId)));
            return CreateIdentityResultFromQueryResult(result);
        }


        public void Remove(ApplicationUser user, string roleName)
        {
            var toRemove = from role in Roles.AsQueryable()
                           where role.RoleName == roleName
                           where role.UserId == user.Id
                           select role;
            var result = Roles.Remove(Query.And(Query.EQ("RoleName", roleName), Query.EQ("UserId", user.Id)));
        }

        public IdentityResult Update(UserRole role)
        {
            var result = Roles.Update(Query.And(Query.EQ("_id", role.RoleId), Query.EQ("UserId", role.UserId)), Update<UserRole>.Replace(role));
            return CreateIdentityResultFromQueryResult(result);
        }

        private static IdentityResult CreateIdentityResultFromQueryResult(WriteConcernResult result)
        {
            if (result?.ErrorMessage == null)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage });
            }
        }

        private MongoCollection<UserRole> Roles => roles.Value;

        public IEnumerator<UserRole> GetEnumerator() => Roles.AsQueryable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private readonly Lazy<MongoCollection<UserRole>> roles;

    }
}