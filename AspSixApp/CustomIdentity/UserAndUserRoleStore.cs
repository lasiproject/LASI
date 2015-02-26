using AspSixApp.Models;
namespace AspSixApp.CustomIdentity
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using User = ApplicationUser;
    using Cancellation = System.Threading.CancellationToken;
    using System.Linq;
    using LASI.Utilities.Specialized.Enhanced.IList.Linq;
    using LASI.Utilities;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Identity;

    public class UserAndUserRoleStore<TRole> : IUserStore<User>, IUserRoleStore<User>, IRoleStore<TRole> where TRole : UserRole, new()
    {
        public UserAndUserRoleStore([Activate]UserProvider<User, string> userProvider, [Activate] RoleProvider<TRole> roleProvider)
        {
            this.userProvider = userProvider;
            this.roleProvider = roleProvider;
        }
        public async Task AddToRoleAsync(User user, string roleName, Cancellation t = default(Cancellation))
        {
            if (!await IsInRoleAsync(user, roleName))
            {
                roleProvider.Add(new TRole
                {
                    UserId = user.Id,
                    RoleName = roleName,
                    NormalizedRoleName = roleName
                });
            }
        }

        public async Task<IdentityResult> CreateAsync(User user, Cancellation t = default(Cancellation)) => await Asnyc(() => userProvider.Add(user), t);
        public async Task<IdentityResult> DeleteAsync(User user, Cancellation t = default(Cancellation)) => await Asnyc(() => userProvider.Delete(user), t);

        public async Task<User> FindByIdAsync(string userId, Cancellation t = default(Cancellation)) => await Asnyc(() => userProvider[userId], t);

        public async Task<User> FindByNameAsync(string normalizedUserName, Cancellation t = default(Cancellation)) =>
            await Asnyc(() => userProvider.FirstOrDefault(u => u.NormalizedUserName == normalizedUserName), t);

        public async Task<string> GetNormalizedUserNameAsync(User user, Cancellation t = default(Cancellation)) => await Asnyc(() => user.NormalizedUserName, t);

        public async Task<IList<string>> GetRolesAsync(User user, Cancellation t = default(Cancellation)) => await Asnyc(() => new List<string>(
                                                                                                                            from role in roleProvider
                                                                                                                            where role.UserId == user.Id
                                                                                                                            select role.RoleId), t);
        public async Task<string> GetUserIdAsync(User user, Cancellation t = default(Cancellation)) => await Asnyc(() => user.Id, t);

        public async Task<string> GetUserNameAsync(User user, Cancellation t = default(Cancellation)) => await Asnyc(() => user.UserName, t);

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, Cancellation t = default(Cancellation)) => await Asnyc(() => (from user in userProvider
                                                                                                                                          join role in from role in roleProvider
                                                                                                                                                       where role.RoleName == roleName
                                                                                                                                                       select role
                                                                                                                                          on user.Id equals role.UserId
                                                                                                                                          select user).ToList(), t);


        public async Task<bool> IsInRoleAsync(User user, string roleName, Cancellation t = default(Cancellation)) => await Asnyc(() => roleProvider.Any(role => role.RoleName == roleName && role.UserId == user.Id), t);

        public async Task RemoveFromRoleAsync(User user, string roleName, Cancellation t = default(Cancellation))
        {
            //var index = roleProvider.IndexOf(roleProvider.Where(role => role.RoleName == roleName && role.UserId == user.Id).FirstOrDefault());
            await Async(() => roleProvider.Remove(user), t);
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, Cancellation t = default(Cancellation)) => await Async(() => user.NormalizedUserName = normalizedName, t);
        public async Task SetUserNameAsync(User user, string userName, Cancellation t = default(Cancellation)) => await Async(() => user.UserName = userName, t);
        public async Task<IdentityResult> UpdateAsync(User user, Cancellation t = default(Cancellation)) => await Asnyc(() => userProvider.Update(user), t);

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls
        private RoleProvider<TRole> roleProvider;

        private UserProvider<User, string> userProvider;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).          
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources. 
        // ~RoleManager() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support

        public async Task<IdentityResult> CreateAsync(TRole role, Cancellation t = default(Cancellation)) => await Asnyc(() => roleProvider.Add(role), t);

        public async Task<IdentityResult> UpdateAsync(TRole role, Cancellation t = default(Cancellation)) => await Asnyc(() => roleProvider.Update(role), t);

        public async Task<IdentityResult> DeleteAsync(TRole role, Cancellation t = default(Cancellation)) => await Asnyc(() => roleProvider.Delete(role), t);

        public async Task<string> GetRoleIdAsync(TRole role, Cancellation t = default(Cancellation)) => await Asnyc(() => role.RoleId, t);

        public async Task<string> GetRoleNameAsync(TRole role, Cancellation t = default(Cancellation)) => await Asnyc(() => role.RoleName, t);
        public async Task SetRoleNameAsync(TRole role, string roleName, Cancellation t = default(Cancellation)) => await Async(() => role.RoleName = roleName, t);

        public async Task<string> GetNormalizedRoleNameAsync(TRole role, Cancellation t = default(Cancellation)) => await Asnyc(() => role.NormalizedRoleName, t);
        public async Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, Cancellation t = default(Cancellation)) => await Asnyc(() => role.NormalizedRoleName = normalizedName, t);
        async Task<TRole> IRoleStore<TRole>.FindByIdAsync(string roleId, Cancellation t) => await Asnyc(() => roleProvider.FirstOrDefault(role => role.RoleId == roleId), t);
        async Task<TRole> IRoleStore<TRole>.FindByNameAsync(string normalizedRoleName, Cancellation t) => await Asnyc(() => roleProvider.FirstOrDefault(role => role.NormalizedRoleName == normalizedRoleName), t);

        #region Helpers
        private async Task<T> Asnyc<T>(Func<T> function, Cancellation t)
        {
            t.ThrowIfCancellationRequested();
            return await Task.FromResult(default(T));
        }

        private async Task Async(Action function, Cancellation t)
        {
            t.ThrowIfCancellationRequested();
            await Task.Yield();
        }

        #endregion Helpers

    }
}