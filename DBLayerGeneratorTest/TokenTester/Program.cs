using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
//using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TokenTester
{
    class Program
    {
        static void Main(string[] args)
        {
            SecurityTokenDescriptor descriptor = Descriptor(ClaimIdentity(), GetSigningCredentials());

            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(descriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
        }

        public static SigningCredentials GetSigningCredentials()
        {
            RSAParameters keyParams;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    keyParams = rsa.ExportParameters(true);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            RsaSecurityKey key = new RsaSecurityKey(keyParams);
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            return signingCredentials;
        }

        public static ClaimsIdentity ClaimIdentity()
        {
            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "myemail@myprovider.com"),
                new Claim(ClaimTypes.Role, "Administrator"),
            }, "Custom");
            return claimsIdentity;
        }

        public static SecurityTokenDescriptor Descriptor(ClaimsIdentity claimsIdentity, SigningCredentials signingCredentials)
        {
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                //AppliesToAddress = "http://my.website.com",
                //TokenIssuerName = "http://my.tokenissuer.com",
                Audience = "http://my.website.com",
                Issuer = "http://my.tokenissuer.com",
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
                
            };
            return securityTokenDescriptor;
        }
    }
}
