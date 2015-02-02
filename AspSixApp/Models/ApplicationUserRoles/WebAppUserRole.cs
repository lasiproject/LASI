using System;

namespace AspSixApp.Models.ApplicationUserRoles
{
    public class WebAppUserRole : Microsoft.AspNet.Identity.IdentityRole<RoleKind>
    {
        public WebAppUserRole(string roleName) : base(roleName) { }
    }
}