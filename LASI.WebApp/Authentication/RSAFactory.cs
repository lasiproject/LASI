using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LASI.WebApp.Authentication
{
    internal static class RSAFactory
    {
        internal static RSAParametersIncludingPrivateParametersImplementation CreateWithPrivate(RSAParameters parameters) =>
            new RSAParametersIncludingPrivateParametersImplementation(
                d: parameters.D,
                dp: parameters.DP,
                dq: parameters.DQ,
                exponent: parameters.Exponent,
                inverseQ: parameters.InverseQ,
                modulus: parameters.Modulus,
                p: parameters.P,
                q: parameters.Q
            );
        internal static RSAParameters LoadRSAKey(string fileName)
        {
            dynamic keyParameters = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(fileName));
            return new RSAParameters
            {
                D = keyParameters.D,
                DP = keyParameters.DP,
                DQ = keyParameters.DQ,
                Exponent = keyParameters.Exponent,
                InverseQ = keyParameters.InverseQ,
                Modulus = keyParameters.Modulus,
                P = keyParameters.P,
                Q = keyParameters.Q
            };
        }

        internal static void CreateRSAKeyParameters(string fileName)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    var parameters = rsa.ExportParameters(true);
                    var withPrivateParameters = RSAFactory.CreateWithPrivate(parameters);
                    System.IO.File.WriteAllText(fileName, Newtonsoft.Json.JsonConvert.SerializeObject(withPrivateParameters));
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }



        internal class RSAParametersIncludingPrivateParametersImplementation
        {
            public RSAParametersIncludingPrivateParametersImplementation(byte[] d, byte[] dp, byte[] dq, byte[] exponent, byte[] inverseQ, byte[] modulus, byte[] p, byte[] q)
            {
                D = d;
                DP = dp;
                DQ = dq;
                Exponent = exponent;
                InverseQ = inverseQ;
                Modulus = modulus;
                P = p;
                Q = q;
            }

            public IReadOnlyList<byte> D { get; }
            public IReadOnlyList<byte> DP { get; }
            public IReadOnlyList<byte> DQ { get; }
            public IReadOnlyList<byte> Exponent { get; }
            public IReadOnlyList<byte> InverseQ { get; }
            public IReadOnlyList<byte> Modulus { get; }
            public IReadOnlyList<byte> P { get; }
            public IReadOnlyList<byte> Q { get; }
        }
    }
}