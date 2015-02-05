using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.ConfigurationModel;
using MongoDB.Driver;

namespace AspSixApp
{
    public class RoleStore<TUser> : IRoleStore<TUser> where TUser : IdentityUserRole<string>
    {
        private const string UserRolesCollectionName = "UserRolesCollectionName";

        public RoleStore() {
            Configuration = new Configuration();
            Configuration.AddJsonFile("resources.json");
            var db = new AspSixApp.MongoDbProvider(AppDomain.CurrentDomain.BaseDirectory, Configuration).MongoDatabase;
            roles = db.GetCollection<TUser>(Configuration[UserRolesCollectionName]);
        }
        //public async Task AddToRoleAsync(TUser user, string roleName, CancellationToken cancellationToken = default(CancellationToken)) {
        //    await DoOrThrowIfCancelled(() => user.Roles.Add(new IdentityUserRole<string> { RoleId = roleName, UserId = user.Id }), cancellationToken);
        //}

        public async Task CreateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            //await DoOrThrowIfCancelled((, cancellationToken);
            await Task.Yield();
        }

        public Task DeleteAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        private MongoCollection<TUser> roles;

        public Configuration Configuration { get; }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).          
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources. 
        // ~UserRoleStore() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        #region Helpers
        private Task DoOrThrowIfCancelled(Action action, CancellationToken cancellationToken) {
            return Task.Run(action, cancellationToken);
        }
        private Task<T> YieldOrThrowIfCancelled<T>(Func<T> valueFactory, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(valueFactory());
        }

        public Task<string> GetRoleIdAsync(TUser role, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(TUser role, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(TUser role, string roleName, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }
        #endregion
    }
}