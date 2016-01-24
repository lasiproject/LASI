using System;
using System.Collections;
using System.Collections.Generic;
using LASI.WebApp.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Linq;
using LASI.WebApp.Persistence.MongoDB.Extensions;

namespace LASI.WebApp.Persistence.MongoDB
{
    public class MongoDBRoleProvider : IRoleAccessor<UserRole>
    {
        public MongoDBRoleProvider(MongoDBService dbService)
        {
            roles = new Lazy<MongoCollection<UserRole>>(() => dbService.GetCollection<UserRole>());
        }

        public UserRole Get(Func<UserRole, bool> predicate) => this.FirstOrDefault(predicate);

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


        public void RemoveFromRole(ApplicationUser user, string roleName) => Roles.Remove(Query.EQ("UserId", user.Id).And(Query.EQ("RoleName", roleName)));


        public IdentityResult Update(UserRole role)
        {
            var result = Roles.Update(Query.EQ("_id", role.RoleId).And(Query.EQ("UserId", role.UserId)), Update<UserRole>.Replace(role));
            return CreateIdentityResultFromQueryResult(result);
        }


        private static IdentityResult CreateIdentityResultFromQueryResult(WriteConcernResult result) =>
            result?.ErrorMessage == null
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError { Description = result.ErrorMessage });

        private MongoCollection<UserRole> Roles => roles.Value;

        public IEnumerator<UserRole> GetEnumerator() => Roles.FindAll().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly Lazy<MongoCollection<UserRole>> roles;

    }
}