using System;
using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    public interface IRoleProvider<TRole> : IEnumerable<TRole> where TRole : class
    {
        void RemoveFromRole(ApplicationUser user, string roleName);
        TRole Get(Func<TRole, bool> match);
        IdentityResult Add(TRole component);
        IdentityResult Update(TRole component);
        IdentityResult Delete(TRole component);
    }
}