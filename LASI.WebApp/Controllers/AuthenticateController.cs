using System;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Authorization;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNet.Authentication.JwtBearer;
using LASI.WebApp.Authorization;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using LASI.WebApp.Models;
using System.Linq;

namespace LASI.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        public AuthenticateController(TokenAuthorizationOptions tokenAuthorizationOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.tokenAuthorizationOptions = tokenAuthorizationOptions;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public async Task<dynamic> Get()
        {
            throw new NotImplementedException();
            //if (HttpContext.User?.Identity.IsAuthenticated ?? false)
            //{
            //    var user = await userManager.FindByIdAsync(User.GetUserId());
            //    return new
            //    {
            //        User = new
            //        {
            //            user.Email,
            //            Username = user.Email,
            //            user.FirstName,
            //            user.LastName,
            //            user.Id
            //        },
            //        Token = GenerateJwtToken()
            //    };
            //}
            //else return null;
        }

        [HttpPost]
        public async Task<dynamic> Post(Credentials body)
        {
            SignInResult signInResult = await signInManager.PasswordSignInAsync(body.Email, body.Password, isPersistent: true, lockoutOnFailure: false);
            ApplicationUser user = await userManager.FindByEmailAsync(body.Email);
            if (signInResult.Succeeded)
            {
                string renderedToken = GenerateJwtToken();
                return new
                {
                    Authenticated = true,
                    User = new
                    {
                        user.Email,
                        Username = user.Email,
                        user.FirstName,
                        user.LastName,
                        user.Id
                    },
                    Token = renderedToken
                };
            }
            return new { Authenticated = false };
        }
        [Authorize("Bearer")]
        [HttpPost("LogOff")]
        public async Task LogOff()
        {
            var token = Request.Headers["Token"];
            await signInManager.SignOutAsync();

        }

        private string GenerateJwtToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(
                                  issuer: tokenAuthorizationOptions.Issuer,
                                  audience: tokenAuthorizationOptions.Audience,
                                  signingCredentials: tokenAuthorizationOptions.SigningCredentials,
                                  subject: User.Identities.FirstOrDefault(),
                                  expires: (DateTimeOffset.UtcNow + TimeSpan.FromHours(1)).DateTime
                              );
            var renderedToken = tokenHandler.WriteToken(token);
            return renderedToken;
        }

        private readonly TokenAuthorizationOptions tokenAuthorizationOptions;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public class Credentials { public string Email { get; set; } public string Password { get; set; } public bool? RememberMe { get; set; } }
    }
}
