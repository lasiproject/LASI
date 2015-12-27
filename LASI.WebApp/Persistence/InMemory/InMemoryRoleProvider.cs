using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using Microsoft.AspNet.Identity;

namespace LASI.WebApp.Persistence
{
    public class InMemoryRoleProvider : IRoleAccessor<UserRole>
    {
        public IImmutableSet<UserRole> Roles { get; private set; } = ImmutableHashSet.Create<UserRole>();

        public IdentityResult Add(UserRole role)
        {
            if (Roles.Any(r => r.RoleId == role.RoleId || r == role))
            {
                return IdentityResult.Failed(errorDescriber.UserAlreadyInRole(role.RoleName));
            }
            else
            {
                Roles = Roles.Add(role);
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
                return IdentityResult.Failed(errorDescriber.UserNotInRole(role.RoleName));
            }
            target._id = role._id;
            target.RoleName = role.RoleName;
            target.NormalizedRoleName = role.NormalizedRoleName;
            target.UserId = role.UserId;
            return IdentityResult.Success;
        }
        
        public IEnumerator<UserRole> GetEnumerator() => Roles.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        public UserRole Get(Func<UserRole, bool> match) => Roles.FirstOrDefault(match);

        private readonly IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();

        #region Synchronization
        /// <summary>
        /// Provides an on which to synchronize. Do not use for any other purpose. Do not expose reference.
        /// </summary>
        private static readonly object Lock = new object();
        private static void WithLock(Action a) { lock (Lock) a(); }

        private static T WithLock<T>(Func<T> f) { lock (Lock) return f(); }

        #endregion
    }
}