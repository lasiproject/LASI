using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;

namespace LASI.WebApp.Authentication
{
    public class TokenAuthorizationOptions
    {
        public string Audience { get; internal set; }
        public string Issuer { get; internal set; }
        public SigningCredentials SigningCredentials { get; internal set; }
    }
}