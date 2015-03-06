namespace AspSixApp.CustomIdentity
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Cancellation = System.Threading.CancellationToken;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using System.Collections;
    using AspSixApp.Models;

    public abstract class RoleProvider<TRole> : IEnumerable<TRole> where TRole : class
    {
        public abstract IEnumerator<TRole> GetEnumerator(); 

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        internal abstract void Remove(ApplicationUser user,string roleName);
        internal abstract IdentityResult Add(TRole role);
        internal abstract IdentityResult Update(TRole role);
        internal abstract IdentityResult Delete(TRole role);
    }
}