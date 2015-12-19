using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LASI.WebApp.Models;
using LASI.WebApp.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.OptionsModel;

namespace LASI.WebApp.Tests.Mocks
{
    class MockUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, UserRole>
    {
        public MockUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<UserRole> roleManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor) { }
        public override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user) => Task.FromResult(new ClaimsPrincipal(user.Claims.Select(claim => claim.Subject)));

    }
}
