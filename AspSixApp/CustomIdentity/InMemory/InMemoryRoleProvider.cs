using System;
using System.Collections.Generic;
using System.Linq;
using AspSixApp.CustomIdentity;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    public class InMemoryRoleProvider : IRoleProvider<UserRole>
    {
        public InMemoryRoleProvider()
        {
            Roles = new System.Collections.Generic.HashSet<UserRole>();
        }

        public System.Collections.Generic.ISet<UserRole> Roles { get; }

        public IdentityResult Add(UserRole role)
        {
            if (Roles.Any(r => r.RoleId == role.RoleId || r == role))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Role already exists" });
            }
            else
            {
                Roles.Add(role);
                return IdentityResult.Success;
            }
        }


        public IdentityResult Delete(UserRole role)
        {
            Roles.Remove(Roles.FirstOrDefault(r => r.RoleId == role.RoleId || r == role));
            return IdentityResult.Success;
        }

        public void RemoveFromRole(ApplicationUser user, string roleName)
        {
            Roles.Remove(Roles.FirstOrDefault(r => r.RoleName == roleName && r.UserId == user.Id));
        }

        public IdentityResult Update(UserRole role)
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

        #region Synchronization
        /// <summary>
        /// Provides an on which to synchronize. Do not use for any other purpose. Do not expose reference.
        /// </summary>
        private static readonly object Lock = new object();

        private static void WithLock(Action a) { lock (Lock) a(); }

        private static T WithLock<T>(Func<T> f) { lock (Lock) return f(); }

        #endregion
        public IEnumerator<UserRole> GetEnumerator() => Roles.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        public UserRole Get(Func<UserRole, bool> match) => Roles.FirstOrDefault(match);
    }
}