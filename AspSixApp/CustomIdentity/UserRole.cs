using System;
using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    public class UserRole : IdentityUserRole
    {
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }

    }
}