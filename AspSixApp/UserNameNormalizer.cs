using System;
using Microsoft.AspNet.Identity;

namespace AspSixApp
{
    internal class UserNameNormalizer : IUserNameNormalizer
    {
        public string Normalize(string userName) => userName.IsNormalized() ? userName : userName.Normalize();
    }
}