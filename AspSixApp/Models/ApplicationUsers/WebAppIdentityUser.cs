using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspSixApp.Models.ApplicationUsers
{
    public abstract class WebAppIdentityUser<TKey> : Microsoft.AspNet.Identity.IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public TKey _id {
            get { return base.Id; }
            set { base.Id = value; }
        }
        protected WebAppIdentityUser() { }

        protected WebAppIdentityUser(string userName) : base(userName) { }
    }
}