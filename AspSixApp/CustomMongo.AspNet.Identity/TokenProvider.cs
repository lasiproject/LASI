using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;

namespace AspSixApp
{
    internal class TokenProvider<T> : IUserTokenProvider<MongoDB.AspNet.Identity.IdentityUser>
    {
        public string Name {
            get {
                throw new NotImplementedException();
            }
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<MongoDB.AspNet.Identity.IdentityUser> manager, MongoDB.AspNet.Identity.IdentityUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<string> GenerateAsync(string purpose, UserManager<MongoDB.AspNet.Identity.IdentityUser> manager, MongoDB.AspNet.Identity.IdentityUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(string token, UserManager<MongoDB.AspNet.Identity.IdentityUser> manager, MongoDB.AspNet.Identity.IdentityUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<MongoDB.AspNet.Identity.IdentityUser> manager, MongoDB.AspNet.Identity.IdentityUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            throw new NotImplementedException();
        }
    }
}