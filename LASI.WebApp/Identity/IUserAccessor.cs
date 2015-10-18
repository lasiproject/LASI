using System;
using Microsoft.AspNet.Identity;

namespace LASI.WebApp.Persistence
{
    public interface IUserAccessor<TUser> : System.Collections.Generic.IEnumerable<TUser> where TUser : class
    {
        /// <summary>
        /// Returns the first user matching the specified predicate or <c>null</c> if no matching user is found.
        /// </summary>
        /// <param name="match">The predicate specifying the how to retrieve the user.</param>
        /// <returns>The first user matching the specified predicate or <c>null</c> if no matching user is found.</returns>
        TUser Get(Func<TUser, bool> match);
        IdentityResult Add(TUser component);
        IdentityResult Update(TUser component);
        IdentityResult Delete(TUser component);
    }
}