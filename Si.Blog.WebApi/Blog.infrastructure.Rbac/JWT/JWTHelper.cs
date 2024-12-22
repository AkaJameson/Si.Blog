using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Si.Framework.ToolKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Infrastructure.Rbac.JWT
{
    public static class JWTHelper
    {
        private static string _secretKey;
        private static string _issuer;
        private static string _audience;
        static JWTHelper()
        {
            var jwtConfit = (IConfiguration)ServiceLocator.GetServiceProvider().GetService(typeof(IConfiguration));
            _secretKey = jwtConfit["JWT:SecretKey"];
            _issuer = jwtConfit["JWT:Issuer"];
            _audience = jwtConfit["JWT:Audience"];
        }
        public static string GenerateToken(string username, List<Claim> claims = null, int expireMinutes = 30)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsList = claims ?? new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claimsList,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static void AddJWTBearer(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // 在开发环境下不强制使用 HTTPS
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
                };
            });
            services.AddAuthorization();
        }

    }
}
