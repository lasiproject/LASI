using System;
using System.Collections.Generic;
using System.Linq;
using AspSixApp.CustomIdentity;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    public class InMemoryRoleProvider : RoleProvider<UserRole>
    {
        public InMemoryRoleProvider()
        {
            Roles = System.Collections.Immutable.ImmutableHashSet.Create<UserRole>();
        }

        public System.Collections.Immutable.IImmutableSet<UserRole> Roles { get; private set; }

        public override IEnumerator<UserRole> GetEnumerator() => Roles.GetEnumerator();

        internal override IdentityResult Add(UserRole role)
        {
            if (Roles.Any(r => r.RoleId == role.RoleId || r == role))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists" });
            }
            else
            {
                Roles = Roles.Add(role);
                return IdentityResult.Success;
            }
        }


        internal override IdentityResult Delete(UserRole role)
        {
            Roles = Roles.Remove(Roles.FirstOrDefault(r => r.RoleId == role.RoleId || r == role));
            return IdentityResult.Success;
        }

        internal override void Remove(IndividualUser user, string roleName)
        {
            Roles = Roles.Remove(Roles.FirstOrDefault(r => r.RoleName == roleName && r.UserId == user.Id));
        }

        internal override IdentityResult Update(UserRole role)
        {
            var target = Roles.FirstOrDefault(r => r.RoleName == role.RoleName || r.RoleId == role.RoleId || r == role);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role could not be found" });
            }
            target.RoleId = role.RoleId;
            target.RoleName = role.RoleName;
            target.NormalizedRoleName = role.NormalizedRoleName;
            target.UserId = role.UserId;
            return IdentityResult.Success;
        }
    }
}