using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspSixApp.Models.ApplicationUsers
{
    using User = FileSystemUser;
    using Cancellation = CancellationToken;
    public class FileSystemUserStore : IUserStore<User>
    {
        private FileSystemAccountProvider dataLayer;

        public FileSystemUserStore() {
            dataLayer = new FileSystemAccountProvider(AppDomain.CurrentDomain.BaseDirectory);
        }
        public Task CreateAsync(User user, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string normalizedUserName, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(User user, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user, Cancellation cancellation = default(Cancellation)) {
            throw new NotImplementedException();
        }
    }
}