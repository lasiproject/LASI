using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspSixApp.Models.ApplicationUsers
{
    using User = MongoUser;
    using Cancellation = CancellationToken;
    public class MongoDbUserStore : IUserStore<User>
    {
        MongoDbAccountProvider PersistenceLayer {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public MongoDbUserStore() {
            PersistenceLayer = new MongoDbAccountProvider(AppDomain.CurrentDomain.BaseDirectory);
        }
        //public MongoDbUserStore(MongoDbAccountProvider persistenceLayer) {
        //    this.persistenceLayer = persistenceLayer;
        //}


        public Task CreateAsync(User user, Cancellation cancellation = default(Cancellation)) {
            return Task.Run(() => PersistenceLayer.InsertAsync(user), cancellation);
        }

        public Task DeleteAsync(User user, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string userId, Cancellation cancellation = default(Cancellation)) {
            return await PersistenceLayer.FindSingleAsync(u => u.Id == MongoDB.Bson.ObjectId.Parse(userId), cancellation);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, Cancellation cancellation = default(Cancellation)) {
            return await PersistenceLayer.FindSingleAsync(u => u.NormalizedUserName == normalizedUserName, cancellation);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, Cancellation cancellation = default(Cancellation)) {
            return await YieldOrThrowCancellation(() => user.NormalizedUserName, cancellation);
        }

        public async Task<string> GetUserIdAsync(User user, Cancellation cancellation = default(Cancellation)) {
            return await YieldOrThrowCancellation(() => user.Id.ToString(), cancellation);
        }

        public async Task<string> GetUserNameAsync(User user, Cancellation cancellation = default(Cancellation)) {
            return await YieldOrThrowCancellation(() => user.UserName, cancellation);
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, Cancellation cancellation = default(Cancellation)) {
            await DoOrThrowCancellation(() => user.NormalizedUserName = normalizedName, cancellation);
        }

        public async Task SetUserNameAsync(User user, string userName, Cancellation cancellation = default(Cancellation)) {
            await DoOrThrowCancellation(() => user.UserName = userName, cancellation);
        }

        public Task UpdateAsync(User user, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        protected Task DoOrThrowCancellation(Action action, CancellationToken cancellation) {
            return Task.Run(action, cancellation);
        }
        protected Task<T> YieldOrThrowCancellation<T>(Func<T> valueFactory, CancellationToken cancellation) {
            cancellation.ThrowIfCancellationRequested();
            return Task.FromResult(valueFactory());
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    PersistenceLayer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        ~MongoDbUserStore() {
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}