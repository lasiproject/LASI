using System.Collections.Generic;
using AspSixApp;

namespace MongoDB.AspNet.Identity
{
    internal class RoleManager<T>
    {
        private IEnumerable<RoleValidator<IdentityUser>> enumerable;
        private UserStore<IdentityUser> userStore;

        public RoleManager(UserStore<IdentityUser> userStore, IEnumerable<RoleValidator<IdentityUser>> enumerable) {
            this.userStore = userStore;
            this.enumerable = enumerable;
        }
    }
}