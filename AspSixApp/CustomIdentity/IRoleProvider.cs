using System;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    public interface IRoleProvider<TRole> : IEnumerable<TRole> where TRole : class
    {
        TRole Get(Func<TRole, bool> match);
        IdentityResult Add(TRole role);
        IdentityResult Delete(TRole role);
        void Remove(ApplicationUser user, string roleName);
        IdentityResult Update(TRole role);
    }
}