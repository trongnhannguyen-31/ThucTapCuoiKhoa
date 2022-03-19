using CongDongBau.Server.Api.Infrastructure;
using CongDongBau.Server.Services.Constants;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


[assembly: OwinStartup(typeof(OwinStartup))]
namespace CongDongBau.Server.Api.Infrastructure
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            //login user/pass
            app.UseJwtBearerAuthentication(new Microsoft.Owin.Security.Jwt.JwtBearerAuthenticationOptions()
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = JwtConfig.Audience,
                    ValidIssuer = JwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Secret))
                }
            });
        }
    }
}