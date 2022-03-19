using Phoenix.Server.Services.Constants;
using Phoenix.Server.Services.Database;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Phoenix.Server.Api.Infrastructure
{
    public class AuthMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //validation jwt bearer
            IEnumerable<string> authHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authHeaders))
            {
                return await base.SendAsync(request, cancellationToken);
            }
            var bearerToken = authHeaders.ElementAt(0);
            var token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : string.Empty;
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Secret));
                SecurityToken securityToken;
                var handler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidAudience = JwtConfig.Audience,
                    ValidIssuer = JwtConfig.Issuer,
                    IssuerSigningKey = securityKey,
                };

                var principalIdentity = handler.ValidateToken(token, validationParameters, out securityToken);
                //var identity = principalIdentity.Identity as ClaimsIdentity;
                //if (identity.Claims.Any())
                //{
                //    var loginType = identity.Claims.FirstOrDefault(x => x.Type == "LoginType")?.Value;
                //    if (loginType != null && loginType == "ProviderIntent")
                //    {
                //        var singleClaim = identity.FindFirst("UserId");
                //        if (singleClaim != null)
                //        {
                //            using (var dbContext = new DataContext())
                //            {
                //                identity.RemoveClaim(singleClaim);
                //                identity.AddClaim(new Claim("UserId", "9999"));
                //            }
                //        }
                //    }
                //}

                Thread.CurrentPrincipal = principalIdentity;
                HttpContext.Current.User = principalIdentity;

                return await base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException e)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        private bool LifetimeValidator(DateTime? expires)
        {
            if (expires.HasValue)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}