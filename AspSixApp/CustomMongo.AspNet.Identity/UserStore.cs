using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using LASI.Utilities;
using LASI.Utilities.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.ConfigurationModel;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDB.AspNet.Identity
{
    /// <summary>
    ///     Class UserStore.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    public class UserStore<TUser> : IUserStore<TUser>, IUserLoginStore<TUser>,/* IUserClaimStore<TUser>,*/
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>, IUserSecurityStampStore<TUser>
        where TUser : IdentityUser<string>
    {
        #region Private Methods & Variables

        /// <summary>
        ///     The database
        /// </summary>
        //private readonly MongoDatabase db;
        private readonly MongoCollection<TUser> users;
        /// <summary>
        ///     The _disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// The AspNetUsers collection name
        /// </summary>
        private const string collectionName = "AspNetUsers";

        public Configuration Configuration { get; private set; }


        /// <summary>
        ///     Gets the database from connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>MongoDatabase.</returns>
        /// <exception cref="System.Exception">No database name specified in connection string</exception>
        private MongoDatabase GetDatabaseFromSqlStyle(string connectionString) {
            var conString = new MongoConnectionStringBuilder(connectionString);
            MongoClientSettings settings = MongoClientSettings.FromConnectionStringBuilder(conString);
            MongoServer server = new MongoClient(settings).GetServer();
            if (conString.DatabaseName == null) {
                throw new Exception("No database name specified in connection string");
            }
            return server.GetDatabase(conString.DatabaseName);
        }

        /// <summary>
        ///     Gets the database from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>MongoDatabase.</returns>
        private MongoDatabase GetDatabaseFromUrl(MongoUrl url) {
            var client = new MongoClient(url);
            MongoServer server = client.GetServer();
            if (url.DatabaseName == null) {
                throw new Exception("No database name specified in connection string");
            }
            return server.GetDatabase(url.DatabaseName); // WriteConcern defaulted to Acknowledged
        }

        /// <summary>
        ///     Uses connectionString to connect to server and then uses databae name specified.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="dbName">Name of the database.</param>
        /// <returns>MongoDatabase.</returns>
        private MongoDatabase GetDatabase(string connectionString, string dbName) {
            var client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            return server.GetDatabase(dbName);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserStore{TUser}" /> class. Uses DefaultConnection name if none was
        ///     specified.
        /// </summary>
        public UserStore() {
            Configuration = new Configuration();
            Configuration.AddJsonFile("resources.json");
            var db = new AspSixApp.MongoDbProvider(AppDomain.CurrentDomain.BaseDirectory, Configuration).MongoDatabase;
            users = db.GetCollection<TUser>(Configuration["UserCollectionName"]);
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="UserStore{TUser}"/> class using a already initialized Mongo Database.
        ///// </summary>
        ///// <param name="mongoDatabase">The mongo database.</param>
        //public UserStore(MongoDatabase mongoDatabase) {
        //    Configuration = new Configuration(); Configuration.AddJsonFile("resources.json");
        //    var db = mongoDatabase; users = db.GetCollection<TUser>(Configuration["UserCollectionName"]);
        //}

        #endregion

        #region Methods

        /// <summary>
        ///     Adds the claim asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claim">The claim.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task AddClaimAsync(TUser user, Claim claim, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            if (!user.Claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value)) {
                user.Claims.Add(new IdentityUserClaim
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                });
            }


            return Task.FromResult(0);
        }

        ///// <summary>
        /////     Gets the claims asynchronous.
        ///// </summary>
        ///// <param name="user">The user.</param>
        ///// <returns>Task{IList{Claim}}.</returns>
        ///// <exception cref="System.ArgumentNullException">user</exception>
        //public Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
        //    ThrowIfDisposed();
        //    if (user == null)
        //        throw new ArgumentNullException("user");

        //    IList<Claim> result = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        //    return Task.FromResult(result);
        //}

        ///// <summary>
        /////     Removes the claim asynchronous.
        ///// </summary>
        ///// <param name="user">The user.</param>
        ///// <param name="claim">The claim.</param>
        ///// <returns>Task.</returns>
        ///// <exception cref="System.ArgumentNullException">user</exception>
        //public Task RemoveClaimAsync(TUser user, Claim claim, CancellationToken cancellationToken = default(CancellationToken)) {
        //    ThrowIfDisposed();
        //    if (user == null)
        //        throw new ArgumentNullException("user");
        //    //foreach (var claim in from  claim in user.Claims where claim 
        //    user.Claims.Remove(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
        //    return Task.FromResult(0);
        //}


        /// <summary>
        ///     Creates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task CreateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            users.Insert(user);

            return Task.FromResult(user);
        }

        /// <summary>
        ///     Deletes the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task DeleteAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            users.Remove((Query.EQ("_id", ObjectId.Parse(user.Id))));
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task{`0}.</returns>
        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            TUser user = users.FindOne((Query.EQ("_id", ObjectId.Parse(userId))));
            return Task.FromResult(user);
        }

        /// <summary>
        ///     Finds the by name asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task{`0}.</returns>
        public Task<TUser> FindByNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();

            TUser user = users.FindOne((Query.EQ("UserName", userName)));
            return Task.FromResult(user);
        }

        /// <summary>
        ///     Updates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task UpdateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            users.Update(Query.EQ("_id", ObjectId.Parse(user.Id)), Update.Replace(user), UpdateFlags.Upsert);

            return Task.FromResult(user);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            _disposed = true;
        }

        /// <summary>
        ///     Adds the login asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The login.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task AddLoginAsync(TUser user, IdentityUserLogin<string> login, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            if (!user.Logins.Any(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey)) {
                user.Logins.Add(login);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Finds the user asynchronous.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>Task{`0}.</returns>
        public Task<TUser> FindAsync(UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken)) {
            TUser user = null;
            user = users.FindOne(Query.And(Query.EQ("Logins.LoginProvider", login.LoginProvider),
                        Query.EQ("Logins.ProviderKey", login.ProviderKey)));

            return Task.FromResult(user);
        }

        /// <summary>
        ///     Gets the logins asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task{IList{UserLoginInfo}}.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.Logins.ToList() as IList<UserLoginInfo>);
        }

        /// <summary>
        ///     Removes the login asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The login.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");
            user.Logins.Where(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey)
                .ToList().ForEach(l => user.Logins.Remove(l));
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Gets the password hash asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task{System.String}.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        ///     Determines whether [has password asynchronous] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.PasswordHash != null);
        }

        /// <summary>
        ///     Sets the password hash asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Adds to role asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="role">The role.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task AddToRoleAsync(TUser user, string role, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            Validator.ThrowIfNull(user, nameof(user));

            if (!user.Roles.Any(r => r.RoleId.EqualsIgnoreCase(role) && r.UserId == user.Id))
                user.Roles.Add(new IdentityUserRole<string> { RoleId = role, UserId = user.Id });

            return Task.FromResult(true);
        }

        /// <summary>
        ///     Gets the roles asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task{IList{System.String}}.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            Validator.ThrowIfNull(user, nameof(user));
            return Task.FromResult((IList<string>)user.Roles);
        }

        /// <summary>
        ///     Determines whether [is in role asynchronous] [the specified user].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="role">The role.</param>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task<bool> IsInRoleAsync(TUser user, string role, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            Validator.ThrowIfNull(user, nameof(user));

            return Task.Run(() => user.Roles.Any(r => r.RoleId.EqualsIgnoreCase(role) && r.UserId == user.Id), cancellationToken);
        }

        /// <summary>
        ///     Removes from role asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="role">The role.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task RemoveFromRoleAsync(TUser user, string role, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            Validator.ThrowIfNull(user, nameof(user));
            return DoOrThrowIfCancelledOrDisposed(() =>
            user.Roles.Where(r => r.RoleId == role)
                .ToList()
                .ForEach(r => user.Roles.Remove(r)),
                cancellationToken);
        }

        /// <summary>
        ///     Gets the security stamp asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task{System.String}.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task<string> GetSecurityStampAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>
        ///     Sets the security stamp asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="stamp">The stamp.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        public Task SetSecurityStampAsync(TUser user, string stamp, CancellationToken cancellationToken = default(CancellationToken)) {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Throws if disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException"></exception>
        private void ThrowIfDisposed() {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken)) {
            return DoOrThrowIfCancelledOrDisposed(() =>
                user.Logins
                    .Where(login => login.LoginProvider == loginProvider && login.ProviderKey == providerKey)
                    .ToList()
                    .ForEach(login => user.Logins.Remove(login)),
            cancellationToken);
        }

        public Task<TUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => users.FindOne(Query.And(Query.EQ("Logins.LoginProvider", loginProvider), Query.EQ("Logins.ProviderKey", providerKey))), cancellationToken);
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            return YieldOrThrowIfCancelledOrDisposed(() => user.Id, cancellationToken);
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            return YieldOrThrowIfCancelledOrDisposed(() => user.UserName, cancellationToken);
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken = default(CancellationToken)) {
            return DoOrThrowIfCancelledOrDisposed(() => user.UserName = userName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken)) {
            return YieldOrThrowIfCancelledOrDisposed(() => user.UserName.Normalize(), cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken)) {
            return DoOrThrowIfCancelledOrDisposed(() => user.UserName = normalizedName, cancellationToken);
        }

        public Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => user.Logins.Add(new IdentityUserLogin<string>
            {
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
                ProviderKey = login.ProviderKey,
                UserId = user.Id
            }), cancellationToken);
        }
        //public Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken)) {
        //    return DoOrThrowIfCancelledOrDisposed(() => user.Claims.AddRange(claims.Select(claim => new IdentityUserClaim(claim.Type, claim.Value))), cancellationToken);
        //}

        //public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken)) {
        //    throw new NotImplementedException();
        //}

        //public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken)) {
        //    throw new NotImplementedException();
        //}
        private Task DoOrThrowIfCancelledOrDisposed(Action action, CancellationToken cancellationToken) {
            ThrowIfDisposed();
            return Task.Run(action, cancellationToken);
        }
        private Task<T> YieldOrThrowIfCancelledOrDisposed<T>(Func<T> valueFactory, CancellationToken cancellationToken) {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(valueFactory());
        }

        #endregion
    }
}
