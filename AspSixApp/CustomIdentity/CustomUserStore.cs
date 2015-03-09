using AspSixApp.Models;
namespace AspSixApp.CustomIdentity
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using User = ApplicationUser;
    using CancellationToken = System.Threading.CancellationToken;
    using System.Linq;
    using LASI.Utilities.Specialized.Enhanced.IList.Linq;
    using LASI.Utilities;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Identity;

    public class CustomUserStore<TRole> :
        IUserStore<User>,
        IUserRoleStore<User>,
        IRoleStore<TRole>,
        IUserPasswordStore<User>,
        IUserEmailStore<User> where TRole : UserRole, new()
    {
        [Activate]
        private ILookupNormalizer lookupNormalizer { get; set; }

        public CustomUserStore(IUserProvider<User> userProvider, IRoleProvider<TRole> roleProvider)
        {
            this.userProvider = userProvider;
            this.roleProvider = roleProvider;
        }
        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellation = default(CancellationToken))
        {
            if (!await IsInRoleAsync(user, roleName))
            {
                roleProvider.Add(new TRole
                {
                    UserId = user.Id,
                    RoleName = roleName
                });
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => userProvider.Add(user), cancellation);
        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => userProvider.Delete(user), cancellation);

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => userProvider[userId], cancellation);

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => userProvider.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName), cancellation);

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.NormalizedUserName, cancellation);

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => new List<string>(
                from role in roleProvider
                where role.UserId == user.Id
                select role.RoleId), cancellation);
        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.Id, cancellation);

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.UserName, cancellation);

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => (from user in userProvider
                                      join role in from role in roleProvider
                                                   where role.RoleName == roleName
                                                   select role
                                      on user.Id equals role.UserId
                                      select user).ToList(), cancellation);


        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => roleProvider.Any(role => role.RoleName == roleName && role.UserId == user.Id), cancellation);

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellation = default(CancellationToken))
        {
            await ExecuteAsync(() => roleProvider.Remove(user, roleName), cancellation);
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsync(() => user.NormalizedUserName = normalizedName, cancellation);
        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsync(() => user.UserName = userName, cancellation);
        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => userProvider.Update(user), cancellation);

        private IRoleProvider<TRole> roleProvider;

        private IUserProvider<User> userProvider;


        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => roleProvider.Add(role), cancellation);

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => roleProvider.Update(role), cancellation);

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => roleProvider.Delete(role), cancellation);

        public async Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => role.RoleId, cancellation);

        public async Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => role.RoleName, cancellation);
        public async Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsync(() => role.RoleName = roleName, cancellation);

        public async Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => role.NormalizedRoleName, cancellation);
        public async Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => role.NormalizedRoleName = normalizedName, cancellation);
        async Task<TRole> IRoleStore<TRole>.FindByIdAsync(string roleId, CancellationToken cancellation) =>
            await ExecuteAsnyc(() => roleProvider.FirstOrDefault(role => role.RoleId == roleId), cancellation);
        async Task<TRole> IRoleStore<TRole>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellation) =>
            await ExecuteAsnyc(() => roleProvider.FirstOrDefault(role => role.NormalizedRoleName == normalizedRoleName), cancellation);

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.PasswordHash = passwordHash, cancellation);


        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.PasswordHash, cancellation);

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.PasswordHash != null, cancellation);

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.Email = email, cancellation);


        public async Task<string> GetEmailAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.Email, cancellation);

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.EmailConfirmed, cancellation);

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => user.EmailConfirmed = confirmed, cancellation);

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellation = default(CancellationToken)) => await ExecuteAsnyc(() => userProvider.FirstOrDefault(user => user.NormalizedEmail == normalizedEmail), cancellation);


        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellation = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.NormalizedEmail, cancellation);

        public async Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellation = default(CancellationToken)) =>
            await cancellation.IfNotCancelled(() => user.NormalizedEmail = normalizedEmail);

        #region Helpers
        private async Task<T> ExecuteAsnyc<T>(Func<T> function, CancellationToken cancellation) => await cancellation.IfNotCancelled(function);
        private static async Task ExecuteAsync(Action function, CancellationToken cancellation) => await cancellation.IfNotCancelled(function);


        #endregion Helpers
        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    disposedValue = true;
                }
            }
        }
        public void Dispose() { Dispose(true); }

        #endregion IDisposable Support

    }
}