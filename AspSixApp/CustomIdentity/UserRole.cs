using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace AspSixApp.CustomIdentity
{
    public class UserRole : IdentityUserRole
    {

        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }

        [Activate]
        private static ILookupNormalizer LookupNormalizer { get; set; }
    }
}