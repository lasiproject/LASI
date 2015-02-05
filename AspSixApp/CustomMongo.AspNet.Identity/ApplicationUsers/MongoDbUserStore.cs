//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;

//namespace AspSixApp.Models.ApplicationUsers
//{
//    public class MongoDbUserStore : IUserStore<MongoUser>
//    {
//        private MongoDbAccountProvider persistenceLayer;

//        public MongoDbUserStore() {
//            persistenceLayer = new MongoDbAccountProvider(AppDomain.CurrentDomain.BaseDirectory);
//        }
//        public MongoDbUserStore(MongoDbAccountProvider persistenceLayer) {
//            this.persistenceLayer = persistenceLayer;
//        }

//        public Task CreateAsync(MongoUser user, CancellationToken cancellationToken = default(CancellationToken)) {
//            return Task.Run(() => persistenceLayer.Insert(user), cancellationToken);
//        }

//        public Task DeleteAsync(MongoUser user, CancellationToken cancellationToken = default(CancellationToken)) {
//            throw new NotImplementedException();
//        }

//        public async Task<MongoUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken)) {
//            return await persistenceLayer.FindSingleAsync(u => u.Id == MongoDB.Bson.ObjectId.Parse(userId), cancellationToken);
//        }

//        public async Task<MongoUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken)) {
//            return await persistenceLayer.FindSingleAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
//        }

//        public async Task<string> GetNormalizedUserNameAsync(MongoUser user, CancellationToken cancellationToken = default(CancellationToken)) {
//            return await YieldOrThrowCancellation(() => user.NormalizedUserName, cancellationToken);
//        }

//        public async Task<string> GetUserIdAsync(MongoUser user, CancellationToken cancellationToken = default(CancellationToken)) {
//            return await YieldOrThrowCancellation(() => user.Id.ToString(), cancellationToken);
//        }

//        public async Task<string> GetUserNameAsync(MongoUser user, CancellationToken cancellationToken = default(CancellationToken)) {
//            return await YieldOrThrowCancellation(() => user.UserName, cancellationToken);
//        }

//        public async Task SetNormalizedUserNameAsync(MongoUser user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken)) {
//            await DoOrThrowCancellation(() => user.NormalizedUserName = normalizedName, cancellationToken);
//        }

//        public async Task SetUserNameAsync(MongoUser user, string userName, CancellationToken cancellationToken = default(CancellationToken)) {
//            await DoOrThrowCancellation(() => user.UserName = userName, cancellationToken);
//        }

//        public Task UpdateAsync(MongoUser user, CancellationToken cancellationToken = default(CancellationToken)) {
//            throw new NotImplementedException();
//        }

//        #region IDisposable Support
//        private bool disposedValue = false; // To detect redundant calls

//        protected virtual void Dispose(bool disposing) {
//            if (!disposedValue) {
//                if (disposing) {
//                    persistenceLayer.Dispose();
//                }

//                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
//                // TODO: set large fields to null.

//                disposedValue = true;
//            }
//        }

//        ~MongoDbUserStore() {
//            Dispose(false);
//        }

//        // This code added to correctly implement the disposable pattern.
//        public void Dispose() {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        #endregion
//        private static Task DoOrThrowCancellation(Action action, CancellationToken cancellationToken) {
//            return Task.Run(action, cancellationToken);
//        }
//        private static Task<T> YieldOrThrowCancellation<T>(Func<T> valueFactory, CancellationToken cancellationToken) {
//            cancellationToken.ThrowIfCancellationRequested();
//            return Task.FromResult(valueFactory());
//        }
//    }
//}