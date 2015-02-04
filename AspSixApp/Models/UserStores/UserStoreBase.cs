using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspSixApp.Models.UserStores
{
    using AspSixApp.Models.ApplicationUsers;
    using Cancellation = CancellationToken;

    //public abstract class UserStore<TUser, TKey> : IUserStore<TUser> where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    //{
    //    protected abstract MongoDbAccountProvider PersistenceLayer { get; set; }

    //}
}