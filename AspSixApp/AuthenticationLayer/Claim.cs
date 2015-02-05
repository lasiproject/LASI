using System;
using System.Collections.Generic;

namespace AspSixApp.AuthenticationLayer
{
    /// <summary>
    /// Used for authentication and Authorization, not user customizations.
    /// </summary>
    public class Claim
    {
        string Issuer { get; set; }
        public string OriginalIssuer { get; set; }
        ICollection<string> Properties { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}