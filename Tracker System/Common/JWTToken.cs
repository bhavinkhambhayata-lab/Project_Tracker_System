using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.IdentityModel.Tokens;

namespace ZipERP
{
    public class JWTToken
    {
        public static string GenerateNewToken(string username)
        {
            try
            {
                //Set issued at date
                DateTime issuedAt = DateTime.UtcNow;
                //set the time when it expires
                DateTime expires = DateTime.UtcNow.AddDays(7);

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                //create a identity and add claims to the user which we want to log in
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                });                             
                
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(Common.constEncKey));
                SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                //create the jwt
                JwtSecurityToken objtoken =
                    (JwtSecurityToken)
                        tokenHandler.CreateJwtSecurityToken(issuer: Common.ConvertDBnullToString(System.Configuration.ConfigurationManager.AppSettings["ZipERPPath"]), audience: Common.ConvertDBnullToString(System.Configuration.ConfigurationManager.AppSettings["ZipERPPath"]),
                            subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
                string tokenString = tokenHandler.WriteToken(objtoken);

                return tokenString;
            }
            catch 
            {              
                return null;
            }
        }

        public static bool ValidateToken(string token, out string username)
        {
            try
            {
                username = null;

                ClaimsPrincipal simplePrinciple = GetPrincipal(token);
                ClaimsIdentity identity = simplePrinciple?.Identity as ClaimsIdentity;

                if (identity == null)
                    return false;

                if (!identity.IsAuthenticated)
                    return false;

                Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
                username = usernameClaim?.Value;

                if (string.IsNullOrEmpty(username))
                    return false;
                
                return true;
            }
            catch
            {                
                username = null;
                return false;
            }
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(Common.constEncKey));
                
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = Common.ConvertDBnullToString(System.Configuration.ConfigurationManager.AppSettings["ZipERPPath"]),
                    ValidIssuer = Common.ConvertDBnullToString(System.Configuration.ConfigurationManager.AppSettings["ZipERPPath"]),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey
                };

                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch
            {                
                return null;
            }
        }

    }
}