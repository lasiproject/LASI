using System.IdentityModel.Tokens;

namespace LASI.WebApp.Authorization
{
    public class TokenAuthorizationOptions
    {
        public string Audience { get; internal set; }
        public string Issuer { get; internal set; }
        public SigningCredentials SigningCredentials { get; internal set; }
    }
}