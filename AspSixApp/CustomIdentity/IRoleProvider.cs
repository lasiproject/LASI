using System.Collections.Generic;
using AspSixApp.Models;
using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    public interface IRoleProvider<TRole> : IEnumerable<TRole> where TRole : class
    {
        IdentityResult Add(TRole role);
        IdentityResult Delete(TRole role);
        void Remove(ApplicationUser user, string roleName);
        IdentityResult Update(TRole role);
    }
}