using System;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    internal class InMemoryUserProvider : IUserProvider<ApplicationUser>
    {
        public InMemoryUserProvider()
        {
            users = new List<ApplicationUser>();
        }
        public IEnumerator<ApplicationUser> GetEnumerator() => users.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        public IdentityResult Add(ApplicationUser user) =>
            WithLock(() =>
            {
                if (!users.Contains(user))
                {
                    users.Add(user);
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            });

        public IdentityResult Delete(ApplicationUser user) =>
            WithLock(() => users.Remove(user) ? IdentityResult.Success : IdentityResult.Failed());

        public IdentityResult Update(ApplicationUser user) =>
            WithLock(() =>
            {
                var existing = this[user.Id];
                if (existing == null)
                {
                    return IdentityResult.Failed();
                }
                else
                {   // Transfer all properties except UserId, UserName,
                    existing.AccessFailedCount = user.AccessFailedCount;
                    existing.ConcurrencyStamp = user.ConcurrencyStamp;
                    existing.NormalizedUserName = user.NormalizedUserName;
                    existing.Email = user.Email;
                    existing.EmailConfirmed = user.EmailConfirmed;
                    existing.FirstName = user.FirstName;
                    existing.LastName = user.LastName;
                    existing.LockoutEnabled = user.LockoutEnabled;
                    existing.LockoutEnd = user.LockoutEnd;
                    existing.NormalizedEmail = user.NormalizedEmail;
                    existing.PasswordHash = user.PasswordHash;
                    existing.PhoneNumber = user.PhoneNumber;
                    existing.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                    existing.Projects = user.Projects;
                    existing.SecurityStamp = user.SecurityStamp;
                    existing.TwoFactorEnabled = user.TwoFactorEnabled;
                    return IdentityResult.Success;
                }
            });
        public ApplicationUser this[string id] => WithLock(() => users.Find(u => u.Id == id));

        private readonly List<ApplicationUser> users;

        #region Synchronization
        private static readonly object Lock = new object();

        private T WithLock<T>(Func<T> f) { lock (Lock) return f(); }
        #endregion
    }
}