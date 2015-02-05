using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspSixApp.Models.ApplicationUsers
{
    public abstract class WebAppIdentityUserBase<TKey> : Microsoft.AspNet.Identity.IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        protected WebAppIdentityUserBase() { }

        protected WebAppIdentityUserBase(string userName) : base(userName) { }
    }
}