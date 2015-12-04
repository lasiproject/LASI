using System;
using System.Collections.Generic;
using LASI.WebApp.Models;
using Microsoft.AspNet.Identity;

namespace LASI.WebApp.Persistence
{
    public interface IRoleAccessor<TRole> : IEnumerable<TRole> where TRole : class
    {
        void RemoveFromRole(ApplicationUser user, string roleName);
        TRole Get(Func<TRole, bool> match);
        IdentityResult Add(TRole component);
        IdentityResult Update(TRole component);
        IdentityResult Delete(TRole component);
    }
}