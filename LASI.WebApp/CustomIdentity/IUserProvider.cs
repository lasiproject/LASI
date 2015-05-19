using System;
using Microsoft.AspNet.Identity;

namespace LASI.WebApp.CustomIdentity
{
    public interface IUserProvider<TUser> : System.Collections.Generic.IEnumerable<TUser> where TUser : class
    {
        TUser Get(Func<TUser, bool> match);
        IdentityResult Add(TUser component);
        IdentityResult Update(TUser component);
        IdentityResult Delete(TUser component);
    }
}