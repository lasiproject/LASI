using System.Collections;
using System.Collections.Generic;

namespace AspSixApp.CustomIdentity
{
    public abstract class UserProvider<TUser, TKey> : IEnumerable<TUser> where TUser : class
    {
        public abstract Microsoft.AspNet.Identity.IdentityResult Delete(TUser user);
        public abstract Microsoft.AspNet.Identity.IdentityResult Add(TUser user);
        public abstract Microsoft.AspNet.Identity.IdentityResult Update(TUser user);

        public abstract IEnumerator<TUser> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public abstract TUser this[string id] { get; }

    }
}