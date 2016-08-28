using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using LASI.WebApp.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using LASI.WebApp.Models;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace LASI.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        public AuthenticateController(TokenAuthorizationOptions tokenAuthorizationOptions, System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler jwtSecurityTokenHandler, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.tokenAuthorizationOptions = tokenAuthorizationOptions;
            this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        // Authorization failing
        [HttpGet]
        [Authorize("Bearer")]
        public async Task<dynamic> Get()
        {
            var user = await userManager.FindByEmailAsync((
                from claim in User.Claims
                where claim.Properties.Values.Contains("unique_name")
                select claim.Value
            ).DefaultIfEmpty("").First());

            if (user != null)
            {
                return new
                {
                    User = UserResponse.FromApplicatinUser(user),
                    Authenticated = true
                };
            }
            return HttpUnauthorized();
        }

        [HttpPost]
        public async Task<dynamic> Post(Credentials body)
        {
            var signInResult = await signInManager.PasswordSignInAsync(
                body.Email,
                body.Password,
                isPersistent: true,
                lockoutOnFailure: false
            );

            var user = await userManager.FindByEmailAsync(body.Email);
            if (signInResult.Succeeded)
            {
                var renderedToken = GenerateJwtToken();
                return new
                {
                    Authenticated = true,
                    User = UserResponse.FromApplicatinUser(user),
                    Token = renderedToken
                };
            }
            return new { Authenticated = false };
        }
        // Authorization failing
        [Authorize("Bearer")]
        [HttpPost("LogOff")]
        public async Task LogOff()
        {
            await signInManager.SignOutAsync();
        }

        private string GenerateJwtToken()
        {
            var utcDateTime = DateTimeOffset.UtcNow.UtcDateTime;
            var token = this.jwtSecurityTokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Issuer = this.tokenAuthorizationOptions.Issuer,
                Audience = this.tokenAuthorizationOptions.Audience,
                //Claims = User.Claims,
                SigningCredentials = this.tokenAuthorizationOptions.SigningCredentials,
                IssuedAt = utcDateTime,
                Expires = DateTimeOffset.UtcNow.UtcDateTime + TimeSpan.FromMinutes(60)
            });
            var renderedToken = jwtSecurityTokenHandler.WriteToken(token);
            return renderedToken;
        }

        private readonly TokenAuthorizationOptions tokenAuthorizationOptions;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public class Credentials
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool? RememberMe { get; set; }
        }
        public class UserResponse
        {
            public static UserResponse FromApplicatinUser(ApplicationUser user) => new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            public string Id { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
