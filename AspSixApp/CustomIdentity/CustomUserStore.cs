using AspSixApp.Models;
namespace AspSixApp.CustomIdentity
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using User = ApplicationUser;
    using CancellationToken = System.Threading.CancellationToken;
    using System.Linq;
    using LASI.Utilities.Specialized.Enhanced.IList.Linq;
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
        private ILookupNormalizer LookupNormalizer { get; set; }

        public CustomUserStore(IUserProvider<User> userProvider, IRoleProvider<TRole> roleProvider)
        {
            this.userProvider = userProvider;
            this.roleProvider = roleProvider;
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken))
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

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => userProvider.Add(user), cancellationToken);

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => userProvider.Delete(user), cancellationToken);

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => userProvider.Get(u => u.Id == userId), cancellationToken);

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => userProvider.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName), cancellationToken);

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.NormalizedUserName, cancellationToken);

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => new List<string>(
                from role in roleProvider
                where role.UserId == user.Id
                select role.RoleId), cancellationToken);

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.Id, cancellationToken);

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.UserName, cancellationToken);

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => (from user in userProvider
                                      join role in from role in roleProvider
                                                   where role.RoleName == roleName
                                                   select role
                                      on user.Id equals role.UserId
                                      select user).ToList(), cancellationToken);

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => roleProvider.Any(role => role.RoleName == roleName && role.UserId == user.Id), cancellationToken);

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsync(() => roleProvider.RemoveFromRole(user, roleName), cancellationToken);

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsync(() => user.NormalizedUserName = normalizedName, cancellationToken);

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsync(() => user.UserName = userName, cancellationToken);

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => userProvider.Update(user), cancellationToken);

        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => roleProvider.Add(role), cancellationToken);

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => roleProvider.Update(role), cancellationToken);

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => roleProvider.Delete(role), cancellationToken);

        public async Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => role.RoleId, cancellationToken);

        public async Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => role.RoleName, cancellationToken);

        public async Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsync(() => role.RoleName = roleName, cancellationToken);

        public async Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => role.NormalizedRoleName, cancellationToken);

        public async Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => role.NormalizedRoleName = normalizedName, cancellationToken);

        async Task<TRole> IRoleStore<TRole>.FindByIdAsync(string roleId, CancellationToken cancellationToken) =>
            await ExecuteAsnyc(() => roleProvider.FirstOrDefault(role => role.RoleId == roleId), cancellationToken);

        async Task<TRole> IRoleStore<TRole>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken) =>
            await ExecuteAsnyc(() => roleProvider.FirstOrDefault(role => role.NormalizedRoleName == normalizedRoleName), cancellationToken);

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.PasswordHash = passwordHash, cancellationToken);


        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.PasswordHash, cancellationToken);

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.PasswordHash != null, cancellationToken);

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.Email = email, cancellationToken);


        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.Email, cancellationToken);

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.EmailConfirmed, cancellationToken);

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.EmailConfirmed = confirmed, cancellationToken);

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => userProvider.FirstOrDefault(user => user.NormalizedEmail == normalizedEmail), cancellationToken);


        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default(CancellationToken)) =>
            await ExecuteAsnyc(() => user.NormalizedEmail, cancellationToken);

        public async Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken)) =>
            await cancellationToken.IfNotCancelled(() => user.NormalizedEmail = normalizedEmail);

        private IRoleProvider<TRole> roleProvider;

        private IUserProvider<User> userProvider;

        #region Helpers

        private async Task<T> ExecuteAsnyc<T>(Func<T> function, CancellationToken cancellationToken) => await cancellationToken.IfNotCancelled(function);
        private static async Task ExecuteAsync(Action function, CancellationToken cancellationToken) => await cancellationToken.IfNotCancelled(function);

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