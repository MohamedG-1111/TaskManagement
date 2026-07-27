using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Abstractions.Identity;
using TaskManagement.Application.Common.Settings;

namespace TaskManagement.Infrastructure.Identity
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly JwtSettings settings;

        public AccessTokenService(IOptions<JwtSettings> settings)
        {
            this.settings = settings.Value;
        }
        public AccessTokenResponse GenerateToken(UserTokenData user)
        {
            DateTimeOffset expiredAt = DateTimeOffset.UtcNow.AddMinutes(settings.AccessTokenExpirationMinutes);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName??user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserId),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            claims.AddRange(
                (user.Roles ?? Enumerable.Empty<string>())
                    .Select(role => new Claim(ClaimTypes.Role, role))
            );
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                                  issuer: settings.Issuer,
                                  audience: settings.Audience,
                                  claims: claims,
                                  expires: expiredAt.UtcDateTime,
                                  signingCredentials: credentials
                                 );

            var written = new JwtSecurityTokenHandler().WriteToken(token);

            return new AccessTokenResponse(written, expiredAt.UtcDateTime);
        }
    }
}
