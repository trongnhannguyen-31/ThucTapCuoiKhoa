using Microsoft.IdentityModel.Tokens;
using Phoenix.Server.Services.Constants;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Phoenix.Server.Services.Framework
{
    public static class TokenAuthHelper
    {
        public static string CreateToken(TokenData claimRequest)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            claims.Add(new Claim("UserId", claimRequest.UserId.ToString()));
            var expiredsIn = (claimRequest.LifeTime);//default 1 day
            if (expiredsIn < 1)
            {
                expiredsIn = 86400;
            }
            var token = new JwtSecurityToken(JwtConfig.Issuer, JwtConfig.Audience, claims, expires: DateTime.Now.Add(TimeSpan.FromSeconds(expiredsIn)), signingCredentials: credentials);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
        public static int GetCurrentUserId()
        {
            var identityModel = Thread.CurrentPrincipal;
            //var identityModel = HttpContext.Current.User;
            int userId = 0;
            if (identityModel.Identity.IsAuthenticated)
            {
                var identity = identityModel.Identity as ClaimsIdentity;
                if (identity != null && identity.Claims.Any())
                {                    
                    var value = identity.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                    int.TryParse(value, out userId);
                }
            }
            return userId;
        }
    }
}
