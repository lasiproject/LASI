using System;
using System.Collections;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Linq;
using AspSixApp.CustomIdentity.MongoDB.Extensions;

namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDBRoleProvider : IRoleProvider<UserRole>
    {
        public MongoDBRoleProvider(MongoDBService dbService)
        {
            roles = new Lazy<MongoCollection<UserRole>>(() => dbService.GetCollection<UserRole>());
        }
        public UserRole Get(Func<UserRole, bool> match) => this.FirstOrDefault(match);

        public IdentityResult Add(UserRole role)
        {
            var result = Roles.Insert(role);
            return CreateIdentityResultFromQueryResult(result);
        }

        public IdentityResult Delete(UserRole role)
        {
            var result = Roles.Remove(Query.EQ("RoleId", role.RoleId).And(Query.EQ("UserId", role.UserId)));
            return CreateIdentityResultFromQueryResult(result);
        }


        public void RemoveFromRole(ApplicationUser user, string roleName)
        {
            var toRemove = from role in Roles.AsQueryable()
                           where role.RoleName == roleName
                           where role.UserId == user.Id
                           select role;
            var result = Roles.Remove(Query.EQ("RoleName", roleName).And(Query.EQ("UserId", user.Id)));
        }

        public IdentityResult Update(UserRole role)
        {
            var result = Roles.Update(Query.EQ("_id", role.RoleId).And(Query.EQ("UserId", role.UserId)), Update<UserRole>.Replace(role));
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

        public IEnumerator<UserRole> GetEnumerator() => Roles.FindAll().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private readonly Lazy<MongoCollection<UserRole>> roles;

    }
}