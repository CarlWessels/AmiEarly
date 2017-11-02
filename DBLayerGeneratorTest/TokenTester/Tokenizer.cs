using System;
using System.Collections.Generic;
//using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace TokenTester
{
    public class Tokenizery
    {
        public SigningCredentials GetSigningCredentials(string text)
        {
            //var plainTextSecurityKey = "This is my shared, not so secret, secret!";
            var signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(text));
            SigningCredentials signingCredentials = new SigningCredentials(signingKey,
                                                            SecurityAlgorithms.HmacSha256Signature,
                                                            SecurityAlgorithms.Sha256Digest);

            return signingCredentials;
        }

        public ClaimsIdentity ClaimIdentity()
        {
            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "myemail@myprovider.com"),
                new Claim(ClaimTypes.Role, "Administrator"),
            }, "Custom");
            return claimsIdentity;
        }

        public void Descriptor(ClaimsIdentity claimsIdentity, SigningCredentials sin)
        {
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                AppliesToAddress = "http://my.website.com",
                TokenIssuerName = "http://my.tokenissuer.com",
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
            };
        }
    }
}