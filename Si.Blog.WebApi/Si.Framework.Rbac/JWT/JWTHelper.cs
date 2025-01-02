using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Si.Framework.Base.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Si.Framework.Rbac.JWT
{
    public static class JWTHelper
    {
        private static string SecretKey;
        private static string Issuer;
        private static string Audience;
        static JWTHelper()
        {
            var configuration = ServiceLocator.GetConfiguration();
            SecretKey = configuration["Jwt:SecretKey"] ?? "Helloworld";
            Issuer = configuration["Jwt:Issuer"] ?? "Helloworld";
            Audience = configuration["Jwt:Audience"] ?? "Helloworld";
        }
        public static string GenerateToken(List<int> Roleids, List<Claim> claims)
        {
            var expiration = DateTime.UtcNow.AddHours(1); // 1小时过期
            var key = new SymmetricSecurityKey(Convert.FromBase64String(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            claims.RemoveAll(p => p.ValueType == ClaimTypes.Role);
            foreach (var role in Roleids)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static TokenValidationParameters GetTokenValidation()
        {
            var TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true, //是否验证Issuer
                ValidIssuer = Issuer, //发行人Issuer
                ValidateAudience = true, //是否验证Audience
                ValidAudience = Audience, //订阅人Audience
                ValidateIssuerSigningKey = true, //是否验证SecurityKey
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(SecretKey)), //SecurityKey
                ValidateLifetime = true, //是否验证失效时间
                ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
                RequireExpirationTime = true,
            };
            return TokenValidationParameters;
        }
    }
}
