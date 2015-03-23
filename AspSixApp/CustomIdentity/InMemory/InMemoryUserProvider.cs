using System;
using System.Collections.Generic;
using System.Linq;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    public class InMemoryUserProvider : IUserProvider<ApplicationUser>
    {
        public InMemoryUserProvider()
        {
            users = new List<ApplicationUser>();
        }
        public IEnumerator<ApplicationUser> GetEnumerator() => users.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        public IdentityResult Add(ApplicationUser user) => WithLock(() =>
        {
            if (!users.Contains(user))
            {
                users.Add(user);
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(
                    IdentityErrorDescriber.Default.DuplicateUserName(user.UserName),
                    IdentityErrorDescriber.Default.DuplicateEmail(user.Email)
                );
            }
        });

        public IdentityResult Delete(ApplicationUser user) =>
            WithLock(() => users.Remove(user) ? IdentityResult.Success : IdentityResult.Failed());

        public IdentityResult Update(ApplicationUser user)
        {
            return WithLock(() =>
            {
                var existing = Get(u => u.Id == user.Id);
                if (existing == null)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Description = $@"Unable to locate the user to update.
                                         Failed to locate user with the Id of {user.Id}"
                    });
                }
                else
                {
                    // update all properties except Id
                    existing.UserName = user.UserName;
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
                    existing.Documents = user.Documents;
                    existing.Projects = user.Projects;
                    existing.Organizations = user.Organizations;
                    return IdentityResult.Success;
                }
            });
        }
        public ApplicationUser Get(Func<ApplicationUser, bool> match) => WithLock(() => users.FirstOrDefault(match));

        private readonly List<ApplicationUser> users;

        #region Synchronization
        private static readonly object Lock = new object();

        private T WithLock<T>(Func<T> f) { lock (Lock) return f(); }

        #endregion
    }
}